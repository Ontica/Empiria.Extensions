/* Empiria Business Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Business Framework                       System   : Supply Network Management         *
*  Namespace : Empiria.Industries.Automotive                    Assembly : Empiria.Industries.Automotive.dll *
*  Type      : OrderingSystemException                          Pattern  : Empiria Exception Class           *
*  Version   : 6.0        Date: 23/Oct/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : HTTP module for Empiria web authentication services.                                          *
*                                                                                                            *
********************************* Copyright (c) 2003-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;

using Empiria.Security;

namespace Empiria.WebServices {

  /// <summary>HTTP module for Empiria web authentication services.</summary>
  [Serializable]
  public sealed class EmpiriaAuthHttpModule : IHttpModule {

    private const string Realm = "Empiria Trade Web Api";
    static private Dictionary<string, EmpiriaPrincipal> sessionDictionary =
                                            new Dictionary<string, EmpiriaPrincipal>(64);


    static public EmpiriaPrincipal Authenticate(string apiClientKey, string userName, string password) {
      var entropy = Guid.NewGuid().ToString();
      userName = Cryptographer.Encrypt(EncryptionMode.EntropyKey, userName, entropy);
      password = Cryptographer.Encrypt(EncryptionMode.EntropyKey, password, entropy);
      EmpiriaPrincipal principal = EmpiriaIdentity.Authenticate(apiClientKey, userName,
                                                                password, entropy, 1);
      if (principal != null) {
        while (true) {
          if (!String.IsNullOrWhiteSpace(EmpiriaAuthHttpModule.StorePrincipal(principal))) {
            break;
          } else {
            principal.RegenerateToken();
          }
        }
      }
      return principal;
    }

    public void Init(HttpApplication httpApplication) {
      httpApplication.AuthenticateRequest += OnApplicationAuthenticateRequest;
      httpApplication.EndRequest += OnApplicationEndRequest;
    }

    public void Dispose() {

    }

    #region Private methods

    static private void AuthenticateWithToken(string sessionToken) {
      var principal = EmpiriaAuthHttpModule.GetPrincipal(sessionToken);
      if (principal != null) {
        EmpiriaAuthHttpModule.SetPrincipal(principal);
      } else {
        var e = new WebServicesException(WebServicesException.Msg.InvalidSessionToken, GetRequestData());
        e.Publish();
      }
    }

    static private string GetAuthenticationHeader() {
      var request = HttpContext.Current.Request;

      return request.Headers["Authorization"];
    }

    static private string GetAuthenticationHeaderValue() {
      string authenticationHeader = GetAuthenticationHeader();

      if (String.IsNullOrWhiteSpace(authenticationHeader)) {
        //throw new WebServicesException(WebServicesException.Msg.AuthenticationHeaderMissed,
        //                               GetUserHostAddress());
        //e.Publish();
        return null;
      }
      var headerValue = AuthenticationHeaderValue.Parse(authenticationHeader);
      // RFC 2617 sec 1.2
      if (headerValue.Scheme.ToLowerInvariant() == "bearer" &&
          headerValue.Parameter != null) {
        return headerValue.Parameter;
      } else {
        var e = new WebServicesException(WebServicesException.Msg.BadAuthenticationHeaderFormat,
                                         GetRequestData());
        e.Publish();
        return null;
      }
    }

    static private EmpiriaPrincipal GetPrincipal(string sessionToken) {
      if (sessionDictionary.ContainsKey(sessionToken)) {
        return sessionDictionary[sessionToken];
      }
      EmpiriaPrincipal principal = EmpiriaIdentity.TryAuthenticate(sessionToken);
      if (principal != null) {
        StorePrincipal(principal);
        return principal;
      }
      return null;
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

    static private string StorePrincipal(EmpiriaPrincipal principal) {
      EmpiriaIdentity identity = (EmpiriaIdentity) principal.Identity;
      if (!sessionDictionary.ContainsKey(identity.Session.Token)) {
        lock (sessionDictionary) {
          if (!sessionDictionary.ContainsKey(identity.Session.Token)) {
            EmpiriaAuthHttpModule.SetPrincipal(principal);
            sessionDictionary.Add(identity.Session.Token, principal);
          }
        }
        return identity.Session.Token;
      }
      return null;
    }

    static private void OnApplicationAuthenticateRequest(object sender, EventArgs e) {
      string token = GetAuthenticationHeaderValue();
      if (!String.IsNullOrWhiteSpace(token)) {
        EmpiriaAuthHttpModule.AuthenticateWithToken(token);
      }
    }

    // If the request was unauthorized, add the WWW-Authenticate header to the response.
    static private void OnApplicationEndRequest(object sender, EventArgs e) {
      var response = HttpContext.Current.Response;
      if (response.StatusCode == (int) HttpErrorCode.Unauthorized) {
        response.Headers.Add("WWW-Authenticate",
                             String.Format("Basic realm=\"{0}\"", EmpiriaAuthHttpModule.Realm));
      }
    }

    #endregion Private methods

  }  // class EmpiriaAuthHttpModule

}  // namespace Empiria.Trade.Api
