/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Information Holder                    *
*  Type     : PagingModel                                  License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Holds the configuration data of a page.                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Empiria.WebApi.Internals {

  /// <summary>Holds the configuration data of a page.</summary>
  [DataContract]
  public class PagingModel {

    private ICollection data;
    private PagingQueryModel query;

    internal PagingModel(PagingQueryModel query, ICollection data) {
      Assertion.Require(query, "query");
      Assertion.Require(data, "data");

      this.query = query;
      this.data = data;
      AssertIsValid();
    }


    private void AssertIsValid() {
      Assertion.Require(0 <= query.Skip && query.Skip <= this.data.Count,
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

} // namespace Empiria.WebApi.Internals
