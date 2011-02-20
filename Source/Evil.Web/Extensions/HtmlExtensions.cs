using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Evil.Web.Binders;

namespace Evil.Web.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString SortLinkFor<TObject>(this HtmlHelper helper, Expression<Func<TObject, object>> field,
                                                   string linkText)
        {
            var sorter = (ISortParameters) helper.ViewData[SortableBinder.Constants.SorterName];
            return helper.ActionLink(linkText, helper.CurrentAction(), sorter.GetSortParameters(field));
        }

        public static string CurrentAction(this HtmlHelper helper)
        {
            return "Index";
        }
    }
}