/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Empiria Web API Services            *
*  Namespace : Empiria.WebApi.Client                          Assembly : Empiria.WebApi.Client.dll           *
*  Type      : HttpApiClient                                  Pattern  : Service provider                    *
*  Version   : 1.2                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Provides methods to inkove web services using Empiria Web API infrastructure.                 *
*                                                                                                            *
********************************* Copyright (c) 2016-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

using Empiria.Json;

namespace Empiria.WebApi.Client {

  public class HttpApiClient {

    #region Fields

    private HttpClient httpClient = new HttpClient();

    #endregion Fields

    #region Constructors and parsers

    /// <summary>Initializes a Web API connector to a fixed server.</summary>
    /// <param name="baseAddress">The server's base address.</param>
    public HttpApiClient(string baseAddress) {
      Assertion.AssertObject(baseAddress, "baseAddress");

      httpClient.BaseAddress = new Uri(baseAddress);

      this.LoadDefaultHeaders();
    }

    #endregion Constructors and parsers

    #region Public methods

    public bool IncludeAuthorizationHeader {
      get;
      set;
    }


    public async Task<T> DeleteAsync<T>(string path, params object[] pars) {
      HttpResponseMessage response = await this.SendRequestAsync(HttpMethod.Delete, String.Empty,
                                                                 path, pars);

      return await this.ConvertHttpContentAsync<T>(response);
    }


    public async Task DeleteAsync(string path, params object[] pars) {
      await this.SendRequestAsync(HttpMethod.Delete, String.Empty,
                                  path, pars);
    }


    public async Task<T> GetAsync<T>(string path, params object[] pars) {
      var response = await this.SendRequestAsync(HttpMethod.Get, String.Empty,
                                                 path, pars);

      return await this.ConvertHttpContentAsync<T>(response);
    }


    /// <summary>Sends a json POST request as an asynchronous operation
    /// discarding the response.</summary>
    public async Task PostAsync<T>(T body, string path, params object[] pars) {
      await this.SendRequestAsync(HttpMethod.Post, body, path, pars);
    }

    /// <summary>Sends a json POST request of type T as an asynchronous operation
    /// returning a response of type R.</summary>
    public async Task<R> PostAsync<T, R>(T body, string path, params object[] pars) {
      HttpResponseMessage response = await this.SendRequestAsync(HttpMethod.Post, body, path, pars);

      return await this.ConvertHttpContentAsync<R>(response);
    }


    /// <summary>Sends a json PUT request as an asynchronous operation
    /// discarding the response.</summary>
    public async Task PutAsync<T>(T body, string path, params object[] pars) {
      await this.SendRequestAsync(HttpMethod.Put, body, path, pars);
    }


    /// <summary>Sends a json PUT request of type T as an asynchronous operation
    /// returning a response of type R.</summary>
    public async Task<R> PutAsync<T, R>(T body, string path, params object[] pars) {
      HttpResponseMessage response = await this.SendRequestAsync(HttpMethod.Put, body, path, pars);

      return await this.ConvertHttpContentAsync<R>(response);
    }

    #endregion Public methods

    #region Private methods

    private void CleanRequestHeaders() {
      this.RemoveAuthorizationHeader();
    }


    private async Task<T> ConvertHttpContentAsync<T>(HttpResponseMessage response) {
      if (typeof(T) == typeof(HttpResponseMessage)) {
        return (T) (object) response;

      } else if (typeof(T) == typeof(string)) {
        var content = await response.Content.ReadAsStringAsync();

        return (T) (object) content;

      } else if (typeof(T) == typeof(JsonObject)) {
        var content = await response.Content.ReadAsStringAsync();

        return (T) (object) JsonObject.Parse(content);

      } else {
        return await response.Content.ReadAsAsync<T>();

      }
    }


    private Task<HttpResponseMessage> InvokeMethodAsync<T>(HttpMethod method, string fullPath, T body) {
      if (method == HttpMethod.Get) {
        return httpClient.GetAsync(fullPath);

      } else if (method == HttpMethod.Post) {
        return httpClient.PostAsJsonAsync(fullPath, body);

      } else if (method == HttpMethod.Put) {
        return httpClient.PutAsJsonAsync(fullPath, body);

      } else if (method == HttpMethod.Delete) {
        return httpClient.DeleteAsync(fullPath);

      } else {
        throw Assertion.AssertNoReachThisCode("Http method handler not defined for '{0}'.",
                                              method.Method);
      }
    }


    private void LoadDefaultHeaders() {
      httpClient.DefaultRequestHeaders.Accept.Clear();
      httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }


    private void RemoveAuthorizationHeader() {
      if (httpClient.DefaultRequestHeaders.Contains("Authorization")) {
        httpClient.DefaultRequestHeaders.Remove("Authorization");
      }
    }


    private async Task<HttpResponseMessage> SendRequestAsync<T>(HttpMethod method, T body,
                                                                string path, object[] pars) {
      Assertion.AssertObject(path, "path");

      string fullPath = EmpiriaString.Format(path, pars);

      this.SetRequestHeaders();
      HttpResponseMessage response = await this.InvokeMethodAsync(method, fullPath, body);
      this.CleanRequestHeaders();

      response.EnsureSuccessStatusCode();

      return response;
    }


    private void SetAuthorizationHeader() {
      if (httpClient.DefaultRequestHeaders.Contains("Authorization")) {
        return;
      }
      if (ExecutionServer.IsAuthenticated) {
        httpClient.DefaultRequestHeaders.Add("Authorization",
                                             "bearer " + ExecutionServer.CurrentPrincipal.Session.Token);
      }
    }


    private void SetRequestHeaders() {
      if (this.IncludeAuthorizationHeader) {
        this.SetAuthorizationHeader();
      }
    }

    #endregion Private methods

  }  // class WebApiClient

}  // namespace Empiria.WebApi.Client
