/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Information Holder                    *
*  Type     : PagingQueryModel                             License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Extends QueryModel type with top and skip conditions to page data according to OData.          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Empiria.WebApi.Internals {

  /// <summary>Extends QueryModel type with top and skip conditions to page data according to OData.</summary>
  [DataContract]
  public class PagingQueryModel : QueryModel {

    internal PagingQueryModel(HttpRequestMessage request) : base(request) {

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

} // namespace Empiria.WebApi.Internals
