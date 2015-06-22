/* Empiria Presentation Framework 2015 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Service-Oriented Framework               System   : Empiria Web Services              *
*  Namespace : Empiria.WebServices                              Assembly : Empiria.WebServices.dll           *
*  Type      : AuthenticationHttpModule                         Pattern  : IHttpModule                       *
*  Version   : 6.5        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : HTTP module for Empiria web authentication services.                                          *
*                                                                                                            *
********************************* Copyright (c) 2003-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Threading;
using System.Web;

using Empiria.Security;

namespace Empiria.WebServices {

  /// <summary>HTTP module for Empiria web authentication services.</summary>
  [Serializable]
  public sealed class AuthenticationHttpModule : IHttpModule {

    #region Fields

    static private readonly string Realm = ConfigurationData.GetString("AuthenticationHttpModule.Realm");

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

    static private string GetAuthenticationHeader() {
      var request = HttpContext.Current.Request;

      return request.Headers["Authorization"];
    }

    static private string GetAuthenticationHeaderValue() {
      string authenticationHeader = GetAuthenticationHeader();

      if (String.IsNullOrWhiteSpace(authenticationHeader)) {
        return String.Empty;
      }
      var headerValue = AuthenticationHeaderValue.Parse(authenticationHeader);
      // RFC 2617 sec 1.2
      if (headerValue.Scheme.ToLowerInvariant() == "bearer" &&
          headerValue.Parameter != null) {
        return headerValue.Parameter;
      } else {
        throw new SecurityException(SecurityException.Msg.BadAuthenticationHeaderFormat, GetRequestData());
      }
    }

    static private string GetRequestData() {
      var request = HttpContext.Current.Request;

      string data = "UserAddress: " + GetUserHostAddress() + "\n";
      data += "Path: " + request.Path;

      return data;
    }

    static private string GetUserHostAddress() {
      if (HttpContext.Current != null && HttpContext.Current.Request != null) {
        return HttpContext.Current.Request.UserHostAddress;
      } else {
        return "0.0.0.0";
      }
    }

    static private void SetPrincipal(EmpiriaPrincipal principal) {
      Thread.CurrentPrincipal = principal;
      if (HttpContext.Current != null) {
        HttpContext.Current.User = principal;
      }
    }

    static private void OnApplicationAuthenticateRequest(object sender, EventArgs e) {
      try {
        string sessionToken = GetAuthenticationHeaderValue();
        if (!String.IsNullOrWhiteSpace(sessionToken)) {
          EmpiriaPrincipal principal = EmpiriaIdentity.Authenticate(sessionToken);
          AuthenticationHttpModule.SetPrincipal(principal);
        } else {
          // no-op
          // There isn't an authentication header so probably is an AllowAnonymous method call
        }
      } catch (SecurityException innerEx) {
        Messaging.Publisher.Publish(innerEx);
        ThrowExceptionAsResponse(HttpStatusCode.Unauthorized, innerEx);
      } catch (Exception innerEx) {
        Messaging.Publisher.Publish(innerEx);
        ThrowExceptionAsResponse(HttpStatusCode.InternalServerError, innerEx);
      }
    }

    // If the request was unauthorized, add the WWW-Authenticate header to the response.
    static private void OnApplicationEndRequest(object sender, EventArgs e) {
      var response = HttpContext.Current.Response;

      if (response.StatusCode == (int) HttpErrorCode.Unauthorized) {
        response.Headers.Add("WWW-Authenticate",
                             String.Format("Basic realm=\"{0}\"", AuthenticationHttpModule.Realm));
      }
    }

    static private void ThrowExceptionAsResponse(HttpStatusCode httpStatusCode, Exception exception) {
      var response = HttpContext.Current.Response;
      response.ClearContent();
      response.StatusCode = (int) httpStatusCode;
      response.Write(exception.Message);
      response.End();
    }

    #endregion Private methods

  }  // class AuthenticationHttpModule

}  // namespace Empiria.WebServices
