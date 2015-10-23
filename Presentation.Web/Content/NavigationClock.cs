/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Web Presentation Services         *
*  Namespace : Empiria.Presentation.Web.Content                 Assembly : Empiria.Presentation.Web.dll      *
*  Type      : NavigationClock                                  Pattern  : Standard Class                    *
*  Version   : 6.5                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Holds the XHTML content for a navigation clock and user name item.                            *
*                                                                                                            *
********************************* Copyright (c) 2002-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using Empiria.Security;

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
      return GetContent("UserNameLink").Replace("{USER_NAME}", EmpiriaUser.Current.UserName);
    }

    #endregion Private methods

  } // class NavigationClock

} // namespace Empiria.Presentation.Web.Content
