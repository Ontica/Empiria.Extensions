/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Web Presentation Services         *
*  Namespace : Empiria.Presentation.Web                         Assembly : Empiria.Presentation.Web.dll      *
*  Type      : Global                                           Pattern  : Global ASP .NET Class             *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Defines the methods, properties, and events common to all application objects used by         *
*              Empiria ASP.NET platform.                                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Presentation.Web {

  /// <summary>Defines the methods, properties, and events common to all application objects used by
  /// Empiria ASP.NET platform.</summary>
  public class Global : WebApplication {

    public Global() {

    }

    protected virtual void Application_PreSendRequestHeaders(Object sender, EventArgs e) {
      base.OnPreSendRequestHeaders(sender, e);
    }

    protected virtual void Application_Start(Object sender, EventArgs e) {
      base.OnStart(sender, e);
    }

    protected virtual void Application_AuthenticateRequest(Object sender, EventArgs e) {
      base.OnAuthenticateRequest(sender, e);
    }

    protected virtual void Application_End(Object sender, EventArgs e) {
      base.OnEnd(sender, e);
    }

    protected virtual void Application_Error(Object sender, EventArgs e) {
      base.OnError(sender, e);
    }

    protected virtual void Session_End(Object sender, EventArgs e) {
      base.OnSessionEnd(sender, e);
    }

    protected virtual void Session_Start(Object sender, EventArgs e) {
      base.OnSessionStart(sender, e);
    }

  } // class Global

} // namespace Empiria.Presentation.Web
