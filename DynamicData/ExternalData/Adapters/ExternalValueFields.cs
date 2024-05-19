/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria.DynamicData / ExternalData         Component : Interface adapters                      *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Input DTO for Fields update             *
*  Type     : ExternalValueFields                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Input DTO used to update external values.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Contacts;
using Empiria.Json;
using Empiria.StateEnums;

using Empiria.DynamicData.Datasets;

namespace Empiria.DynamicData.ExternalData.Adapters {

  /// <summary>Input DTO used to update external values.</summary>
  public class ExternalValueFields : DynamicFields {

    public string VariableUID {
      get; set;
    }

    public string VariableCode {
      get; set;
    }

    public DateTime ApplicationDate {
      get; set;
    }

    public Contact UpdatedBy {
      get; set;
    }

    public DateTime UpdatedDate {
      get; set;
    }

    public Dataset Dataset {
      get;
      internal set;
    }

    public int Position {
      get; set;
    }

    public EntityStatus Status {
      get; set;
    }


    public ExternalVariable GetExternalVariable() {
      var variable = ExternalVariable.TryParseWithCode((ExternalVariablesSet) Dataset.DatasetFamily, VariableCode);

      Assertion.Require(variable,
                        $"No existe la variable externa con clave '{VariableCode}' " +
                        $"dentro del conjunto de variables '{Dataset.DatasetFamily.Name}'.");

      return variable;
    }

    internal JsonObject GetDynamicFieldsAsJson() {
      return this.ToJson();
    }

  }  // class ExternalValueInputDto

} // namespace Empiria.DynamicData.ExternalData.Adapters
