using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Evil.Infrastructure.Nhibernate;
using Evil.Web.Initialization;

namespace Evil.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        void Application_EndRequest(object sender, EventArgs e)
        {
            var instance = DependencyResolver.Current.GetService<ITransactionBoundary>();
            try
            {
                instance.Commit();
            }
            catch
            {
                instance.RollBack();
                throw;
            }
            finally
            {
                instance.Dispose();
            }
        }

        void Application_BeginRequest(object sender, EventArgs e)
        {
            var transactionBoundary = DependencyResolver.Current.GetService<ITransactionBoundary>();
            transactionBoundary.Begin();
        }

        protected void Application_Start()
        {
            new WebBootstrapper().Bootstrap();
        }
    }
}