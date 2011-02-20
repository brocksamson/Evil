using System.Collections.Generic;
using System.Web.Mvc;
using Evil.Common;
using Evil.Web.Binders;

namespace Evil.Web.Initialization
{
    public class ModelBinderInitializer : IStartupTask
    {
        private IEnumerable<IFilteredModelBinder> _binders;

        public ModelBinderInitializer(IEnumerable<IFilteredModelBinder> binders)
        {
            _binders = binders;
        }

        public void Execute()
        {
            var defaultBinder = ModelBinders.Binders.DefaultBinder;
            ModelBinders.Binders.DefaultBinder = new FilteredModelBinder(_binders, defaultBinder);
        }
    }
}
