using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Evil.Web.Extensions
{
    public static class ModelStateExtensions
    {
        public static bool IsValidFieldFor<TModel>
            (this ModelStateDictionary modelState, Expression<Func<TModel, object>> expression)
        {
            string propertyName = ExpressionHelper.GetExpressionText(expression);
            return modelState.IsValidField(propertyName);
        }

        public static void AddModelErrorFor<TModel>
            (this ModelStateDictionary modelState, Expression<Func<TModel, object>> expression, string message)
        {
            string propertyName = ExpressionHelper.GetExpressionText(expression);
            modelState.AddModelError(propertyName, message);
        }
    }
}