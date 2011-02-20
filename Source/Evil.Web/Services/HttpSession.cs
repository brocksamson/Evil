using System.Web;

namespace Evil.Web.Services
{
    public class HttpSession : IHttpSession
    {
        public object this[string key]
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    return HttpContext.Current.Session[key];
                }
                return null;
            }
            set
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session[key] = value;
                }
            }

        }
    }
}