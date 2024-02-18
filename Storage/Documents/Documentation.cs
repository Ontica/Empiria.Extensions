/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Document Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Storage.dll                        Pattern   : Information holder                      *
*  Type     : Documentation                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds required and optional documents and their status for an entity in a given context.       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Storage.Documents {

  /// <summary>Holds required and optional documents and their status
  /// for an entity in a given context.</summary>
  public class Documentation {

    private readonly List<DocumentationRule> _rules;
    private readonly List<Document> _documents;

    public Documentation(List<DocumentationRule> documentationRules, List<Document> documents) {
      _rules = documentationRules;
      _documents = documents;
    }


    public FixedList<DocumentationRule> Rules => _rules.ToFixedList();

    public FixedList<Document> Documents => _documents.ToFixedList();


    public DocumentationStatus OverallStatus {
      get {
        return DocumentationStatus.NotAllowed;
      }
    }

    public FixedList<DocumentFulfillment> GetFulfillment() {
      var list = new List<DocumentFulfillment>(_rules.Count);

      foreach (var rule in _rules) {
        var document = _documents.Find(x => x.DocumentType.Equals(rule.DocumentType));
        DocumentFulfillment fulfillment;

        if (document != null) {
          fulfillment = new DocumentFulfillment(rule, document);
        } else {
          fulfillment = new DocumentFulfillment(rule);
        }
        list.Add(fulfillment);
      }
      return list.ToFixedList();
    }

  }  // class Documentation

}  // namespace Empiria.Storage.Documents
