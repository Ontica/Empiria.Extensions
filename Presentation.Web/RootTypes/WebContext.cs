/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Web Presentation Services         *
*  Namespace : Empiria.Presentation.Web                         Assembly : Empiria.Presentation.Web.dll      *
*  Type      : WebContext                                       Pattern  : Static Class                      *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Provides the objects from the current HTTP request.                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;

namespace Empiria.Presentation.Web {

  /// <summary>Provides the objects from the current HTTP request.</summary>
  public sealed class WebContext {

    #region Fields

    static private readonly string lastErrorTag = "Empiria.LastError";
    static private readonly string workplaceManagerTag = "Empiria.WorkplaceManager";

    #endregion Fields

    #region Constructors and parsers

    private WebContext() {
      // Object creation of this type not allowed
    }

    #endregion Constructors and parsers

    #region Public properties

    /// <summary>Gets the HttpApplicationState object for the current HTTP request.</summary>
    static public HttpApplicationState Application {
      get { return HttpContext.Current.Application; }
    }

    /// <summary>Gets or sets the HttpApplication object for the current HTTP request.</summary>
    static public HttpApplication ApplicationInstance {
      get { return HttpContext.Current.ApplicationInstance; }
    }

    /// <summary>Gets the Cache object for the current HTTP current application.</summary>
    static public Cache Cache {
      get { return HttpRuntime.Cache; }
    }

    /// <summary>Gets the HttpContext object for the current HTTP request.</summary>
    static public HttpContext Context {
      get { return HttpContext.Current; }
    }

    /// <summary>Gets the HttpRequest object for the current HTTP request.</summary>
    static public HttpRequest Request {
      get { return HttpContext.Current.Request; }
    }

    /// <summary>Gets the HttpResponse object for the current HTTP response.</summary>
    static public HttpResponse Response {
      get { return HttpContext.Current.Response; }
    }

    /// <summary>Gets the HttpServerUtility object that provides methods
    /// used in processing Web requests.</summary>
    static public HttpServerUtility Server {
      get { return HttpContext.Current.Server; }
    }

    /// <summary>Gets the HttpSessionState instance for the current HTTP request.</summary>
    static public HttpSessionState Session {
      get { return HttpContext.Current.Session; }
    }

    static public WorkplaceManager WorkplaceManager {
      get { return Session[workplaceManagerTag] as WorkplaceManager; }
    }

    #endregion Public properties

    #region Public methods

    static public void ClearError() {
      Session[lastErrorTag] = null;
    }

    static public Exception GetLastError() {
      return Session[lastErrorTag] as Exception;
    }

    #endregion Public methods

    #region Internal methods

    static internal void CreateWorkplaceManager() {
      DisposeWorkplaceManager();

      WebViewManager webViewManager = new WebViewManager();
      WorkplaceManager workplaceManager = new WorkplaceManager(webViewManager);
      Session.Add(workplaceManagerTag, workplaceManager);
    }

    static internal void DisposeWorkplaceManager() {
      if (Session[workplaceManagerTag] != null) {
        WorkplaceManager workplaceManager = (WorkplaceManager) Session[workplaceManagerTag];
        workplaceManager.Dispose();
        Session.Remove(workplaceManagerTag);
      }
    }

    #endregion Internal methods

  } // class WebContext

} // naespace Empiria.Presentation.Web
