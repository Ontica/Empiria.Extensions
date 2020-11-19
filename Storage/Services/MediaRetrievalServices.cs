/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Service provider                        *
*  Type     : MediaRetrievalServices                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Collaborates with EFilingRequest entities controlling their documentation elements.            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Services;

using Empiria.Storage.Documents;

namespace Empiria.Storage.Services {

  public class MediaRetrievalServices : Service {

    #region Constructors and parsers

    static public MediaRetrievalServices CreateInstance() {
      return Service.CreateInstance<MediaRetrievalServices>();
    }


    public FixedList<IMediaDocument> GetEntityDocuments(IIdentifiable entity) {
      throw new NotImplementedException();
    }


    public FixedList<IMediaDocument> GetEntityDocuments(IIdentifiable entity, string linkType) {
      throw new NotImplementedException();
    }


    #endregion Constructors and parsers

  }  // class MediaRetrievalServices

}  // namespace Empiria.Storage.Services
