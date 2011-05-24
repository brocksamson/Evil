using System;
using System.Reactive.Linq;
using Evil.Agents;
using Evil.Common;

namespace Evil.Missions
{
    public class MissionTimer : ISubscriber<MissionBrief>
    {
        private readonly ILocator _locator;

        public MissionTimer(ILocator locator)
        {
            _locator = locator;
        }

        public IDisposable Subscribe(IObservable<MissionBrief> source)
        {
            return source.Subscribe(
                brief =>
                    {
                        var timerSource =
                            Observable.Return(brief.Agent.Id).Delay(brief.MissionStart.Add(brief.MissionDuration));
                        timerSource.Subscribe(
                            agentId =>
                                {
                                    var repo = _locator.GetInstance<IRepository<Agent>>();
                                    var agent = repo.GetById(agentId);
                                    var mission = _locator.GetInstance<InfiltrationMission>();
                                    mission.Complete(agent);
                                });
                    });
        }
    }

    public interface ISubscriber<in T>
    {
        IDisposable Subscribe(IObservable<T> source);
    }
}