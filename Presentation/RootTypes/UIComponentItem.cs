/* Empiria Presentation Framework 2014 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation                             Assembly : Empiria.Presentation.dll          *
*  Type      : UIComponentItem                                  Pattern  : Composite (GoF Component part)    *
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
  public abstract class UIComponentItem {

    #region Abstract members

    protected abstract string ImplementsParseContentAsString(IContentParser contentParser);

    #endregion Abstract members

    #region Fields

    private UIComponent uiTargetComponent = null;

    private int id = 0;
    private string type = String.Empty;
    private string templateName = String.Empty;
    private int parentUIComponentId = 0;
    private int rowIndex = 0;
    private int columnIndex = 0;
    private int rowSpan = 0;
    private int columnSpan = 0;
    private string title = String.Empty;
    private string toolTip = String.Empty;
    private bool disabled = false;
    private int displayViewId = 0;
    private string style = String.Empty;
    private string attributesString = String.Empty;
    private string valuesString = String.Empty;
    private string dataBoundMode = String.Empty;
    private string dataMember = String.Empty;
    private string dataItem = String.Empty;
    private string controlID = String.Empty;

    #endregion Fields

    #region Constructors and parsers

    private UIComponentItem() {
      // Abstract class. Object creation of this type not allowed.
    }

    protected UIComponentItem(UIComponent component) {
      this.uiTargetComponent = component;
    }

    static internal UIComponentItem ParseWithDataRow(UIComponent component, DataRowView dataRow) {
      UIComponentItem uiComponentItem = null;

      if (PresentationDataReader.IsContainer(dataRow)) {
        uiComponentItem = new UIContainer(component);
      } else {
        uiComponentItem = new UIControl(component);
      }

      uiComponentItem.id = (int) dataRow["UIComponentItemId"];
      uiComponentItem.templateName = (string) dataRow["UITemplate"];
      uiComponentItem.parentUIComponentId = (int) dataRow["ParentUIComponentId"];
      uiComponentItem.rowIndex = (int) dataRow["RowIndex"];
      uiComponentItem.columnIndex = (int) dataRow["ColumnIndex"];
      uiComponentItem.rowSpan = (int) dataRow["RowSpan"];
      uiComponentItem.columnSpan = (int) dataRow["ColumnSpan"];
      uiComponentItem.title = (string) dataRow["DisplayName"];
      uiComponentItem.toolTip = (string) dataRow["ToolTip"];
      uiComponentItem.disabled = (bool) dataRow["Disabled"];
      uiComponentItem.displayViewId = (int) dataRow["DisplayViewId"];
      uiComponentItem.style = (string) dataRow["Style"];
      uiComponentItem.attributesString = (string) dataRow["AttributesString"];
      uiComponentItem.valuesString = (string) dataRow["ValuesString"];
      uiComponentItem.dataBoundMode = (string) dataRow["DataBoundMode"];
      uiComponentItem.dataMember = (string) dataRow["DataMember"];
      uiComponentItem.dataItem = (string) dataRow["DataItem"];
      uiComponentItem.controlID = (string) dataRow["UIControlID"];

      if (uiComponentItem is UIContainer) {
        DataView childsDataView = PresentationDataReader.GetUIComponentChildItems(uiComponentItem.Id);
        ((UIContainer) uiComponentItem).LoadChilds(childsDataView);
      }

      return uiComponentItem;
    }

    #endregion Constructors and parsers

    #region Public properties

    public string AttributesString {
      get { return attributesString; }
      internal set { attributesString = value; }
    }

    public int ColumnIndex {
      get { return columnIndex; }
      internal set { columnIndex = value; }
    }

    public int ColumnSpan {
      get { return columnSpan; }
      internal set { columnSpan = value; }
    }

    public string ControlID {
      get { return controlID; }
      internal set { controlID = value; }
    }

    public string DataBoundMode {
      get { return dataBoundMode; }
      internal set { dataBoundMode = value; }
    }

    public string DataItem {
      get { return dataItem; }
      internal set { dataItem = value; }
    }

    public string DataMember {
      get { return dataMember; }
      internal set { dataMember = value; }
    }

    public bool Disabled {
      get { return disabled; }
      internal set { disabled = value; }
    }

    public int DisplayViewId {
      get { return displayViewId; }
      internal set { displayViewId = value; }
    }

    public int Id {
      get { return id; }
    }

    public int ParentUIComponentItem {
      get { return parentUIComponentId; }
      internal set { parentUIComponentId = value; }
    }

    public int RowIndex {
      get { return rowIndex; }
      internal set { rowIndex = value; }
    }

    public int RowSpan {
      get { return rowSpan; }
      internal set { rowSpan = value; }
    }

    public string Style {
      get { return style; }
      internal set { style = value; }
    }

    public string Title {
      get { return title; }
      internal set { title = value; }
    }

    public string ToolTip {
      get { return toolTip; }
      internal set { toolTip = value; }
    }

    public string Type {
      get { return type; }
      internal set { type = value; }
    }

    public string TemplateName {
      get { return templateName; }
      internal set { templateName = value; }
    }

    public UIComponent UITargetComponent {
      get { return uiTargetComponent; }
    }

    public string ValuesString {
      get { return valuesString; }
      internal set { valuesString = value; }
    }

    #endregion Public properties

    #region Internal methods

    internal string ParseContentAsString(IContentParser contentParser) {
      return ImplementsParseContentAsString(contentParser);
    }

    #endregion Internal methods

    #region Private methods

    #endregion Private methods

  } // class UIComponentItem

} // namespace Empiria.Presentation.Web.Content