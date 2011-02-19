using Evil.Tests.Extensions;
using MbUnit.Framework;

// ReSharper disable InconsistentNaming

namespace Evil.Tests.Common
{
    [TestFixture]
    public class When_TestingEquality_For_Entities_With_No_Domain_Signature
    {
        [Test]
        public void Should_Return_True_if_Same_Instance()
        {
            var obj = new ObjectWithNoDomainSignature();
            ObjectWithNoDomainSignature obj2 = obj;
            Assert.AreEqual(obj, obj2);
        }

        [Test]
        public void Should_Return_True_If_Different_Instances_With_Same_Id()
        {
            var obj = new ObjectWithNoDomainSignature();
            obj.SetProperty(m => m.Id, 2);
            var obj2 = new ObjectWithNoDomainSignature();
            obj2.SetProperty(m => m.Id, 2);
            Assert.AreEqual(obj, obj2);
        }

        [Test]
        public void Should_Return_False_If_Different_Instances_With_Different_Id()
        {
            var obj = new ObjectWithNoDomainSignature();
            obj.SetProperty(m => m.Id, 2);
            var obj2 = new ObjectWithNoDomainSignature();
            obj2.SetProperty(m => m.Id, 1);
            Assert.AreNotEqual(obj, obj2);
        }
    }
}

// ReSharper restore InconsistentNaming