using System;
using System.Web.Mvc;
using Evil.Common;

namespace Evil.Web.Binders
{
    public class EntityBinder : IFilteredModelBinder
    {
        private readonly IDependencyResolver _resolver;

        public EntityBinder(IDependencyResolver resolver)
        {
            _resolver = resolver;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var providerValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (providerValue == null) return null;
            if (string.IsNullOrEmpty(providerValue.AttemptedValue)) return null;
            int entityId;
            if (!int.TryParse(providerValue.AttemptedValue, out entityId)) return null;

            var repositoryType = typeof(IRepository<>).MakeGenericType(bindingContext.ModelType);
            var repository = _resolver.GetService(repositoryType);

            //eww -> can't case IRepository right, so reflecting for now...
            return repositoryType.GetMethod("GetById").Invoke(repository, new object[] { entityId });
        }

        public bool IsMatch(Type type)
        {
            return typeof(Entity).IsAssignableFrom(type);
        }
    }
}