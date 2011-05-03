using System;
using System.Collections.Generic;
using System.Text;
using Evil.Agents;
using Evil.Common;
using Evil.Engine;
using Evil.Lairs;
using Evil.Missions;
using Evil.Tests.Extensions;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using Rhino.Mocks;

namespace Evil.Tests.Agents
{
    [TestFixture]
    public class When_infiltrating_a_bar_with_a_generic_agent
    {
        private Lair _enemyBar;
        private InfiltrationMission _mission;
        private Agent _agent;
        private IDice _dice;

        [SetUp]
        public void Arrange()
        {
            _enemyBar = new Lair();
            _agent = new Agent();
            _dice = MockRepository.GenerateMock<IDice>();
            _mission = new InfiltrationMission(_dice);
        }

        [Test]
        public void Should_fail_if_agent_on_mission()
        {
            _agent.CurrentMission = new MissionDetails
                                        {MissionStart = DateTime.Now, MissionDuration = new TimeSpan(1, 0, 0)};
            Assert.Throws<ArgumentException>(() => _mission.Begin(_agent, _enemyBar));
        }

        [Test]
        public void Should_fail_to_complete_if_no_current_mission()
        {
            Assert.Throws<ArgumentException>(() =>_mission.Complete(_agent, _enemyBar));

        }

        [Test]
        public void Should_fail_if_agent_mission_not_ready_for_completion()
        {
            _agent.CurrentMission = new MissionDetails { MissionStart = DateTime.Now, MissionDuration = new TimeSpan(1, 0, 0) };
            Assert.Throws<ArgumentException>(() => _mission.Complete(_agent, _enemyBar));
        }

        [Test]
        public void Should_take_1_hour()
        {
            var missionDetails = _mission.Begin(_agent, _enemyBar);
            Assert.AreEqual(new TimeSpan(1,0,0), missionDetails.MissionDuration);
        }

        [Test]
        public void Should_have_20_percent_chance_to_infiltrate()
        {
            var missionDetails = _mission.Begin(_agent, _enemyBar);
            Assert.AreEqual(.20M, missionDetails.SuccessChance);
            missionDetails.SetProperty(m => m.MissionStart, DateTime.Now.AddHours(-1));
            var missionReport = _mission.Complete(_agent, _enemyBar);
            //_dice.GetArgumentsForCallsMadeOn(m => m.RollPercentage(0)).First<decimal>();
            throw new NotImplementedException();
        }

        [Test]
        public void Should_have_5_percent_chance_of_discovery()
        {
            throw new NotImplementedException();
        }
        
        [Test]
        public void Should_have_25_percent_chance_to_infiltrate_second_try()
        {
            var missionDetails = _mission.Begin(_agent, _enemyBar);    
            throw new NotImplementedException();
        }

        [Test]
        public void Should_have_10_percent_chance_of_discovery_second_try()
        {
            throw new NotImplementedException();
        }

    }

    public static class EntityExtensions
    {
        public static void SetId(this Entity entity, int id)
        {
            entity.SetProperty(m => m.Id, id);
        }
    }
}
