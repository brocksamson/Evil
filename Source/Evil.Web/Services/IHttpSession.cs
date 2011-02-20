namespace Evil.Web.Services
{
    public interface IHttpSession
    {
        object this[string key] { get; set; }
    }
}