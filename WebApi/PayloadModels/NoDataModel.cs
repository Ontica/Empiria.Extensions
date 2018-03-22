/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Response Model                        *
*  Type     : NoDataModel                                  License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Handles a consistent web api response for Http 200 no-data responses.                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Net.Http;
using System.Runtime.Serialization;

using Empiria.WebApi.Internals;

namespace Empiria.WebApi {

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

} // namespace Empiria.WebApi
