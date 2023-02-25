/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Base controllers                      *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Web Api Controller                    *
*  Type     : FormerLoginController                        License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Former web api methods for authenticate users.                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.Security;
using Empiria.Security.Claims;

namespace Empiria.WebApi.Controllers {

  /// <summary>Former web api methods for authenticate users.</summary>
  public class FormerLoginController : WebApiController {

    #region Former Login Controllers

    [HttpPost, AllowAnonymous]
    [Route("v1/security/login")]
    public SingleObjectModel LoginVersion1([FromBody] FormerLoginModel login) {
      try {
        base.RequireBody(login);
        base.RequireHeader("User-Agent");

        IEmpiriaPrincipal principal = this.GetPrincipal(login);

        return new SingleObjectModel(base.Request, FormerLoginModel.ToOAuth(principal),
                                     "Empiria.Security.OAuthObject");
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost, AllowAnonymous]
    [Route("v1.5/security/login")]
    public SingleObjectModel LoginVersion1_5([FromBody] FormerLoginModel login) {
      try {
        base.RequireBody(login);
        base.RequireHeader("User-Agent");

        IClientApplication clientApp = base.GetClientApplication();
        login.api_key = clientApp.Key;

        login.password = FormerCryptographer.Encrypt(EncryptionMode.EntropyHashCode, login.password, login.user_name);
        login.password = FormerCryptographer.Decrypt(login.password, login.user_name);

        IEmpiriaPrincipal principal = this.GetPrincipal(login);

        return new SingleObjectModel(base.Request, FormerLoginModel.ToOAuth(principal),
                                     "Empiria.Security.OAuthObject");

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost, AllowAnonymous]
    [Route("v1.6/security/login")]
    public SingleObjectModel LoginVersion1_6([FromBody] FormerLoginModel login) {
      try {
        base.RequireBody(login);
        base.RequireHeader("User-Agent");

        IClientApplication clientApp = base.GetClientApplication();
        login.api_key = clientApp.Key;

        login.password = FormerCryptographer.Encrypt(EncryptionMode.EntropyHashCode, login.password, login.user_name);
        login.password = FormerCryptographer.Decrypt(login.password, login.user_name);

        IEmpiriaPrincipal principal = this.GetPrincipal(login);

        string claimsToken = clientApp.ClaimsToken;

        ClaimsService.EnsureClaim(ExecutionServer.CurrentUser,
                                  ClaimType.UserClientApplication, claimsToken,
                                  $"{ExecutionServer.CurrentUser.UserName} does not have access permissions " +
                                  $"to this application {claimsToken}.");

        return new SingleObjectModel(base.Request, FormerLoginModel.ToOAuth(principal),
                                     "Empiria.Security.OAuthObject");
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost, AllowAnonymous]
    [Route("v2/security/login")]
    public SingleObjectModel LoginVersion2([FromBody] FormerLoginModel login) {
      try {
        base.RequireBody(login);
        base.RequireHeader("User-Agent");

        IClientApplication clientApp = base.GetClientApplication();
        login.api_key = clientApp.Key;

        IEmpiriaPrincipal principal = this.GetPrincipal(login);

        return new SingleObjectModel(base.Request, FormerLoginModel.ToOAuth(principal),
                                     "Empiria.Security.OAuthObject");
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion Former Login Controllers

    #region Private methods

    private IEmpiriaPrincipal GetPrincipal(FormerLoginModel login) {
      login.AssertValid();

      var credentials = new UserCredentialsDto {
        AppKey = login.api_key,
        UserID = login.user_name,
        Password = login.password,
      };

      IEmpiriaPrincipal principal = AuthenticationHttpModule.AuthenticateFormer(credentials);

      Assertion.Ensure(principal, nameof(principal));

      return principal;
    }

    #endregion Private methods

  }  // class FormerLoginController

}  // namespace Empiria.WebApi.Controllers
