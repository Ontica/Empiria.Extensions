/* Empiria Presentation Framework 2014 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation                             Assembly : Empiria.Presentation.dll          *
*  Type      : UIComponent                                      Pattern  : Composite (GoF Component part)    *
*  Version   : 5.5        Date: 28/Mar/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Abstract type that represents a prime (UIControl) or compound (UIContainer) user interface    *
*              container of other controls or containers.                                                    *
*                                                                                                            *
********************************* Copyright (c) 1999-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Data;

using Empiria.Presentation.Data;

namespace Empiria.Presentation {

  /// <summary>Abstract type that represents a prime (UIControl) or compound (UIContainer) user interface 
  /// container of other controls or containers.</summary>
  public abstract class UIComponent : GeneralObject {

    #region Abstract members

    internal protected abstract string ParseComponentItemAsString(UIComponentItem item, IContentParser contentParser);

    #endregion Abstract members

    #region Fields

    private const string thisTypeName = "ObjectType.GeneralObject.UIComponent";

    private UIComponentItemList items = null;

    #endregion Fields

    #region Constructors and parsers

    private UIComponent()
      : base(thisTypeName) {
      // Abstract class. Object creation of this type not allowed.
    }

    protected UIComponent(string typeName)
      : base(typeName) {
      // Empiria Object Type pattern classes always has this constructor. Don't delete
    }

    static public UIComponent Parse(int id) {
      return BaseObject.Parse<UIComponent>(thisTypeName, id);
    }

    static public UIComponent Parse(string uiComponentNamespace) {
      return BaseObject.Parse<UIComponent>(thisTypeName, uiComponentNamespace);
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

    #endregion Public methods

    public string ParseContentAsString(IContentParser contentParser) {
      string content = String.Empty;

      for (int i = 0; i < this.Items.Count; i++) {
        content += this.Items[i].ParseContentAsString(contentParser);
      }
      return content;
    }

    #region Private methods

    private UIComponentItemList LoadRootItems() {
      DataView rootsDataView = PresentationDataReader.GetUIRootComponents(this.NamedKey);

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