using System;
using System.Linq;
using System.Web.Mvc;
using Evil.Common;
using Evil.Lairs;
using Evil.Users;
using Evil.Web.Controllers;
using Evil.Web.Models;
using Evil.Web.Services;
using Evil.Web.Tests.TestHelpers;
using MbUnit.Framework;
using Rhino.Mocks;

// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
namespace Evil.Web.UnitTests.Controllers
{
    [TestFixture]
    public class When_starting_a_game
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            _mother = new ObjectMother();
            _account = _mother.GetAccountByEmailAddress("email@address.com");
            _playerRepository = MockRepository.GenerateMock<IRepository<Player>>();
            _areaRepository = MockRepository.GenerateMock<IRepository<Area>>();
            _mapGenerator = new MapGenerator();
            _controller = new GameController(_playerRepository, _areaRepository, _mapGenerator);
            _view = new CreatePlayerView
                             {
                                 Name = "Dr. Evil",
                                 BaseName = "My Base",
                                 BaseLatitude = _latitude,
                                 BaseLongitude = _longitude
                             };
        }

        #endregion

        private const double _latitude = 37.771008;
        private const double _longitude = -122.41175;

        private GameController _controller;
        private CreatePlayerView _view;
        private IRepository<Player> _playerRepository;
        private ObjectMother _mother;
        private IRepository<Area> _areaRepository;
        private IMapGenerator _mapGenerator;
        private Account _account;

        [Test]
        public void creates_json_startup_locations()
        {
            var result = _controller.StartupLocations();
            Assert.IsInstanceOfType<JsonResult>(result);
            var json = result as JsonResult;
            Assert.IsInstanceOfType<GoogleMap>(json.Data);

            var mapData = json.Data as GoogleMap;
            Assert.IsNotNull(mapData.StartingPosition);
            var count = 0;
            foreach (var location in mapData.Locations)
            {
                Assert.IsNotEmpty(location.Name);
                location.Position.AssertIsValid();
                count++;
            }
            Assert.GreaterThanOrEqualTo(count, 5, "At least 5 locations should be specified.");
        }

        [Test]
        public void creates_player_starting_information()
        {
            var result = _controller.Start(_account, _view);
            Assert.IsInstanceOfType<RedirectToRouteResult>(result);
            var playerSaveList = _playerRepository.GetArgumentsForCallsMadeOn(m => m.Save(null));
            var player = playerSaveList[0][0] as Player;
            Assert.IsNotNull(player, "Save was not passed a player");
            Assert.AreEqual(player.Name, _view.Name);
            var count = 0;
            Assert.AreEqual(player.Agents.Count(), 0);
            Assert.AreEqual(player.Account, _account);
            AssertStartupBase(player.MainLair);
        }

        private void AssertStartupBase(Lair lair)
        {
            Assert.IsNotNull(lair);
            Assert.AreEqual(lair.Name, _view.BaseName);
            Assert.AreEqual(lair.Location.Latitude, _latitude);
            Assert.AreEqual(lair.Location.Longitude, _longitude);
            //Assert.AreEqual(@base.Sections.Count(), Is.EqualTo(4));
            //Assert.AreEqual(@base.Sections.Any(m => m.GetType() == typeof(BarSection)), Is.True, "No bar section found");
            //Assert.AreEqual(@base.Sections.Where(m => m.GetType() == typeof(EmptySection)).Count(), Is.EqualTo(3), "Base should have 3 empty sections");
        }

        [Test]
        [Ignore("not need it yet!")]
        public void zip_code_is_in_allowed_list()
        {
            throw new NotImplementedException();
        }
    }
}
// ReSharper restore InconsistentNaming
// ReSharper restore PossibleNullReferenceException
