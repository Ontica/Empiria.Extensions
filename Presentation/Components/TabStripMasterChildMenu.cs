/* Empiria Presentation Framework 2014 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation.Components                  Assembly : Empiria.Presentation.dll          *
*  Type      : TabStripMasterChildMenu                          Pattern  : User Interface Component Class    *
*  Version   : 5.5        Date: 25/Jun/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : User interface component that holds the structure for a tabstrip master child menu.           *
*                                                                                                            *
********************************* Copyright (c) 2009-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/

namespace Empiria.Presentation.Components {

  /// <summary>User interface component that holds the structure for a tabstrip master child menu.</summary>
  public class TabStripMasterChildMenu : UIComponent {

    #region Fields

    private const string thisTypeName = "ObjectType.GeneralObject.UIComponent.TabStripMasterChildMenu";

    TabStripMasterMenu parentMasterMenu = null;

    #endregion Fields

    #region Constructors and parsers

    private TabStripMasterChildMenu()
      : this(thisTypeName) {

    }

    protected TabStripMasterChildMenu(string typeName)
      : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    static public new TabStripMasterChildMenu Parse(int id) {
      return BaseObject.Parse<TabStripMasterChildMenu>(thisTypeName, id);
    }

    static public new TabStripMasterChildMenu Parse(string masterMenuNamedKey) {
      return BaseObject.Parse<TabStripMasterChildMenu>(thisTypeName, masterMenuNamedKey);
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
                                          base.ObjectTypeInfo.Name, item.TemplateName);
      }
    }

    #endregion Protected methods

  } // class TabStripMasterChildMenu

} // namespace Empiria.Presentation.Components