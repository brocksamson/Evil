using FluentNHibernate.Cfg;

namespace Evil.Infrastructure.Nhibernate
{
    public interface IMappingConfiguration
    {
        void Configure(MappingConfiguration mappingConfig);
    }
}