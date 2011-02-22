using System.Linq;
using System.Web.Mvc;
using Evil.Common;
using Evil.Tests.TestHelpers;
using Evil.Users;
using Evil.Web.Controllers;
using Evil.Web.Extensions;
using Evil.Web.Models;
using Evil.Web.Services;
using Evil.Web.Tests.TestHelpers;
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
            _existingAccount = mother.GetAccountByEmailAddress(_model.EmailAddress);
            _newModel = mother.GetValidAccount();
            _newModel.EmailAddress = "newemail@test.com";
            _accountRepository = new InMemoryRepository<Account>(_existingAccount);
            _formsService = MockRepository.GenerateMock<IFormsService>();
            _playerRepository = MockRepository.GenerateMock<IRepository<Player>>();
            _controller = new AccountController(_accountRepository, _playerRepository, _formsService);
        }

        #endregion

        private AccountController _controller;
        private AccountCreationModel _model;
        private InMemoryRepository<Account> _accountRepository;
        private Account _existingAccount;
        private IFormsService _formsService;
        private IRepository<Player> _playerRepository;
        private AccountCreationModel _newModel;

        [Test]
        public void duplicate_emailAddress_displays_error()
        {
            ActionResult result = _controller.Create(_model);
            Assert.IsInstanceOfType(typeof (ViewResult), result);
            var viewResult = result as ViewResult;
            Assert.IsTrue(!viewResult.ViewData.ModelState.IsValidFieldFor<AccountCreationModel, string>(m => m.EmailAddress),
                        "EmailAddress address should be invalid");
        }

        [Test]
        public void Should_validate_account_correctly()
        {
            var model = new ObjectMother().GetValidAccount();
            model.AssertValid();
            model.AssertErrorFor(m => m.EmailAddress, "notValid");
            model.AssertErrorFor(m => m.EmailAddress, null);
            model.AssertErrorFor(m => m.FirstName, null);
            model.AssertErrorFor(m => m.LastName, null);
            model.AssertErrorFor(m => m.ConfirmPassword, "nottherightpassword");
        }

        [Test]
        public void Should_validate_player_correctly()
        {
            var player = new CreatePlayerView
            {
                Name = "Dr. Evil",
                BaseName = "My Base",
                BaseLatitude = 37.771008,
                BaseLongitude = -122.41175
            };
            player.AssertErrorFor(m => m.Name, null);
            player.AssertErrorFor(m => m.BaseLatitude, -91);
            player.AssertErrorFor(m => m.BaseLatitude, 91);
            player.AssertErrorFor(m => m.BaseLongitude, -181);
            player.AssertErrorFor(m => m.BaseLongitude, 181);
        }


        [Test]
        public void duplicate_emailAddress_ignores_case()
        {
            var result = _controller.Create(_model);
            Assert.IsInstanceOfType(typeof (ViewResult), result);
            var viewResult = result as ViewResult;
            Assert.IsTrue(!viewResult.ViewData.ModelState.IsValidFieldFor<AccountCreationModel, string>(m => m.EmailAddress),
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
            var result = _controller.Create(_newModel);
            Assert.IsInstanceOfType(typeof (RedirectToRouteResult), result);
            var player = _accountRepository.GetSavedEntities().First();
            Assert.AreEqual(player.FirstName, _newModel.FirstName);
            Assert.AreEqual(player.LastName, _newModel.LastName);
            Assert.AreEqual(player.Password, _newModel.Password);
            Assert.AreEqual(player.EmailAddress, _newModel.EmailAddress);
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