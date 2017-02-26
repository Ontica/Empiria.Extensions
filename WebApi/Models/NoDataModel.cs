/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi.Models                            Assembly : Empiria.WebApi.dll                *
*  Type      : NoDataModel                                      Pattern  : Web Api Response Model            *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Handles a consistent web api response for Http 200 no-data responses.                         *
*                                                                                                            *
********************************* Copyright (c) 2014-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Empiria.WebApi.Models {

  /// <summary>Handles a consistent web api response for Http 200 no-data responses.</summary>
  [DataContract]
  public class NoDataModel : BaseResponseModel<object> {

    #region Constructors and parsers

    public NoDataModel(HttpRequestMessage request) : base(request, new Array[0], "Empty") {

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

  }  // class NoDataModel

} // namespace Empiria.WebApi.Models
