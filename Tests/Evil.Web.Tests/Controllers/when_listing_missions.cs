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

// ReSharper disable InconsistentNaming
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
            _mapGenerator.Expect(m => m.GenerateTargetMap(_targets)).Return(_map);

            _targetRepository = new InMemoryRepository<Target>(_targets);
            _controller = new MissionController(_mapGenerator, _targetRepository);
        }

        [Test]
        public void returns_map_data()
        {
            var result = _controller.GetMissions(_player, MissionTypes.All);
            Assert.IsInstanceOfType<JsonResult>(result);
            var viewResult = result as JsonResult;
            Assert.IsInstanceOfType<GoogleMap>(viewResult.Data);
            _targetRepository.AssertWasCalled(m => m.Get.ValidTargetsFor(_player));
            _mapGenerator.AssertWasCalled(m => m.GenerateTargetMap(_targets));
        }
    }
}

// ReSharper restore InconsistentNaming
// ReSharper restore PossibleNullReferenceException
