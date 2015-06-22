/* Empiria Presentation Framework 2015 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation.Data                        Assembly : Empiria.Presentation.dll          *
*  Type      : PresentationDataReader                           Pattern  : Data Services Static Class        *
*  Version   : 6.5        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : static internal class with data read methods for user interface objects.                      *
*                                                                                                            *
********************************* Copyright (c) 2002-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System.Data;

using Empiria.Data;

namespace Empiria.Presentation.Data {

  /// <summary>static internal class with data read methods for user interface objects.</summary>
  static internal class PresentationDataReader {

    #region Fields

    static private DataTable itemsDataTable = null;

    #endregion Fields

    #region Internal methods

    static internal DataView GetUIComponentChildItems(int parentUIComponentId) {
      string filter = "(ParentUIComponentId = " + parentUIComponentId.ToString() + ")";
      string sort = "RowIndex, ColumnIndex";

      return new DataView(GetUIComponentItemsDataTable(), filter, sort, DataViewRowState.CurrentRows);
    }

    static internal DataView GetUIRootComponents(string uiComponentNamedKey) {
      string filter = "(UIComponentNamedKey = '" + uiComponentNamedKey + "' AND ParentUIComponentId = 0)";
      string sort = "RowIndex, ColumnIndex";

      return new DataView(GetUIComponentItemsDataTable(), filter, sort, DataViewRowState.CurrentRows);
    }

    static internal bool IsContainer(DataRowView dataRow) {
      return ((string) dataRow["UIComponentType"] == "UIContainer");
    }

    static internal void RefreshCache() {
      itemsDataTable = null;
    }

    #endregion Internal methods

    #region Private methods

    static private DataTable GetUIComponentItemsDataTable() {
      if (itemsDataTable == null) {
        itemsDataTable = GeneralDataOperations.GetEntities("vwUIComponentItems");
      }
      return itemsDataTable;
    }

    #endregion Private methods

  } // class ContentReader

} // namespace Empiria.Presentation.Data
