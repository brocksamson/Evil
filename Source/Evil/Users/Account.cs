using Evil.Common;

namespace Evil.Users
{
    public class Account : Entity
    {
        public virtual string EmailAddress { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

    }
}