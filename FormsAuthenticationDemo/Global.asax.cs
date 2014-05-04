using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FormsAuthenticationDemo.Transformers;
using RedirectToRouteResult = System.Web.Http.Results.RedirectToRouteResult;

namespace FormsAuthenticationDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }

        protected void Application_PostAuthenticateRequest()
        {
            var principal = ClaimsPrincipal.Current;
            var transformer = new AuthenticationManager();
            var newPrincipal = transformer.Authenticate(string.Empty, principal);

            Thread.CurrentPrincipal = newPrincipal;
            HttpContext.Current.User = newPrincipal;
        }

        //void Application_Error(object sender, EventArgs e)
        //{
        //    Exception objErr = Server.GetLastError().GetBaseException();
        //    string err = "Error Caught in Application_Error event\n" +
        //            "Error in: " + Request.Url.ToString() +
        //            "\nError Message:" + objErr.Message.ToString() +
        //            "\nStack Trace:" + objErr.StackTrace.ToString();
        //    Server.ClearError();           
        //}
    }
}
