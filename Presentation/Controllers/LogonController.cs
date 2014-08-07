/* Empiria Presentation Framework 2014 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation.Controllers                 Assembly : Empiria.Presentation.dll          *
*  Type      : LogonController                                  Pattern  : Controller Class                  *
*  Version   : 6.0        Date: 23/Oct/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Abstract controller class that performs user authentication and system entrance.              *
*                                                                                                            *
********************************* Copyright (c) 2002-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
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

    protected bool Logon(string sessionId, string userName, string password, int regionId) {
      OnValidate();
      EmpiriaIdentity identity = null;

      identity = EmpiriaIdentity.Authenticate(userName, password, sessionId, regionId);

      if (identity == null) {
        SetException("El usuario no está registrado o la contraseña de acceso proporcionada es incorrecta.");
        OnAuthenticateFails();
        return false;
      }
      EmpiriaPrincipal principal = new EmpiriaPrincipal(identity);

      OnAuthenticate(principal);
      return true;
    }

    protected bool GuestLogon() {
      OnValidate();
      EmpiriaIdentity identity = null;

      identity = EmpiriaIdentity.AuthenticateGuest();

      if (identity == null) {
        OnAuthenticateFails();
        return false;
      }
      EmpiriaPrincipal principal = new EmpiriaPrincipal(identity);

      OnAuthenticate(principal);
      return true;
    }

    #endregion Protected methods

  } // class LogonController

} // namespace Empiria.Presentation.Controllers
