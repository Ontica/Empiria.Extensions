/* Empiria® Presentation Framework 2014 **********************************************************************
*                                                                                                            *
*  Solution  : Empiria® Presentation Framework                  System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation.Components                  Assembly : Empiria.Presentation.dll          *
*  Type      : TabStripMasterMenu                               Pattern  : User Interface Component Class    *
*  Date      : 28/Mar/2014                                      Version  : 5.5     License: CC BY-NC-SA 4.0  *
*                                                                                                            *
*  Summary   : User interface component that holds the structure for a tabstrip master menu.                 *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2014. **/

namespace Empiria.Presentation.Components {

  /// <summary>User interface component that holds the structure for a tabstrip master menu.</summary>
  public class TabStripMasterMenu : UIComponent {

    #region Fields

    private const string thisTypeName = "ObjectType.GeneralObject.UIComponent.TabStripMasterMenu";

    #endregion Fields

    #region Constructors and parsers

    private TabStripMasterMenu()
      : this(thisTypeName) {

    }

    protected TabStripMasterMenu(string typeName)
      : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    static public new TabStripMasterMenu Parse(int id) {
      return BaseObject.Parse<TabStripMasterMenu>(thisTypeName, id);
    }

    static public new TabStripMasterMenu Parse(string masterMenuNamedKey) {
      return BaseObject.Parse<TabStripMasterMenu>(thisTypeName, masterMenuNamedKey);
    }

    #endregion Constructors and parsers

    #region Public methods

    public ObjectList<TabStripMasterChildMenu> GetSubMenus() {
      return base.GetLinks<TabStripMasterChildMenu>("MasterMenu_SubMenus");
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
                                          base.ObjectTypeInfo.Name, item.TemplateName);
      }
    }

    #endregion Protected methods

  } // class TabStripMasterMenu

} // namespace Empiria.Presentation.Components