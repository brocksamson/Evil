using System.Web.Mvc;
using Evil.Common;
using Evil.Users;
using Evil.Web.Controllers;
using Evil.Web.Extensions;
using Evil.Web.Models;
using Evil.Web.Services;
using MbUnit.Framework;
using Rhino.Mocks;

namespace Evil.Web.UnitTests.Controllers
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class When_creating_an_account
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            var mother = new ObjectMother();
            _model = mother.GetValidAccount();
            _account = mother.GetAccountByEmailAddress(_model.EmailAddress);
            _accountRepository = MockRepository.GenerateMock<IRepository<Account>>();
            _formsService = MockRepository.GenerateMock<IFormsService>();
            _playerRepository = MockRepository.GenerateMock<IRepository<Player>>();
            _controller = new AccountController(_accountRepository, _playerRepository, _formsService);
        }

        #endregion

        private AccountController _controller;
        private AccountCreationModel _model;
        private IRepository<Account> _accountRepository;
        private Account _account;
        private IFormsService _formsService;
        private IRepository<Player> _playerRepository;

        [Test]
        public void duplicate_emailAddress_displays_error()
        {
            _accountRepository.Expect(m => m.Get.ByEmailAddress(_model.EmailAddress)).Return(_account);
            ActionResult result = _controller.Create(_model);
            Assert.IsInstanceOfType(typeof (ViewResult), result);
            var viewResult = result as ViewResult;
            Assert.IsTrue(!viewResult.ViewData.ModelState.IsValidFieldFor<AccountCreationModel>(m => m.EmailAddress),
                        "EmailAddress address should be invalid");
        }

        [Test]
        public void duplicate_emailAddress_ignores_case()
        {
            _accountRepository.Expect(m => m.Get.ByEmailAddress(_model.EmailAddress)).Return(_account);
            ActionResult result = _controller.Create(_model);
            Assert.IsInstanceOfType(typeof (ViewResult), result);
            var viewResult = result as ViewResult;
            Assert.IsTrue(!viewResult.ViewData.ModelState.IsValidFieldFor<AccountCreationModel>(m => m.EmailAddress),
                        "EmailAddress address should be invalid");
        }

        [Test]
        [Ignore("Not implemented yet")]
        public void invalid_captcha_displays_error()
        {
        }

        [Test]
        public void new_player_is_created()
        {
            _accountRepository.Expect(m => m.Get.ByEmailAddress(_model.EmailAddress)).Return(new Account());
            _accountRepository.Expect(m => m.Save(null)).IgnoreArguments();
            ActionResult result = _controller.Create(_model);
            Assert.IsInstanceOfType(typeof (RedirectToRouteResult), result);
            var accountList = _accountRepository.GetArgumentsForCallsMadeOn(m => m.Save(null));
            Assert.IsInstanceOfType(typeof(Account), accountList[0][0]);
            var player = accountList[0][0] as Account;
            Assert.AreEqual(player.FirstName, _model.FirstName);
            Assert.AreEqual(player.LastName, _model.LastName);
            Assert.AreEqual(player.Password, _model.Password);
            Assert.AreEqual(player.EmailAddress, _model.EmailAddress);
        }

        [Test]
        [Ignore("Not current functionality")]
        public void redirects_to_Created_page()
        {
            //before site goes live this needs to be updated
        }

        [Test]
        [Ignore("Not implemented yet")]
        public void sends_authentication_email()
        {
        }
    }

    // ReSharper restore InconsistentNaming
}