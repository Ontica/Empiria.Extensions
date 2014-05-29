using System;
using System.ComponentModel;
using System.Web;
using System.Web.Security;

using Empiria.Security;

namespace Empiria.Services {

  public class EmpiriaWebService : System.Web.Services.WebService {

    private IContainer components = null;

    public EmpiriaWebService() {

    }

    public void Authenticate() {
      string cookieName = FormsAuthentication.FormsCookieName;
      HttpCookie authCookie = Context.Request.Cookies[cookieName];
      if (authCookie != null) {
        FormsAuthenticationTicket authTicket = null;
        authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        authTicket = FormsAuthentication.RenewTicketIfOld(authTicket);
        EmpiriaIdentity identity = Empiria.Security.EmpiriaIdentity.Authenticate(Context.Request.Url, authTicket);
        HttpContext.Current.Session["Principal"] = new EmpiriaPrincipal(identity);
      } else {
        throw new Exception("Remote user not authentificated.");
      }
    }

    protected override void Dispose(bool disposing) {
      if (disposing && components != null) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

  } // class EmpiriaWebService

} //namespace Empiria.Services
