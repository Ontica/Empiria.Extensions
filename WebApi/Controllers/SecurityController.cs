/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Base controllers                      *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Web Api Controller                    *
*  Type     : SecurityController                           License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Contains web api methods for login and logout users and for change credentials.                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;

using Empiria.Json;
using Empiria.Security;

using Empiria.Services.UserManagement;

namespace Empiria.WebApi.Controllers {

  /// <summary>Contains web api methods for login and logout users and for change credentials.</summary>
  public class SecurityController : WebApiController {

    #region Public APIs

    [HttpPost]
    [Route("v2/security/change-password")]
    [Route("v3/security/change-password")]
    public NoDataModel ChangePassword([FromBody] object body) {
      base.RequireBody(body);

      var json = JsonObject.Parse(body);

      var formData = JsonObject.Parse(json.Get<string>("payload/formData"));
      var currentPassword = formData.Get<string>("current");
      var newPassword = formData.Get<string>("new");

      using (var usecases = UserCredentialsUseCases.UseCaseInteractor()) {
        usecases.ChangeUserPassword(currentPassword, newPassword);

        return new NoDataModel(this.Request);
      }
    }


    [HttpPost]
    [Route("v1/security/change-password/{userEmail}")]
    [Route("v3/security/change-password/{userEmail}")]
    public NoDataModel ChangePassword([FromBody] LoginModel login, [FromUri] string userEmail) {
      base.RequireBody(login);

      using (var usecases = UserCredentialsUseCases.UseCaseInteractor()) {
        usecases.CreateUserPassword(login.api_key, login.user_name,
                                    userEmail, login.password);

        return new NoDataModel(this.Request);
      }
    }


    [HttpPost, AllowAnonymous]
    [Route("v3/security/login-token")]
    public SingleObjectModel GetLoginToken([FromBody] LoginModel login) {
      try {
        base.RequireHeader("User-Agent");

        string token = GenerateLoginToken(login.user_name);

        return new SingleObjectModel(base.Request, token);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost, AllowAnonymous]
    [Route("v3/security/login")]
    public SingleObjectModel Login([FromBody] LoginModel login) {
      try {
        base.RequireBody(login);
        base.RequireHeader("User-Agent");

        ClientApplication clientApp = base.GetClientApplication();

        login.api_key = clientApp.Key;

        string token = TryGetLoginToken(login.user_name);

        Assertion.AssertObject(token, "token");

        EmpiriaPrincipal principal = this.GetPrincipal(login, token);

        return new SingleObjectModel(base.Request, LoginModel.ToOAuth(principal),
                                     "Empiria.Security.OAuthObject");
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion Public APIs

    #region Private methods

    private string GenerateLoginToken(string userID) {
      string rawToken = GetRawToken(userID);

      var salt = RequestTokenSalt(rawToken);

      return Cryptographer.CreateHashCode(rawToken, salt);
    }


    private string TryGetLoginToken(string userID) {
      string rawToken = GetRawToken(userID);

      if (!tokens.ContainsKey(rawToken)) {
        return null;
      }

      var salt = tokens[rawToken];

      tokens.Remove(rawToken);

      return Cryptographer.CreateHashCode(rawToken, salt);
    }


    private string GetRawToken(string userID) {
      ClientApplication clientApp = base.GetClientApplication();

      var ipAddress = GetClientIpAddress();

      return $"/{userID}/{clientApp.Key}/{ipAddress}/";
    }

    private static Empiria.Collections.EmpiriaDictionary<string, string> tokens = new Collections.EmpiriaDictionary<string, string>(32);


    private string RequestTokenSalt(string rawToken) {
      var salt = Guid.NewGuid().ToString();

      tokens.Insert(rawToken, salt);

      return salt;
    }

    private EmpiriaPrincipal GetPrincipal(LoginModel login, string entropy) {
      login.AssertValid();

      EmpiriaPrincipal principal = AuthenticationService.Authenticate(login.api_key, login.user_name,
                                                                      login.password, entropy);

      Assertion.AssertObject(principal, "principal");

      AuthenticationHttpModule.SetPrincipal(principal);

      return principal;
    }

    private string GetClientIpAddress() {
      var request = this.Request;

      if (request.Properties.ContainsKey("MS_HttpContext")) {
        return ((HttpContextWrapper) request.Properties["MS_HttpContext"]).Request.UserHostAddress;
      } else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name)) {
        RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty) request.Properties[RemoteEndpointMessageProperty.Name];
        return prop.Address;
      } else if (HttpContext.Current != null) {
        return HttpContext.Current.Request.UserHostAddress;
      } else {
        return null;
      }
    }

    #endregion Private methods

  }  // class SecurityController

}  // namespace Empiria.WebApi.Controllers
