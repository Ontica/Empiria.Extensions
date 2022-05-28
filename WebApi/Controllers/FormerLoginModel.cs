/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Base controllers                      *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Model                                 *
*  Type     : FormerLoginModel                             License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Former data to authenticate a user.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Security;

namespace Empiria.WebApi.Controllers {

  /// <summary>Former data to authenticate a user.</summary>
  public class FormerLoginModel {

    #region Properties

    public string api_key {
      get;
      set;
    }

    public string user_name {
      get;
      set;
    }

    public string password {
      get;
      set;
    }

    #endregion Properties

    #region Methods

    public void AssertValid() {
      Assertion.Require(api_key, "api_key");
      Assertion.Require(user_name, "user_name");
      Assertion.Require(password, "password");
    }

    static public object ToOAuth(EmpiriaPrincipal principal) {

      return new {
        access_token = principal.Session.Token,
        token_type = principal.Session.TokenType,
        expires_in = principal.Session.ExpiresIn,
        refresh_token = principal.Session.RefreshToken,
        user = new {
          uid = principal.Identity.User.Id,
          username = principal.Identity.User.UserName,
          email = principal.Identity.User.EMail
        }
      };
    }

    #endregion Methods

  }  // class FormerLoginModel

} // namespace Empiria.WebApi.Controllers
