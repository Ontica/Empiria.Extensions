/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Empiria Web API Services            *
*  Namespace : Empiria.WebApi.Client                          Assembly : Empiria.WebApi.Client.dll           *
*  Type      : ServiceHandler                                 Pattern  : Information holder                  *
*  Version   : 1.2                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Describes a web service and provides a handler to invoke it.                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.WebApi.Client {

  /// <summary>Describes a web service and provides a handler to invoke it.</summary>
  internal class ServiceHandler {

    #region Constructors and parsers

    private static Dictionary<string, HttpApiClient> cache = new Dictionary<string, HttpApiClient>();
    private static object locker = new object();

    #endregion Constructors and parsers

    #region Constructors and parsers

    internal ServiceHandler(EndpointConfig endpoint) {
      Assertion.Require(endpoint, "endpoint");

      this.Endpoint = endpoint;
    }

    #endregion Constructors and parsers

    #region Properties

    internal EndpointConfig Endpoint {
      get;
    }

    #endregion Properties

    #region Methods

    internal HttpApiClient GetHandler() {
      string serverUID = this.Endpoint.BaseAddress;

      if (!cache.ContainsKey(serverUID)) {
        lock (locker) {
          if (!cache.ContainsKey(serverUID)) {
            var handler = new HttpApiClient(this.Endpoint.BaseAddress);

            cache.Add(serverUID, handler);
          }
        }
      }
      return this.PrepareHandler(cache[serverUID]);
    }


    internal HttpApiClient PrepareHandler(HttpApiClient handler) {
      Assertion.Require(handler, "handler");

      handler.IncludeAuthorizationHeader = this.Endpoint.IsProtected;

      return handler;
    }

    #endregion Methods

  } // class ServiceHandler

} // namespace Empiria.WebApi.Client
