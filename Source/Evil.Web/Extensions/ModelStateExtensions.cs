using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Evil.Web.Extensions
{
    public static class ModelStateExtensions
    {
        public static bool IsValidFieldFor<TModel, TObject>
            (this ModelStateDictionary modelState, Expression<Func<TModel, TObject>> expression)
        {
            string propertyName = ExpressionHelper.GetExpressionText(expression);
            return modelState.IsValidField(propertyName);
        }

        public static void AddModelErrorFor<TModel, TObject>
            (this ModelStateDictionary modelState, Expression<Func<TModel, TObject>> expression, string message)
        {
            string propertyName = ExpressionHelper.GetExpressionText(expression);
            modelState.AddModelError(propertyName, message);
        }
    }
}