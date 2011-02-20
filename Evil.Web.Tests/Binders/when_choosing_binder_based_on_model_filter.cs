using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Mvc;
using Evil.Web.Binders;
using MbUnit.Framework;
using Rhino.Mocks;

// ReSharper disable PossibleNullReferenceException
// ReSharper disable InconsistentNaming

namespace Evil.Web.UnitTests.Binders
{
    [TestFixture]
    public class when_choosing_binder_based_on_model_filter : BaseBinderTest
    {
        private FilteredModelBinder _filteredBinder;
        private IEnumerable<IFilteredModelBinder> _binders;
        private string _returnValue;
        private int _returnInt;
        private IModelBinder _defaultBinder;
        private string _defaultValue;

        protected override Type ModelType
        {
            get { return typeof (string); }
        }

        protected override string PropertyName
        {
            get { return "property"; }
        }

        [SetUp]
        public void SetUp()
        {
            Init();
            _returnValue = "ReturnedValue";
            _defaultValue = "DefaultValue";
            _returnInt = 1;

            _defaultBinder = MockRepository.GenerateMock<IModelBinder>();
            _defaultBinder.Stub(m => m.BindModel(null, null)).IgnoreArguments().Return(_defaultValue);

            _binders = new Collection<IFilteredModelBinder>
                           {
                               new TestFilteredModelBinder(ModelType, _returnValue),
                               new TestFilteredModelBinder(typeof(int), _returnInt),
                           };
            _filteredBinder = new FilteredModelBinder(_binders, _defaultBinder);
        }

        [Test]
        public void should_choose_binder_correctly()
        {
            var result = _filteredBinder.BindModel(_controllerContext, _bindingContext);
            Assert.AreEqual(result.ToString(), _returnValue);
        }

        [Test]
        public void should_use_default_if_no_match()
        {
            var notFoundBindingContext = CreateBindingContext(PropertyName, typeof (TestFilteredModelBinder));
            var result = _filteredBinder.BindModel(_controllerContext, notFoundBindingContext);
            Assert.AreEqual(result.ToString(), _defaultValue);
        }
    }

    public class TestFilteredModelBinder : IFilteredModelBinder
    {
        private readonly Type _modelType;
        private readonly object _returnValue;

        public TestFilteredModelBinder(Type modelType, object returnValue)
        {
            _modelType = modelType;
            _returnValue = returnValue;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return _returnValue;
        }

        public bool IsMatch(Type type)
        {
            return _modelType == type;
        }
    }
}

// ReSharper restore PossibleNullReferenceException
// ReSharper restore InconsistentNaming