using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using Evil.Common;
using Evil.Missions;
using Evil.Tests.Extensions;
using MbUnit.Framework;
using Rhino.Mocks;

namespace Evil.Tests.Agents
{
    [TestFixture]
    public class When_infiltrating_a_lair_with_a_generic_agent : InfiltrationMissionBase
    {
        [Test]
        public void Should_fail_if_agent_on_mission()
        {
            _agent.CurrentMission = new MissionBrief { MissionStart = DateTime.Now, MissionDuration = new TimeSpan(1, 0, 0) };
            Assert.Throws<ArgumentException>(() => _mission.Begin(_agent, _enemyBar));
        }

        [Test]
        public void Should_fail_to_complete_if_no_current_mission()
        {
            Assert.Throws<ArgumentException>(() => _mission.Complete(_agent));

        }

        [Test]
        public void Should_fail_if_agent_mission_not_ready_for_completion()
        {
            _agent.CurrentMission = new MissionBrief { MissionStart = DateTime.Now, MissionDuration = new TimeSpan(1, 0, 0) };
            Assert.Throws<ArgumentException>(() => _mission.Complete(_agent));
        }

        [Test]
        public void Should_take_1_hour()
        {
            Act(missionDetails => Assert.AreEqual(new TimeSpan(1, 0, 0), missionDetails.MissionDuration));

        }

        [Test]
        [Row(.20, 1)]
        [Row(.25, 2)]
        [Row(.30, 3)]
        [Row(.35, 4)]
        [Row(.40, 5)]
        [Row(.45, 6)]
        public void Should_have_increasing_percent_chance_to_infiltrate(decimal chance, int attempt)
        {
            AddMissions(attempt);
            Act(missionDetails => Assert.AreEqual(chance, missionDetails.SuccessChance));
        }

        [Test]
        [Row(.05, 1)]
        [Row(.10, 2)]
        [Row(.15, 3)]
        [Row(.20, 4)]
        public void Should_have_increasing_percent_chance_of_discovery(decimal chance, int attempt)
        {
            AddMissions(attempt);
            Act(missionDetails => Assert.AreEqual(chance, missionDetails.DiscoveryChance));
        }

        [Test]
        public void Should_assign_target()
        {
            Act(missionDetails => Assert.AreEqual(_enemyBar, missionDetails.Target));
        }

        [Test]
        public void Should_assign_agent()
        {
            Act(missionDetails => Assert.AreEqual(_agent, missionDetails.Agent));
        }



        private void Act(Action<MissionBrief> assertions)
        {
            var called = false;
            _mission.Subscribe(Observer.Create(assertions));
            _mission.Subscribe(Observer.Create<MissionBrief>(m => called = true));
            _mission.Begin(_agent, _enemyBar);
            _dice.VerifyAllExpectations();
            Assert.IsTrue(called, "Mission Complete event not raised.");
        }
    }

    [TestFixture]
    public class When_timing_an_infiltration_mission : InfiltrationMissionBase
    {
        [Test]
        public void Should_fire_once_for_each_input_sequence_after_delay_of_mission_duration()
        {
            //var inputs = new List<MissionBrief>
            //                 {
            //                     new MissionBrief{Agent = _agent, MissionDuration = new TimeSpan(0,0,0,0,10), MissionStart = DateTime.Now},
            //                 };
            //var sequence = inputs.ToObservable().MissionTimer();

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