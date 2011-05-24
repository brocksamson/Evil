using System;
using System.Collections.Generic;
using System.Linq;
using Evil.Agents;
using Evil.Engine;
using Evil.Lairs;

namespace Evil.Missions
{
    //TODO: pretty sure this class isn't entirely setup per Rx best practices.  Need to review.
    public class InfiltrationMission :IObservable<MissionDetails>, IObservable<MissionOutcome>, IDisposable
    {
        private readonly IDice _dice;
        private readonly List<IObserver<MissionOutcome>> _outcomeObservers;
        private readonly List<IObserver<MissionDetails>> _detailObservers;

        public InfiltrationMission(IDice dice)
        {
            _dice = dice;
            _outcomeObservers = new List<IObserver<MissionOutcome>>();
            _detailObservers = new List<IObserver<MissionDetails>>();
        }

        public void Begin(Agent agent, Lair target)
        {
            var details = new MissionDetails
                       {
                           MissionStart = DateTime.Now,
                           MissionDuration = new TimeSpan(0, 0, 5),
                           SuccessChance = CalculateSuccessChance(agent, target),
                           DiscoveryChance = CalculateDiscoveryChance(agent, target),
                           Target = target
                       };
            agent.BeginMission(details);
            OnMissionStarted(details);
        }


        public void Complete(Agent agent)
        {
            if (agent.CurrentMission == null) throw new ArgumentException("Could not complete a mission, agent is currently idle.");
            var outcome = new MissionOutcome
                              {
                                  Success = _dice.RollPercentage(agent.CurrentMission.SuccessChance),
                                  Discovered = _dice.RollPercentage(agent.CurrentMission.DiscoveryChance)
                              };
            agent.CompleteMission(outcome);
            OnMissionCompleted(outcome);
        }

        private static decimal CalculateDiscoveryChance(Agent agent, Lair target)
        {
            var tries = agent.MissionHistory.Count(query => query.Target == target);
            return .05M + (.05M * tries);
        }

        private static decimal CalculateSuccessChance(Agent agent, Lair target)
        {
            var tries = agent.MissionHistory.Count(query => query.Target == target);
            return .20M + (.05M*tries);
        }

        private void OnMissionStarted(MissionDetails details)
        {
            foreach (var observer in _detailObservers)
            {
                try
                {
                    observer.OnNext(details);
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                }
            }
        }

        private void OnMissionCompleted(MissionOutcome outcome)
        {
            foreach (var observer in _outcomeObservers)
            {
                try
                {
                    observer.OnNext(outcome);
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                }
            }
        }

        public IDisposable Subscribe(IObserver<MissionDetails> observer)
        {
            _detailObservers.Add(observer);
            return this;
        }

        public IDisposable Subscribe(IObserver<MissionOutcome> observer)
        {
            _outcomeObservers.Add(observer);
            return this;
        }

        public void Dispose()
        {
            //BUG: this is wrong -> multiple calls shouldn't call complete each time.
            _detailObservers.ForEach(m => m.OnCompleted());
            _outcomeObservers.ForEach(m => m.OnCompleted());
        }
    }
}