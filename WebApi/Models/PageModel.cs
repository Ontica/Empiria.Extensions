/* Empiria Service-Oriented Architecture Framework ***********************************************************
*                                                                                                            *
*  Solution  : Empiria SOA Framework                            System   : Empiria Web Api Framework         *
*  Namespace : Empiria.WebApi.Models                            Assembly : Empiria.WebApi.dll                *
*  Type      : PagingModel                                      Pattern  : Information Holder                *
*  Version   : 1.0        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Holds the configuration data of a page.                                                       *
*                                                                                                            *
********************************* Copyright (c) 2014-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Empiria.WebApi.Models {


  /// <summary>Holds the configuration data of a page.</summary>
  [DataContract]
  public class PagingModel {

    private ICollection data;

    public PagingModel(ICollection data) {
      Assertion.AssertObject(data, "data");

      this.data = data;
    }

    #region Public properties

    [DataMember(Name="totalItemsCount")]
    public int ItemsCount {
      get {
        return data.Count;
      }
    }

    [DataMember(Name = "pageNo")]
    public int Page {
      get {
        return 1;
      }
    }

    [DataMember(Name = "pagesCount")]
    public int PageCount {
      get {
        return 1;
      }
    }

    [DataMember(Name = "pageSize")]
    public int PageSize {
      get {
        return 100;
      }
    }

    #endregion Public properties

  }  // class PagedDataModel

} // namespace Empiria.WebApi.Models
