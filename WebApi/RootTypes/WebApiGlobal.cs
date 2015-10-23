/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi                                   Assembly : Empiria.WebApi.dll                *
*  Type      : WebApiGlobal                                     Pattern  : Global ASP .NET Class             *
*  Version   : 1.0                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Defines the methods, properties, and events common to all application objects used by         *
*              the Empiria web api platform.                                                                 *
*                                                                                                            *
********************************* Copyright (c) 2014-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Web;
using System.Web.Caching;

namespace Empiria.WebApi {

  /// <summary>Defines the methods, properties, and events common to all application objects used by
  /// the Empiria web api platform.</summary>
  public class WebApiGlobal : System.Web.HttpApplication {

    #region Constructors and parsers

    protected WebApiGlobal() {
      // no-op
    }

    #endregion Constructors and parsers

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

    #endregion Public methods

    #region Protected methods

    protected virtual void Application_PreSendRequestHeaders(Object sender, EventArgs e) {

    }

    protected virtual void Application_Start(Object sender, EventArgs e) {
      ExecutionServer.Start(ExecutionServerType.WebApiServer);
    }

    protected virtual void Application_AuthenticateRequest(Object sender, EventArgs e) {

    }

    protected virtual void Application_End(Object sender, EventArgs e) {

    }

    protected virtual void Application_Error(Object sender, EventArgs e) {

    }

    #endregion Protected methods

  } // class WebApiGlobal

} // namespace Empiria.WebApi
