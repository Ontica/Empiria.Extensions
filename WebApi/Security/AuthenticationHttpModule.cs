/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi                                   Assembly : Empiria.WebApi.dll                *
*  Type      : AuthenticationHttpModule                         Pattern  : IHttpModule                       *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Http module for Empiria web api authentication services.                                      *
*                                                                                                            *
********************************* Copyright (c) 2014-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Threading;
using System.Web;

using Empiria.Security;

namespace Empiria.WebApi {

  /// <summary>Http module for Empiria web api authentication services.</summary>
  [Serializable]
  public sealed class AuthenticationHttpModule : IHttpModule {

    #region Fields

    static string _realm = null;
    static private string Realm {
      get {
        if (_realm == null) {
          _realm = ConfigurationData.GetString("AuthenticationHttpModule.Realm");
        }
        return _realm;
      }
    }

    #endregion Fields

    #region Public methods

    static public EmpiriaPrincipal Authenticate(string apiClientKey, string userName, string password,
                                                  string entropy = "", int contextId = -1) {
      Assertion.AssertObject(apiClientKey, "apiClientKey");
      Assertion.AssertObject(userName, "userName");
      Assertion.AssertObject(password, "password");

      EmpiriaPrincipal principal = EmpiriaIdentity.Authenticate(apiClientKey, userName, password,
                                                                entropy, contextId);
      Assertion.AssertObject(principal, "principal");
      AuthenticationHttpModule.SetPrincipal(principal);

      return principal;
    }

    static public EmpiriaPrincipal AuthenticateGuest(string apiClientKey, int contextId = -1) {
      Assertion.AssertObject(apiClientKey, "apiClientKey");

      EmpiriaPrincipal principal = EmpiriaIdentity.Authenticate(apiClientKey, AnonymousUser.Guest, contextId);
      Assertion.AssertObject(principal, "principal");
      AuthenticationHttpModule.SetPrincipal(principal);

      return principal;
    }

    public void Init(HttpApplication httpApplication) {
      httpApplication.AuthenticateRequest += OnApplicationAuthenticateRequest;
      httpApplication.EndRequest += OnApplicationEndRequest;
    }

    public void Dispose() {

    }

    #endregion Constructors and parsers

    #region Private methods

    static private string GetAuthenticationHeaderValue() {
      string authenticationHeader = WebApiUtilities.TryGetRequestHeader("Authorization");

      if (String.IsNullOrWhiteSpace(authenticationHeader)) {
        return String.Empty;
      }
      var headerValue = AuthenticationHeaderValue.Parse(authenticationHeader);
      // RFC 2617 sec 1.2
      if (headerValue.Scheme.ToLowerInvariant() == "bearer" &&
          headerValue.Parameter != null) {
        return headerValue.Parameter;
      } else {
        throw new WebApiException(WebApiException.Msg.BadAuthenticationHeaderFormat);
      }
    }

    static private void LogAndThrowExceptionAsResponse(HttpStatusCode httpStatusCode,
                                                       Exception exception, WebApiAuditTrail auditLog) {
      // 1) Publish the exception in the eventlog
      Messaging.Publisher.Publish(exception);

      // 2) Clear the current response and set their status code
      var response = HttpContext.Current.Response;
      response.ClearContent();
      response.StatusCode = (int) httpStatusCode;

      // 3) Put the exception in a standard json response
      var exceptionAsJson = WebApiUtilities.GetExceptionAsJsonObject(exception, HttpContext.Current.Request);
      response.Write(exceptionAsJson.ToString(true));

      // 4) Try to write an audit log in the system database
      auditLog.Write(HttpContext.Current.Request, "Authentication",
                     HttpContext.Current.Response, exception);

      // 5) Send the response to the Http client.
      response.End();
    }

    static private void SetPrincipal(EmpiriaPrincipal principal) {
      Thread.CurrentPrincipal = principal;
      if (HttpContext.Current != null) {
        HttpContext.Current.User = principal;
        WebApiRequest.Current.SetPrincipal(principal);
      }
    }

    static private void OnApplicationAuthenticateRequest(object sender, EventArgs e) {
      var auditLog = new WebApiAuditTrail();
      try {
        string sessionToken = GetAuthenticationHeaderValue();
        if (!String.IsNullOrWhiteSpace(sessionToken)) {
          EmpiriaPrincipal principal = EmpiriaIdentity.Authenticate(sessionToken);
          SetPrincipal(principal);
        } else {
          // no-op
          // There isn't an authentication header so probably is an AllowAnonymous method call
        }
      } catch (WebApiException innerEx) {
        LogAndThrowExceptionAsResponse(HttpStatusCode.Unauthorized, innerEx, auditLog);
      } catch (SecurityException innerEx) {
        LogAndThrowExceptionAsResponse(HttpStatusCode.Unauthorized, innerEx, auditLog);
      } catch (Exception innerEx) {
        LogAndThrowExceptionAsResponse(HttpStatusCode.InternalServerError, innerEx, auditLog);
      }
    }

    // If the request was unauthorized, add the WWW-Authenticate header to the response.
    static private void OnApplicationEndRequest(object sender, EventArgs e) {
      var response = HttpContext.Current.Response;

      if (response.StatusCode == (int) HttpErrorCode.Unauthorized) {
        response.Headers.Add("WWW-Authenticate",
                             String.Format("Basic realm=\"{0}\"", Realm));
      }
    }

    #endregion Private methods

  }  // class AuthenticationHttpModule

}  // namespace Empiria.WebApi
