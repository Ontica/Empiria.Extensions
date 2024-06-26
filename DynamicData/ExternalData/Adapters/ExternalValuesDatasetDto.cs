﻿/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria.DynamicData / ExternalData         Component : Interface adapters                      *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Data Transfer Object                    *
*  Type     : ExternalValuesDatasetDto                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Input DTO used to describe external variables data using datasets.                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.DynamicData.Datasets.Adapters;

namespace Empiria.DynamicData.ExternalData.Adapters {

  /// <summary>Input DTO used to describe external variables data using datasets.</summary>
  public class ExternalValuesDatasetDto {

    public ExternalValuesDatasetDto() {
      // no-op
    }

    public string ExternalVariablesSetUID {
      get; set;
    }


    public string DatasetKind {
      get; set;
    } = String.Empty;



    public DateTime Date {
      get; set;
    } = ExecutionServer.DateMinValue;

  }  // class ExternalValuesDatasetDto



  /// <summary>ExternalValuesDatasetDto type extension methods.</summary>
  static internal class ExternalValuesDatasetDtoExtensions {

    static public void EnsureIsValid(this ExternalValuesDatasetDto dto) {
      Assertion.Require(dto.ExternalVariablesSetUID, nameof(dto.ExternalVariablesSetUID));
      Assertion.Require(dto.Date != ExecutionServer.DateMinValue, nameof(dto.Date));
    }


    static internal ExternalVariablesSet GetExternalVariablesSet(this ExternalValuesDatasetDto dto) {
      return ExternalVariablesSet.Parse(dto.ExternalVariablesSetUID);
    }


    static internal DatasetInputDto MapToCoreDatasetInputDto(this ExternalValuesDatasetDto dto) {
      var set = ExternalVariablesSet.Parse(dto.ExternalVariablesSetUID);

      return new DatasetInputDto {
        DatasetFamilyUID = set.UID,
        DatasetKind = dto.DatasetKind,
        Date = dto.Date
      };
    }

  }  // class ExternalValuesDatasetDtoExtensions

}  // namespace Empiria.DynamicData.ExternalData.Adapters
