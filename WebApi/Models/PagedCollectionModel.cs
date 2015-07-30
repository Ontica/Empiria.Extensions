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
using System.Collections.Specialized;
using System.Data;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web;

namespace Empiria.WebApi.Models {

  /// <summary>Holds the information for a web response of paged collection of objects.</summary>
  [DataContract]
  public class PagedCollectionModel : CollectionModel {

    public PagedCollectionModel(HttpRequestMessage request,
                                ArrayList data, string typeName = "") : base(request, data, typeName) {
      this.DoDataPaging(data);
    }

    public PagedCollectionModel(HttpRequestMessage request,
                                DataTable data, string typeName = "") : base(request, data, typeName) {
      this.DoDataPaging(data);
    }

    #region Public properties

    public override LinksCollectionModel Links {
      get {
        var links = new LinksCollectionModel(this);

        if (this.Paging.PagesCount == 0) {
          return links;
        }

        if (1 < this.Paging.PageNumber) {
          links.Add(this.GetPreviousPageUrl(), LinkRelation.Previous);
        }
        if (Paging.PageNumber < Paging.PagesCount) {
          links.Add(this.GetNextPageUrl(), LinkRelation.Next);
        }

        return links;
      }
    }

    PagingModel _pagingModel = null;
    [DataMember(Name = "paging", Order = 20)]
    public PagingModel Paging {
      get {
        if (_pagingModel == null) {
          _pagingModel = new PagingModel((PagingQueryModel) this.Query, base.Data);
        }
        return _pagingModel;
      }
    }

    public override QueryModel Query {
      get {
        return new PagingQueryModel(base.Request);
      }
    }

    #endregion Public properties

    #region Private methods

    private void DoDataPaging(ArrayList array) {
      array = array.GetRange(this.Paging.StartIndex, this.Paging.PageSize);

      base.RefreshData(array);
    }

    private void DoDataPaging(DataTable sourceTable) {
      DataTable pagedTable = sourceTable.Clone();

      for (int i = this.Paging.StartIndex; i <= this.Paging.EndIndex; i++) {
        pagedTable.Rows.Add(sourceTable.Rows[i].ItemArray);
      }
      base.RefreshData(pagedTable.DefaultView);
    }

    private string GetNextPageUrl() {
      Uri requestUri = base.Request.RequestUri;
      NameValueCollection qs = requestUri.ParseQueryString();

      qs["$top"] = this.Paging.PageSize.ToString();
      qs["$skip"] = ((this.Paging.PageNumber) * this.Paging.PageSize).ToString();

      return this.GetNewUrlWithUpdatedQueryString(requestUri, qs);
    }

    private string GetPreviousPageUrl() {
      Uri requestUri = base.Request.RequestUri;
      NameValueCollection qs = requestUri.ParseQueryString();

      qs["$top"] = this.Paging.PageSize.ToString();
      qs["$skip"] = ((this.Paging.PageNumber - 2) * this.Paging.PageSize).ToString();

      return this.GetNewUrlWithUpdatedQueryString(requestUri, qs);
    }

    private string GetNewUrlWithUpdatedQueryString(Uri requestUri, NameValueCollection qs) {
      string url = String.Empty;

      if (String.IsNullOrWhiteSpace(requestUri.Query)) {
        url = requestUri.AbsoluteUri + "?" + qs.ToString();
      } else {
        url = requestUri.AbsoluteUri.Replace(requestUri.Query, String.Empty) + "?" + qs.ToString();
      }
      return HttpUtility.UrlDecode(url);
    }

    #endregion Private methods

  }  // class PagedDataModel

} // namespace Empiria.WebApi.Models
