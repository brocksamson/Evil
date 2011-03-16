using System.Configuration;

namespace Evil.Infrastructure.Nhibernate
{
    public class EvilConnectionString : IConnectionString
    {
        public string Connection
        {
            get { return ConfigurationManager.ConnectionStrings["Evil"].ConnectionString; }
        }
    }
}