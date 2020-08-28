/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Data Objects                       Component : Service Provider                        *
*  Assembly : Empiria.Data.DataObjects.dll               Pattern   : Information Holder                      *
*  Type     : DataStore                                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Data stores are elements responsible of store and persist data.                                *
*             Typically they allow read or write access using special protocols and secure connections.      *
*             Can be databases, e-mail storages, file systems or simple spreedsheet or PDF files.            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Data.DataObjects {

  /// <summary>Data stores are elements responsible of store and persist data.
  /// Typically they allow read or write access using special protocols and secure connections.
  /// Can be databases, e-mail storages, file systems or simple spreedsheet or PDF files.</summary>
  public class DataStore : DataItem {

    #region Constructors and parsers

    protected DataStore(DataItemType powertype) : base(powertype) {
      // Required by Empiria Framework for all partitioned types.
    }


    static internal new DataStore Parse(int id) {
      return BaseObject.ParseId<DataStore>(id);
    }


    static public new DataStore Parse(string uid) {
      return BaseObject.ParseKey<DataStore>(uid);
    }


    static public FixedList<DataStore> GetList() {
      return DataItemsRepository.GetDataStores();
    }


    static public new DataStore Empty => BaseObject.ParseEmpty<DataStore>();


    #endregion Constructors and parsers

    #region Properties

    private string libraryBaseAddress = ConfigurationData.GetString("Empiria.Governance", "DocumentsLibrary.BaseAddress");
    public string TemplateUrl {
      get {
        return ExtensionData.Replace("~", libraryBaseAddress);
      }
    }


    #endregion Properties

  }  // class DataStore

}  // namespace Empiria.Data.DataObjects
