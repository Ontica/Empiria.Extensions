/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Core Services                        Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Materialized List                     *
*  Type     : LinksCollectionModel                         License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : List of LinkModel instances.                                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.WebApi.Internals {

  /// <summary>List of LinkModel instances.</summary>
  public class LinksCollectionModel : List<LinkModel> {

    #region Constructors and parsers

    internal LinksCollectionModel(IBaseResponseModel responseModel) {
      Assertion.AssertObject(responseModel, "responseModel");

      // ToDo:
      // Move these default links to some caller
      //
      this.Add(responseModel.Request.RequestUri.AbsoluteUri, LinkRelation.Self,
               responseModel.Request.Method.Method);

      this.Add(responseModel.Request.RequestUri.GetLeftPart(UriPartial.Authority) +
               "/api/v1/metadata/types/" + responseModel.TypeName, LinkRelation.Metadata);
    }

    #endregion Constructors and parsers

    #region Methods

    internal void Add(string url, LinkRelation relation, string method = "GET") {
      var link = new LinkModel(url, relation, method);

      this.Add(link);
    }


    internal void Add(string url, string relation, string method = "GET") {
      var link = new LinkModel(url, relation, method);

      this.Add(link);
    }

    #endregion Methods

  }  // class LinksCollectionModel

}  // namespace Empiria.WebApi.Internals
