﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Web Presentation Services         *
*  Namespace : Empiria.Presentation.Web                         Assembly : Empiria.Presentation.Web.dll      *
*  Type      : AjaxWebPage                                      Pattern  : Model View Controller             *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Abstract type that serves as a processor of XMLHttpRequest requests.                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Presentation.Web {

  /// <summary>Abstract type that serves as a processor of XMLHttpRequest requests.</summary>
  public abstract class AjaxWebPage : System.Web.UI.Page {

    #region Abstract members

    protected abstract string ImplementsCommandRequest(string commandName);

    #endregion Abstract members

    #region Fields

    private string commandName = "undefined";
    private string xmlResponse = String.Empty;

    #endregion Fields

    #region Protected properties

    protected bool IsSessionAlive {
      get { return (ExecutionServer.CurrentPrincipal != null || ExecutionServer.CurrentUserId == -5); }
    }

    protected new Empiria.Security.EmpiriaUser User {
      get {
        VerifySession();
        return Empiria.Security.EmpiriaUser.Current;
      }
    }

    #endregion Protected properties

    #region Protected methods

    protected T GetCommandParameter<T>(string parameterName) {
      string value = String.IsNullOrEmpty(Request.QueryString[parameterName]) ?
                                          String.Empty : Request.QueryString[parameterName];
      if (!String.IsNullOrEmpty(value)) {
        return (T) Convert.ChangeType(value, typeof(T));
      } else {
        throw new WebPresentationException(WebPresentationException.Msg.NullCommandParameter,
                                           commandName, parameterName);
      }
    }

    protected T GetCommandParameter<T>(string parameterName, T defaultValue) {
      string value = String.IsNullOrEmpty(Request.QueryString[parameterName]) ? String.Empty : Request.QueryString[parameterName];
      if (!String.IsNullOrEmpty(value)) {
        return (T) Convert.ChangeType(value, typeof(T));
      } else {
        return defaultValue;
      }
    }

    protected string GetCommandParameter(string parameterName, bool required) {
      string value = String.IsNullOrEmpty(Request.QueryString[parameterName]) ? String.Empty : Request.QueryString[parameterName];
      value = value.Trim();
      if (!String.IsNullOrEmpty(value)) {
        return value;
      } else if (!required) {
        return String.Empty;
      } else {
        throw new WebPresentationException(WebPresentationException.Msg.NullCommandParameter,
                                           commandName, parameterName);
      }
    }

    protected string GetCommandParameter(string parameterName, bool required, string defaultValue) {
      string value = String.IsNullOrEmpty(Request.QueryString[parameterName]) ? String.Empty : Request.QueryString[parameterName];
      value = value.Trim();
      if (!String.IsNullOrEmpty(value)) {
        return value;
      } else if (!required) {
        return defaultValue;
      } else {
        throw new WebPresentationException(WebPresentationException.Msg.NullCommandParameter,
                                           commandName, parameterName);
      }
    }

    protected override void OnLoad(EventArgs e) {
      try {
        Response.Clear();
        if (!String.IsNullOrEmpty(Request.QueryString["commandName"])) {
          commandName = Request.QueryString["commandName"];
        } else {
          xmlResponse = "@ERROR@NULL_COMMAND_NAME";
          WriteContent();
          Response.End();
          return;
        }
        if (!IsPostBack) {
          if (IsSessionAlive) {
            xmlResponse = ImplementsCommandRequest(this.commandName);
          } else {
            xmlResponse = "@ERROR@SESSION_TIMEOUT_RESPONSE";
          }
          WriteContent();
        }
        Response.End();
      } catch (System.Threading.ThreadAbortException) {
        // no-op
      } catch (Exception innerException) {
        WebPresentationException exception =
                      new WebPresentationException(WebPresentationException.Msg.AjaxInvocationError,
                                                   innerException, this.commandName);
        exception.Publish();
        throw exception;
      }
    }

    #endregion Protected methods

    #region Private methods

    private void VerifySession() {
      if (!IsSessionAlive) {
        throw new WebPresentationException(WebPresentationException.Msg.SessionTimeout);
      }
    }

    private void WriteContent() {
      Response.ContentType = "text/xml";
      Response.Charset = "utf-8";
      Response.AddHeader("Cache-Control", "no-cache");
      Response.Write(xmlResponse);
    }

    #endregion Private methods

  } // class AjaxWebPage

} // namespace Empiria.Presentation.Web
