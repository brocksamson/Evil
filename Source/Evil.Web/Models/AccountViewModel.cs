namespace Evil.Web.Models
{
    public class AccountCreationModel
    {
        public string ConfirmPassword { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
    }
}