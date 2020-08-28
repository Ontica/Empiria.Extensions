/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Data Objects                       Component : Service Provider                        *
*  Assembly : Empiria.Data.DataObjects.dll               Pattern   : Information Holder                      *
*  Type     : DataItem                                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Partitioned abstract type that is base for all DataItems. A DataItem can be a file,            *
*             a document, a database, a Microsoft Excel column, a data field, a json structure,              *
*             a web service response or any subject that holds data that can be read or write.               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.StateEnums;

using Empiria.Ontology;

namespace Empiria.Data.DataObjects {

  /// <summary>Partitioned abstract type that is base for all DataItems. A DataItem can be a file,
  /// a document, a database, a Microsoft Excel column, a data field, a json structure,
  /// a web service response or any subject that holds data that can be read or write.</summary>
  [PartitionedType(typeof(DataItemType))]
  public abstract class DataItem : BaseObject {

    #region Constructors and parsers

    protected DataItem(DataItemType powertype) : base(powertype) {
      // Required by Empiria Framework for all partitioned types.
    }


    static internal DataItem Parse(int id) {
      return BaseObject.ParseId<DataItem>(id);
    }


    static public DataItem Parse(string uid) {
      return BaseObject.ParseKey<DataItem>(uid);
    }


    static public FixedList<T> GetList<T>() where T : DataItem {
      return BaseObject.GetList<T>()
                       .ToFixedList();
    }


    static public DataItem Empty => BaseObject.ParseEmpty<DataItem>();


    #endregion Constructors and parsers

    #region Properties

    public DataItemType DataItemType {
      get {
        return (DataItemType) base.GetEmpiriaType();
      }
    }


    [DataField("DataItemNamedKey")]
    public string NamedKey {
      get;
      internal protected set;
    }


    [DataField("DataItemFamily")]
    public string Family {
      get;
      internal protected set;
    }


    [DataField("DataItemName")]
    public string Name {
      get;
      internal protected set;
    }


    [DataField("DataItemDescription")]
    public string Description {
      get;
      internal protected set;
    }


    [DataField("DataItemTerms")]
    public string Terms {
      get;
      internal protected set;
    }


    public virtual string Keywords {
      get;
    }


    [DataField("DataItemLabel")]
    public string Label {
      get;
      internal protected set;
    }


    [DataField("DataItemStorageType")]
    public string StorageType {
      get;
      internal protected set;
    }


    [DataField("DataItemStorage")]
    public string Storage {
      get;
      internal protected set;
    }


    [DataField("DataItemExtData")]
    protected string ExtensionData {
      get;
      set;
    }


    [DataField("DataItemStatus", Default = EntityStatus.Pending)]
    public EntityStatus Status {
      get;
      internal protected set;
    }


    #endregion Properties

  }  // class DataItem

}  // namespace Empiria.Data.DataObjects
