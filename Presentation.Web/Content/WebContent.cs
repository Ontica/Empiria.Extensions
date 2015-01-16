/* Empiria Presentation Framework 2015 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Web Presentation Framework        *
*  Namespace : Empiria.Presentation.Web.Content                 Assembly : Empiria.Presentation.Web.dll      *
*  Type      : MasterPageContent                                Pattern  : Standard Class with Items Cache   *
*  Version   : 6.0        Date: 04/Jan/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Sealed class that represents the XHTML contents of a master view used as a canvas for a       *
*              specific WebPage object.                                                                      *
*                                                                                                            *
********************************* Copyright (c) 2002-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/

namespace Empiria.Presentation.Web.Content {

  /// <summary>Sealed class that represents the XHTML contents of a master view used as a canvas for a
  /// specific WebPage object.</summary>
  public abstract class WebContent : IContentParser {

    #region Fields

    private string themeFolder = null;
    private WebPage webPage = null;

    #endregion Fields

    #region Constructors and parsers

    protected WebContent(WebPage webPage) {
      this.webPage = webPage;
    }

    #endregion Constructors and parsers

    #region Public properties

    public string ThemeFolder {
      get {
        if (themeFolder == null) {
          themeFolder = WebApplication.ThemesPath + "default/";
        }
        return themeFolder;
      }
    }

    public WebPage WebPage {
      get { return webPage; }
    }

    #endregion Public properties

    #region Public methods

    string IContentParser.GetContent(string itemName) {
      XhtmlTemplate xhtmlTemplate = XhtmlTemplate.Parse(itemName);

      return xhtmlTemplate.TemplateString;
    }

    internal string GetContent(string itemName) {
      XhtmlTemplate xhtmlTemplate = XhtmlTemplate.Parse(itemName);

      return xhtmlTemplate.TemplateString;
    }

    internal string GetContent(UIComponent uiComponent) {
      return uiComponent.ParseContentAsString(this);
    }

    #endregion Public methods

  } // class WebContent

} // namespace Empiria.Presentation.Web.Content
