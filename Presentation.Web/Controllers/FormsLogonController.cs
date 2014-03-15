/* Empiria Presentation Framework 2014 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Web Presentation Framework        *
*  Namespace : Empiria.Presentation.Web.Controllers             Assembly : Empiria.Presentation.Web.dll      *
*  Type      : FormsLogonController                             Pattern  : Logon Controller Class            *
*  Version   : 5.5        Date: 28/Mar/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Logon controller class that uses .NET Forms Authentication.                                   *
*                                                                                                            *
********************************* Copyright (c) 2009-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Web;
using System.Web.Security;
using Empiria.Presentation.Controllers;
using Empiria.Security;

namespace Empiria.Presentation.Web.Controllers {

  public sealed class FormsLogonController : LogonController {

    #region Constructors and parsers

    public FormsLogonController() {

    }

    #endregion Constructors and parsers

    #region Public methods

    public bool Logon(string userName, string password, int regionId) {
      bool success = base.Logon(WebContext.Session.SessionID, userName, password, regionId);

      if (success) {
        SetLastWorkplaceCookie(regionId.ToString());
      }
      return success;
    }

    public string GetLastAuthenticatedUserName() {
      System.Web.HttpCookie cookie = WebContext.Request.Cookies["empiriaLastUserName_" + ExecutionServer.LicenseName];

      if (cookie != null) {
        return cookie.Value; //WebContext.Server.HtmlDecode(cookie.Value);
      } else {
        return String.Empty;
      }
    }

    public string GetLastAuthenticatedWorkplace() {
      System.Web.HttpCookie cookie = WebContext.Request.Cookies["empiriaLastWorkplace_" + ExecutionServer.LicenseName];

      if (cookie != null) {
        return cookie.Value;
      } else {
        return Empiria.ExecutionServer.OrganizationId.ToString();
      }
    }

    #endregion Public methods

    #region Protected methods

    protected override void OnAuthenticateFails() {
      AttemptsCount++;
      if (AttemptsCount > WebApplication.MaxLogonAttempts) {
        WebContext.Response.Redirect(ProductInformation.Url);
      }
    }

    protected override void OnAuthenticate(EmpiriaPrincipal principal) {
      EmpiriaIdentity identity = (EmpiriaIdentity) principal.Identity;

      CreateAuthenticationTicket(identity.User.UserName, identity.Session.Token);
      SetLastUserNameCookie(identity.User.UserName);
      ExecutionServer.CurrentPrincipal = principal;
      AttemptsCount = 0;
      WebContext.WorkplaceManager.Start(true);
    }

    protected override void OnSignOut() {
      FormsAuthentication.SignOut();
    }

    protected override void OnValidate() {
      ValidateClient();
    }

    /// <summary>Throws a secutiry exception if the authentication request arise from 
    /// an unauthorized client.</summary>
    static private void ValidateClient() {
      string problemMsg = null;

      if (WebContext.Request.HttpMethod.ToLowerInvariant() != "post") {
        problemMsg = SecurityException.GetMessage(SecurityException.Msg.InvalidHttpMethod);
      } else {
        string urlPath = WebContext.Request.Url.GetLeftPart(UriPartial.Path).ToLowerInvariant();
        //if  ((urlPath != WebApplication.LogonPageURL) && (urlPath != MobileWebApplication.LogonPageURL)) {
        //  problemMsg = SecurityException.GetMessage(SecurityException.Msg.InvalidRequestPath) + " URL = " + urlPath;
        //}
      }
      if (problemMsg != null) {
        SecurityException exception =
                          new SecurityException(SecurityException.Msg.InvalidAuthenticationClient, problemMsg);
        exception.Publish();
        throw exception;
      }
    }

    #endregion Protected methods

    #region Private properties

    private int AttemptsCount {
      get {
        if (WebContext.Session["LogonAttemptsCount"] == null) {
          WebContext.Session.Add("LogonAttemptsCount", 0);
        }
        return (int) WebContext.Session["LogonAttemptsCount"];
      }
      set {
        if (WebContext.Session["LogonAttemptsCount"] == null) {
          WebContext.Session.Add("LogonAttemptsCount", value);
        } else {
          WebContext.Session["LogonAttemptsCount"] = value;
        }
      }
    }

    #endregion Private properties

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

    private void CreateLastUserCookie() {

    }

    private void SetLastUserNameCookie(string userName) {
      System.Web.HttpCookie cookie = new System.Web.HttpCookie("empiriaLastUserName_" + ExecutionServer.LicenseName);

      cookie.Value = userName;
      cookie.Expires = DateTime.Now.AddDays(3d);

      WebContext.Response.Cookies.Add(cookie);
    }

    private void SetLastWorkplaceCookie(string workplace) {
      System.Web.HttpCookie cookie = new System.Web.HttpCookie("empiriaLastWorkplace_" + ExecutionServer.LicenseName);

      cookie.Value = workplace;
      cookie.Expires = DateTime.Now.AddDays(3d);

      WebContext.Response.Cookies.Add(cookie);
    }

    #endregion Private methods

  } // class FormsLogonController

} // namespace Empiria.Presentation.Web.Controllers