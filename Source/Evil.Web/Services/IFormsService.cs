using System;
using System.Web;
using System.Web.Security;

namespace Evil.Web.Services
{
    public interface IFormsService
    {
        void SignIn(string emailAddress, bool rememberMe);
        void SignOut();
    }

    public class FormsService : IFormsService
    {
        #region IFormsService Members

        public void SignIn(string emailAddress, bool rememberMe)
        {
            FormsAuthentication.SetAuthCookie(emailAddress, rememberMe);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Abandon();
        }

        #endregion
    }
}