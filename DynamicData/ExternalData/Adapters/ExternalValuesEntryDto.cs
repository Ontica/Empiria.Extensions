/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria.DynamicData / ExternalData         Component : Interface adapters                      *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Data Transfer Object                    *
*  Type     : ExternalValuesEntryDto                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO that holds an external value with dynamic fields.                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.DynamicData.ExternalData.Adapters {

  /// <summary>Output DTO that holds an external value with dynamic fields.</summary>
  public class ExternalValuesEntryDto : DynamicFields {

    internal ExternalValuesEntryDto() {
      // no-op
    }

    public string VariableCode {
      get; internal set;
    }

    public string VariableName {
      get; internal set;
    }

    public override IEnumerable<string> GetDynamicMemberNames() {
      List<string> members = new List<string>();

      members.Add("VariableCode");
      members.Add("VariableName");

      members.AddRange(base.GetDynamicMemberNames());

      return members;
    }

  }  // class ExternalValuesEntryDto

}  // namespace Empiria.DynamicData.ExternalData.Adapters
