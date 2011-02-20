using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace Evil.Web.Binders
{
    public interface ISortParameters
    {
        RouteValueDictionary GetSortParameters<T, TPropType>(Expression<Func<T, TPropType>> fieldName);
    }
    public class Sorter<T> : ISortParameters
    {
        private readonly string _modelName;
        private readonly string _fieldName;
        private readonly Expression<Func<T, object>> _sortExpression;
        public SortDirection SortDirection { get; private set; }

        public Func<T, object> SortField
        {
            get { return _sortExpression.Compile(); }
        }

        public Sorter(string modelName, string fieldName, SortDirection sortDirection)
        {
            SortDirection = sortDirection;
            _sortExpression = CreateSortField(fieldName);
            _modelName = modelName;
            _fieldName = fieldName;
        }

        private static Expression<Func<T, object>> CreateSortField(string fieldName)
        {
            var classType = typeof(T);
            var param = Expression.Parameter(classType, "m");
            var memberInfo = classType.GetProperty(fieldName);
            var expr = Expression.MakeMemberAccess(param, memberInfo);
            var convert = Expression.Convert(expr, typeof(object));
            return Expression.Lambda<Func<T, object>>(convert, param);
        }

        public RouteValueDictionary GetSortParameters<TType, TPropType>(Expression<Func<TType, TPropType>> fieldName)
        {
            var sortField = ExpressionHelper.GetExpressionText(fieldName);
            var sortDirection = SortDirection.Ascending;
            if(sortField == _fieldName && SortDirection == SortDirection.Ascending)
            {
                sortDirection = SortDirection.Descending;
            }
            var fieldData = sortField
                + SortableBinder.Constants.FieldSeparator + sortDirection;
            return new RouteValueDictionary
                       {
                           {_modelName, fieldData}
                       };
        }

        public IOrderedEnumerable<T> Sort(IEnumerable<T> list)
        {
            return SortDirection == SortDirection.Ascending
                ? list.OrderBy(SortField)
                : list.OrderByDescending(SortField);

        }
    }
}