using Evil.Common;
using Evil.Infrastructure.Nhibernate;
using FluentNHibernate;
using NHibernate;
using StructureMap.Configuration.DSL;

namespace Evil.Infrastructure.Structuremap
{
    public class DomainRegistry : Registry
    {
        public DomainRegistry()
        {
            For(typeof (IRepository<>)).Use(typeof (Repository<>));
            Scan(scanner =>
                     {
                         scanner.TheCallingAssembly();
                         scanner.WithDefaultConventions();
                         scanner.AddAllTypesOf<IStartupTask>();
                         scanner.AddAllTypesOf<IMappingConfiguration>();
                         scanner.ConnectImplementationsToTypesClosing(typeof (IRepository<>));
                     });
            For<IConnectionString>().Singleton().Use<EvilConnectionString>();
            For<ISessionSource>().Singleton().Use<NHibernateSessionSource>();
            For<ITransactionBoundary>().HybridHttpOrThreadLocalScoped().Use<NHibernateTransactionBoundary>();
            For<ISession>().Use(c => c.GetInstance<ITransactionBoundary>().CurrentSession);
        }
    }

}