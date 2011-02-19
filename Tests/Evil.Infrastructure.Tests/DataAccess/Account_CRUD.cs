using System;
using Evil.Infrastructure.Nhibernate;
using Evil.Infrastructure.Tests.Schema;
using Evil.Users;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using MbUnit.Framework;
using NHibernate;

namespace Evil.Infrastructure.Tests.DataAccess
{
    [TestFixture]
    public class Account_CRUD : DbTest
    {
        private Repository<Account> _accountRepository;

        #region Setup/Teardown

        protected override void Arrange()
        {
            _accountRepository = new Repository<Account>(_session);
        }

        #endregion

        private void AccountEquals(Account account, Account savedAccount)
        {
            Assert.AreEqual(account.EmailAddress, savedAccount.EmailAddress);
            Assert.AreEqual(account.FirstName, savedAccount.FirstName);
            Assert.AreEqual(account.LastName, savedAccount.LastName);
            Assert.AreEqual(account.Password, savedAccount.Password);
        }

        private Account CreateAccount()
        {
            var account = CreateNewAccount();
            Assert.AreEqual(0, account.Id);
            _accountRepository.Save(account);
            Assert.GreaterThan(account.Id, 0);
            return account;
        }

        public static Account CreateNewAccount()
        {
            return new Account
                       {
                           EmailAddress = "email@TEST.COM",
                           FirstName = "FirstName",
                           LastName = "LastName",
                           Password = "Password"
                       };
        }

        [Test]
        public void gets_by_EmailAddress()
        {
            var account = CreateAccount();

            var savedAccount = _accountRepository.Get.ByEmailAddress(account.EmailAddress);
            AccountEquals(account, savedAccount);
        }

        [Test]
        public void gets_by_EmailAddress_is_not_case_sensitive()
        {
            var account = CreateAccount();

            var lowerCaseEmail = account.EmailAddress.ToLower();
            Assert.AreNotEqual(lowerCaseEmail, account.EmailAddress);
            Account savedAccount = _accountRepository.Get.ByEmailAddress(lowerCaseEmail);
            AccountEquals(account, savedAccount);
        }
    }
}