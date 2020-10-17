/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Document Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Storage.dll                        Pattern   : Enumeration Type                        *
*  Type     : DocumentationStatus                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Indicates the overall documentation filing status for an entity in a given context.            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Storage.Documents {

  /// <summary>Indicates the overall documentation filing status for an entity in a given context.</summary>
  public enum DocumentationStatus {

    NotAllowed,

    Optional,

    Required,

    Completed,

  }  // enum DocumentationStatus

}  // namespace Empiria.Storage.Documents
