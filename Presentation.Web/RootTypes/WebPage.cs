/* Empiria Presentation Framework 2014 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Web Presentation Framework        *
*  Namespace : Empiria.Presentation.Web                         Assembly : Empiria.Presentation.Web.dll      *
*  Type      : WebPage                                          Pattern  : Model View Controller             *
*  Version   : 6.0        Date: 23/Oct/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Abstract type that represents a web page. All Empiria web pages types must be descendants of  *
*              this class.                                                                                   *
*                                                                                                            *
********************************* Copyright (c) 2002-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Specialized;

namespace Empiria.Presentation.Web {

  /// <summary>Abstract type that represents a web page. All Empiria web pages types must be
  /// descendants of this class.</summary>
  public abstract class WebPage : System.Web.UI.Page {

    #region Fields

    private WebViewModel viewModel = null;
    private string viewParameters = null;
    private bool isReloaded = false;
    private string contentHint = String.Empty;

    private string commandName = String.Empty;

    private string commandParametersRaw = String.Empty;
    private NameValueCollection commandParameters = null;

    #endregion Fields

    #region Public properties

    public string CommandName {
      get { return this.commandName; }
    }

    public NameValueCollection CommandParameters {
      get {
        if (commandParameters == null) {
          commandParameters = EmpiriaString.ParseQueryString(commandParametersRaw);
        }
        return this.commandParameters;
      }
    }

    public string CommandParametersRaw {
      get { return this.commandParametersRaw; }
    }

    public bool IsSessionAlive {
      get { return (ExecutionServer.CurrentPrincipal != null); }
    }

    public string ThemePath {
      get { return Master.ThemePath; }
    }

    public new Empiria.Security.EmpiriaUser User {
      get { 
        return Empiria.Security.EmpiriaUser.Current;
      }
    }

    public WebViewModel ViewModel {
      get { return viewModel; }
    }

    public string ViewParameters {
      get { return viewParameters; }
    }

    public Workplace Workplace {
      get {
        if (Request.Form["hdnEmpiriaWorkplace"] != null) {
          System.Guid workplaceGuid = new System.Guid(Request.Form["hdnEmpiriaWorkplace"]);

          return WebContext.WorkplaceManager.GetWorkplace(workplaceGuid);
        } else {
          return WebContext.WorkplaceManager.GetCurrent();
        }
      }
    }

    #endregion Public properties

    #region Protected properties

    protected bool CloseWindow {
      get { return Master.CloseWindow; }
      set { Master.CloseWindow = value; }
    }

    protected string ContentHint {
      get {
        if (contentHint == String.Empty) {
          contentHint = ViewModel.ContentHint;
        }
        return contentHint;
      }
      set { contentHint = value; }
    }

    protected bool IsReloaded {
      get { return isReloaded; }
      private set { isReloaded = value; }
    }

    protected new MasterWebPage Master {
      get { return (MasterWebPage) base.Master; }
    }

    protected bool RefreshParent {
      get { return Master.RefreshParent; }
      set { Master.RefreshParent = value; }
    }

    protected new string Title {
      get { return base.Title; }
      set {
        base.Title = value + " » " + ExecutionServer.CustomerName + " » " + ProductInformation.Name;
      }
    }

    #endregion Protected properties

    #region Protected methods

    public string GetControlState(string controlUniqueID) {
      return String.IsNullOrEmpty(Request.Form[controlUniqueID]) ? String.Empty : Request.Form[controlUniqueID];
    }

    protected override void OnPreLoad(EventArgs e) {
      VerifySession();

      if ((this.Workplace != null) && (this.Workplace.CurrentViewModel != null)) {
        viewModel = (WebViewModel) this.Workplace.CurrentViewModel;
        viewParameters = this.Workplace.CurrentViewParameters;
        this.Title = this.ViewModel.Title;
      }
      if (!String.IsNullOrEmpty(Request.Form["hdnEmpiriaPageCommandName"])) {
        this.commandName = Request.Form["hdnEmpiriaPageCommandName"];
      }
      if (!String.IsNullOrEmpty(Request.Form["hdnEmpiriaPageCommandArguments"])) {
        this.commandParametersRaw = Request.Form["hdnEmpiriaPageCommandArguments"];
      }
      //if (!IsPostBack) {
      //  isReloaded = false;
      //} else {
      //  isReloaded = (Workplace.LastRequestGuid.ToString() != Request.Form["hdnEmpiriaLastRequestGuid"]);
      //}
      SetControlFields();
    }

    protected string GetCommandParameter(string parameterName) {
      return GetCommandParameter(parameterName, true);
    }

    protected string GetCommandParameter(string parameterName, bool required) {
      return GetCommandParameter(parameterName, required, String.Empty);
    }

    protected string GetCommandParameter(string parameterName, bool required, object defaultValue) {
      Assertion.AssertObject(parameterName, "parameterName");

      string value = String.IsNullOrEmpty(this.CommandParameters[parameterName]) ? 
                                          String.Empty : this.CommandParameters[parameterName];
      value = value.Trim();
      if (!String.IsNullOrEmpty(value)) {
        return value;
      } else if (!required) {
        return Convert.ToString(defaultValue);
      } else {
        throw new WebPresentationException(WebPresentationException.Msg.NullCommandParameter,
                                           this.commandName, parameterName);
      }
    }

    protected T GetCommandParameter<T>(string parameterName) {
      string value = String.IsNullOrEmpty(this.CommandParameters[parameterName]) ?
                                        String.Empty : this.CommandParameters[parameterName];
      try {
        if (!String.IsNullOrEmpty(value)) {
          return (T) Convert.ChangeType(value, typeof(T));
        } else {
          throw new WebPresentationException(WebPresentationException.Msg.NullCommandParameter,
                                             this.commandName, parameterName);
        }
      } catch (Exception e) {
        throw new WebPresentationException(WebPresentationException.Msg.CommandParameterError, e,
                                           this.commandName, parameterName, value);
      }
    }

    protected T GetCommandParameter<T>(string parameterName, T defaultValue) {
      string value = String.IsNullOrEmpty(this.CommandParameters[parameterName]) ?
                                              String.Empty : this.CommandParameters[parameterName];
      try {    
        if (!String.IsNullOrEmpty(value)) {
          return (T) Convert.ChangeType(value, typeof(T));
        } else {
          return defaultValue;
        }
      } catch (Exception e) {
        throw new WebPresentationException(WebPresentationException.Msg.CommandParameterError, e,
                                           this.commandName, parameterName, value);
      }
    }

    protected void SetOKScriptMsg() {
      SetOKScriptMsg("La operación se ejecutó satisfactoriamente.");
    }

    protected void SetOKScriptMsg(string message) {
      this.Master.SetOKScriptMsg(message);
    }

    #endregion Protected methods

    #region Private methods

    private void SetControlFields() {
      ClientScript.RegisterHiddenField("hdnEmpiriaWorkplace", Workplace.Guid.ToString());
      ClientScript.RegisterHiddenField("hdnEmpiriaPageCommandName", String.Empty);
      ClientScript.RegisterHiddenField("hdnEmpiriaPageCommandArguments", String.Empty);

      //Workplace.UpdateLastRequestGuid();
      //if (!IsReloaded) {
      //  ClientScript.RegisterHiddenField("hdnEmpiriaLastRequestGuid", System.Guid.NewGuid().ToString());
      //} else {
      //  ClientScript.RegisterHiddenField("hdnEmpiriaLastRequestGuid", Workplace.LastRequestGuid.ToString());
      //  Response.Write("RELOADED");
      //}
    }

    private void VerifySession() {
      if (!IsSessionAlive) {
        throw new WebPresentationException(WebPresentationException.Msg.SessionTimeout);
      }
    }

    #endregion Private methods

  } // class WebPage

} // namespace Empiria.Presentation.Web
