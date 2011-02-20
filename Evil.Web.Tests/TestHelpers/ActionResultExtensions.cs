using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using MbUnit.Framework;

namespace Evil.Web.Tests.TestHelpers
{
    public static class ActionResultExtensions
    {
        public static void IsModelErrorFor<T>(this ActionResult result, Expression<Func<T, string>> expression)
        {
            Assert.IsInstanceOfType(typeof (ViewResult), result);
            var viewResult = result as ViewResult;
            string fieldName = ExpressionHelper.GetExpressionText(expression);
            Assert.IsTrue(!viewResult.ViewData.ModelState.IsValidField(fieldName),
                        fieldName + " was supposed to contain an error");
        }

        public static void IsModelErrorFor<T>(this ActionResult result, Expression<Func<T, double>> expression)
        {
            Assert.IsInstanceOfType(typeof(ViewResult), result);
            var viewResult = result as ViewResult;
            string fieldName = ExpressionHelper.GetExpressionText(expression);
            Assert.IsTrue(!viewResult.ViewData.ModelState.IsValidField(fieldName),
                        fieldName + " was supposed to contain an error");
        }
    }
}