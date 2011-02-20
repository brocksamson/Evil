using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Evil.Web.Extensions
{
    public static class ControllerExtensions
    {
        //public static void SetCurrentSort<T>(this Controller controller, 
        //    Expression<Func<T,object>> sortField, SortDirection sortDirection)
        //{
        //    controller.ViewData[Constants.SortFieldName] = ExpressionUtilities.GetPropertyName(sortField);
        //    controller.ViewData[Constants.SortTypeName] = sortDirection;
        //}
    }

    public static class ExpressionUtilities
    {
        public static string GetPropertyName<T>(Expression<Func<T, object>> expression)
        {
            MemberExpression memberExpression = null;
            if (expression.Body.NodeType == ExpressionType.Convert)
            {
                //we need to strip out the convert
                var convert = expression.Body as UnaryExpression;
                if (convert != null) memberExpression = (MemberExpression)convert.Operand;
            }
            return memberExpression == null ? ExpressionHelper.GetExpressionText(expression) : memberExpression.Member.Name;
        }
    }
}
