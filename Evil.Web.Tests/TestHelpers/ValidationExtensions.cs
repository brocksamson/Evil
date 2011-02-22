using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using MbUnit.Framework;

namespace Evil.Web.Tests.TestHelpers
{
    public static class ValidationExtensions
    {
        public static void AssertErrorFor<TObject, TMember>(this TObject obj, Expression<Func<TObject, TMember>> expression, TMember value)
            where TObject : class
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);

            //get the initial value
            var func = expression.Compile();
            var initialValue = func.Invoke(obj);

            var type = GetModelType<TObject>();

            var propertyInfo = type.GetProperty(fieldName);
            propertyInfo.SetValue(obj, value, null);

            var messages = GetErrors(obj, propertyInfo);
            foreach (var message in messages)
            {
                Console.WriteLine(message);
            }
            propertyInfo.SetValue(obj, initialValue, null);

            Assert.GreaterThan(messages.Count(), 0, "No Validations errors found for {0}", fieldName);
        }


        public static void AssertValid<T>(this T obj)
        {
            var type = GetModelType<T>();
            var error = false;
            foreach (var propertyInfo in type.GetProperties())
            {
                var messages = GetErrors(obj, propertyInfo);
                foreach (var message in messages)
                {
                    Console.WriteLine(message);
                    error = true;
                }
            }
            Assert.IsFalse(error, "Validation errors found, please check console for specifics.");
        }

        private static IEnumerable<string> GetErrors<TObject>(TObject obj, PropertyInfo propertyInfo)
        {
            var messages = new Collection<string>();
            var attributes = propertyInfo.GetCustomAttributes(false).OfType<ValidationAttribute>().ToList();
            foreach (var attribute in attributes)
            {
                try
                {
                    var context = new ValidationContext(obj, null, null);
                    attribute.Validate(propertyInfo.GetValue(obj, null), context);
                }
                catch (ValidationException ex)
                {
                    messages.Add(ex.Message);
                }
            }
            return messages;
        }

        private static Type GetModelType<TObject>()
        {
            var type = typeof(TObject);

            //this is for a special case
            var meta = type.GetCustomAttributes(false).OfType<MetadataTypeAttribute>().FirstOrDefault();
            if (meta != null)
            {
                type = meta.MetadataClassType;
            }
            return type;
        }        
    }
}