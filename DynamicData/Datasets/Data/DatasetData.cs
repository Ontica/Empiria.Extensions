﻿/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria.DynamicData / Datasets             Component : Data Layer                              *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Data Service                            *
*  Type     : DatasetsMapper                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read and write methods for datasets.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;

namespace Empiria.DynamicData.Datasets.Data {

  /// <summary>Provides data read and write methods for datasets.</summary>
  static internal class DatasetData {

    static internal FixedList<Dataset> GetDatasets(DatasetFamily datasetFamily,
                                                   DateTime date) {
      var sql = "SELECT * " +
                "FROM COF_ARCHIVOS " +
               $"WHERE ID_TIPO_ARCHIVO = {datasetFamily.Id} AND STATUS <> 'X' AND " +
               $"FECHA_OPERACION = {DataCommonMethods.FormatSqlDbDate(date)} " +
               $"ORDER BY ID_ARCHIVO";

      var operation = DataOperation.Parse(sql);

      return DataReader.GetFixedList<Dataset>(operation);
    }


    static internal FixedList<Dataset> GetDatasetsBeforeDate(DatasetFamily datasetFamily, DateTime date) {
      var sql = "SELECT * " +
          "FROM COF_ARCHIVOS " +
         $"WHERE ID_TIPO_ARCHIVO = {datasetFamily.Id} AND STATUS <> 'X' AND " +
         $"FECHA_ACTUALIZACION <= {DataCommonMethods.FormatSqlDbDateTime(date)} " +
         $"ORDER BY ID_ARCHIVO";

      var operation = DataOperation.Parse(sql);

      return DataReader.GetFixedList<Dataset>(operation);
    }


    static internal void WriteDataset(Dataset o) {
      var op = DataOperation.Parse("write_cof_archivo",
                        o.Id, o.UID, o.DatasetFamily.Id,
                        o.DatasetKind.UID, o.OperationDate, o.UpdatedTime,
                        o.ExtData.ToString(), o.OriginalFileName, o.FileName, -1,
                        o.MediaType, o.MediaLength, o.UploadedBy.Id,
                        (char) o.Status);

      DataWriter.Execute(op);
    }


  } // class DatasetData

} // namespace Empiria.DynamicData.Datasets.Data
