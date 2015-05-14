using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using HomeCenter.AuthenticationDomain;
using Microsoft.Practices.Unity;

namespace HomeCenter
{
    public partial class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (FormsAuthentication.CookiesSupported != true) return;
            if (Request.Cookies[FormsAuthentication.FormsCookieName] == null) return;

            var username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            if (username == null || Bootstrapper.Container == null) return;

            var authenticationService = Bootstrapper.Container.Resolve<IAuthenticationService>();
            var roles = authenticationService.GetRolesForUser(username);

            HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
                new System.Security.Principal.GenericIdentity(username, "Forms"), roles.Split(';'));
        }
    }
}