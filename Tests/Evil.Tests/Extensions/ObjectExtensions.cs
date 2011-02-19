using System;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using MbUnit.Framework;

namespace Evil.Tests.Extensions
{
    public static class ObjectExtensions
    {
        public static void SetProperty<TObject, TProperty>(this TObject obj,
                                                           Expression<Func<TObject, TProperty>> expression,
                                                           TProperty value)
        {
            MemberExpression memberExpression = null;
            switch (expression.Body.NodeType)
            {
                case ExpressionType.Convert:
                    {
                        //we need to strip out the convert
                        var convert = expression.Body as UnaryExpression;
                        if (convert != null) memberExpression = (MemberExpression) convert.Operand;
                    }
                    break;
                case ExpressionType.MemberAccess:
                    memberExpression = (MemberExpression) expression.Body;
                    break;
            }
            if (memberExpression == null ||
                memberExpression.Member.MemberType != MemberTypes.Property) return;
            string propertyName = memberExpression.Member.Name;
            PropertyInfo prop = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            prop.SetValue(obj, value, null);
        }

        public static void AssertEqual(this object obj, object objCompare)
        {
            AssertEqual(obj, objCompare, string.Format("Objects were not equal: {0}, {1}", obj, objCompare));
        }

        public static void AssertEqual(this object obj, object objCompare, string message)
        {
            if (ReferenceEquals(obj, objCompare))
                return;

            var type = obj.GetType();
            while (type != null && !type.Namespace.Contains("Evil"))
                type = type.BaseType;

            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var valueOfThisObject = property.GetValue(obj, null);
                var valueToCompareTo = property.GetValue(objCompare, null);

                if (valueOfThisObject == null && valueToCompareTo == null)
                    continue;
                if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                {
                    if ((valueOfThisObject == null ^ valueToCompareTo == null) ||
                        (!valueOfThisObject.Equals(valueToCompareTo)))
                    {
                        Assert.Fail(message);
                    }
                }
                else if (valueOfThisObject as IEnumerable != null)
                {
                    CompareList(valueOfThisObject, valueToCompareTo, message);
                }
                else
                {
                    AssertEqual(valueOfThisObject, valueToCompareTo);
                }

            }
        }


        private static void CompareList(object obj, object objCompare, string message)
        {

            var objList = obj as IEnumerable;
            var objCompareList = objCompare as IEnumerable;

            int cnt = 0;
            foreach (var o1 in objList)
            {
                int cntInner = 0;
                foreach (var o2 in objCompareList)
                {
                    if (cntInner++ == cnt)
                        AssertEqual(o1, o2);
                }
                cnt++;
            }
        }
    }
}