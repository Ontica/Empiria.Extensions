/* Empiria® Business Framework 2013 **************************************************************************
*                                                                                                            *
*  Solution  : Empiria® Business Framework                      System   : Supply Network Management         *
*  Namespace : Empiria.Industries.Automotive                    Assembly : Empiria.Industries.Automotive.dll *
*  Type      : OrderingSystemException                          Pattern  : Empiria Exception Class           *
*  Date      : 23/Oct/2013                                      Version  : 5.2     License: CC BY-NC-SA 3.0  *
*                                                                                                            *
*  Summary   : The exception that is thrown when a problem occurs in the Supply Network Management System.   *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2013. **/
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;

using Empiria.Security;

namespace Empiria.WebServices {

  /// <summary>The exception that is thrown when a problem occurs in the Supply Network Management System.</summary>
  [Serializable]
  public sealed class EmpiriaAuthHttpModule : IHttpModule {
    
    private const string Realm = "Empiria Trade Web Api";
    static private Dictionary<string, EmpiriaPrincipal> sessionDictionary =
                                            new Dictionary<string, EmpiriaPrincipal>(64);

    public void Init(HttpApplication context) {
      context.AuthenticateRequest += OnApplicationAuthenticateRequest;
      context.EndRequest += OnApplicationEndRequest;
    }

    public void Dispose() {

    }

    public static string StorePrincipal(EmpiriaPrincipal principal) {
      EmpiriaIdentity identity = (EmpiriaIdentity) principal.Identity;
      if (!sessionDictionary.ContainsKey(identity.Session.Token)) {
        lock (sessionDictionary) {          
          EmpiriaAuthHttpModule.SetPrincipal(principal);
          sessionDictionary.Add(identity.Session.Token, principal);
        }
        return identity.Session.Token;
      }
      return null;
    }

    #region Private methods

    private static void AuthenticateUser(string sessionToken) {
      var principal = EmpiriaAuthHttpModule.GetPrincipal(sessionToken);
      if (principal != null) {
        EmpiriaAuthHttpModule.SetPrincipal(principal);
      } else {
        var e = new WebServicesException(WebServicesException.Msg.InvalidSessionToken, GetUserHostAddress());
        e.Publish();
      }
    }

    private static string GetAuthenticationHeaderValue() {
      var request = HttpContext.Current.Request;
      var authenticationHeader = request.Headers["Authorization"];

      if (authenticationHeader == null) {
        var e = new WebServicesException(WebServicesException.Msg.AuthenticationHeaderMissed,
                                         GetUserHostAddress());
        e.Publish();
        return null;
      }
      var authHeaderVal = AuthenticationHeaderValue.Parse(authenticationHeader);
      // RFC 2617 sec 1.2, "scheme" name is case-insensitive
      if (authHeaderVal.Scheme.Equals("bearer", StringComparison.OrdinalIgnoreCase) &&
          authHeaderVal.Parameter != null) {
        return authHeaderVal.Parameter;
      } else {
        var e = new WebServicesException(WebServicesException.Msg.BadAuthenticationHeaderFormat,
                                         GetUserHostAddress());
        e.Publish();
        return null;
      }
    }

    private static EmpiriaPrincipal GetPrincipal(string sessionToken) {
      if (sessionDictionary.ContainsKey(sessionToken)) {
        return sessionDictionary[sessionToken];
      } else {
        return null;
      }
    }

    private static string GetUserHostAddress() {
      if (HttpContext.Current != null && HttpContext.Current.Request != null) {
        return HttpContext.Current.Request.UserHostAddress;
      } else {
        return "0.0.0.0";
      }
    }

    private static void SetPrincipal(EmpiriaPrincipal principal) {
      Thread.CurrentPrincipal = principal;
      if (HttpContext.Current != null) {
        HttpContext.Current.User = principal;
      }
    }

    private static void OnApplicationAuthenticateRequest(object sender, EventArgs e) {
      string token = GetAuthenticationHeaderValue();
      if (!String.IsNullOrWhiteSpace(token)) {
        EmpiriaAuthHttpModule.AuthenticateUser(token);
      }
    }

    // If the request was unauthorized, add the WWW-Authenticate header to the response.
    private static void OnApplicationEndRequest(object sender, EventArgs e) {
      var response = HttpContext.Current.Response;
      if (response.StatusCode == 401) {
        response.Headers.Add("WWW-Authenticate", 
                             String.Format("Basic realm=\"{0}\"", EmpiriaAuthHttpModule.Realm));
      }
    }

    #endregion Private methods

  }  // class EmpiriaAuthHttpModule

}  // namespace Empiria.Trade.Api