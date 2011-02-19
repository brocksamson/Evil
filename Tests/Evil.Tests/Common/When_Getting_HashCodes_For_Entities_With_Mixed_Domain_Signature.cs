using Evil.Tests.Extensions;
using MbUnit.Framework;

// ReSharper disable InconsistentNaming

namespace Evil.Tests.Common
{
    [TestFixture]
    public class When_Getting_HashCodes_For_Entities_With_Mixed_Domain_Signature
    {
        [Test]
        public void Two_Persistant_Objects_With_Different_Types_And_Same_Ids_Generate_Different_HashCodes()
        {
            var obj1 = new ObjectWithNoDomainSignature();
            obj1.SetProperty(m => m.Id, 1);

            var obj2 = new ObjectWithOneDomainSignature();
            obj2.SetProperty(m => m.Id, 1);
            Assert.AreNotEqual(obj1.GetHashCode(), obj2.GetHashCode());
        }

        [Test]
        public void Two_Transient_Objects_With_Different_Types_And_Same_DomainSignature_Generate_Different_HashCodes()
        {
            var obj1 = new ObjectWithOneDomainSignature2 {Foo = "test1"};

            var obj2 = new ObjectWithOneDomainSignature {Foo = "test1"};

            Assert.AreNotEqual(obj1.GetHashCode(), obj2.GetHashCode());
        }

        [Test]
        public void
            Two_Transient_Objects_With_Different_Types_And_Different_DomainSignature_And_Same_Values_Generate_Different_HashCodes
            ()
        {
            var obj1 = new ObjectWithMultipleDomainSignature {Foo = "test1", Bar = 2};

            var obj2 = new ObjectWithOneDomainSignature {Foo = "test1", Bar = 2};

            Assert.AreNotEqual(obj1.GetHashCode(), obj2.GetHashCode());
        }
    }
}

// ReSharper restore InconsistentNaming