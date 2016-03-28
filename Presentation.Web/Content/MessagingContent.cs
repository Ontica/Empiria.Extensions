/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Web Presentation Services         *
*  Namespace : Empiria.Presentation.Web.Content                 Assembly : Empiria.Presentation.Web.dll      *
*  Type      : MessagingContent                                 Pattern  : Standard Class                    *
*  Version   : 6.7                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Sealed class that represents the XHTML content for messaging services.                        *
*                                                                                                            *
********************************* Copyright (c) 2002-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
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
