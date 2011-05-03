using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Evil.Common
{
    public abstract class EntityWithTypedId<T>
    {
        /// <summary>
        /// To help ensure hashcode uniqueness, a carefully selected random number multiplier 
        /// is used within the calculation.  Goodrich and Tamassia's Data Structures and
        /// Algorithms in Java asserts that 31, 33, 37, 39 and 41 will produce the fewest number
        /// of collissions.  See http://computinglife.wordpress.com/2008/11/20/why-do-hash-functions-use-prime-numbers/
        /// for more information.
        /// </summary>
        private const int HASH_MULTIPLIER = 31;

        /// <summary>
        /// This static member caches the domain signature properties to avoid looking them up for 
        /// each instance of the same type.
        /// 
        /// A description of the very slick ThreadStatic attribute may be found at 
        /// http://www.dotnetjunkies.com/WebLog/chris.taylor/archive/2005/08/18/132026.aspx
        /// </summary>
        [ThreadStatic] private static Dictionary<Type, IEnumerable<PropertyInfo>> _signaturePropertiesDictionary;

        private int? _hashCode;
        public virtual T Id { get; protected set; }

        /// <summary>
        /// Transient objects are not associated with an item already in storage.  For instance,
        /// a Customer is transient if its Id is 0.  It's virtual to allow NHibernate-backed 
        /// objects to be lazily loaded.
        /// </summary>
        public virtual bool IsTransient()
        {
            return Id == null || Id.Equals(default(T));
        }

        public override int GetHashCode()
        {
            if (!_hashCode.HasValue)
                _hashCode = IsTransient() ? GetHashCodeFromDomainSignature(GetType()) : GetHashCodeFromKey(GetType());
            return _hashCode.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as EntityWithTypedId<T>;
            if (obj == null) return false;
            if (IsTransient() || other.IsTransient())
                return CheckDomainSignature(obj);
            return (other.Id.Equals(Id));
        }

        private bool CheckDomainSignature(object obj)
        {
            IEnumerable<PropertyInfo> domainProperties = GetSignatureProperties(GetType());
            if (!domainProperties.Any())
                return base.Equals(obj);
            foreach (PropertyInfo domainProperty in domainProperties)
            {
                if (!domainProperty.GetValue(obj, null).Equals(domainProperty.GetValue(this, null)))
                    return false;
            }
            return true;
        }

        private int GetHashCodeFromKey(Type type)
        {
            unchecked
            {
                int typeHash = type.GetHashCode();
                return (Id.GetHashCode()*HASH_MULTIPLIER) ^ typeHash;
            }
        }

        private int GetHashCodeFromDomainSignature(Type type)
        {
            unchecked
            {
                IEnumerable<PropertyInfo> signatureProperties = GetSignatureProperties(type);
                if (signatureProperties.Any())
                {
                    int hashCode = type.GetHashCode();
                    foreach (PropertyInfo signatureProperty in signatureProperties)
                    {
                        object val = signatureProperty.GetValue(this, null);
                        if (val != null)
                        {
                            hashCode = (hashCode*HASH_MULTIPLIER) ^ val.GetHashCode();
                        }
                    }
                    return hashCode;
                }
                return base.GetHashCode();
            }
        }

        private IEnumerable<PropertyInfo> GetSignatureProperties(Type type)
        {
            if (_signaturePropertiesDictionary == null)
            {
                _signaturePropertiesDictionary = new Dictionary<Type, IEnumerable<PropertyInfo>>();
            }
            IEnumerable<PropertyInfo> properties;
            if (_signaturePropertiesDictionary.TryGetValue(type, out properties))
            {
                return properties;
            }
            return _signaturePropertiesDictionary[type] = GetTypeSpecificSignatureProperties();
        }

        /// <summary>
        /// The property getter for SignatureProperties should ONLY compare the properties which make up 
        /// the "domain signature" of the object.
        /// 
        /// If you choose NOT to override this method (which will be the most common scenario), 
        /// then you should decorate the appropriate property(s) with [DomainSignature] and they 
        /// will be compared automatically.  This is the preferred method of managing the domain
        /// signature of entity objects.
        /// </summary>
        /// <remarks>
        /// This ensures that the entity has at least one property decorated with the 
        /// [DomainSignature] attribute.
        /// </remarks>
        protected virtual IEnumerable<PropertyInfo> GetTypeSpecificSignatureProperties()
        {
            return GetType().GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof (DomainSignatureAttribute), true));
        }
    }
}