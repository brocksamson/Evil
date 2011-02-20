using Evil.Web.Initialization;
using MbUnit.Framework;
using StructureMap;

namespace Evil.Web.UnitTests.Configuration
{
    [TestFixture]
    public class While_bootstrapping
    {
        [Test]
        public void serviceLocator_container_is_set_correctly()
        {
            new WebBootstrapper().Bootstrap();
            ObjectFactory.AssertConfigurationIsValid();
        }

    }
}