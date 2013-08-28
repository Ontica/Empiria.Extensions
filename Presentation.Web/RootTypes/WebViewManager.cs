/* Empiria® Presentation Framework 2013 **********************************************************************
*                                                                                                            *
*  Solution  : Empiria® Presentation Framework                  System   : Web Presentation Framework        *
*  Namespace : Empiria.Presentation.Web                         Assembly : Empiria.Presentation.Web.dll      *
*  Type      : WebViewManager                                   Pattern  : View Manager Class                *
*  Date      : 23/Oct/2013                                      Version  : 5.2     License: CC BY-NC-SA 3.0  *
*                                                                                                            *
*  Summary   : Provides methods to manipulate WebView objects.                                               *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2013. **/
using System;

namespace Empiria.Presentation.Web {

  /// <summary>Provides methods to manipulate WebView objects.</summary>
  public sealed class WebViewManager : IViewManager {

    #region Constructors and parsers

    internal WebViewManager() {
      // External object creation of this class not allowed
    }

    #endregion Constructors and parsers

    #region Public methods

    public void ActivateView(ViewModel viewModel) {
      lock (WebContext.Response) {
        if (WebContext.Response.IsClientConnected) {
          WebContext.Response.Redirect(viewModel.Source, true);
        }
      } // lock
    }

    public void ActivateView(ViewModel viewModel, string queryString) {
      lock (WebContext.Response) {
        if (WebContext.Response.IsClientConnected) {
          if (!String.IsNullOrEmpty(queryString)) {
            WebContext.Response.Redirect(viewModel.Source + "?" + queryString, true);
          } else {
            WebContext.Response.Redirect(viewModel.Source, true);
          }
        }
      } // lock
    }

    public void ActivateView(ViewModel viewModel, bool preserve) {
      lock (WebContext.Response) {
        if (WebContext.Response.IsClientConnected) {
          WebContext.Server.Transfer(viewModel.Source, preserve);
        }
      } // lock
    }

    public void ActivateView(ViewModel viewModel, string queryString, bool preserve) {
      lock (WebContext.Response) {
        if (WebContext.Response.IsClientConnected) {
          if (!String.IsNullOrEmpty(queryString)) {
            WebContext.Response.Redirect(viewModel.Source + "?" + queryString, preserve);
          } else {
            WebContext.Response.Redirect(viewModel.Source, preserve);
          }
        }
      } // lock
    }

    public void Dispose() {

    }

    #endregion Public methods

  } // class WebViewManager

} // namespace Empiria.Presentation.Web