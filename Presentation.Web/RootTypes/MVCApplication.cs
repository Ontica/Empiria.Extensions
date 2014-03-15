/* Empiria Presentation Framework 2014 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Web Presentation Framework        *
*  Namespace : Empiria.Presentation.Web                         Assembly : Empiria.Presentation.Web.dll      *
*  Type      : MVCApplication                                   Pattern  : Standard Class                    *
*  Version   : 5.5        Date: 28/Mar/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Provides the methods from the current MVC web application.                                    *
*                                                                                                            *
********************************* Copyright (c) 2009-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Web;
using System.Web.Security;

namespace Empiria.Presentation.Web {

  /// <summary>Provides the methods from the current MVC web application.</summary>
  public abstract class MVCApplication : HttpApplication {

    #region Fields

    static private readonly string exceptionsPageName = "exception.aspx";
    static private readonly string lastExceptionTag = "empiriaLastException";
    static private readonly string xhtmlMIMEType = "application/xhtml+xml";

    static private int maxLogonAttempts = -1;
    static private int sessionTimeout = 0;
    static private string rootPath = String.Empty;

    #endregion Fields

    #region Constructors and parsers

    protected MVCApplication() {
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
      string cookieName = FormsAuthentication.FormsCookieName;
      HttpCookie authCookie = Request.Cookies[cookieName];

      if (authCookie != null) {
        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        authTicket = FormsAuthentication.RenewTicketIfOld(authTicket);
      }
    }

    public void OnEnd(object sender, EventArgs e) {
      //no-op
    }

    public void OnError(object sender, EventArgs e) {
      //var error = Server.GetLastError();
      //var code = (error is HttpException) ? (error as HttpException).GetHttpCode() : 500;

      //if (code != 404) {
      //  // Generate email with error details and send to administrator
      //  // I'm using RazorMail which can be downloaded from the Nuget Gallery
      //  // I also have an extension method on type Exception that creates a string representation
      //  var email = new RazorMailMessage("Website Error");
      //  email.To.Add("errors@wduffy.co.uk");
      //  email.Templates.Add(error.GetAsHtml(new HttpRequestWrapper(Request)));
      //  Kernel.Get<IRazorMailSender>().Send(email);
      //}

      //Response.Clear();
      //Server.ClearError();

      //string path = Request.Path;
      //Context.RewritePath(string.Format("~/Errors/Http{0}", code), false);
      //IHttpHandler httpHandler = new MvcHttpHandler();
      //httpHandler.ProcessRequest(Context);
      //Context.RewritePath(path, false);
    }

    public void OnPreSendRequestHeaders() {
      if (Array.IndexOf(Request.AcceptTypes, xhtmlMIMEType) != -1 && Server.GetLastError() == null) {
        Response.ContentType = xhtmlMIMEType; // Firefox, Opera, but not IE 6.0
      }
    }

    public void OnSessionEnd(object sender, EventArgs e) {
      try {
        if (ExecutionServer.CurrentPrincipal != null) {
          ExecutionServer.CurrentPrincipal.Dispose();
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
      Empiria.ExecutionServer.Start(ExecutionServerType.WebApplicationServer);
      
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
        throw new WebPresentationException(WebPresentationException.Msg.WebApplicationInitializationFails, innerException);
      }
    }

    private void SelectSite() {
      string userAgent = Request.ServerVariables["HTTP_USER_AGENT"];
      bool isPocketPCClient = (userAgent.IndexOf("Windows CE") != -1);
      bool isMSMobileExplorerClient = (userAgent.IndexOf("MME") != -1);

      if (isPocketPCClient || isMSMobileExplorerClient) {
        string mobileAppDefaultUrl = ConfigurationData.GetString("Mobile.Server.RootPath");
        Response.Redirect(mobileAppDefaultUrl, true);
      }
    }

    #endregion Private methods

  } // class MVCApplication

} // namespace Empiria.Presentation.Web