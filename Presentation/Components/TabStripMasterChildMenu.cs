/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Presentation Services             *
*  Namespace : Empiria.Presentation.Components                  Assembly : Empiria.Presentation.dll          *
*  Type      : TabStripMasterChildMenu                          Pattern  : User Interface Component Class    *
*  Version   : 6.5                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : User interface component that holds the structure for a tabstrip master child menu.           *
*                                                                                                            *
********************************* Copyright (c) 2002-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

namespace Empiria.Presentation.Components {

  /// <summary>User interface component that holds the structure for a tabstrip master child menu.</summary>
  public class TabStripMasterChildMenu : UIComponent {

    #region Fields

    TabStripMasterMenu parentMasterMenu = null;

    #endregion Fields

    #region Constructors and parsers

    private TabStripMasterChildMenu() {
      // Required by Empiria Framework.
    }

    static public new TabStripMasterChildMenu Parse(int id) {
      return BaseObject.ParseId<TabStripMasterChildMenu>(id);
    }

    static public new TabStripMasterChildMenu Parse(string masterMenuNamedKey) {
      return BaseObject.ParseKey<TabStripMasterChildMenu>(masterMenuNamedKey);
    }

    #endregion Constructors and parsers

    #region Protected methods

    internal void SetParentMasterMenu(TabStripMasterMenu parentMasterMenu) {
      this.parentMasterMenu = parentMasterMenu;
    }

    protected internal override string ParseComponentItemAsString(UIComponentItem item,
                                                                  IContentParser contentParser) {
      string content = contentParser.GetContent(item.TemplateName);

      switch (item.TemplateName) {
        case "TabStripMasterChildMenu.MenuItem":
          content = content.Replace("{DASHBOARD.ID}", item.DisplayViewId.ToString());
          content = content.Replace("{SUBMENU.HINT}", item.ToolTip);
          return content.Replace("{SUBMENU.TITLE}", item.Title);
        case "TabStripMasterChildMenu.Container":
          content = content.Replace("{CONTROL.ID}", item.ControlID);
          return content.Replace("{SUB.MENU.ITEMS}", ((UIContainer) item).GetParsedContent());
        default:
          throw new PresentationException(PresentationException.Msg.UnexpectedUIComponentItem,
                                          base.GetEmpiriaType().Name, item.TemplateName);
      }
    }

    #endregion Protected methods

  } // class TabStripMasterChildMenu

} // namespace Empiria.Presentation.Components
