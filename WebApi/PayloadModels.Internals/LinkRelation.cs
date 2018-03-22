/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Enumeration                           *
*  Type     : LinkRelation                                 License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Describes a link as are identified by IRIs according to the RFC 5988.                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.WebApi.Internals {

  /// <summary>Describes a link as are identified by Internationalised Resource Identifiers (IRIs)
  /// according to the RFC 5988.</summary>
  internal enum LinkRelation {

    Self,

    Append,

    Edit,

    Delete,

    Metadata,

    Help,

    First,

    Previous,

    Next,

    Last,

    Up,

  }  // enum LinkRelation

} // namespace Empiria.WebApi.Internals
