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
using System.Web.Http;

using Empiria.Security;
using Empiria.Security.Claims;

namespace Empiria.WebApi.Controllers {

  /// <summary>Contains web api methods for login and logout users and for change credentials.</summary>
  public class SecurityController : WebApiController {

    #region Public APIs

    [HttpPost]
    [Route("v1/security/change-password/{userEmail}")]
    public void ChangePassword([FromBody] LoginModel login, [FromUri] string userEmail) {
      try {
        base.RequireBody(login);

        UserManagementService.ChangePassword(login.api_key, login.user_name,
                                             userEmail, login.password);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #region Login Controllers

    [HttpPost, AllowAnonymous]
    [Route("v1/security/login")]
    public SingleObjectModel LoginVersion1([FromBody] LoginModel login) {
      try {
        base.RequireBody(login);
        base.RequireHeader("User-Agent");

        EmpiriaPrincipal principal = this.GetPrincipal(login);

        return new SingleObjectModel(base.Request, LoginModel.ToOAuth(principal),
                                     "Empiria.Security.OAuthObject");
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost, AllowAnonymous]
    [Route("v1.5/security/login")]
    public SingleObjectModel LoginVersion1_5([FromBody] LoginModel login) {
      try {
        base.RequireBody(login);
        base.RequireHeader("User-Agent");

        ClientApplication clientApp = base.GetClientApplication();
        login.api_key = clientApp.Key;

        login.password = FormerCryptographer.Encrypt(EncryptionMode.EntropyHashCode, login.password, login.user_name);
        login.password = FormerCryptographer.Decrypt(login.password, login.user_name);

        EmpiriaPrincipal principal = this.GetPrincipal(login);

        return new SingleObjectModel(base.Request, LoginModel.ToOAuth(principal),
                                     "Empiria.Security.OAuthObject");
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost, AllowAnonymous]
    [Route("v1.6/security/login")]
    public SingleObjectModel LoginVersion1_6([FromBody] LoginModel login) {
      try {
        base.RequireBody(login);
        base.RequireHeader("User-Agent");

        ClientApplication clientApp = base.GetClientApplication();
        login.api_key = clientApp.Key;

        login.password = FormerCryptographer.Encrypt(EncryptionMode.EntropyHashCode, login.password, login.user_name);
        login.password = FormerCryptographer.Decrypt(login.password, login.user_name);

        EmpiriaPrincipal principal = this.GetPrincipal(login);

        string claimsToken = ((IClaimsSubject) clientApp).ClaimsToken;

        ClaimsService.EnsureClaim(principal.Identity.User, ClaimType.UserAppAccess, claimsToken,
                                  $"{principal.Identity.User.UserName} does not have access permissions " +
                                  $"to this application {claimsToken}.");

        return new SingleObjectModel(base.Request, LoginModel.ToOAuth(principal),
                                     "Empiria.Security.OAuthObject");
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost, AllowAnonymous]
    [Route("v2/security/login")]
    public SingleObjectModel LoginVersion2([FromBody] LoginModel login) {
      try {
        base.RequireBody(login);
        base.RequireHeader("User-Agent");

        ClientApplication clientApp = base.GetClientApplication();
        login.api_key = clientApp.Key;

        EmpiriaPrincipal principal = this.GetPrincipal(login);

        return new SingleObjectModel(base.Request, LoginModel.ToOAuth(principal),
                                     "Empiria.Security.OAuthObject");
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion Login Controllers

    [HttpPost]
    [Route("v1/security/logout")]
    public void Logout() {
      try {
          return;
        //throw new NotImplementedException();
        //AuthenticationHttpModule.Logout();
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion Public APIs

    #region Private methods

    private EmpiriaPrincipal GetPrincipal(LoginModel login) {
      login.AssertValid();

      EmpiriaPrincipal principal = AuthenticationHttpModule.Authenticate(login.api_key,
                                                                         login.user_name,
                                                                         login.password);
      Assertion.AssertObject(principal, "principal");

      return principal;
    }

    #endregion Private methods

  }  // class SecurityController

}  // namespace Empiria.WebApi.Controllers
