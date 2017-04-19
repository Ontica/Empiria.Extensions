/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Empiria Web API Services            *
*  Namespace : Empiria.WebApi.Client                          Assembly : Empiria.WebApi.Client.dll           *
*  Type      : WebApiClient                                   Pattern  : Service provider                    *
*  Version   : 1.2                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Provides methods to inkove web services using Empiria Web API infrastructure.                 *
*                                                                                                            *
********************************* Copyright (c) 2016-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Empiria.WebApi.Client {

  /// <summary>Provides methods to inkove web services using Empiria Web API infrastructure.</summary>
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

    public async Task DeleteAsync(string path, params object[] pars) {
      ServiceHandler service = ServiceDirectory.Instance.GetService(HttpMethod.Delete, path);

      HttpApiClient handler = this.GetApiClientHandler(service);

      await handler.DeleteAsync(service.Path, pars);
    }


    public async Task<T> DeleteAsync<T>(string path, params object[] pars) {
      ServiceHandler service = ServiceDirectory.Instance.GetService(HttpMethod.Delete, path);

      HttpApiClient handler = this.GetApiClientHandler(service);

      string dataScopeParameter = UtilityMethods.BuildDataScopeParameter(typeof(T), service, path);

      return await handler.DeleteAsync<T>(service.Path + dataScopeParameter, pars);
    }


    public async Task<T> GetAsync<T>(string path, params object[] pars) {
      ServiceHandler service = ServiceDirectory.Instance.GetService(HttpMethod.Get, path);

      HttpApiClient handler = this.GetApiClientHandler(service);

      string dataScopeParameter = UtilityMethods.BuildDataScopeParameter(typeof(T), service, path);

      return await handler.GetAsync<T>(service.Path + dataScopeParameter, pars);
    }


    public async Task PostAsync<T>(T body, string path, params object[] pars) {
      ServiceHandler service = ServiceDirectory.Instance.GetService(HttpMethod.Post, path);

      HttpApiClient handler = this.GetApiClientHandler(service);

      await handler.PostAsync(body, service.Path, pars);
    }


    public async Task<R> PostAsync<T, R>(T body, string path, params object[] pars) {
      ServiceHandler service = ServiceDirectory.Instance.GetService(HttpMethod.Post, path);

      HttpApiClient handler = this.GetApiClientHandler(service);

      string dataScopeParameter = UtilityMethods.BuildDataScopeParameter(typeof(T), service, path);

      return await handler.PostAsync<T, R>(body, service.Path + dataScopeParameter, pars);
    }


    public async Task PutAsync<T>(T body, string path, params object[] pars) {
      ServiceHandler service = ServiceDirectory.Instance.GetService(HttpMethod.Put, path);

      HttpApiClient handler = this.GetApiClientHandler(service);

      await handler.PostAsync(body, service.Path, pars);
    }


    public async Task<R> PutAsync<T, R>(T body, string path, params object[] pars) {
      ServiceHandler service = ServiceDirectory.Instance.GetService(HttpMethod.Put, path);

      HttpApiClient handler = this.GetApiClientHandler(service);

      string dataScopeParameter = UtilityMethods.BuildDataScopeParameter(typeof(T), service, path);

      return await handler.PostAsync<T, R>(body, service.Path + dataScopeParameter, pars);
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
