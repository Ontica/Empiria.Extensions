/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Web Api                               *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Controller                            *
*  Type     : AuthenticationController                     License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web api methods for user authentication.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Web.Http;

using Empiria.Security;
using Empiria.Security.UseCases;

namespace Empiria.WebApi.Controllers {

  /// <summary>Web api methods for user authentication.</summary>
  public class AuthenticationController : WebApiController {

    #region Web Apis

    [HttpPost, AllowAnonymous]
    [Route("v3/security/login-token")]
    public SingleObjectModel GetLoginToken([FromBody] UserCredentialsDto credentials) {

      PrepareAuthenticationFields(credentials);

      using (var usecases = AuthenticationUseCases.UseCaseInteractor()) {
        string token = usecases.GenerateAuthenticationToken(credentials);

        return new SingleObjectModel(base.Request, token);
      }
    }


    [HttpGet]
    [Route("v3/security/principal")]
    public SingleObjectModel GetPrincipalData() {

      using (var usecases = AuthenticationUseCases.UseCaseInteractor()) {

        PrincipalDto principal = usecases.PrincipalData();

        return new SingleObjectModel(base.Request, principal);
      }
    }


    [HttpPost, AllowAnonymous]
    [Route("v3/security/login")]
    public SingleObjectModel Login([FromBody] UserCredentialsDto credentials) {

      PrepareAuthenticationFields(credentials);

      using (var usecases = AuthenticationUseCases.UseCaseInteractor()) {
                IEmpiriaPrincipal principal = usecases.Authenticate(credentials);

                AuthenticationHttpModule.SetHttpContextPrincipal(principal);

        return new SingleObjectModel(base.Request, MapToOAuthResponse(principal),
                                     "Empiria.Security.OAuthObject");
      }
    }


    [HttpPost]
    [Route("v3/security/logout")]
    public NoDataModel Logout() {

      using (var usecases = AuthenticationUseCases.UseCaseInteractor()) {

        usecases.Logout();

        return new NoDataModel(base.Request);
      }
    }

    #endregion Web Apis

    #region Helper methods

    static public object MapToOAuthResponse(IEmpiriaPrincipal principal) {
      return new {
        access_token = principal.Session.Token,
        token_type = principal.Session.TokenType,
        expires_in = principal.Session.ExpiresIn,
        refresh_token = principal.Session.RefreshToken,
        user = new {
          uid = ExecutionServer.CurrentContact.Id,
          username = principal.Identity.Name,
          email = ExecutionServer.CurrentContact.EMail
        }
      };
    }

    private void PrepareAuthenticationFields(UserCredentialsDto credentials) {
      base.RequireHeader("User-Agent");
      base.RequireBody(credentials);

      credentials.AppKey = base.GetApplicationKeyFromHeader();
      credentials.UserHostAddress = base.GetClientIpAddress();
    }

    #endregion Helper methods

  }  // class LoginController

}  // namespace Empiria.WebApi.Controllers
