/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Presentation Services             *
*  Namespace : Empiria.Presentation.Controllers                 Assembly : Empiria.Presentation.dll          *
*  Type      : LogonController                                  Pattern  : Controller Class                  *
*  Version   : 6.7                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Abstract controller class that performs user authentication and system entrance.              *
*                                                                                                            *
********************************* Copyright (c) 2002-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
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

    public LogonController() {
      OnSignOut();
    }

    #endregion Constructors and parsers

    #region Protected methods

    protected bool Logon(string clientAppKey, string userName, string password,
                         string entropy, int contextId) {
      OnValidate();
      EmpiriaPrincipal principal = null;
      try {
        principal = EmpiriaIdentity.Authenticate(clientAppKey, userName, password,
                                                 entropy, contextId);
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
