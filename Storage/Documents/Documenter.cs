/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Document Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Storage.dll                        Pattern   : Service provider                        *
*  Type     : Documenter                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides services to fill out the defined documentation of an entity in a given context.       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Storage.Documents {

  /// <summary>Provides services to fill out the defined documentation
  /// of an entity in a given context.</summary>
  public class Documenter {

    private readonly IIdentifiable _definer;
    private readonly IIdentifiable _target;

    private readonly List<DocumentationRule> _documentationRules;
    private readonly List<Document> _documents;

    private readonly Documentation _documentation;


    public Documenter(IIdentifiable definer, IIdentifiable target) {
      _definer = definer;
      _target = target;

      _documentationRules = DocumentationRepository.GetDocumentationRules(_definer);
      _documents = DocumentationRepository.GetDocuments(_target);

      _documentation = new Documentation(_documentationRules, _documents);
    }


    public Documentation Documentation {
      get {
        return _documentation;
      }
    }

    #region Definer documentation rules

    public void AddDocumentationRule(DocumentationRule rule) {
      Assertion.Require(rule, "rule");

      _documentationRules.Add(rule);
    }


    public void RemoveDocumentationRule(DocumentationRule rule) {
      Assertion.Require(rule, "rule");

      _documentationRules.Remove(rule);
    }


    public void UpdateDocumentationRule(DocumentationRule rule) {
      Assertion.Require(rule, "rule");

      int index = _documentationRules.IndexOf(rule);

      _documentationRules[index] = rule;
    }


    #endregion Definer documentation rules

    #region Target attached documents


    public void AttachDocument(DocumentationRule rule, Document document) {
      throw new NotImplementedException();
    }


    public void DetachDocument(DocumentationRule rule, Document document) {
      throw new NotImplementedException();
    }


    public void ReattachDocument(DocumentationRule rule, Document document) {
      throw new NotImplementedException();
    }


    #endregion Target attached documents

  }  // class Documenter

}  // namespace Empiria.Storage.Documents
