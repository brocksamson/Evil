using Evil.Web.Models;
using MbUnit.Framework;

namespace Evil.Web.UnitTests.ViewModels
{
    [TestFixture]
    public class when_validating_an_AccountCreationViewModel : ValidationTestBase<AccountCreationModel>
    {
        private AccountCreationModel _model;

        [SetUp]
        public void SetUp()
        {
            _model = new ObjectMother().GetValidAccount();
        }

        [Test]
        public void error_if_email_address_invalid()
        {
            AssertFieldError(m => m.EmailAddress, "notValid");
        }

        [Test]
        public void error_if_email_address_null()
        {
            AssertFieldError(m => m.EmailAddress, null);
        }

        [Test]
        public void error_if_first_name_not_entered()
        {
            AssertFieldError(m => m.FirstName, null);
        }

        [Test]
        public void error_if_last_name_not_entered()
        {
            AssertFieldError(m => m.LastName, null);
        }

        [Test]
        public void passwords_must_Match()
        {
            _model.ConfirmPassword = "NotTheRightPassword";
            AssertFormError();
        }

        protected override AccountCreationModel Model
        {
            get { return _model; }
        }
    }
}