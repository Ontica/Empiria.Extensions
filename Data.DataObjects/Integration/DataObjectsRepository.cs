/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Data Objects                       Component : Service Provider                        *
*  Assembly : Empiria.Data.DataObjects.dll               Pattern   : Data Services                           *
*  Type     : DataObjectsRepository                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data access services for DataObjects instances.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Data.DataObjects {

  /// <summary>Provides data access services for DataObjects instances.</summary>
  static internal class DataObjectsRepository {

    static internal FixedList<DataSource> GetDataSources() {
      var sql = "SELECT * FROM EXFDataObjects " +
                "WHERE (DataObjectType LIKE 'DataSource.%') AND (DataObjectStatus <> 'X')";

      DataOperation operation = DataOperation.Parse(sql);

      return DataReader.GetFixedList<DataSource>(operation);
    }

  }  // class DataObjectsRepository

}  // namespace Empiria.Data.DataObjects
