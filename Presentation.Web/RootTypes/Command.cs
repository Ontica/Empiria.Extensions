/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Web Presentation Services         *
*  Namespace : Empiria.Presentation.Web                         Assembly : Empiria.Presentation.Web.dll      *
*  Type      : WebUserControl                                   Pattern  : None                              *
*  Version   : 6.7                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Represents a command in the context of an http request.                                       *
*                                                                                                            *
********************************* Copyright (c) 2002-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Web;
using System.Collections.Specialized;

using Empiria.Security;

namespace Empiria.Presentation.Web {

  /// <summary>Represents a command in the context of an http request.</summary>
  public class Command {

    #region Constructors and parsers

    public Command(HttpRequest request) {
      Assertion.AssertObject(request, "request");

      this.Request = request;
    }

    #endregion Constructors and parsers

    #region Public properties

    public string Name {
      get {
        if (!String.IsNullOrEmpty(this.Request.Form["hdnEmpiriaPageCommandName"])) {
          return this.Request.Form["hdnEmpiriaPageCommandName"];
        } else {
          return "Undefined";
        }
      }
    }

    private NameValueCollection _commandParameters = null;
    public NameValueCollection Parameters {
      get {
        if (_commandParameters == null) {
          if (!String.IsNullOrEmpty(Request.Form["hdnEmpiriaPageCommandArguments"])) {
            _commandParameters = EmpiriaString.ParseQueryString(Request.Form["hdnEmpiriaPageCommandArguments"]);
          } else {
            _commandParameters = new NameValueCollection();
          }
        }
        return this._commandParameters;
      }
    }

    public HttpRequest Request {
      get;
      private set;
    }

    public EmpiriaUser User {
      get {
        return Empiria.Security.EmpiriaUser.Current;
      }
    }

    #endregion Public properties

    #region Public methods

    public T GetParameter<T>(string parameterName) {
      string value = String.IsNullOrEmpty(this.Parameters[parameterName]) ?
                                        String.Empty : this.Parameters[parameterName];
      try {
        if (!String.IsNullOrEmpty(value)) {
          return (T) Convert.ChangeType(value, typeof(T));
        } else {
          throw new WebPresentationException(WebPresentationException.Msg.NullCommandParameter,
                                             this.Name, parameterName);
        }
      } catch (Exception e) {
        throw new WebPresentationException(WebPresentationException.Msg.CommandParameterError, e,
                                           this.Name, parameterName, value);
      }
    }

    public T GetParameter<T>(string parameterName, T defaultValue) {
      string value = String.IsNullOrEmpty(this.Parameters[parameterName]) ?
                                          String.Empty : this.Parameters[parameterName];
      try {
        if (!String.IsNullOrEmpty(value)) {
          return (T) Convert.ChangeType(value, typeof(T));
        } else {
          return defaultValue;
        }
      } catch (Exception e) {
        throw new WebPresentationException(WebPresentationException.Msg.CommandParameterError, e,
                                           this.Name, parameterName, value);
      }
    }

    #endregion Public methods

  } // class Command

} // namespace Empiria.Presentation.Web
