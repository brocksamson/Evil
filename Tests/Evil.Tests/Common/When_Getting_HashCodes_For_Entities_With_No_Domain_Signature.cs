using Evil.Tests.Extensions;
using MbUnit.Framework;

// ReSharper disable InconsistentNaming

namespace Evil.Tests.Common
{
    [TestFixture]
    public class When_Getting_HashCodes_For_Entities_With_No_Domain_Signature
    {
        [Test]
        public void Transient_Object_Should_Return_Consistent_HashCode()
        {
            var obj = new ObjectWithNoDomainSignature();
            int firstHash = obj.GetHashCode();
            int secondHash = obj.GetHashCode();
            Assert.AreEqual(firstHash, secondHash);
        }

        [Test]
        public void Two_Transient_Objects_Of_Same_Type_Should_Return_Different_Signatures()
        {
            var obj = new ObjectWithNoDomainSignature();
            var obj2 = new ObjectWithNoDomainSignature();
            int firstHash = obj.GetHashCode();
            int secondHash = obj2.GetHashCode();
            Assert.AreNotEqual(firstHash, secondHash);
        }

        [Test]
        public void Transient_Object_Should_Return_Consistent_HashCode_When_Made_Persistant()
        {
            var obj = new ObjectWithNoDomainSignature();
            int firstHash = obj.GetHashCode();
            obj.SetProperty(m => m.Id, 1);
            int secondHash = obj.GetHashCode();
            Assert.AreEqual(firstHash, secondHash);
        }

        [Test]
        public void Two_Persistant_Objects_With_Different_Ids_Generate_Different_HashCodes()
        {
            var obj1 = new ObjectWithNoDomainSignature();
            obj1.SetProperty(m => m.Id, 1);

            var obj2 = new ObjectWithNoDomainSignature();
            obj2.SetProperty(m => m.Id, 2);

            Assert.AreNotEqual(obj1.GetHashCode(), obj2.GetHashCode());
        }

        [Test]
        public void Two_Persistant_Objects_With_Same_Ids_Generate_Same_HashCode()
        {
            var obj1 = new ObjectWithNoDomainSignature();
            obj1.SetProperty(m => m.Id, 1);

            var obj2 = new ObjectWithNoDomainSignature();
            obj2.SetProperty(m => m.Id, 1);
            Assert.AreEqual(obj1.GetHashCode(), obj2.GetHashCode());
        }
    }
}

// ReSharper restore InconsistentNaming