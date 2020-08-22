/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Data Objects                       Component : Service Provider                        *
*  Assembly : Empiria.Data.DataObjects.dll               Pattern   : Information Holder                      *
*  Type     : DataSource                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : DataSource instances describe files, documents, database tables or views, web services,        *
*             entities, data structures, or in general, any storage technology that holds data that          *
*             can be read or write.                                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.StateEnums;

namespace Empiria.Data.DataObjects {

  /// <summary>DataSource instances describe files, documents, database tables or views, web services,
  /// entities, data structures, or in general, any storage technology that holds data that
  /// can be read or write.</summary>
  public class DataSource : BaseObject {

    #region Constructors and parsers


    private DataSource() {
      // required by Empiria Framework
    }


    static internal DataSource Parse(int id) {
      return BaseObject.ParseId<DataSource>(id);
    }


    static public DataSource Parse(string uid) {
      return BaseObject.ParseKey<DataSource>(uid);
    }


    static public FixedList<DataSource> GetList() {
      return DataObjectsRepository.GetDataSources();
    }


    static public DataSource Empty => BaseObject.ParseEmpty<DataSource>();


    #endregion Constructors and parsers


    #region Properties

    [DataField("DataObjectType")]
    public string DataSourceType {
      get;
      set;
    }


    [DataField("DataObjectFamily")]
    public string Family {
      get;
      set;
    }


    [DataField("DataObjectName")]
    public string Name {
      get;
      set;
    }


    [DataField("DataObjectDescription")]
    public string Description {
      get;
      set;
    }


    [DataField("DataObjectDataType")]
    public string DataType {
      get;
      set;
    }


    [DataField("DataObjectSize")]
    public int Size {
      get;
      set;
    }


    [DataField("DataObjectPrecision")]
    public int Precision {
      get;
      set;
    }


    [DataField("DefaultValue")]
    public string DefaultValue {
      get;
      set;
    }


    [DataField("DataObjectExtData")]
    public string ExtensionData {
      get;
      set;
    }

    [DataField("DataObjectPosition")]
    public int Position {
      get;
      set;
    }


    [DataField("ReferenceOfId")]
    public int ReferenceOfId {
      get;
      set;
    }


    [DataField("ParentDataObjectId")]
    public int ParentDataObjectId {
      get;
      set;
    }


    [DataField("BaseDataObjectId")]
    public int BaseDataObjectId {
      get;
      set;
    }


    [DataField("DataObjectStatus", Default = EntityStatus.Pending)]
    public EntityStatus Status {
      get;
      set;
    }


    private string libraryBaseAddress = ConfigurationData.GetString("Empiria.Governance", "DocumentsLibrary.BaseAddress");
    public string TemplateUrl {
      get {
        return ExtensionData.Replace("~", libraryBaseAddress);
      }
    }


    #endregion Properties

  }  // class DataSource

}  // namespace Empiria.Data.DataObjects
