using System.Web.Mvc;
using Evil.Common;

namespace Evil.Web.Initialization
{
    public class RegisterAreas : IStartupTask
    {
        public void Execute()
        {
            AreaRegistration.RegisterAllAreas();

        }
    }
}
