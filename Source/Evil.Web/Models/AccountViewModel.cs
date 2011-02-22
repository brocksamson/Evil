using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Evil.Web.Models
{
    public class AccountCreationModel
    {
        [Required, Email()]
        public virtual string EmailAddress { get; set; }
        
        public virtual string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password must match Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public virtual string FirstName { get; set; }
        [Required]
        public virtual string LastName { get; set; }
    }

    public class EmailAttribute : RegularExpressionAttribute
    {
        public EmailAttribute() 
            : base(@"^(([A-Za-z0-9_\+\-]+\.)*[A-Za-z0-9_\+\-]+@([A-Za-z0-9]+\.)+([A-Za-z]{2,4})(\s*(;)\s*))*([A-Za-z0-9_\+\-]+\.)*[A-Za-z0-9_\+\-]+@([A-Za-z0-9]+\.)+([A-Za-z]{2,4})$")
        {
            ErrorMessage = "{0} is not a valid email address";
        }
    }
}