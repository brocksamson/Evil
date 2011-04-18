using AutoMapper;
using Evil.Web.Initialization;
using MbUnit.Framework;

namespace Evil.Web.Tests.Initialization
{
    [TestFixture]
    public class When_mapping_domain_to_view_model
    {
        [Test]
        public void Should_have_valid_maps()
        {
            new RegisterMaps().Execute();
            Mapper.AssertConfigurationIsValid();
        }
    }
}
