/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Presentation Services             *
*  Namespace : Empiria.Presentation.Components                  Assembly : Empiria.Presentation.dll          *
*  Type      : TabStripMasterMenu                               Pattern  : User Interface Component Class    *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : User interface component that holds the structure for a tabstrip master menu.                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Presentation.Components {

  /// <summary>User interface component that holds the structure for a tabstrip master menu.</summary>
  public class TabStripMasterMenu : UIComponent {

    #region Constructors and parsers

    private TabStripMasterMenu() {
      // Required by Empiria Framework.
    }

    static public new TabStripMasterMenu Parse(int id) {
      return BaseObject.ParseId<TabStripMasterMenu>(id);
    }

    static public new TabStripMasterMenu Parse(string masterMenuNamedKey) {
      return BaseObject.ParseKey<TabStripMasterMenu>(masterMenuNamedKey);
    }

    #endregion Constructors and parsers

    #region Public methods

    public FixedList<TabStripMasterChildMenu> GetSubMenus() {
      throw new NotImplementedException("GetSubMenus()");

      // return base.GetLinks<TabStripMasterChildMenu>("MasterMenu_SubMenus");
    }

    #endregion Public methods

    #region Protected methods

    protected internal override string ParseComponentItemAsString(UIComponentItem item,
                                                                  IContentParser contentParser) {
      string content = contentParser.GetContent(item.TemplateName);

      switch (item.TemplateName) {
        case "TabStripMasterMenu.MenuItem":
          content = content.Replace("{CONTROL.ID}", item.ControlID);
          content = content.Replace("{DASHBOARD.ID}", item.DisplayViewId.ToString());
          content = content.Replace("{MENU.HINT}", item.ToolTip);
          return content.Replace("{MENU.TITLE}", item.Title);
        case "TabStripMasterMenu.Container":
          return content.Replace("{MASTER.MENU.ITEMS}", ((UIContainer) item).GetParsedContent());
        default:
          throw new PresentationException(PresentationException.Msg.UnexpectedUIComponentItem,
                                          base.GetEmpiriaType().Name, item.TemplateName);
      }
    }

    #endregion Protected methods

  } // class TabStripMasterMenu

} // namespace Empiria.Presentation.Components
