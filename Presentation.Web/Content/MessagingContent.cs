/* Empiria® Presentation Framework 2013 **********************************************************************
*                                                                                                            *
*  Solution  : Empiria® Presentation Framework                  System   : Web Presentation Framework        *
*  Namespace : Empiria.Presentation.Web.Content                 Assembly : Empiria.Presentation.Web.dll      *
*  Type      : MessagingContent                                 Pattern  : Standard Class                    *
*  Date      : 23/Oct/2013                                      Version  : 5.2     License: CC BY-NC-SA 3.0  *
*                                                                                                            *
*  Summary   : Sealed class that represents the XHTML content for messaging services.                        *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2013. **/
using System;

namespace Empiria.Presentation.Web.Content {

  /// <summary>Sealed class that represents the XHTML content for messaging services.</summary>
  public sealed class MessagingContent : WebContent {

    #region Fields

    private string content = null;
    private string shortContent = null;

    #endregion Fields

    #region Constructors and parsers

    public MessagingContent(WebPage webPage)
      : base(webPage) {

    }

    #endregion Constructors and parsers

    #region Public methods

    public string GetAlertMessageBox() {
      return GetContent("AlertMessageBox");
    }

    public string GetContent() {
      if (content == null) {
        content = BuildContent();
      }
      return content;
    }

    public string GetShortContent() {
      if (shortContent == null) {
        shortContent = BuildShortContent();
      }
      return shortContent;
    }

    #endregion Public methods

    #region Private methods

    private string BuildContent() {
      string xhtml = String.Empty;

      xhtml = GetContent("MessagesLink");
      xhtml += GetContent("RemindersLink");
      xhtml += GetContent("TasksLink");

      return xhtml;
    }

    private string BuildShortContent() {
      string xhtml = String.Empty;

      xhtml = GetContent("MessagesShortLink");
      xhtml += GetContent("ShortContentSeparator");
      xhtml += GetContent("RemindersShortLink");
      xhtml += GetContent("ShortContentSeparator");
      xhtml += GetContent("TasksShortLink");

      return xhtml;
    }

    #endregion Private methods

  } // class MessagingContent

} // namespace Empiria.Presentation.Web.Content