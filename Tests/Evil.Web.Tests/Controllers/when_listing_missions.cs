using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using Evil.Common;
using Evil.Tests.TestHelpers;
using Evil.Users;
using Evil.Web.Controllers;
using Evil.Web.Models;
using Evil.Web.Services;
using MbUnit.Framework;
using Rhino.Mocks;
using MvcContrib.TestHelper;

// ReSharper disable PossibleNullReferenceException

namespace Evil.Web.UnitTests.Controllers
{
    [TestFixture]
    public class when_listing_missions
    {
        private MissionController _controller;
        private IMapGenerator _mapGenerator;
        private IRepository<Target> _targetRepository;
        private IEnumerable<Target> _targets;
        private GoogleMap _map;
        private Player _player;

        [SetUp]
        public void SetUp()
        {
            _targets = new Collection<Target>();
            _map = new GoogleMap();
            _player = new Player();

            _mapGenerator = MockRepository.GenerateMock<IMapGenerator>();
            _mapGenerator.Expect(m => m.GenerateTargetMap(null)).Return(_map).IgnoreArguments();

            _targetRepository = new InMemoryRepository<Target>(_targets);
            _controller = new MissionController(_mapGenerator, _targetRepository);
        }

        [Test]
        [Ignore("Need to make changes per When_retrieving_targets before this will work.")]
        public void returns_map_data()
        {
            var result = _controller.GetMissions(_player, MissionTypes.All).AssertResultIs<JsonResult>();
            Assert.IsInstanceOfType<GoogleMap>(result.Data);
            var mapResult = result.Data as GoogleMap;
            Assert.AreEqual(mapResult.StartingPosition, _map.StartingPosition);
            Assert.AreEqual(mapResult.Locations, _map.Locations);
        }
    }
}

// ReSharper restore PossibleNullReferenceException
