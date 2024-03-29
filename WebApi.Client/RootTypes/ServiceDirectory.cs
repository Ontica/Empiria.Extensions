﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Empiria Web API Services            *
*  Namespace : Empiria.WebApi.Client                          Assembly : Empiria.WebApi.Client.dll           *
*  Type      : ServiceDirectory                               Pattern  : Singleton type                      *
*  Version   : 1.2                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Singleton that holds a list of web services that can be searched by path or by its unique ID. *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Net.Http;

using Empiria.Security;

namespace Empiria.WebApi.Client {

  /// <summary>Singleton that holds a list of web services that can be searched
  /// by path or by its unique ID.</summary>
  internal class ServiceDirectory {

    #region Fields

    private readonly Dictionary<string, ServiceHandler> services = new Dictionary<string, ServiceHandler>();

    #endregion Fields

    #region Constructors and parsers

    private ServiceDirectory() {
      this.LoadServices();
    }


    private static ServiceDirectory _instance = null;
    private static object _syncRoot = new Object();
    static public ServiceDirectory Instance {
      get {
        if (_instance == null) {
          lock (_syncRoot) {
            if (_instance == null) {
              _instance = new ServiceDirectory();
            }
          }  // lock
        }
        return _instance;
      }
    }

    #endregion Constructors and parsers

    #region Public methods

    public ServiceHandler GetService(HttpMethod method, string serviceUID) {
      Assertion.Require(serviceUID, "serviceUID");

      string searchKey = String.Empty;

      if (UtilityMethods.HasUriPathFormat(serviceUID)) {
        searchKey = method.Method + " " + UtilityMethods.RemoveDataScopeFromPath(serviceUID);

      } else {
        searchKey = UtilityMethods.RemoveDataScopeFromPath(serviceUID);

      }

      if (services.ContainsKey(searchKey)) {
        return services[searchKey];

      } else {
        throw new WebApiClientException(WebApiClientException.Msg.UndefinedServiceUIDOrEndpoint, searchKey);

      }
    }

    #endregion Public methods

    #region Private methods

    private void LoadServices() {
      var endpointsList = EndpointConfig.GetList(ExecutionServer.CurrentPrincipal.ClientApp);

      this.services.Clear();

      foreach (var endpoint in endpointsList) {
        var serviceHandler = new ServiceHandler(endpoint);

        this.services.Add(serviceHandler.Endpoint.UID,
                          serviceHandler);

        this.services.Add($"{serviceHandler.Endpoint.Method} {serviceHandler.Endpoint.Path}",
                          serviceHandler);
      }
    }

    #endregion Private methods

  }  // class ServiceDirectory

}  // namespace Empiria.WebApi.Client
