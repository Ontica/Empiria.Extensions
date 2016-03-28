/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi.Models                            Assembly : Empiria.WebApi.dll                *
*  Type      : CollectionModel                                  Pattern  : Web Api Response Model            *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Handles a web api response for a collection of objects or DataTable instances.                *
*                                                                                                            *
********************************* Copyright (c) 2014-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections;
using System.Data;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Empiria.WebApi.Models {

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

} // namespace Empiria.WebApi.Models
