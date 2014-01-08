/* Empiria® Presentation Framework 2014 **********************************************************************
*                                                                                                            *
*  Solution  : Empiria® Presentation Framework                  System   : Web Presentation Framework        *
*  Namespace : Empiria.Presentation.Web.Controllers             Assembly : Empiria.Presentation.Web.dll      *
*  Type      : GuestLogonController                             Pattern  : Logon Controller Class            *
*  Date      : 28/Mar/2014                                      Version  : 5.5     License: CC BY-NC-SA 4.0  *
*                                                                                                            *
*  Summary   : Logon controller class for Guest User Authentication.                                         *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2014. **/
using System;
using System.Web;
using System.Web.Security;
using Empiria.Presentation.Controllers;
using Empiria.Security;

namespace Empiria.Presentation.Web.Controllers {

  public sealed class GuestLogonController : LogonController {

    #region Constructors and parsers

    public GuestLogonController() {

    }

    #endregion Constructors and parsers

    #region Public methods

    public bool Logon() {
      return base.GuestLogon();
    }

    #endregion Public methods

    #region Protected methods

    protected override void OnAuthenticateFails() {
      throw new SecurityException(SecurityException.Msg.GuestUserNotFound);
    }

    protected override void OnAuthenticate(EmpiriaPrincipal principal) {
      EmpiriaIdentity identity = (EmpiriaIdentity) principal.Identity;

      CreateAuthenticationTicket(identity.User.UserName, identity.Session.Token);
      ExecutionServer.CurrentPrincipal = principal;

      WebContext.WorkplaceManager.Start(false);
    }

    protected override void OnSignOut() {
      FormsAuthentication.SignOut();
    }

    protected override void OnValidate() {

    }

    #endregion Protected methods

    #region Private methods

    private void CreateAuthenticationTicket(string userName, string userData) {
      FormsAuthenticationTicket ticket = null;
      string cookieStr = String.Empty;

      ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now,
                                             DateTime.Now.AddHours(10), true, userData);
      cookieStr = FormsAuthentication.Encrypt(ticket);
      HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieStr);
      WebContext.Response.Cookies.Add(cookie);
    }

    #endregion Private methods

  } // class GuestLogonController

} // namespace Empiria.Presentation.Web.Controllers