/* Empiria Presentation Framework 2015 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Web Presentation Framework        *
*  Namespace : Empiria.Presentation.Web                         Assembly : Empiria.Presentation.Web.dll      *
*  Type      : MVCGlobal                                        Pattern  : Global ASP .NET Class             *
*  Version   : 6.5        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Defines the methods, properties, and events common to all application objects used by         *
*              Empiria ASP MVC platform.                                                                     *
*                                                                                                            *
********************************* Copyright (c) 2002-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

namespace Empiria.Presentation.Web {

  /// <summary>Defines the methods, properties, and events common to all application objects used by
  /// Empiria ASP MVC platform.</summary>
  public class MVCGlobal : MVCApplication {

    public MVCGlobal() {

    }

    protected virtual void Application_AuthenticateRequest(object sender, EventArgs e) {
      base.OnAuthenticateRequest(sender, e);
    }

    protected virtual void Application_Start(object sender, EventArgs e) {
      base.OnStart(sender, e);
    }

    protected virtual void Application_End(object sender, EventArgs e) {
      base.OnEnd(sender, e);
    }

    protected void Application_Error(object sender, EventArgs e) {
      base.OnError(sender, e);
    }

    protected virtual void Session_Start(object sender, EventArgs e) {
      base.OnSessionStart(sender, e);
    }

    protected virtual void Session_End(object sender, EventArgs e) {
      base.OnSessionEnd(sender, e);
    }

  } // class MVCGlobal

} // namespace Empiria.Presentation.Web
