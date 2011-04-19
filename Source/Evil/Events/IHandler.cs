namespace Evil.Events
{
    public interface IHandler<in T> where T : IEventSource
    {
        void Handle(T args);
    }
}