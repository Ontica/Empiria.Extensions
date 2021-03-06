﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Presentation Services             *
*  Namespace : Empiria.Presentation.Controllers                 Assembly : Empiria.Presentation.dll          *
*  Type      : LogonController                                  Pattern  : Controller Class                  *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Abstract controller class that performs user authentication and system entrance.              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Security;

namespace Empiria.Presentation.Controllers {

  public abstract class LogonController : Controller {

    #region Abstract members

    protected abstract void OnAuthenticate(EmpiriaPrincipal principal);
    protected abstract void OnAuthenticateFails();
    protected abstract void OnValidate();

    protected abstract void OnSignOut();

    #endregion Abstract members

    #region Constructors and parsers

    protected LogonController() {
      // TODO Check if this code call is ok -> OnSignOut();
    }

    #endregion Constructors and parsers

    #region Protected methods

    protected bool Logon(string clientAppKey, string userName, string password,
                         string entropy, Json.JsonObject contextData = null) {
      OnValidate();
      EmpiriaPrincipal principal = null;
      try {
        principal = AuthenticationService.Authenticate(clientAppKey, userName, password,
                                                       entropy, contextData);
      } catch {
        // no-op
      }
      if (principal == null) {
        SetException("El usuario no está registrado o la contraseña de acceso proporcionada es incorrecta.");
        OnAuthenticateFails();
        return false;
      }
      OnAuthenticate(principal);
      return true;
    }

    protected bool GuestLogon() {
      OnValidate();

      throw new NotImplementedException();

      //EmpiriaIdentity identity = EmpiriaIdentity.AuthenticateGuest();

      //if (identity == null) {
      //  OnAuthenticateFails();
      //  return false;
      //}
      //EmpiriaPrincipal principal = new EmpiriaPrincipal(identity);

      //OnAuthenticate(principal);
      //return true;
    }

    #endregion Protected methods

  } // class LogonController

} // namespace Empiria.Presentation.Controllers
