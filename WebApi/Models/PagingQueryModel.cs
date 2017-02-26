/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi.Models                            Assembly : Empiria.WebApi.dll                *
*  Type      : PagingQueryModel                                 Pattern  : Information Holder                *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Extends QueryModel type with top and skip conditions to page data according to OData.         *
*                                                                                                            *
********************************* Copyright (c) 2014-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Empiria.WebApi.Models {

  /// <summary>Extends QueryModel type with top and skip conditions to page data according to OData.</summary>
  [DataContract]
  public class PagingQueryModel : QueryModel {

    public PagingQueryModel(HttpRequestMessage request) : base(request) {

    }

    #region Public properties

    [DataMember(Name = "skip")]
    public int Skip {
      get {
        int skip = base.GetQueryStringValue<int>("$skip", 0);

        Assertion.Assert(skip >= 0, "$skip query parameter should be greater or equal to zero.");

        return skip;
      }
    }

    [DataMember(Name = "top")]
    public int Top {
      get {
        int top = base.GetQueryStringValue<int>("$top", 100);

        Assertion.Assert(top >= 1, "$top query parameter should be greater or equal to one.");

        return top;
      }
    }

    #endregion Public properties

  }  // class PagingQueryModel

} // namespace Empiria.WebApi.Models
