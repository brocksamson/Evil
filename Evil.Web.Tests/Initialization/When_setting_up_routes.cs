using Evil.Users;
using Evil.Web.Controllers;
using Evil.Web.Initialization;
using MbUnit.Framework;
using MvcContrib.TestHelper;

// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException

namespace Evil.Web.UnitTests.Configuration
{
    [TestFixture]
    public class When_setting_up_routes
    {
        private Player _player;

        [SetUp]
        public void SetUp()
        {
            _player = new Player();
            new RegisterRoutes().Execute();
        }

        [Test]
        public void home_is_setup_correctly()
        {
            "~/".ShouldMapTo<HomeController>(m => m.Index());
        }

        [Test]
        public void game_routes_correctly()
        {
            "~/Game/Start".ShouldMapTo<GameController>(m => m.Start());
            "~/Start".ShouldMapTo<GameController>(m => m.Start());
        }

        [Test]
        public void mission_routes_correctly()
        {
            "~/Missions/".ShouldMapTo<MissionController>(m => m.Index(_player, MissionTypes.All));
            "~/Missions/All/".ShouldMapTo<MissionController>(m => m.Index(_player, MissionTypes.All));
            "~/Missions/Robberies".ShouldMapTo<MissionController>(m => m.Index(_player, MissionTypes.Robberies));
        }
    }
}

// ReSharper restore InconsistentNaming
// ReSharper restore PossibleNullReferenceException
