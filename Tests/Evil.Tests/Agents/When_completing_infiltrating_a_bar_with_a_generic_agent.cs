using System;
using System.Reactive;
using Evil.Missions;
using MbUnit.Framework;
using Rhino.Mocks;

namespace Evil.Tests.Agents
{
    [TestFixture]
    public class When_completing_infiltrating_a_bar_with_a_generic_agent : InfiltrationMissionBase
    {
        protected override void Arrange()
        {
            _agent.CurrentMission = new MissionBrief
                                        {
                                            DiscoveryChance = .20M,
                                            MissionDuration = new TimeSpan(1, 0, 0),
                                            MissionStart = DateTime.Now.AddHours(-1),
                                            Target = _enemyBar,
                                        };
        }

        [Test]
        public void Should_fail_if_discovery_roll_fails_and_infiltrate_roll_fails()
        {
            _dice.Expect(m => m.RollPercentage(_agent.CurrentMission.SuccessChance)).Return(false);
            _dice.Expect(m => m.RollPercentage(_agent.CurrentMission.DiscoveryChance)).Return(false);

            Act(missionOutcome =>
                    {
                        Assert.IsFalse(missionOutcome.Success);
                        Assert.IsFalse(missionOutcome.Discovered);
                    });
        }

        [Test]
        public void Should_infiltrate_if_infiltrate_roll_succeeds()
        {
            _dice.Expect(m => m.RollPercentage(_agent.CurrentMission.SuccessChance)).Return(true);

            Act(missionOutcome =>
                    {
                        Assert.IsTrue(missionOutcome.Success);
                        Assert.IsFalse(missionOutcome.Discovered);
                        Assert.AreEqual(_enemyBar, missionOutcome.Target);
                    });
        }

        [Test]
        public void Should_be_discovered_if_discovery_roll_succeed()
        {
            _dice.Expect(m => m.RollPercentage(_agent.CurrentMission.SuccessChance)).Return(false);
            _dice.Expect(m => m.RollPercentage(_agent.CurrentMission.DiscoveryChance)).Return(true);

            Act(missionOutcome =>
                    {
                        Assert.IsFalse(missionOutcome.Success);
                        Assert.IsTrue(missionOutcome.Discovered);
                    });
        }

        [Test]
        public void Should_set_current_mission_to_null_after_completion()
        {
            _mission.Complete(_agent);
            Assert.IsNull(_agent.CurrentMission);
        }


        private void Act(Action<MissionOutcome> assertions)
        {
            var called = false;
            _mission.Subscribe(Observer.Create(assertions));
            _mission.Subscribe(Observer.Create<MissionOutcome>(m => called = true));
            _mission.Complete(_agent);
            _dice.VerifyAllExpectations();
            Assert.IsTrue(called, "Mission Complete event not raised.");
        }
    }
}