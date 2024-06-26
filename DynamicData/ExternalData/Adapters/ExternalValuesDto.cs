﻿/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria.DynamicData / ExternalData         Component : Interface adapters                      *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Data Transfer Object                    *
*  Type     : ExternalValuesDto                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO for external values query.                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.DynamicData.ExternalData.Adapters {

  /// <summary>Output DTO for external values query.</summary>
  public class ExternalValuesDto {

    internal ExternalValuesDto() {
      // no-op
    }

    public ExternalValuesQuery Query {
      get; internal set;
    }


    public FixedList<DataTableColumn> Columns {
      get; internal set;
    }


    public FixedList<ExternalValuesEntryDto> Entries {
      get; internal set;
    }

  }  // class ExternalValuesDto

}  // namespace Empiria.DynamicData.ExternalData.Adapters
