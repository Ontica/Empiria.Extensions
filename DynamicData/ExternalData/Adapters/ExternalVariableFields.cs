/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria.DynamicData / ExternalData         Component : Interface adapters                      *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Input DTO for Fields update             *
*  Type     : ExternalVariableFields                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Input DTO used to update external variables.                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.DynamicData.ExternalData.Adapters {

  /// <summary>Input DTO used to update external variables.</summary>
  public class ExternalVariableFields {

    public string Code {
      get; set;
    } = string.Empty;


    public string Name {
      get; set;
    } = string.Empty;


    public string Notes {
      get; set;
    } = string.Empty;


    public DateTime StartDate {
      get; set;
    } = ExecutionServer.DateMinValue;


    public DateTime EndDate {
      get; set;
    } = ExecutionServer.DateMinValue;

  }  // class ExternalVariableFields

}  // namespace Empiria.DynamicData.ExternalData.Adapters
