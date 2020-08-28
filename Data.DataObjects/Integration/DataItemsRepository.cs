/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Data Objects                       Component : Service Provider                        *
*  Assembly : Empiria.Data.DataObjects.dll               Pattern   : Data Services                           *
*  Type     : DataItemsRepository                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data access services for DataItem instances.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Data.DataObjects {

  /// <summary>Provides data access services for DataItem instances.</summary>
  static internal class DataItemsRepository {

    static internal FixedList<DataStore> GetDataStores() {
      var sql = "SELECT * FROM EXFDataItems " +
                "WHERE (DataItemNamedKey LIKE 'DataStore.%') AND (DataItemStatus <> 'X')";

      DataOperation operation = DataOperation.Parse(sql);

      return DataReader.GetFixedList<DataStore>(operation);
    }

  }  // class DataItemsRepository

}  // namespace Empiria.Data.DataObjects
