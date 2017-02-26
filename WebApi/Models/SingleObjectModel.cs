/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi.Models                            Assembly : Empiria.WebApi.dll                *
*  Type      : SingleObjectModel                                Pattern  : Web Api Response Model            *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Handles a consistent web api response for single objects.                                     *
*                                                                                                            *
********************************* Copyright (c) 2014-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Empiria.WebApi.Models {

  /// <summary>Handles a consistent web api response for single objects.</summary>
  [DataContract]
  public class SingleObjectModel : BaseResponseModel<object> {

    #region Constructors and parsers

    public SingleObjectModel(HttpRequestMessage request,
                             object instance, string typeName = "") : base(request, instance, typeName) {

    }

    #endregion Constructors and parsers

    #region Public properties

    public override LinksCollectionModel Links {
      get {
        var links = new LinksCollectionModel(this);

        return links;
      }
    }

    #endregion Public properties

  }  // class SingleObjectModel

} // namespace Empiria.WebApi.Models
