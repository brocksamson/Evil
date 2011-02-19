using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;

namespace Evil.Infrastructure.Nhibernate
{
    public class NHibernateSessionSource : ISessionSource
    {
        private readonly Configuration _configuration;
        private readonly IConnectionString _connectionString;
        private readonly IEnumerable<IMappingConfiguration> _mappingConfigurations;
        private readonly ISessionFactory _sessionFactory;

        public NHibernateSessionSource(IEnumerable<IMappingConfiguration> mappingConfigurations,
                                       IConnectionString connectionString)
        {
            _mappingConfigurations = mappingConfigurations;
            _connectionString = connectionString;
            _configuration = AssembleConfiguration(null);
            _sessionFactory = _configuration.BuildSessionFactory();
        }

        #region ISessionSource Members

        public ISession CreateSession()
        {
            return _sessionFactory.OpenSession();
        }

        public void BuildSchema()
        {
            throw new NotImplementedException();
        }

        public Configuration Configuration
        {
            get { return _configuration; }
        }

        public ISessionFactory SessionFactory
        {
            get { return _sessionFactory; }
        }

        #endregion

        public Configuration AssembleConfiguration(string mappingExportPath)
        {
            return Fluently.Configure()
                .Mappings(mappings => _mappingConfigurations.ToList().ForEach(m => m.Configure(mappings)))
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(_connectionString.Connection)
                              .ShowSql()
                              .FormatSql())
                .BuildConfiguration();
        }
    }
}