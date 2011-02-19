using System;
using MbUnit.Framework;

// ReSharper disable InconsistentNaming

namespace Evil.Tests.Common
{
    [TestFixture]
    public class When_Getting_HashCodes_For_Entities_With_Inherited_Domain_Signature
    {
        [Test]
        public void Two_Transient_Objects_With_Different_Inherited_DomainSignatures_Should_Return_Different_HashCodes()
        {
            DateTime hammerTime = DateTime.Now.Date;
            var obj = new ObjectWithInheritedDomainSignature {Foo = "Test1", Hammer = hammerTime};
            var obj2 = new ObjectWithInheritedDomainSignature {Foo = "Test2", Hammer = hammerTime};
            int firstHash = obj.GetHashCode();
            int secondHash = obj2.GetHashCode();
            Assert.AreNotEqual(firstHash, secondHash);
        }

        [Test]
        public void Two_Transient_Objects_With_Different_DomainSignatures_Should_Return_Different_HashCodes()
        {
            DateTime hammerTime = DateTime.Now.Date;
            var obj = new ObjectWithInheritedDomainSignature {Foo = "Test1", Hammer = hammerTime};
            var obj2 = new ObjectWithInheritedDomainSignature {Foo = "Test1", Hammer = hammerTime.AddDays(-5)};
            int firstHash = obj.GetHashCode();
            int secondHash = obj2.GetHashCode();
            Assert.AreNotEqual(firstHash, secondHash);
        }

        [Test]
        public void Two_Transient_Objects_With_Same_DomainSignatures_Should_Return_Same_HashCodes()
        {
            DateTime hammerTime = DateTime.Now.Date;
            var obj = new ObjectWithInheritedDomainSignature {Foo = "Test1", Hammer = hammerTime};
            var obj2 = new ObjectWithInheritedDomainSignature {Foo = "Test1", Hammer = hammerTime};
            int firstHash = obj.GetHashCode();
            int secondHash = obj2.GetHashCode();
            Assert.AreEqual(firstHash, secondHash);
        }
    }
}

// ReSharper restore InconsistentNaming