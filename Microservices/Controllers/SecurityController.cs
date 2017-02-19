/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.Core.WebApi                              Assembly : Empiria.WebApi.dll                *
*  Type      : SecurityController                               Pattern  : Web API Controller                *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Contains web api methods for login and logout users and for change credentials.               *
*                                                                                                            *
********************************* Copyright (c) 2009-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Web.Http;

using Empiria.Security;
using Empiria.WebApi;
using Empiria.WebApi.Models;

namespace Empiria.Core.WebApi {

  /// <summary>Contains web api methods for login and logout users and for change credentials.</summary>
  public class SecurityController : WebApiController {

    #region Public APIs

    [HttpPost]
    [Route("v1/security/change-password/{userEmail}")]
    public void ChangePassword([FromBody] LoginModel login, [FromUri] string userEmail) {
      try {
        base.RequireBody(login);

        EmpiriaUser.ChangePassword(login.api_key, login.user_name, userEmail, login.password);
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #region Login Controllers

    [HttpPost, AllowAnonymous]
    [Route("v1/security/login")]
    public SingleObjectModel Login(LoginModel login) {
      try {
        base.RequireHeader("User-Agent");
        base.RequireBody(login);

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
        throw new NotImplementedException();
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

}  // namespace Empiria.Core.WebApi
