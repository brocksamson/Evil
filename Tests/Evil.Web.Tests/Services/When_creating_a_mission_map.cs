using System.Collections.ObjectModel;
using Evil.Common;
using Evil.Web.Services;
using Evil.Web.Tests.TestHelpers;
using MbUnit.Framework;

// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException

namespace Evil.Web.Tests.Services
{
    [TestFixture]
    public class When_creating_a_target_selection_map
    {
        private MapGenerator _generator;
        private Collection<Target> _targets;
        private Target _target1;
        private Target _target2;
        private Target _target3;
        private Target _target4;
        private Target _target5;
        private Target _target6;
        private Target _target7;
        private Target _target8;
        private Target _target9;
        private Target _target10;

        [SetUp]
        public void SetUp()
        {
            _target1 = new Target{Position = new Position(1, 1)};
            _target2 = new Target{Position = new Position(2, 1)};
            _target3 = new Target {Position = new Position(1.6, 1.45)};
            _target4 = new Target {Position = new Position(5, 9)};
            _target5 = new Target {Position = new Position(5, 3)};
            _target6 = new Target {Position = new Position(6, 4)};
            _target7 = new Target {Position = new Position(7, 6)};
            _target8 = new Target {Position = new Position(8, 4)};
            _target9 = new Target {Position = new Position(3, 5)};
            _target10 = new Target {Position = new Position(11, 11)};
            
            _targets = new Collection<Target>{_target1};
            _generator = new MapGenerator();
        }

        [Test]
        public void Generated_map_is_valid()
        {
            var map = _generator.GenerateTargetMap(_targets);


            map.StartingPosition.AssertIsValid();
            var count = 0;
            foreach (var location in map.Locations)
            {
                location.Position.AssertIsValid();
                var found = false;
                foreach(var target in _targets)
                {
                    if (target.Position == location.Position)
                    {
                        found = true;
                    }
                }
                Assert.IsTrue(found, "location Latitude=" + location.Position.Latitude + " Longitude=" + location.Position.Longitude + " was not found");
                count++;
            }
            Assert.AreEqual(count, _targets.Count);
        }

        [Test]
        public void Generated_map_centers_correctly_with_1_mission()
        {
            var map = _generator.GenerateTargetMap(_targets);
            map.StartingPosition.AssertIsValid();
            map.StartingPosition.AssertIsEqualTo(_target1.Position);
        }

        [Test]
        public void Generated_map_centers_correctly_with_2_missions()
        {
            var map = _generator.GenerateTargetMap(new Collection<Target>{_target1, _target2});
            map.StartingPosition.AssertIsValid();
            map.StartingPosition.AssertIsEqualTo(new Position(1.5, 1));
        }

        [Test]
        public void Generated_map_centers_correctly_with_4_missions()
        {
            var map =
                _generator.GenerateTargetMap(new Collection<Target>
                                                 {
                                                     _target1,
                                                     _target2,
                                                     _target3,
                                                     _target4
                                                 });
            map.StartingPosition.AssertIsValid();
            map.StartingPosition.AssertIsEqualTo(new Position(3, 5));
        }

        [Test]
        public void Generated_map_centers_correctly_with_10_missions()
        {
            var map =
                _generator.GenerateTargetMap(new Collection<Target>
                                                 {
                                                     _target1,
                                                     _target2,
                                                     _target3,
                                                     _target4,
                                                     _target5,
                                                     _target6,
                                                     _target7,
                                                     _target8,
                                                     _target9,
                                                     _target10
                                                 });
            map.StartingPosition.AssertIsValid();
            map.StartingPosition.AssertIsEqualTo(new Position(6, 6));
        }

        [Test]
        public void Generated_map_centers_correctly_with_positive_negatives()
        {
            var map =
                _generator.GenerateTargetMap(new Collection<Target>
                                                 {
                                                     new Target(){Position = new Position(-1, -1)},
                                                     new Target(){Position = new Position(1, 1)},
                                                     new Target(){Position = new Position(-2, 2)},
                                                 });
            map.StartingPosition.AssertIsValid();
            map.StartingPosition.AssertIsEqualTo(new Position(-0.5, 0.5));
        }
    }
}

// ReSharper restore InconsistentNaming
// ReSharper restore PossibleNullReferenceException
