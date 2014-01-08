/* Empiria® Presentation Framework 2014 **********************************************************************
*                                                                                                            *
*  Solution  : Empiria® Presentation Framework                  System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation                             Assembly : Empiria.Presentation.dll          *
*  Type      : UIContainer                                      Pattern  : Composite (GoF Composite part)    *
*  Date      : 28/Mar/2014                                      Version  : 5.5     License: CC BY-NC-SA 4.0  *
*                                                                                                            *
*  Summary   : Represents a compound user interface container of user interface controls (UIControl) or      *
*              other user interface containers.                                                              *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2014. **/
using System;
using System.Data;

namespace Empiria.Presentation {

  /// <summary>Represents a compound user interface container of user interface controls (UIControl) or other 
  /// user interface containers.</summary>
  public class UIContainer : UIComponentItem {

    #region Fields

    UIComponentItemList childs = new UIComponentItemList();
    private string parsedStringContent = String.Empty;

    #endregion Fields

    #region Constructors and parsers

    internal UIContainer(UIComponent component)
      : base(component) {

    }

    #endregion Constructors and parsers

    #region Public properties

    public UIComponentItemList Childs {
      get { return childs; }
    }

    public string GetParsedContent() {
      return this.parsedStringContent;
    }

    #endregion Public properties

    #region Protected methods

    protected override string ImplementsParseContentAsString(IContentParser contentParser) {
      this.parsedStringContent = String.Empty;
      for (int i = 0; i < childs.Count; i++) {
        this.parsedStringContent += base.UITargetComponent.ParseComponentItemAsString(childs[i],
                                                                                      contentParser);
      }
      this.parsedStringContent = base.UITargetComponent.ParseComponentItemAsString(this, contentParser);

      return this.parsedStringContent;
    }

    protected internal void LoadChilds(DataView childsDataView) {
      this.childs = new UIComponentItemList(childsDataView.Count);
      for (int i = 0; i < childsDataView.Count; i++) {
        UIComponentItem uiComponentItem = UIComponentItem.ParseWithDataRow(this.UITargetComponent, childsDataView[i]);
        this.childs.Add(uiComponentItem);
      }
    }

    #endregion Protected methods

  } // class UIContainer

} // namespace Empiria.Presentation.Web.Content