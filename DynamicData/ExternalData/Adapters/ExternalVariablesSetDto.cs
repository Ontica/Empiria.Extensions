/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria.DynamicData / ExternalData         Component : Interface adapters                      *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Data Transfer Object                    *
*  Type     : ExternalVariablesSetDto                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : DTO for ExternalVariable instances.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using Empiria.Storage;

namespace Empiria.DynamicData.ExternalData.Adapters {

  /// <summary>DTO for ExternalVariablesSet instances.</summary>
  public class ExternalVariablesSetDto {

    internal ExternalVariablesSetDto() {
      // no-op
    }

    public string UID {
      get; internal set;
    }


    public string Name {
      get; internal set;
    }

    public FixedList<ExportToDto> ExportTo {
      get; internal set;
    }

  }  // class ExternalVariablesSetDto

}  // namespace Empiria.DynamicData.ExternalData.Adapters
