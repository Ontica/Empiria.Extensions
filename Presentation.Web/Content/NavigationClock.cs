/* Empiria® Presentation Framework 2013 **********************************************************************
*                                                                                                            *
*  Solution  : Empiria® Presentation Framework                  System   : Web Presentation Framework        *
*  Namespace : Empiria.Presentation.Web.Content                 Assembly : Empiria.Presentation.Web.dll      *
*  Type      : NavigationClock                                  Pattern  : Standard Class                    *
*  Date      : 25/Jun/2013                                      Version  : 5.1     License: CC BY-NC-SA 3.0  *
*                                                                                                            *
*  Summary   : Holds the XHTML content for a navigation clock and user name item.                            *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1994-2013. **/

namespace Empiria.Presentation.Web.Content {

  /// <summary>Holds the XHTML content for a navigation clock and user name item.</summary>
  public sealed class NavigationClock : WebContent {

    #region Fields

    static private string clockContent = null;
    private string userNameContent = null;

    #endregion Fields

    #region Constructors and parsers

    public NavigationClock(WebPage webPage)
      : base(webPage) {

    }

    #endregion Constructors and parsers

    #region Public properties

    #endregion Public properties

    #region Public methods

    public string GetContent() {
      return GetClockContent() + " <img class='decoratorImage' src='../themes/default/bullets/menu_separator.gif' alt='' title='' /> " + GetUserNameContent();
    }

    public string GetClockContent() {
      if (clockContent == null) {
        clockContent = BuildClockContent();
      }
      return clockContent;
    }

    public string GetUserNameContent() {
      if (userNameContent == null) {
        userNameContent = BuildUserNameContent();
      }
      return userNameContent;
    }

    #endregion Public methods

    #region Private methods

    private string BuildClockContent() {
      return GetContent("ClockLink");
    }

    private string BuildUserNameContent() {
      return GetContent("UserNameLink").Replace("{USER_NAME}", ExecutionServer.CurrentUser.UserName);
    }

    #endregion Private methods

  } // class NavigationClock

} // namespace Empiria.Presentation.Web.Content