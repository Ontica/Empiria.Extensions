/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Presentation Services             *
*  Namespace : Empiria.Presentation                             Assembly : Empiria.Presentation.dll          *
*  Type      : UIContainer                                      Pattern  : Composite (GoF Composite part)    *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Represents a compound user interface container of user interface controls (UIControl) or      *
*              other user interface containers.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
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
