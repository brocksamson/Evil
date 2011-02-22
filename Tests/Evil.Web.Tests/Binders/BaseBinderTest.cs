using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Evil.Web.Tests.TestHelpers;
using Rhino.Mocks;

namespace Evil.Web.UnitTests.Binders
{
    public abstract class BaseBinderTest
    {
        protected ControllerContext _controllerContext;
        protected RequestContext _requestContext;
        protected HttpContextBase _httpContext;
        protected IValueProvider _valueProvider;
        protected ModelMetadataProvider _metadataProvider;
        protected ModelBindingContext _bindingContext;
        protected abstract Type ModelType { get; }
        protected abstract string PropertyName { get; }

        public void Init()
        {
            _httpContext = MockRepository.GenerateMock<HttpContextBase>();
            _requestContext = new RequestContext(_httpContext, new RouteData());
            _controllerContext = new ControllerContext(_requestContext, new FakeController());
            _valueProvider = MockRepository.GenerateMock<IValueProvider>();
            _metadataProvider = MockRepository.GenerateMock<ModelMetadataProvider>();

            _bindingContext = CreateBindingContext(PropertyName, ModelType);
        }

        protected ModelBindingContext CreateBindingContext(string propertyName, Type modelType)
        {
            return new ModelBindingContext()
                                  {
                                      FallbackToEmptyPrefix = true,
                                      ModelName = propertyName,
                                      ModelMetadata = new ModelMetadata(_metadataProvider, null, null, modelType, propertyName),
                                      ValueProvider = _valueProvider
                                  };
        }
    }
}