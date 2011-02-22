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
    public class when_binding_account : BaseBinderTest
    {
        private AccountBinder _accountBinder;
        private IUserProvider _userProvider;
        private Account _account;

        protected override Type ModelType
        {
            get { return typeof(Account); }
        }

        protected override string PropertyName
        {
            get { return "account"; }
        }

        [SetUp]
        public void SetUp()
        {
            Init();
            _account = new Account {FirstName = "FName"};
            _userProvider = MockRepository.GenerateMock<IUserProvider>();            

            _userProvider.Stub(m => m.CurrentAccount()).Return(_account);
            _accountBinder = new AccountBinder(_userProvider);
            _valueProvider.Stub(m => m.GetValue(PropertyName)).Return(new ValueProviderResult(null, null, CultureInfo.CurrentUICulture));
        }

        [Test]
        public void should_load_account_correctly()
        {
            var account = _accountBinder.BindModel(_controllerContext, _bindingContext) as Account;
            Assert.IsNotNull(account);
            Assert.AreEqual(account.FirstName, _account.FirstName);
        }

        [Test]
        public void should_match_correctly()
        {
            Assert.IsTrue(_accountBinder.IsMatch(ModelType));
        }
    }
}
// ReSharper restore PossibleNullReferenceException
// ReSharper restore InconsistentNaming
