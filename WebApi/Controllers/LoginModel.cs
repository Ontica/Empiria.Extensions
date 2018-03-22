/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Base controllers                      *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Model                                 *
*  Type     : LoginModel                                   License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Holds information to log in a user into the web api system.                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Security;

namespace Empiria.WebApi.Controllers {

  /// <summary>Holds information to log in a user into the web api system.</summary>
  public class LoginModel {

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
      Assertion.AssertObject(api_key, "api_key");
      Assertion.AssertObject(user_name, "user_name");
      Assertion.AssertObject(password, "password");
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

  }  // class LoginModel

} // namespace Empiria.WebApi.Controllers
