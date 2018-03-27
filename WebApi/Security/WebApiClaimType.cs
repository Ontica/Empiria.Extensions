/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Security                                     Component : Web Api Security Services             *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Enumeration Type                      *
*  Type     : WebApiClaimType                              License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Defines Web API security claims for client applications and users.                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.WebApi {

  // Defines Web API security claims for client applications and users.
  public enum WebApiClaimType {

    ClientApp_Controller,

    ClientApp_Method,

    User_Role,

  }  // enum WebApiClaimType

}  // namespace Empiria.WebApi
