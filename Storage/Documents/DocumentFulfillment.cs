/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Document Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Storage.dll                        Pattern   : Information holder                      *
*  Type     : Documentation                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides infomation about the fulfillemnt of a document based on a fulfillment rule.           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

/// <summary>Provides infomation about the fulfillemnt of a document based on a fulfillment rule.</summary>
namespace Empiria.Storage.Documents {

  public class DocumentFulfillment {


    internal DocumentFulfillment(DocumentationRule rule) {
      this.Rule = rule;
      this.Document = Document.Empty;
    }


    internal DocumentFulfillment(DocumentationRule rule, Document document) {
      this.Rule = rule;
      Document = document;
    }


    public DocumentationRule Rule {
      get;
    }


    public Document Document {
      get;
    }


    public DocumentationStatus Status {
      get {
        if (this.Document.IsEmptyInstance) {
          return this.Rule.Required ? DocumentationStatus.Required : DocumentationStatus.Optional;
        }
        return DocumentationStatus.Completed;
      }
    }

  }  // class DocumentFulfillment

} // namespace Empiria.Storage.Documents
