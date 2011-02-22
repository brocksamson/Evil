using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Evil.Web.Binders;
using MbUnit.Framework;
using Rhino.Mocks;

namespace Evil.Web.UnitTests.Binders
{
    [TestFixture]
    public class when_creating_sort_function : BaseBinderTest
    {
        private Sortable _sort;
        private SortableBinder _sortableBinder;

        protected override Type ModelType
        {
            get { return typeof(Sorter<Sortable>); }
        }

        protected override string PropertyName
        {
            get { return "sortField"; }
        }

        [SetUp]
        public void SetUp()
        {            
            Init();
            _sortableBinder = new SortableBinder();
            _sort = new Sortable
                        {
                            AnotherString = "AnotherString",
                            DoubleField = 12.23,
                            EnumField = ExpressionType.And,
                            Id = 5,
                            Name = "Name"
                        };
        }

        [Test]
        public void should_work_with_int()
        {
            var func = Act("Id", "Descending");
            var result = func.Invoke(_sort);
            Assert.AreEqual(result, _sort.Id);
        }

        private Func<Sortable, object> Act(string fieldName, string sortDirection)
        {
            var fieldData = string.Empty;
            if(!string.IsNullOrEmpty(fieldName))
                fieldData = fieldName + "." + sortDirection;
            var valueResult = new ValueProviderResult(fieldData, fieldData, CultureInfo.CurrentUICulture);

            _valueProvider.Stub(m => m.GetValue(PropertyName)).Return(valueResult);

            var sorter = (Sorter<Sortable>) _sortableBinder.BindModel(_controllerContext, _bindingContext);
            return sorter.SortField;
        }

        [Test]
        public void should_work_with_string()
        {
            var func = Act("Name", "Ascending");
            var result = func.Invoke(_sort);
            Assert.AreEqual(result, _sort.Name);
        }

        [Test]
        public void should_work_with_enum()
        {
            var func = Act("EnumField", "Ascending");
            var result = func.Invoke(_sort);
            Assert.AreEqual(result, _sort.EnumField);
        }

        [Test]
        public void should_work_with_double()
        {
            var func = Act("DoubleField", "Ascending");
            var result = func.Invoke(_sort);
            Assert.AreEqual(result, _sort.DoubleField);
        }

        [Test]
        public void should_work_with_composite_string()
        {
            var func = Act("CompositeField", "Ascending");
            var result = func.Invoke(_sort);
            Assert.AreEqual(result, _sort.CompositeField);
        }

        [Test]
        public void should_default_field_using_DefaultSort()
        {
            var func = Act(null, null);
            var result = func.Invoke(_sort);
            Assert.AreEqual(_sort.Name, result);
        }

        //[Test]
        //public void should_error_if_no_default_field()
        //{
        //    Assert.Throws(typeof (ArgumentException), () => _sorter.BindField(typeof (Sortable2), null));
        //}
    }

    public class Sortable
    {
        public int Id { get; set; }
        [DefaultSort(SortDirection.Ascending)]
        public string Name { get; set; }
        public ExpressionType EnumField { get; set; }
        public double DoubleField { get; set; }
        public string AnotherString { get; set; }
        public string CompositeField { get { return Name + "const" + AnotherString; } }
    }

    public class Sortable2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ExpressionType EnumField { get; set; }
        public double DoubleField { get; set; }
        public string AnotherString { get; set; }
        public string CompositeField { get { return Name + "const" + AnotherString; } }
    }
}
