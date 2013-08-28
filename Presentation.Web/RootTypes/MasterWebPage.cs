/* Empiria® Presentation Framework 2013 **********************************************************************
*                                                                                                            *
*  Solution  : Empiria® Presentation Framework                  System   : Web Presentation Framework        *
*  Namespace : Empiria.Presentation.Web                         Assembly : Empiria.Presentation.Web.dll      *
*  Type      : MasterWebPage                                    Pattern  : Model View Controller             *
*  Date      : 23/Oct/2013                                      Version  : 5.2     License: CC BY-NC-SA 3.0  *
*                                                                                                            *
*  Summary   : Abstract type that represents a master page that serves as template and container for web     *
*              pages. Every Empiria web page must be sited over a MasterWebPage, and all master web pages    *
*              types must be descendants of this class.                                                      *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2013. **/
using System;

namespace Empiria.Presentation.Web {

  /// <summary>Abstract type that represents a master page that serves as template and container for 
  /// web pages. Every Empiria web page must be sited over a MasterWebPage, and all master web pages 
  /// types must be descendants of this class.</summary>
  public abstract class MasterWebPage : System.Web.UI.MasterPage {

    #region Abstract members

    protected abstract void Initialize();
    protected abstract void LoadMasterPageControls();

    #endregion Abstract members

    #region Fields

    private bool refreshParent = false;
    private bool closeWindow = false;
    private string themeFolder = String.Empty;
    private string validationScript = String.Empty;
    private string behaviorScript = String.Empty;
    private string deleteScript = String.Empty;
    private string beginLoadScript = String.Empty;
    private string endLoadScript = String.Empty;
    private string suspendScript = String.Empty;

    #endregion Fields

    #region Internal and protected properties

    internal bool CloseWindow {
      get { return closeWindow; }
      set { closeWindow = value; }
    }

    internal bool RefreshParent {
      get { return refreshParent; }
      set { refreshParent = value; }
    }

    public string JavaScriptFiles {
      get { return String.Empty; }
    }

    protected new WebPage Page {
      get { return (WebPage) base.Page; }
    }

    protected string StyleSheetFiles {
      get { return String.Empty; }
    }

    public string ThemePath {
      get {
        if (themeFolder.Length == 0) {
          themeFolder = "../themes/" + ((this.User != null) ? this.User.UITheme : "default") + "/";
        }
        return themeFolder;
      }
    }

    protected Empiria.Security.IEmpiriaUser User {
      get { return ExecutionServer.CurrentUser; }
    }

    #endregion Internal and protected properties

    #region Protected methods

    public void AppendBeginLoadScript(string script) {
      beginLoadScript += (beginLoadScript.Length != 0 ? Environment.NewLine : String.Empty) + script;
    }

    public void AppendEndLoadScript(string script) {
      endLoadScript += (endLoadScript.Length != 0 ? Environment.NewLine : String.Empty) + script;
    }

    protected string OnBeginLoadScript() {
      string script = String.Empty;

      if (closeWindow && refreshParent) {
        script = "window.opener.execScript(\"doCommand('refreshViewCmd');\");\n";
        script += "self.close(); return;\n";
      } else if (closeWindow && !refreshParent) {
        script = "self.close(); return;\n";
      } else if (!closeWindow && refreshParent) {
        script = "window.opener.execScript(\"doCommand('refreshViewCmd');\");\n";
      } else {
        // no-op
      }
      return beginLoadScript + "\n" + script;
    }

    protected string OnChangeBehaviorScript() {
      return behaviorScript;
    }

    protected string OnDeleteScript() {
      return deleteScript;
    }

    protected string OnEndLoadScript() {
      return endLoadScript;
    }

    protected string OnSuspendScript() {
      return suspendScript;
    }

    protected string OnValidationScript() {
      return validationScript;
    }

    public void SetBehaviorScript(string script) {
      behaviorScript = script;
    }

    public void SetDeleteScript(string script) {
      deleteScript = script;
    }

    public void SetOKScriptMsg(string message) {
      string script = String.Empty;
      script = "alert('" + message + "');\n";
      AppendEndLoadScript(script);
    }

    public void SetSuspendScript(string script) {
      suspendScript = script;
    }

    public void SetValidationScript(string script) {
      validationScript = script;
    }

    protected void Page_Load(object sender, EventArgs e) {
      Initialize();
      LoadMasterPageControls();
    }

    #endregion Protected methods

  } // class MasterWebPage

} // namespace Empiria.Presentation.Web