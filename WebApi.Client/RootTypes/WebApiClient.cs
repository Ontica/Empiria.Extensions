﻿/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Client                             Component : Services Layer                          *
*  Assembly : Empiria.WebApi.Client.dll                  Pattern   : Service provider                        *
*  Type     : WebApiClient                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides methods to inkove web services using the Empiria Web API infrastructure.              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Net.Http;
using System.Threading.Tasks;

namespace Empiria.WebApi.Client {

  /// <summary>Provides methods to inkove web services using the Empiria Web API infrastructure.</summary>
  public class WebApiClient : IWebApiClient {

    #region Fields

    private readonly HttpApiClient fixedApiClientHandler = null;

    #endregion Fields

    #region Constructors and parsers

    public WebApiClient() {
      // no-op
    }

    /// <summary>Create a web api client that sends all the requests to a specific server regardless
    /// of the servers defined in the service directory rules.</summary>
    /// <param name="baseAddress">The server's base address.</param>
    public WebApiClient(string baseAddress) {
      this.fixedApiClientHandler = new HttpApiClient(baseAddress);
    }

    #endregion Constructors and parsers

    #region Public methods

    public Task DeleteAsync(string path, params object[] pars) {
      ServiceHandler service = ServiceDirectory.Instance.GetService(HttpMethod.Delete, path);

      HttpApiClient handler = this.GetApiClientHandler(service);

      return handler.DeleteAsync(service.Endpoint.Path, pars);
    }


    public Task<T> DeleteAsync<T>(string path, params object[] pars) {
      ServiceHandler service = ServiceDirectory.Instance.GetService(HttpMethod.Delete, path);

      HttpApiClient handler = this.GetApiClientHandler(service);

      string dataScopeParameter = UtilityMethods.BuildDataScopeParameter(typeof(T), service, path);

      return handler.DeleteAsync<T>(service.Endpoint.Path + dataScopeParameter, pars);
    }


    public Task<T> GetAsync<T>(string path, params object[] pars) {
      ServiceHandler service = ServiceDirectory.Instance.GetService(HttpMethod.Get, path);

      HttpApiClient handler = this.GetApiClientHandler(service);

      string dataScopeParameter = UtilityMethods.BuildDataScopeParameter(typeof(T), service, path);

      return handler.GetAsync<T>(service.Endpoint.Path + dataScopeParameter, pars);
    }


    public Task<T> PostAsync<T>(string path, params object[] pars) {
      ServiceHandler service = ServiceDirectory.Instance.GetService(HttpMethod.Post, path);

      HttpApiClient handler = this.GetApiClientHandler(service);

      return handler.PostAsync<T>(service.Endpoint.Path, pars);
    }


    public Task<T> PostAsync<T>(object body, string path, params object[] pars) {
      ServiceHandler service = ServiceDirectory.Instance.GetService(HttpMethod.Post, path);

      HttpApiClient handler = this.GetApiClientHandler(service);

      return handler.PostAsync<T>(body, service.Endpoint.Path, pars);
    }


    public Task<T> PutAsync<T>(object body, string path, params object[] pars) {
      ServiceHandler service = ServiceDirectory.Instance.GetService(HttpMethod.Put, path);

      HttpApiClient handler = this.GetApiClientHandler(service);

      string dataScopeParameter = UtilityMethods.BuildDataScopeParameter(typeof(T), service, path);

      return handler.PutAsync<T>(body, service.Endpoint.Path + dataScopeParameter, pars);
    }


    #endregion Public methods

    #region Private methods

    private HttpApiClient GetApiClientHandler(ServiceHandler service) {
      if (fixedApiClientHandler == null) {
        return service.GetHandler();
      } else {
        return service.PrepareHandler(fixedApiClientHandler);
      }
    }

    #endregion Private methods

  }  // class WebApiClient

}  // namespace Empiria.WebApi.Client
