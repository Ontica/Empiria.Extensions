/* Empiria Service-Oriented Architecture Framework ***********************************************************
*                                                                                                            *
*  Solution  : Empiria SOA Framework                            System   : Empiria Web Api Framework         *
*  Namespace : Empiria.WebApi.Models                            Assembly : Empiria.WebApi.dll                *
*  Type      : LoginModel                                       Pattern  : Information Holder                *
*  Version   : 1.0        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Holds information to login a user into the web api system.                                    *
*                                                                                                            *
********************************* Copyright (c) 2014-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Empiria.Security;

namespace Empiria.WebApi.Models {

  /// <summary>Holds information to login a user into the web api system.</summary>
  public class LoginModel : IDataModel {

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
          uid = principal.Identity.UserId,
          username = principal.Identity.Name,
          email = principal.Identity.User.EMail,
          fullname = principal.Identity.User.FullName,
        }
      };
    }

    #endregion Methods

  }  // class LoginModel

} // namespace Empiria.WebApi.Models
