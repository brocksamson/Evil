using Evil.Tests.Extensions;
using MbUnit.Framework;

// ReSharper disable InconsistentNaming

namespace Evil.Tests.Common
{
    [TestFixture]
    public class When_Getting_HashCodes_For_Entities_With_One_Domain_Signature
    {
        [Test]
        public void Transient_Object_Should_Return_Consistent_HashCode()
        {
            var obj = new ObjectWithOneDomainSignature();
            int firstHash = obj.GetHashCode();
            int secondHash = obj.GetHashCode();
            Assert.AreEqual(firstHash, secondHash);
        }

        [Test]
        public void Two_Transient_Objects_With_Different_DomainSignatures_Should_Return_Different_HashCodes()
        {
            var obj = new ObjectWithOneDomainSignature {Foo = "Test1"};
            var obj2 = new ObjectWithOneDomainSignature {Foo = "Test2"};
            int firstHash = obj.GetHashCode();
            int secondHash = obj2.GetHashCode();
            Assert.AreNotEqual(firstHash, secondHash);
        }

        [Test]
        public void Two_Transient_Objects_With_Same_DomainSignatures_Should_Return_Same_HashCodes()
        {
            var obj = new ObjectWithOneDomainSignature {Foo = "Test1"};
            var obj2 = new ObjectWithOneDomainSignature {Foo = "Test1"};
            int firstHash = obj.GetHashCode();
            int secondHash = obj2.GetHashCode();
            Assert.AreEqual(firstHash, secondHash);
        }

        [Test]
        public void Transient_Object_Should_Return_Consistent_HashCode_When_Made_Persistant()
        {
            var obj = new ObjectWithOneDomainSignature();
            int firstHash = obj.GetHashCode();
            obj.SetProperty(m => m.Id, 1);
            int secondHash = obj.GetHashCode();
            Assert.AreEqual(firstHash, secondHash);
        }

        [Test]
        public void Two_Persistant_Objects_With_Different_Ids_Generate_Different_HashCodes()
        {
            var obj1 = new ObjectWithOneDomainSignature();
            obj1.SetProperty(m => m.Id, 1);

            var obj2 = new ObjectWithOneDomainSignature();
            obj2.SetProperty(m => m.Id, 2);

            Assert.AreNotEqual(obj1.GetHashCode(), obj2.GetHashCode());
        }

        [Test]
        public void Two_Persistant_Objects_With_Same_Ids_Generate_Same_HashCode()
        {
            var obj1 = new ObjectWithOneDomainSignature();
            obj1.SetProperty(m => m.Id, 1);

            var obj2 = new ObjectWithOneDomainSignature();
            obj2.SetProperty(m => m.Id, 1);
            Assert.AreEqual(obj1.GetHashCode(), obj2.GetHashCode());
        }
    }
}

// ReSharper restore InconsistentNaming