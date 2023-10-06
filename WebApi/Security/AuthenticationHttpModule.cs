/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Core Services                        Component : Security services                     *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Http Module                           *
*  Type     : AuthenticationHttpModule                     License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Http module for Empiria Web Api authentication services.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using System.Net;
using System.Net.Http.Headers;
using System.Threading;
using System.Web;

using Empiria.Security;

namespace Empiria.WebApi {

  /// <summary>Http module for Empiria Web Api authentication services.</summary>
  [Serializable]
  public sealed class AuthenticationHttpModule : IHttpModule {

    #region Public methods

    static internal IEmpiriaPrincipal AuthenticateFormer(UserCredentialsDto credentials) {
      Assertion.Require(credentials, nameof(credentials));

      IEmpiriaPrincipal principal = AuthenticationService.Authenticate(credentials);

      SetHttpContextPrincipal(principal);

      return principal;
    }


    static public void SetHttpContextPrincipal(IEmpiriaPrincipal principal) {
      Assertion.Require(principal, nameof(principal));

      Thread.CurrentPrincipal = principal;

      if (HttpContext.Current != null) {
        HttpContext.Current.User = principal;
        WebApiRequest.Current.SetPrincipal(principal);
      }
    }


    public void Init(HttpApplication context) {
      context.AuthenticateRequest += OnApplicationAuthenticateRequest;
    }


    public void Dispose() {
      // no-op
    }

    #endregion Constructors and parsers

    #region Helpers

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

      // 1) Clear the current response and set their status code

      var response = HttpContext.Current.Response;

      response.TrySkipIisCustomErrors = true;

      response.ClearContent();

      response.StatusCode = (int) httpStatusCode;

      // 2) Put the exception in a standard json response
      var exceptionAsJson = WebApiUtilities.GetExceptionAsJsonObject(exception, HttpContext.Current.Request);
      response.Write(exceptionAsJson.ToString(true));

      // 3) Try to write an audit log in the system database
      auditLog.Write(HttpContext.Current.Request, "Authentication",
                     HttpContext.Current.Response, exception);

      // 4) Send the response to the Http client.
      response.End();
    }

    static private void OnApplicationAuthenticateRequest(object sender, EventArgs e) {
      var auditLog = new WebApiAuditTrail();

      try {

        ExecutionServer.UserHostAddress = HttpContext.Current.Request.UserHostAddress;

        string sessionToken = GetAuthenticationHeaderValue();

        if (!String.IsNullOrWhiteSpace(sessionToken)) {

          IEmpiriaPrincipal principal = AuthenticationService.Authenticate(sessionToken,
                                                                           HttpContext.Current.Request.UserHostAddress);

          SetHttpContextPrincipal(principal);

        } else {
          // no-op
          // There isn't an authentication header, so probably is an AllowAnonymous method call
        }

      } catch (WebApiException innerEx) {
        LogAndThrowExceptionAsResponse(HttpStatusCode.Unauthorized, innerEx, auditLog);

      } catch (SecurityException innerEx) {
        LogAndThrowExceptionAsResponse(HttpStatusCode.Unauthorized, innerEx, auditLog);

      } catch (AssertionFailsException innerEx) {
        LogAndThrowExceptionAsResponse(HttpStatusCode.BadRequest, innerEx, auditLog);

      } catch (ServiceException innerEx) {
        LogAndThrowExceptionAsResponse(HttpStatusCode.ServiceUnavailable, innerEx, auditLog);

      } catch (Exception innerEx) {
        LogAndThrowExceptionAsResponse(HttpStatusCode.InternalServerError, innerEx, auditLog);

      }
    }

    #endregion Private methods

  }  // class Helpers

}  // namespace Empiria.WebApi
