using System;
using System.Linq.Expressions;

namespace Evil.Web.UnitTests.ViewModels
{
    public abstract class ValidationTestBase<T> where T : class
    {
        protected abstract T Model { get; }

        protected void AssertFieldError(Expression<Func<T, object>> expression, object value)
        {
            //var model = Model;
            //var fieldName = ExpressionUtilities.GetPropertyName(expression);
            //var propertyInfo = typeof(T).GetProperty(fieldName);
            //var oldValue = propertyInfo.GetValue(model, null);
            //propertyInfo.SetValue(model, value, null);
            //var errors = _validator.Validate(model);
            //var error = errors.FirstOrDefault(m => m.PropertyName == fieldName);
            //propertyInfo.SetValue(model, oldValue, null);
            //if (error != null)
            //{
            //    Console.WriteLine(error.Message);
            //    return;
            //}
            //Assert.Fail("No error found for field " + fieldName);
        }

        protected void AssertFormError()
        {
            //var model = Model;
            //var errors = _validator.Validate(model);
            //foreach (var error in errors)
            //{
            //    if (String.IsNullOrEmpty(error.PropertyName))
            //    {
            //        Console.WriteLine(error.Message);
            //        return;
            //    }
            //}
            //Assert.Fail("No error found for form");
        }

    }
}