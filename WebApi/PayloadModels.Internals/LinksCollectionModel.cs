/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Information Holder                    *
*  Type     : LinksCollectionModel                         License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Holds a collection of web links objects.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.WebApi.Internals {

  /// <summary>Holds a collection of web links objects.</summary>
  public class LinksCollectionModel : List<LinkModel> {

    #region Constructors and parsers

    internal LinksCollectionModel(IBaseResponseModel responseModel) {
      Assertion.AssertObject(responseModel, "responseModel");

      this.Add(responseModel.Request.RequestUri.AbsoluteUri, LinkRelation.Self);

      this.Add(responseModel.Request.RequestUri.GetLeftPart(UriPartial.Authority) +
               "/api/v1/metadata/types/" + responseModel.TypeName, LinkRelation.Metadata);
    }

    #endregion Constructors and parsers

    #region Methods

    internal void Add(string url, LinkRelation relation) {
      var link = new LinkModel(url, relation);
      this.Add(link);
    }


    internal void Add(string url, string relation, string method = "GET") {
      var link = new LinkModel(url, relation, method);
      this.Add(link);
    }

    #endregion Methods

  }  // class LinksCollectionModel

}  // namespace Empiria.WebApi.Internals
