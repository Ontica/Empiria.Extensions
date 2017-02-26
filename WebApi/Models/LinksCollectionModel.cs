/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi.Models                            Assembly : Empiria.WebApi.dll                *
*  Type      : LinksCollectionModel                             Pattern  : Collection class                  *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Holds a collection of web links objects.                                                      *
*                                                                                                            *
********************************* Copyright (c) 2014-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;

namespace Empiria.WebApi.Models {

  /// <summary>Holds a collection of web links objects.</summary>
  public class LinksCollectionModel : List<LinkModel> {

    public LinksCollectionModel(IBaseResponseModel responseModel) {
      Assertion.AssertObject(responseModel, "responseModel");

      this.Add(responseModel.Request.RequestUri.AbsoluteUri, LinkRelation.Self);

      this.Add(responseModel.Request.RequestUri.GetLeftPart(UriPartial.Authority) +
               "/api/v1/metadata/types/" + responseModel.TypeName, LinkRelation.Metadata);
    }

    public void Add(string url, LinkRelation relation) {
      var link = new LinkModel(url, relation);
      this.Add(link);
    }

    public void Add(string url, string relation, string method = "GET") {
      var link = new LinkModel(url, relation, method);
      this.Add(link);
    }

  }  // class LinksCollectionModel

}  // namespace Empiria.WebApi.Models
