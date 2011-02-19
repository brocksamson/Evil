using Evil.Tests.Extensions;
using MbUnit.Framework;

// ReSharper disable InconsistentNaming

namespace Evil.Tests.Common
{
    [TestFixture]
    public class When_Getting_HashCodes_For_Entities_With_Complex_Domain_Signature
    {
        [Test]
        public void Transient_Object_Should_Return_Consistent_HashCode()
        {
            var obj = new ObjectWithComplexDomainSignature();
            int firstHash = obj.GetHashCode();
            int secondHash = obj.GetHashCode();
            Assert.AreEqual(firstHash, secondHash);
        }

        [Test]
        public void Two_Transient_Objects_With_Different_DomainSignatures_Should_Return_Different_HashCodes()
        {
            var obj = new ObjectWithComplexDomainSignature
                          {Complex = new ObjectWithOneDomainSignature {Foo = "Testing"}};
            var obj2 = new ObjectWithComplexDomainSignature
                           {Complex = new ObjectWithOneDomainSignature {Foo = "Testing2"}};
            int firstHash = obj.GetHashCode();
            int secondHash = obj2.GetHashCode();
            Assert.AreNotEqual(firstHash, secondHash);
        }

        [Test]
        public void Two_Transient_Objects_With_Same_DomainSignatures_Should_Return_Same_HashCodes()
        {
            var obj = new ObjectWithComplexDomainSignature
                          {Complex = new ObjectWithOneDomainSignature {Foo = "Testing"}};
            var obj2 = new ObjectWithComplexDomainSignature
                           {Complex = new ObjectWithOneDomainSignature {Foo = "Testing"}};
            int firstHash = obj.GetHashCode();
            int secondHash = obj2.GetHashCode();
            Assert.AreEqual(firstHash, secondHash);
        }

        [Test]
        public void Transient_Object_Should_Return_Consistent_HashCode_When_Made_Persistant()
        {
            var obj = new ObjectWithComplexDomainSignature
                          {Complex = new ObjectWithOneDomainSignature {Foo = "Testing"}};
            int firstHash = obj.GetHashCode();
            obj.SetProperty(m => m.Id, 1);
            int secondHash = obj.GetHashCode();
            Assert.AreEqual(firstHash, secondHash);
        }

        [Test]
        public void Two_Persistant_Objects_With_Different_Ids_Generate_Different_HashCodes()
        {
            var obj1 = new ObjectWithComplexDomainSignature
                           {Complex = new ObjectWithOneDomainSignature {Foo = "Testing"}};
            obj1.SetProperty(m => m.Id, 1);

            var obj2 = new ObjectWithComplexDomainSignature
                           {Complex = new ObjectWithOneDomainSignature {Foo = "Testing"}};
            obj2.SetProperty(m => m.Id, 2);

            Assert.AreNotEqual(obj1.GetHashCode(), obj2.GetHashCode());
        }

        [Test]
        public void Two_Persistant_Objects_With_Same_Ids_Generate_Same_HashCode()
        {
            var obj1 = new ObjectWithComplexDomainSignature
                           {Complex = new ObjectWithOneDomainSignature {Foo = "Testing"}};
            obj1.SetProperty(m => m.Id, 1);

            var obj2 = new ObjectWithComplexDomainSignature
                           {Complex = new ObjectWithOneDomainSignature {Foo = "Testing2"}};
            obj2.SetProperty(m => m.Id, 1);

            Assert.AreEqual(obj1.GetHashCode(), obj2.GetHashCode());
        }
    }
}

// ReSharper restore InconsistentNaming