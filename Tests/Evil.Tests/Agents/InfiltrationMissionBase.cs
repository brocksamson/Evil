using System;
using Evil.Agents;
using Evil.Engine;
using Evil.Lairs;
using Evil.Missions;
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
                                                  CompletionDate = DateTime.Now.AddHours(0 - i)
                                              });
            }
        }
    }
}