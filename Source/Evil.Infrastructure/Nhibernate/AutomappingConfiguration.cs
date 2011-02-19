using System;
using System.Linq;
using System.Reflection;
using Evil.Bases;
using Evil.Common;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Linq;

namespace Evil.Infrastructure.Nhibernate
{
    public class AutomappingConfiguration : IMappingConfiguration
    {
        #region IMappingConfiguration Members

        public void Configure(MappingConfiguration mappingConfig)
        {
            var assemblies = Assembly.GetAssembly(typeof (Entity));
            var conventions = new IConvention[]
                       {
                           DefaultCascade.All(),
                           ConventionBuilder.Id.Always(m => m.GeneratedBy.HiLo("1000"))
                       };

            mappingConfig.AutoMappings.Add(
                AutoMap.Assemblies(new AutoMapConfig(), assemblies)
                    .Conventions
                    .Add(conventions));
        }

        #endregion

        #region Nested type: AutoMapConfig

        public class AutoMapConfig : DefaultAutomappingConfiguration
        {
            public override bool IsComponent(Type type)
            {
                return type == typeof (Position);
            }

            public override bool AbstractClassIsLayerSupertype(Type type)
            {
                return type == typeof(Entity);
            }
            public override bool ShouldMap(Type type)
            {
                var assignableFrom = typeof(Entity).IsAssignableFrom(type);
                return assignableFrom;
            }
        }

        #endregion
    }

    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public T GetById(int id)
        {
            return _session.Load<T>(id);
        }

        public IQueryable<T> Get
        {
            get { return _session.Query<T>(); }
        }

        public void Save(T entity)
        {
            _session.SaveOrUpdate(entity);
        }

        public void Delete(T entity)
        {
            _session.Delete(entity);
        }
    }
}