/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Response Model                        *
*  Type     : CollectionModel                              License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Handles a web api response for a collection of objects or DataTable instances.                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;
using System.Data;
using System.Net.Http;
using System.Runtime.Serialization;

using Empiria.WebApi.Internals;

namespace Empiria.WebApi {

  /// <summary>Handles a web api response for a collection of objects or DataTable instances. </summary>
  [DataContract]
  public class CollectionModel : BaseResponseModel<ICollection> {

    #region Constructors and parsers

    public CollectionModel(HttpRequestMessage request, ICollection data,
                           string typeName = "") : base(request, data, typeName) {
    }


    public CollectionModel(HttpRequestMessage request, DataTable data,
                           string typeName = "")  : base(request, data.DefaultView, typeName) {

    }

    #endregion Constructors and parsers

    #region Public properties

    public override LinksCollectionModel Links {
      get {
        return new LinksCollectionModel(this);
      }
    }


    [DataMember(Name = "query", Order = 21)]
    public virtual QueryModel Query {
      get {
        return new QueryModel(base.Request);
      }
    }

    #endregion Public properties

  }  // class CollectionModel

} // namespace Empiria.WebApi
