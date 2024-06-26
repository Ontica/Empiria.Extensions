﻿/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria.DynamicData / ExternalData         Component : Data Access Layer                       *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Data Service                            *
*  Type     : ExternalValuesData                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Data access layer for external values.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;

using Empiria.Data;
using Empiria.DynamicData.Datasets;

namespace Empiria.DynamicData.ExternalData.Data {

  /// <summary>Data access layer for external values.</summary>
  static internal class ExternalValuesData {

    static internal FixedList<ExternalValue> GetValues(ExternalVariable variable) {
      var sql = "SELECT * FROM COF_VALORES_EXTERNOS " +
               $"WHERE ID_VARIABLE_EXTERNA = {variable.Id} AND " +
               $"STATUS_VALOR_EXTERNO <> 'X' " +
               "ORDER BY FECHA_APLICACION";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<ExternalValue>(op);
    }


    static internal FixedList<ExternalValue> GetValues(Dataset dataset) {
      var sql = "SELECT * FROM COF_VALORES_EXTERNOS " +
               $"WHERE ID_ARCHIVO = {dataset.Id} AND " +
               $"STATUS_VALOR_EXTERNO <> 'X' " +
               "ORDER BY ID_VALOR_EXTERNO";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<ExternalValue>(op);
    }


    static internal FixedList<ExternalValue> GetValues(ExternalVariablesSet variablesSet, DateTime date) {
      var sql = "SELECT * FROM COF_VALORES_EXTERNOS " +
               $"WHERE ID_CONJUNTO_BASE = {variablesSet.Id} AND " +
               $"FECHA_APLICACION = {DataCommonMethods.FormatSqlDbDate(date)} AND " +
               $"STATUS_VALOR_EXTERNO <> 'X' " +
               "ORDER BY ID_VALOR_EXTERNO";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<ExternalValue>(op);
    }


    static internal void RemoveDataset(Dataset dataset) {
      FixedList<ExternalValue> values = GetValues(dataset);

      values.ToList().ForEach(x => x.Delete());

      var sql = "UPDATE COF_VALORES_EXTERNOS " +
                "SET STATUS_VALOR_EXTERNO = 'X' " +
               $"WHERE ID_ARCHIVO = {dataset.Id}";

      var op = DataOperation.Parse(sql);

      DataWriter.Execute(op);
    }


    static internal void Write(ExternalValue o) {
      var op = DataOperation.Parse("write_cof_valor_externo",
                      o.Id, o.UID, o.ExternalVariable.Set.Id, o.ExternalVariable.Id,
                      o.ValuesExtData.ToString(),
                      o.ApplicationDate, o.UpdatedDate,
                      o.SourceDataset.Id, (char) o.Status, o.UpdatedBy.Id);

      DataWriter.Execute(op);
    }

  }  // class ExternalValuesData

}  // namespace Empiria.DynamicData.ExternalData.Data
