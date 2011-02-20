using Evil.Users;
using Evil.Web.Models;

namespace Evil.Web.UnitTests
{
    internal class ObjectMother
    {
        private const string _firstname = "FirstName";
        private const string _lastname = "LastName";
        private const string _password = "password";

        public AccountCreationModel GetValidAccount()
        {
            return new AccountCreationModel
                       {
                           FirstName = _firstname,
                           LastName = _lastname,
                           Password = _password,
                           ConfirmPassword = _password,
                           EmailAddress = "email@address.com"
                       };
        }

        public Account GetAccountByEmailAddress(string emailAddress)
        {
            return GetAccountByEmailAddress(emailAddress, _password);
        }

        public Account GetAccountByEmailAddress(string emailAddress, string password)
        {
            return new Account
                       {EmailAddress = emailAddress, FirstName = _firstname, LastName = _lastname, Password = password};
        }
    }
}