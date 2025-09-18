/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi                                   Assembly : Empiria.WebApi.dll                *
*  Type      : WebApiGlobal                                     Pattern  : Global ASP .NET Class             *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Defines the methods, properties, and events common to all application objects used by         *
*              the Empiria web api platform.                                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Collections.Specialized;
using System.Linq;

using System.Web;
using System.Web.Caching;

namespace Empiria.WebApi {

  /// <summary>Defines the methods, properties, and events common to all application objects used by
  /// the Empiria web api platform.</summary>
  public class WebApiGlobal : HttpApplication {

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

    protected virtual void Application_AuthenticateRequest(object sender, EventArgs e) {
      // no-op
    }

    protected virtual void Application_End(object sender, EventArgs e) {
      // no-op
    }

    protected virtual void Application_Error(object sender, EventArgs e) {
      // no-op
    }

    protected virtual void Application_PreSendRequestHeaders(object sender, EventArgs e) {

      RemoveServerInformationHeaders();

      AddSecurityHeaders();
    }


    protected virtual void Application_Start(object sender, EventArgs e) {

    }

    #endregion Protected methods

    #region Helpers

    private void AddSecurityHeaders() {
      NameValueCollection headers = Response.Headers;

      string[] headersNames = headers.AllKeys;

      if (!headersNames.Contains("X-Content-Type-Options")) {
        headers.Add("X-Content-Type-Options", "nosniff");
      }

      if (!headersNames.Contains("X-Frame-Options")) {
        headers.Add("X-Frame-Options", "DENY");
      }

      if (!headersNames.Contains("X-XSS-Protection")) {
        headers.Add("X-XSS-Protection", "1; mode=block");
      }

      if (!headersNames.Contains("Referrer-Policy")) {
        headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
      }
    }

    private void RemoveServerInformationHeaders() {
      var headers = Response.Headers;

      headers.Remove("Server");
      headers.Remove("X-AspNet-Version");
      headers.Remove("X-AspNetMvc-Version");
      headers.Remove("X-Powered-By");
    }

    #endregion Helpers

  } // class WebApiGlobal

} // namespace Empiria.WebApi
