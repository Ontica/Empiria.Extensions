/* Empiria Presentation Framework 2014 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Web Presentation Framework        *
*  Namespace : Empiria.Presentation.Web                         Assembly : Empiria.Presentation.Web.dll      *
*  Type      : Global                                           Pattern  : Global ASP .NET Class             *
*  Version   : 5.5        Date: 25/Jun/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Defines the methods, properties, and events common to all application objects used by         *
*              Empiria ASP.NET platform.                                                                     *
*                                                                                                            *
********************************* Copyright (c) 2009-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
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