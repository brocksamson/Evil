using System;
using System.Reactive;
using Evil.Agents;
using Evil.Common;
using Evil.Engine;
using Evil.Lairs;
using Evil.Missions;
using Evil.Tests.Extensions;
using MbUnit.Framework;
using Rhino.Mocks;

namespace Evil.Tests.Agents
{
    public class InfiltrationMissionBase
    {
        protected Lair _enemyBar;
        protected InfiltrationMission _mission;
        protected Agent _agent;
        protected IDice _dice;

        [SetUp]
        public void SetUp()
        {
            _enemyBar = new Lair();
            _agent = new Agent();
            _dice = MockRepository.GenerateStub<IDice>();
            _mission = new InfiltrationMission(_dice);
            Arrange();
        }

        protected virtual void Arrange()
        {
            
        }

        protected void AddMissions(int count)
        {
            for (var i = 1; i < count; i++)
            {
                _agent.MissionHistory.Add(new MissionOutcome
                                              {
                                                  Success = false,
                                                  Target = _enemyBar,
                                                  Agent = _agent,
                                                  CompletionDate = DateTime.Now.AddHours(0-i)
                                              });
            }
        }
    }

    [TestFixture]
    public class When_infiltrating_a_bar_with_a_generic_agent : InfiltrationMissionBase
    {
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
            Assert.Throws<ArgumentException>(() =>_mission.Complete(_agent));

        }

        [Test]
        public void Should_fail_if_agent_mission_not_ready_for_completion()
        {
            _agent.CurrentMission = new MissionDetails { MissionStart = DateTime.Now, MissionDuration = new TimeSpan(1, 0, 0) };
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


        private void Act(Action<MissionDetails> assertions)
        {
            var called = false;
            _mission.Subscribe(Observer.Create(assertions));
            _mission.Subscribe(Observer.Create<MissionDetails>(m => called = true));
            _mission.Begin(_agent, _enemyBar);
            _dice.VerifyAllExpectations();
            Assert.IsTrue(called, "Mission Complete event not raised.");
        }
    }

    [TestFixture]
    public class When_completing_infiltrating_a_bar_with_a_generic_agent : InfiltrationMissionBase
    {
        protected override void Arrange()
        {
            _agent.CurrentMission = new MissionDetails
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

    public static class EntityExtensions
    {
        public static void SetId(this Entity entity, int id)
        {
            entity.SetProperty(m => m.Id, id);
        }
    }
}
