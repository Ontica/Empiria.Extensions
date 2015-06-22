/* Empiria Service-Oriented Architecture Framework ***********************************************************
*                                                                                                            *
*  Solution  : Empiria SOA Framework                            System   : Empiria Web Api Framework         *
*  Namespace : Empiria.WebApi.Models                            Assembly : Empiria.WebApi.dll                *
*  Type      : PagedCollectionModel                             Pattern  : Web Api Response Model            *
*  Version   : 1.0        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Holds the information for a web response of paged collection of objects.                      *
*                                                                                                            *
********************************* Copyright (c) 2014-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections;
using System.Data;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Empiria.WebApi.Models {

  /// <summary>Holds the information for a web response of paged collection of objects.</summary>
  [DataContract]
  public class PagedCollectionModel : CollectionModel {

    public PagedCollectionModel(HttpRequestMessage request,
                                ICollection data, string typeName = "") : base(request, data, typeName) {
    }

    public PagedCollectionModel(HttpRequestMessage request,
                                DataTable data, string typeName = "") : base(request, data, typeName) {
    }

    #region Public properties

    public override LinksCollectionModel Links {
      get {
        var links = new LinksCollectionModel(this);

        links.Add("~/api/v0/.../first", LinkRelation.First);
        links.Add("~/api/v0/.../previous", LinkRelation.Previous);
        links.Add("~/api/v0/.../next", LinkRelation.Next);
        links.Add("~/api/v0/.../last", LinkRelation.Last);

        return links;
      }
    }

    [DataMember(Name = "paging", Order = 20)]
    public PagingModel Paging {
      get {
        return new PagingModel(base.Data);
      }
    }

    #endregion Public properties

    #region Private methods

    #endregion Private methods

  }  // class PagedDataModel

} // namespace Empiria.WebApi.Models
