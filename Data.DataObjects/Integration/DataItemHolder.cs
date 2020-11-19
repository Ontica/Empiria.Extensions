/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Data Objects                       Component : Service Provider                        *
*  Assembly : Empiria.Data.DataObjects.dll               Pattern   : Data Holder                             *
*  Type     : DataItemHolder                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Data structure used to hold DataItem data.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.StateEnums;

namespace Empiria.Data.DataObjects {

  /// <summary>Data structure used to hold DataItem data.</summary>
  internal class DataItemHolder {

    #region Constructors and parsers

    private DataItemHolder() {
      // Required by Empiria Framework
    }

    #endregion Constructors and parsers

    #region Properties

    [DataField("DataItemNamedKey")]
    internal string NamedKey {
      get;
      set;
    }


    [DataField("DataItemFamily")]
    internal string Family {
      get;
      set;
    }


    [DataField("DataItemName")]
    internal string Name {
      get;
      set;
    }


    [DataField("DataItemDescription")]
    internal string Description {
      get;
      set;
    }


    [DataField("DataItemTerms")]
    internal string Terms {
      get;
      set;
    }


    [DataField("DataItemKeywords")]
    internal string Keywords {
      get;
      set;
    }


    [DataField("DataItemLabel")]
    internal string Label {
      get;
      set;
    }


    [DataField("DataItemStorageType")]
    internal string StorageType {
      get;
      set;
    }


    [DataField("DataItemStorage")]
    internal string Storage {
      get;
      set;
    }


    [DataField("DataItemDataType")]
    internal string DataType {
      get;
      set;
    }


    [DataField("DataItemSize")]
    internal int Size {
      get;
      set;
    }


    [DataField("DataItemPrecision")]
    internal int Precision {
      get;
      set;
    }


    [DataField("DataItemDefaultValue")]
    internal string DefaultValue {
      get;
      set;
    }


    [DataField("DataItemExtData")]
    internal string ExtensionData {
      get;
      set;
    }

    [DataField("DataItemPosition")]
    internal int Position {
      get;
      set;
    }


    [DataField("DataItemParentId")]
    internal int ParentDataObjectTypeId {
      get;
      set;
    }


    [DataField("DataItemBaseId")]
    internal int BaseDataObjectTypeId {
      get;
      set;
    }


    [DataField("DataItemStatus", Default = EntityStatus.Pending)]
    internal EntityStatus Status {
      get;
      set;
    }

    #endregion Properties

  }  // class DataItemHolder

}  // namespace Empiria.Data.DataObjects
