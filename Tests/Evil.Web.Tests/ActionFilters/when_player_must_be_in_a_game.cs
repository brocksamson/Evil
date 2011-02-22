using System.Web.Mvc;
using Evil.Users;
using Evil.Web.ActionFilters;
using Evil.Web.Controllers;
using Evil.Web.Initialization;
using Evil.Web.Services;
using MbUnit.Framework;
using MvcContrib.TestHelper;
using Rhino.Mocks;

namespace Evil.Web.Tests.ActionFilters
{
    [TestFixture]
    public class when_player_must_be_in_a_game
    {
        private InGameFilter _filter;
        private IUserProvider _userProvider;
        private ActionExecutingContext _actionExecuting;

        [SetUp]
        public void SetUp()
        {
            new RegisterRoutes().Execute();
            _userProvider = MockRepository.GenerateMock<IUserProvider>();
            _actionExecuting = MockRepository.GenerateMock<ActionExecutingContext>();
            _filter = new InGameFilter(_userProvider);
        }

        [Test]
        public void redirects_if_not_in_game()
        {
            _userProvider.Stub(m => m.CurrentPlayer()).Return(null);
            _filter.OnActionExecuting(_actionExecuting);
            Assert.IsInstanceOfType(typeof(RedirectResult), _actionExecuting.Result);
        }

        [Test]
        public void allows_access_if_in_game()
        {
            _userProvider.Stub(m => m.CurrentPlayer()).Return(new Player());
            _filter.OnActionExecuting(_actionExecuting);
            Assert.IsNull(_actionExecuting.Result);
        }

        [Test]
        public void route_maps_correctly()
        {
            //DEBT: This test ensures that the hard coded magic string within ingame is correct.  Should get rid of string instead.
            "~/Game/Start".ShouldMapTo<GameController>(m => m.Start());
        }
    }
}
