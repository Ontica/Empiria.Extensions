/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria.DynamicData / ExternalData         Component : Interface adapters                      *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Query payload                           *
*  Type     : ExternalValuesQuery                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query payload used to retrieve external values.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.DynamicData.ExternalData.Adapters {

  /// <summary>Describes the items returned for a ExternalValues query.</summary>
  public enum ExternalValuesDataSetMode {

    AllValues,

    OnlyLoadedValues,

  }  // enum ExternalValuesDataSetMode



  /// <summary>Query payload used to retrieve external values.</summary>
  public class ExternalValuesQuery {

    public ExternalValuesQuery() {
      // no-op
    }

    public string ExternalVariablesSetUID {
      get; set;
    }

    public DateTime Date {
      get; set;
    } = ExecutionServer.DateMinValue;


    public string ExportTo {
      get; set;
    } = string.Empty;


    public ExternalValuesDataSetMode DatasetMode {
      get; set;
    } = ExternalValuesDataSetMode.OnlyLoadedValues;


    internal ExternalVariablesSet GetExternalVariablesSet() {
      return ExternalVariablesSet.Parse(this.ExternalVariablesSetUID);
    }


    public void EnsureValid() {
      Assertion.Require(ExternalVariablesSetUID, "ExternalVariablesSetUID");
      Assertion.Require(Date != ExecutionServer.DateMinValue, "Date");
    }

  }  // class ExternalValuesQuery

}  // namespace Empiria.DynamicData.ExternalData.Adapters
