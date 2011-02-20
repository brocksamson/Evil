using System;
using System.Globalization;
using System.Web.Mvc;
using Evil.Users;
using Evil.Web.Binders;
using Evil.Web.Services;
using MbUnit.Framework;
using Rhino.Mocks;
// ReSharper disable PossibleNullReferenceException
// ReSharper disable InconsistentNaming

namespace Evil.Web.UnitTests.Binders
{
    [TestFixture]
    public class when_binding_player : BaseBinderTest
    {
        private PlayerBinder _playerBinder;
        private IUserProvider _userProvider;
        private Player _player;

        protected override Type ModelType
        {
            get { return typeof(Player); }
        }

        protected override string PropertyName
        {
            get { return "account"; }
        }

        [SetUp]
        public void SetUp()
        {
            Init();
            _player = new Player { Name = "Name" };
            _userProvider = MockRepository.GenerateMock<IUserProvider>();

            _userProvider.Stub(m => m.CurrentPlayer()).Return(_player);
            _playerBinder = new PlayerBinder(_userProvider);
            _valueProvider.Stub(m => m.GetValue(PropertyName)).Return(new ValueProviderResult(null, null, CultureInfo.CurrentUICulture));
        }

        [Test]
        public void should_load_account_correctly()
        {
            var player = _playerBinder.BindModel(_controllerContext, _bindingContext) as Player;
            Assert.IsNotNull(player);
            Assert.AreEqual(player.Name, _player.Name);
        }

        [Test]
        public void should_match_correctly()
        {
            Assert.IsTrue(_playerBinder.IsMatch(ModelType));
        }
    }
}
// ReSharper restore PossibleNullReferenceException
// ReSharper restore InconsistentNaming
