/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Document Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Storage.dll                        Pattern   : Information holder                      *
*  Type     : Documentation                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds required and optional documents and their status for an entity in a given context.       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Storage.Documents {

  /// <summary>Holds required and optional documents and their status
  /// for an entity in a given context.</summary>
  public class Documentation {

    public DocumentationStatus Status {
      get {
        return DocumentationStatus.NotAllowed;
      }
    }

  }  // class Documentation

}  // namespace Empiria.Storage.Documents
