using System;
using System.Configuration;

namespace Evil.Infrastructure.Nhibernate
{
    public interface IConnectionString
    {
        string Connection { get; }
    }

    public class EvilConnectionString : IConnectionString
    {
        public string Connection
        {
            get { return ConfigurationManager.ConnectionStrings["Evil"].ConnectionString; }
        }
    }
}