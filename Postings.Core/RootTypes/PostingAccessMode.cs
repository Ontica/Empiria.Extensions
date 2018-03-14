/* Empiria Postings ******************************************************************************************
*                                                                                                            *
*  Module   : Empiria Postings                             Component : Domain services                       *
*  Assembly : Empiria.Postings.dll                         Pattern   : Enumeration                           *
*  Type     : PostingAccessMode                            License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Describes the access level permissions for a posting.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Postings {

  /// <summary>Describes the access level permissions for a posting.</summary>
  public enum PostingAccessMode {

    Empty = 'E',

    Public = 'P',

    Internal = 'I',

    Private = 'R',

  }  // enum PostingAccessMode

}  // namespace Empiria.Postings
