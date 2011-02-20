using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Evil.Web.Binders
{
    public class FilteredModelBinder : IModelBinder
    {
        private readonly IEnumerable<IFilteredModelBinder> _binders;
        private readonly IModelBinder _defaultBinder;

        public FilteredModelBinder(IEnumerable<IFilteredModelBinder> binders, IModelBinder defaultBinder)
        {
            _binders = binders;
            _defaultBinder = defaultBinder;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            foreach (var binder in _binders.Where(binder => binder.IsMatch(bindingContext.ModelType)))
            {
                return binder.BindModel(controllerContext, bindingContext);
            }
            return _defaultBinder.BindModel(controllerContext, bindingContext);
        }
    }
}