/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Web Presentation Services         *
*  Namespace : Empiria.Presentation.Web.Content                 Assembly : Empiria.Presentation.Web.dll      *
*  Type      : NavigationButtons                                Pattern  : Standard Class                    *
*  Version   : 6.5                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Holds the XHTML content for the main navigation buttons in a web application.                 *
*                                                                                                            *
********************************* Copyright (c) 2002-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

namespace Empiria.Presentation.Web.Content {

  /// <summary>Holds the XHTML content for the main navigation buttons in a web application.</summary>
  public sealed class NavigationButtons : WebContent {

    #region Fields

    static private string applicationButtonsContent = null;
    private string historyButtonsContent = null;

    #endregion Fields

    #region Constructors and parsers

    public NavigationButtons(WebPage webPage)
      : base(webPage) {

    }

    #endregion Constructors and parsers

    #region Public methods

    public string GetContent() {
      string xhtml = GetHistoryButtonsContent();

      xhtml = xhtml.Replace("{THEME_FOLDER}", ThemeFolder);
      return xhtml;
    }

    public string GetApplicationButtonsContent() {
      if (applicationButtonsContent == null) {
        applicationButtonsContent = BuildApplicationButtonsContent();
      }
      return applicationButtonsContent;
    }

    public string GetHistoryButtonsContent() {
      if (historyButtonsContent == null) {
        historyButtonsContent = BuildHistoryButtonsContent();
      }
      return historyButtonsContent;
    }

    #endregion Public methods

    #region Private methods

    private string BuildApplicationButtonsContent() {
      return String.Empty; //"N_B".Replace("{THEME_FOLDER}", ThemeFolder);
    }

    private string BuildHistoryButtonsContent() {
      string xhtml = String.Empty;

      if (WebPage.Workplace.NavigationHistory.CanBack) {
        xhtml = GetContent("BackHistoryButton");
      } else {
        xhtml = GetContent("BackHistoryButton.Disabled");
      }
      if (WebPage.Workplace.NavigationHistory.Count != 0) {
        xhtml += GetContent("GoToHistoryButton");
      } else {
        xhtml += GetContent("GoToHistoryButton.Disabled");
      }
      if (WebPage.Workplace.NavigationHistory.CanForward) {
        xhtml += GetContent("ForwardHistoryButton");
      } else {
        xhtml += GetContent("ForwardHistoryButton.Disabled");
      }
      return xhtml.Replace("{THEME_FOLDER}", ThemeFolder);
    }

    #endregion Private methods

  } // class NavigationButtons

} // namespace Empiria.Presentation.Web.Content
