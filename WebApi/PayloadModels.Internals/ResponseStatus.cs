/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Enumeration                           *
*  Type     : ResponseStatus                               License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Propietary response status returned in all Empiria Web Api payloads.                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.WebApi.Internals {

  /// <summary>Propietary response status returned in all Empiria Web Api payloads.</summary>
  public enum ResponseStatus {

    Ok,

    Ok_No_Data,

    Denied,

    Over_Limit,

    Unavailable,

    Invalid_Request,

    Error

  }  // enum ResponseStatus

} // namespace Empiria.WebApi.Internals
