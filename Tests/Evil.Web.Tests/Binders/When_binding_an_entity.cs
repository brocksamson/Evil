using System;
using System.Web.Mvc;
using Evil.Common;
using Evil.TestHelpers;
using Evil.Web.Binders;
using Evil.Web.Tests.TestHelpers;
using MbUnit.Framework;
using MvcContrib.TestHelper;
using Rhino.Mocks;

namespace Evil.Web.Tests.Binders
{
    [TestFixture]
    public class When_binding_an_entity
    {
        private EntityBinder _entityBinder;
        private IDependencyResolver _resolver;
        private IRepository<TestEntity> _repository;

        [SetUp]
        public void Arrange()
        {
            _resolver = MockRepository.GenerateMock<IDependencyResolver>();
            _repository = MockRepository.GenerateMock<IRepository<TestEntity>>();
            _resolver.Stub(m => m.GetService(typeof(IRepository<TestEntity>))).Return(_repository);
            _entityBinder = new EntityBinder(_resolver);
        }

        [Test]
        public void Should_accept_type_that_implement_entity()
        {
            Assert.IsTrue(_entityBinder.IsMatch(typeof(TestEntity)));
        }

        [Test]
        public void Should_deny_type_that_does_not_implement_entity()
        {
            Assert.IsFalse(_entityBinder.IsMatch(typeof(DateTime)));
        }

        [Test]
        public void Should_request_correct_repository_type()
        {
            const int id = 1;
            const string propertyName = "entity";

            var builder = new TestControllerBuilder();
            var controllerContext = new ControllerContext(builder.HttpContext, builder.RouteData, builder.CreateController<FakeController>());
            var metaDataProvider = new DataAnnotationsModelMetadataProvider();
            var bindingContext = new ModelBindingContext
                                     {
                                         ModelName = propertyName,
                                         ValueProvider = new FormCollection
                                                             {
                                                                 {propertyName, id.ToString()}
                                                             },
                                         ModelMetadata = new ModelMetadata(metaDataProvider, null, null, typeof(TestEntity), propertyName)
                                     };
            _entityBinder.BindModel(controllerContext, bindingContext);
            var requestedType = _resolver.GetArgumentsForCallsMadeOn(m => m.GetService(null)).First<Type>();
            var requestedId = _repository.GetArgumentsForCallsMadeOn(m => m.GetById(0)).First<int>();
            Assert.AreEqual(typeof(IRepository<TestEntity>), requestedType);
            Assert.AreEqual(id, requestedId);
        }

        [Test]
        public void Guard_for_reflection_call()
        {
            //this test will fail if reflection call changes -> gives us better warning...
            var repoType = typeof(IRepository<TestEntity>);
            var info = repoType.GetMethod("GetById");
            Assert.IsNotNull(info, "Method GetById on IRepository was changed, please update reflection call in EntityBinder");
            var parameters = info.GetParameters();
            Assert.Count(1, parameters);
            Assert.AreEqual(typeof(int), parameters[0].ParameterType);
        }

        public class TestEntity : Entity { }

        public class TestValueProvider : IValueProvider
        {
            public bool ContainsPrefix(string prefix)
            {
                throw new NotImplementedException();
            }

            public ValueProviderResult GetValue(string key)
            {
                throw new NotImplementedException();
            }
        }
    }
}