using Evil.Web.Initialization;
using MbUnit.Framework;

namespace Evil.Web.Tests
{
    [AssemblyFixture]
    public class TestInitialization
    {
        [SetUp]
        public void Arrange()
        {
            new RegisterMaps().Execute();
        }
    }
}
