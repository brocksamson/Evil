namespace Evil.Common
{
    public interface ILocator
    {
        T GetInstance<T>();
    }
}