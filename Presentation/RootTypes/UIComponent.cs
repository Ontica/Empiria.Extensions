/* Empiria Presentation Framework 2015 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation                             Assembly : Empiria.Presentation.dll          *
*  Type      : UIComponent                                      Pattern  : Composite (GoF Component part)    *
*  Version   : 6.5        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Abstract type that represents a prime (UIControl) or compound (UIContainer) user interface    *
*              container of other controls or containers.                                                    *
*                                                                                                            *
********************************* Copyright (c) 2002-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Data;

using Empiria.Presentation.Data;

namespace Empiria.Presentation {

  /// <summary>Abstract type that represents a prime (UIControl) or compound (UIContainer) user interface
  /// container of other controls or containers.</summary>
  public abstract class UIComponent : GeneralObject {

    #region Fields

    private UIComponentItemList items = null;

    #endregion Fields

    #region Constructors and parsers

    protected UIComponent() {
      // Required by Empiria Framework.
    }

    static public UIComponent Parse(int id) {
      return BaseObject.ParseId<UIComponent>(id);
    }

    static public UIComponent Parse(string uiComponentNamespace) {
      return BaseObject.ParseKey<UIComponent>(uiComponentNamespace);
    }

    #endregion Constructors and parsers

    #region Public properties

    public UIComponentItemList Items {
      get {
        if (items == null) {
          items = LoadRootItems();
        }
        return items;
      }
    }

    #endregion Public properties

    #region Public methods

    protected internal abstract string ParseComponentItemAsString(UIComponentItem item,
                                                                  IContentParser contentParser);

    public string ParseContentAsString(IContentParser contentParser) {
      string content = String.Empty;

      for (int i = 0; i < this.Items.Count; i++) {
        content += this.Items[i].ParseContentAsString(contentParser);
      }
      return content;
    }

    #endregion Public methods

    #region Private methods

    private UIComponentItemList LoadRootItems() {
      DataView rootsDataView = PresentationDataReader.GetUIRootComponents(base.NamedKey);

      UIComponentItemList list = new UIComponentItemList(rootsDataView.Count);
      for (int i = 0; i < rootsDataView.Count; i++) {
        UIComponentItem uiComponentItem = UIComponentItem.ParseWithDataRow(this, rootsDataView[i]);
        list.Add(uiComponentItem);
      }
      return list;
    }

    #endregion Private methods

  } // class UIComponent

} // namespace Empiria.Presentation.Web.Content
