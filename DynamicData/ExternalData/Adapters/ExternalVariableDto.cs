/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria.DynamicData / ExternalData         Component : Interface adapters                      *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Data Transfer Object                    *
*  Type     : ExternalVariableDto                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : DTO for ExternalVariable instances.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.DynamicData.ExternalData.Adapters {

  /// <summary>DTO for ExternalVariable instances.</summary>
  public class ExternalVariableDto {

    internal ExternalVariableDto() {
      // no-op
    }

    public ExternalVariableDto(string code, string name) {
      Code = code;
      Name = name;
    }

    public string UID {
      get; internal set;
    }

    public string Code {
      get; internal set;
    }

    public string Name {
      get; internal set;
    }

    public string Notes {
      get; internal set;
    }

    public DateTime StartDate {
      get; set;
    }

    public DateTime EndDate {
      get; set;
    }

    public int? Position {
      get; internal set;
    }

    public string SetUID {
      get; internal set;
    }

  }  // class ExternalVariableDto

}  // namespace Empiria.DynamicData.ExternalData.Adapters
