using FluentNHibernate;
using MbUnit.Framework;
using NHibernate.Tool.hbm2ddl;
using StructureMap;

namespace Evil.Infrastructure.Tests.Schema
{
    [TestFixture]
    public class Generate_schema
    {
        [Test]
        [Ignore("don't want to run accidentally")]
        public void apply_schema_updates()
        {
            var sessionSource = ObjectFactory.GetInstance<ISessionSource>();
            var config = sessionSource.Configuration;
            new SchemaUpdate(config).Execute(System.Console.WriteLine, true);
        }
        
        [Test]
        public void List_pending_schema_changes()
        {
            var sessionSource = ObjectFactory.GetInstance<ISessionSource>();
            var config = sessionSource.Configuration;
            new SchemaUpdate(config).Execute(System.Console.WriteLine, false);
        }   
    }
}