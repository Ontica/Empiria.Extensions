/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Document Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Storage.dll                        Pattern   : Service provider                        *
*  Type     : DocumentationRepository                    p[ara manLicense   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides services to fill out the defined documentation of an entity in a given context.       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Storage.Documents {

  static internal class DocumentationRepository {

    internal static List<DocumentationRule> GetDocumentationRules(IIdentifiable definer) {
      return new List<DocumentationRule>();
    }

    internal static List<Document> GetDocuments(IIdentifiable target) {
      return new List<Document>();
    }

  }  // class DocumentationRepository

}  // namespace Empiria.Storage.Documents
