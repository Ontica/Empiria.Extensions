/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi.Models                            Assembly : Empiria.WebApi.dll                *
*  Type      : PagingModel                                      Pattern  : Information Holder                *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Holds the configuration data of a page.                                                       *
*                                                                                                            *
********************************* Copyright (c) 2014-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Empiria.WebApi.Models {

  /// <summary>Holds the configuration data of a page.</summary>
  [DataContract]
  public class PagingModel {

    private ICollection data;
    private PagingQueryModel query;

    public PagingModel(PagingQueryModel query, ICollection data) {
      Assertion.AssertObject(query, "query");
      Assertion.AssertObject(data, "data");

      this.query = query;
      this.data = data;
      AssertIsValid();
    }

    private void AssertIsValid() {
      Assertion.Assert(0 <= query.Skip && query.Skip <= this.data.Count,
                       "$skip value is out of range for the data set.");
    }

    #region Public properties

    [DataMember(Name = "totalDataItems", Order = 3)]
    public int ItemsCount {
      get {
        return data.Count;
      }
    }

    [DataMember(Name = "page", Order = 1)]
    public int PageNumber {
      get {
        if (query.Skip < data.Count) {
          return (query.Skip / query.Top) + 1;
        }
        return -1;
      }
    }

    [DataMember(Name = "pagesCount", Order = 2)]
    public int PagesCount {
      get {
        return (data.Count / query.Top) + ((data.Count % query.Top != 0) ? 1 : 0);
      }
    }

    public int PageSize {
      get {
        if (query.Skip < data.Count) {
          return Math.Min(query.Top, data.Count - query.Skip);
        } else {
          return query.Top;
        }
      }
    }

    public int StartIndex {
      get {
        if (1 <= query.Skip && query.Skip < data.Count) {
          return query.Skip - 1;
        } else {
          return 0;
        }
      }
    }

    public int EndIndex {
      get {
        if (0 <= query.Skip && query.Skip < data.Count) {
          return this.StartIndex + this.PageSize - 1;
        } else {
          return -1;
        }
      }
    }

    #endregion Public properties

  }  // class PagedDataModel

} // namespace Empiria.WebApi.Models
