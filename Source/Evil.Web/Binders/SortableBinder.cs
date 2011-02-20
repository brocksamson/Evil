using System;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Evil.Web.Binders
{
    public class SortableBinder : IFilteredModelBinder
    {
        public class Constants
        {
            public const string SorterName = "Sorter";
            public const char FieldSeparator = '.';
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext == null) throw new ArgumentNullException("controllerContext");
            if (bindingContext == null) throw new ArgumentNullException("bindingContext");
            var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var fieldData = string.Empty;
            if(result != null)
                fieldData = (string) result.ConvertTo(typeof (string));
            var sorter = string.IsNullOrEmpty(fieldData)
                                ? CreateDefaultSorter(bindingContext.ModelType, bindingContext.ModelName)
                                : CreateSorter(bindingContext.ModelType, bindingContext.ModelName, fieldData);
            controllerContext.Controller.ViewData[Constants.SorterName] = sorter;
            return sorter;
        }

        public bool IsMatch(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Sorter<>);
        }

        private static object CreateSorter(Type modelType, string modelName, string fieldData)
        {
            var fieldInfo = fieldData.Split(Constants.FieldSeparator);
            var sortDirection = (SortDirection)Enum.Parse(typeof (SortDirection), fieldInfo[1], true);
            return InitializeSorter(modelType, modelName, fieldInfo[0], sortDirection);
        }


        private static object CreateDefaultSorter(Type modelType, string modelName)
        {
            var classType = modelType.GetGenericArguments()[0];
            foreach (var property in classType.GetProperties())
            {
                var attributes = property.GetCustomAttributes(typeof(DefaultSortAttribute), true);
                if (attributes != null && attributes.Length > 0)
                {
                    return InitializeSorter(modelType, modelName, property.Name,
                                            ((DefaultSortAttribute) attributes[0]).SortDirection);
                }
            }
            throw new ArgumentException(string.Format("Class {0} Doesn't contain a Property marked with [DefaultSort]", classType.FullName));
        }

        private static object InitializeSorter(Type sorterType, string modelName, string fieldName, SortDirection sort)
        {
            return Activator.CreateInstance(sorterType, modelName, fieldName, sort);
        }
    }
}
