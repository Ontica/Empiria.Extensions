/* Empiria Presentation Framework 2014 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Service-Oriented Framework               System   : Empiria Web Services              *
*  Namespace : Empiria.WebServices                              Assembly : Empiria.WebServices.dll           *
*  Type      : Global                                           Pattern  : Global ASP .NET Class             *
*  Version   : 5.5        Date: 28/Mar/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Defines the methods, properties, and events common to all application objects used by         *
*              Empiria ASP.NET Web Services platform.                                                        *
*                                                                                                            *
********************************* Copyright (c) 1999-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;

namespace Empiria.WebServices {

  /// <summary>Defines the methods, properties, and events common to all application objects used by
  /// Empiria ASP.NET Web Services platform.</summary>
  public class Global : System.Web.HttpApplication {

    #region Fields

    static private readonly string lastExceptionTag = "empiriaLastException";
    //static private int sessionTimeout = 0;
    //static private string rootPath = String.Empty;

    #endregion Fields

    #region Constructors and parsers

    protected Global() {
      // no-op
    }

    #endregion Constructors and parsers

    #region Public properties

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

    //static public string RootPath {
    //  get { return rootPath; }
    //}

    //static internal int SessionTimeout {
    //  get { return sessionTimeout; }
    //}

    #endregion Public properties

    #region Public methods

    /// <summary>Gets the HttpApplicationState object for the current HTTP request.</summary>
    static public HttpApplicationState GetApplication() {
      return HttpContext.Current.Application;
    }

    /// <summary>Gets or sets the HttpApplication object for the current HTTP request.</summary>
    static public HttpApplication GetApplicationInstance() {
      return HttpContext.Current.ApplicationInstance;
    }

    /// <summary>Gets the Cache object for the current HTTP current application.</summary>
    static public Cache GetCache() {
      return HttpRuntime.Cache;
    }

    /// <summary>Gets the HttpContext object for the current HTTP request.</summary>
    static public HttpContext GetContext() {
      return HttpContext.Current;
    }

    /// <summary>Gets the HttpRequest object for the current HTTP request.</summary>
    static public HttpRequest GetRequest() {
      return HttpContext.Current.Request;
    }

    /// <summary>Gets the HttpResponse object for the current HTTP response.</summary>
    static public HttpResponse GetResponse() {
      return HttpContext.Current.Response;
    }

    /// <summary>Gets the HttpServerUtility object that provides methods
    /// used in processing Web requests.</summary>
    static public HttpServerUtility GetServer() {
      return HttpContext.Current.Server;
    }

    /// <summary>Gets the HttpSessionState instance for the current HTTP request.</summary>
    static public HttpSessionState GetSession() {
      return HttpContext.Current.Session;
    }

    #endregion Public methods

    #region Protected methods

    protected void Application_PreSendRequestHeaders(Object sender, EventArgs e) {

    }

    protected void Application_Start(Object sender, EventArgs e) {
      try {
        Empiria.ExecutionServer.Start(Empiria.ExecutionServerType.WebServicesServer);
      } catch (Exception innerException) {
        throw new WebServicesException(WebServicesException.Msg.WebServicesServerInitializationFails, innerException);
      }
    }

    protected void Application_AuthenticateRequest(Object sender, EventArgs e) {

    }

    protected void Application_End(Object sender, EventArgs e) {

    }

    protected void Application_Error(Object sender, EventArgs e) {

    }

    protected void Session_End(Object sender, EventArgs e) {

    }

    protected void Session_Start(Object sender, EventArgs e) {

    }

    #endregion Protected methods

    #region Private methods

    //static private void Initialize() {
    //  try {
    //    sessionTimeout = ConfigurationData.GetInteger("Session.Timeout");
    //    if (!(5 <= sessionTimeout && sessionTimeout <= 600)) {
    //      throw new WebServicesException(WebServicesException.Msg.InvalidSessionTimeout);
    //    }
    //    rootPath = ConfigurationData.GetString("Server.RootPath");
    //    throw new Exception(ConfigurationData.GetStack());
    //  } catch (Exception innerException) {
    //    throw new WebServicesException(WebServicesException.Msg.WebServicesServerInitializationFails, innerException);
    //  }
    //}

    #endregion Private methods

  } // class Global

} // namespace Empiria.Presentation.Web