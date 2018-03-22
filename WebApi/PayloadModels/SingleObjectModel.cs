/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Response Model                        *
*  Type     : SingleObjectModel                            License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Handles a consistent web api response for single objects.                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Net.Http;
using System.Runtime.Serialization;

using Empiria.WebApi.Internals;

namespace Empiria.WebApi {

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

} // namespace Empiria.WebApi
