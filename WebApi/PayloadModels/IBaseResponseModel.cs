/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Interface                             *
*  Type     : IBaseResponseModel                           License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Base interface for web api response types.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Net.Http;

namespace Empiria.WebApi {

  /// <summary>Base interface for web api response types.</summary>
  public interface IBaseResponseModel {

    string Status {
      get;
    }


    string Version {
      get;
    }


    string TypeName {
      get;
    }


    int DataItemsCount {
      get;
    }


    HttpRequestMessage Request {
      get;
    }


    Guid RequestId {
      get;
    }

  }  // interface IBaseResponseModel

} // namespace Empiria.WebApi
