/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Document Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Storage.dll                        Pattern   : Information Holder                      *
*  Type     : DocumentationRule                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Contains data that defines a documentation rule for a given entity.                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Storage.Documents {

  /// <summary>Contains data that defines a documentation rule for a given entity.</summary>
  public class DocumentationRule {
    public object DocumentType {
      get;
      internal set;
    }
    public bool Required {
      get;
      internal set;
    }
  }  // class DocumentationRule

}  // namespace Empiria.Storage.Documents
