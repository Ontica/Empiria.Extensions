/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Web Presentation Services         *
*  Namespace : Empiria.Presentation.Web                         Assembly : Empiria.Presentation.Web.dll      *
*  Type      : WebApplication                                   Pattern  : Standard Class                    *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Provides the methods from the current web application.                                        *
*                                                                                                            *
********************************* Copyright (c) 2002-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Web;
using System.Web.Security;

using Empiria.Security;

namespace Empiria.Presentation.Web {

  /// <summary>Provides the methods from the current web application.</summary>
  public abstract class WebApplication : System.Web.HttpApplication {

    #region Fields

    static private readonly string exceptionsPageName = "exception.aspx";
    static private readonly string lastExceptionTag = "empiriaLastException";
    static private readonly string xhtmlMIMEType = "application/xhtml+xml";

    static private int maxLogonAttempts = 3;
    static private int sessionTimeout = 0;
    static private string rootPath = String.Empty;

    #endregion Fields

    #region Constructors and parsers

    protected WebApplication() {
      // no-op
    }

    #endregion Constructors and parsers

    #region Public properties

    static public string DefaultThemesPath {
      get { return ThemesPath + "default/"; }
    }

    static public string ExceptionsPageUrl {
      get { return WebApplication.RootPath + exceptionsPageName; }
    }

    public Exception LastException {
      get {
        if (Server.GetLastError() != null) {
          return Server.GetLastError();
        } else if ((Session != null) && (Session[lastExceptionTag] != null)) {
          return (Exception) Session[lastExceptionTag];
        } else {
          return null;
        }
      }
    }

    static internal string LogonPageUrl {
      get { return WebApplication.RootPath + "default.aspx"; }
    }

    static internal int MaxLogonAttempts {
      get { return maxLogonAttempts; }
    }

    static public string RootPath {
      get { return rootPath; }
    }

    static internal int SessionTimeout {
      get { return sessionTimeout; }
    }

    static public string TemplatesPath {
      get { return WebApplication.RootPath + "templates/"; }
    }

    static public string ThemesPath {
      get { return WebApplication.RootPath + "themes/"; }
    }

    #endregion Public properties

    #region Public methods

    public void OnAuthenticateRequest(object sender, EventArgs e) {
      HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

      if (authCookie != null) {
        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

        authTicket = FormsAuthentication.RenewTicketIfOld(authTicket);

        EmpiriaPrincipal principal = AuthenticationService.Authenticate(authTicket.UserData);

        System.Threading.Thread.CurrentPrincipal = principal;

        this.Context.User = principal;
      }
    }

    public void OnEnd(object sender, EventArgs e) {
      //no-op
    }

    public void OnError(object sender, EventArgs e) {

    }

    public void OnPreSendRequestHeaders(object sender, EventArgs e) {
      if (Array.IndexOf(Request.AcceptTypes, xhtmlMIMEType) != -1 && Server.GetLastError() == null) {
        Response.ContentType = xhtmlMIMEType; // Firefox, Opera, but not IE 6.0
      }
    }

    public void OnSessionEnd(object sender, EventArgs e) {
      try {
        if (ExecutionServer.IsAuthenticated) {
          ExecutionServer.CurrentPrincipal.CloseSession();
        }
        if (Request.IsAuthenticated) {
          FormsAuthentication.SignOut();
        }
        WebContext.DisposeWorkplaceManager();
        if (Response.IsClientConnected) {
          Response.Redirect(WebApplication.LogonPageUrl);
        }
      } catch {
        return;
      }
    }

    public void OnSessionStart(object sender, EventArgs e) {
      SelectSite();
      WebContext.CreateWorkplaceManager();
      Session.Timeout = SessionTimeout;
    }

    public void OnStart(object sender, EventArgs e) {
      Initialize();
    }

    #endregion Public methods

    #region Private methods

    private void Initialize() {
      try {
        maxLogonAttempts = ConfigurationData.GetInteger("Session.MaxLogonAttempts");
        if (!(1 <= maxLogonAttempts && maxLogonAttempts <= 10)) {
          throw new WebPresentationException(WebPresentationException.Msg.InvalidMaxLogAttempts);
        }
        sessionTimeout = ConfigurationData.GetInteger("Session.Timeout");
        if (!(5 <= sessionTimeout && sessionTimeout <= 120)) {
          throw new WebPresentationException(WebPresentationException.Msg.InvalidSessionTimeout);
        }
        rootPath = ConfigurationData.GetString("Server.RootPath");
      } catch (Exception innerException) {
        throw new WebPresentationException(WebPresentationException.Msg.WebApplicationInitializationFails,
                                           innerException);
      }
    }

    private void SelectSite() {
      string userAgent = Request.ServerVariables["HTTP_USER_AGENT"];
      bool isPocketPCClient = (userAgent.IndexOf("Windows CE") != -1);
      bool isMSMobileExplorerClient = (userAgent.IndexOf("MME") != -1);

      if (isPocketPCClient || isMSMobileExplorerClient) {
        var redirectTo = ConfigurationData.GetString("Mobile.Server.RootPath");
        Response.Redirect(redirectTo, true);
      }
    }

    #endregion Private methods

  } // class WebApplication

} // namespace Empiria.Presentation.Web
