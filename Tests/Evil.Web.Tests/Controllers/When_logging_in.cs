﻿using System.Web.Mvc;
using Evil.Common;
using Evil.Tests.TestHelpers;
using Evil.Users;
using Evil.Web.Controllers;
using Evil.Web.Models;
using Evil.Web.Services;
using Evil.Web.Tests.TestHelpers;
using MbUnit.Framework;
using MvcContrib.TestHelper;
using Rhino.Mocks;

namespace Evil.Web.UnitTests.Controllers
{
    [TestFixture]
    public class When_logging_in
    {
        #region Setup/Teardown

        [SetUp]
        public void Arrange()
        {

            _login = new LoginModel {EmailAddress = _emailAddress, Password = _password};
            _mother = new ObjectMother();
            _account = _mother.GetAccountByEmailAddress(_emailAddress, _password);
            _currentPlayer = new Player
            {
                Account = _account,
                Name = "Player1"
            };

            _accountRepository = new InMemoryRepository<Account>(_account);
            _playerRepository = new InMemoryRepository<Player>(_currentPlayer);

            _formsService = MockRepository.GenerateMock<IFormsService>();

            _controller = new AccountController(_accountRepository, _playerRepository, _formsService);
        }

        #endregion

        private const string _emailAddress = "userName@email.com";
        private const string _password = "password";
        private const bool _rememberMe = false;
        private const string _invalidEmailAddress = "notReal@test.com";
        private AccountController _controller;
        private InMemoryRepository<Account> _accountRepository;
        private LoginModel _login;
        private ObjectMother _mother;
        private Account _account;
        private IFormsService _formsService;
        private InMemoryRepository<Player> _playerRepository;
        private Player _currentPlayer;

        [Test]
        public void empty_emailAddress_creates_an_error()
        {
            _login.EmailAddress = "";
            var result = _controller.Login(_login);
            result.IsModelErrorFor<LoginModel>(m => m.EmailAddress);
        }

        [Test]
        public void empty_password_creates_an_error()
        {
            _login.Password = "";
            var result = _controller.Login(_login);
            result.IsModelErrorFor<LoginModel>(m => m.Password);
        }

        [Test]
        public void invalid_emailAddress_creates_an_error()
        {
            _login.EmailAddress = "invalidEmail";
            var result = _controller.Login(_login);
            result.IsModelErrorFor<LoginModel>(m => m.EmailAddress);
        }

        [Test]
        public void invalid_password_creates_an_error()
        {
            _login.Password = "WrongPassword";
            var result = _controller.Login(_login);
            result.IsModelErrorFor<LoginModel>(m => m.Password);
        }

        [Test]
        public void unknown_emailAddress_creates_an_error()
        {
            _login.EmailAddress = _invalidEmailAddress;
            var result = _controller.Login(_login);
            result.IsModelErrorFor<LoginModel>(m => m.EmailAddress);
        }

        [Test]
        public void valid_username_password_succeeds()
        {
            var success = _controller.Login(_login);
            Assert.IsInstanceOfType<RedirectToRouteResult>(success);
            _formsService.AssertWasCalled(m => m.SignIn(_emailAddress, _rememberMe));
        }

        [Test]
        public void success_redirects_to_game_in_progress()
        {
            var success = _controller.Login(_login);
            Assert.IsInstanceOfType<RedirectToRouteResult>(success);
            var redirect = success as RedirectToRouteResult;
            //TODO: strong type these.
            Assert.AreEqual(redirect.RouteValues["Controller"], "Game");
            Assert.AreEqual(redirect.RouteValues["Action"], "Index");
        }


        [Test]
        public void success_redirects_to_start_game_if_no_current_game()
        {
            _playerRepository.Clear();
            var success = _controller.Login(_login);
            var redirect = success.AssertActionRedirect().ToAction<GameController>(m => m.Start());
        }
    }
}