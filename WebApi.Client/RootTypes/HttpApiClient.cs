/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Empiria Web API Services            *
*  Namespace : Empiria.WebApi.Client                          Assembly : Empiria.WebApi.Client.dll           *
*  Type      : HttpApiClient                                  Pattern  : Service provider                    *
*  Version   : 1.2                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Provides methods to inkove web services using Empiria Web API infrastructure.                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

using Empiria.Json;

namespace Empiria.WebApi.Client {

  public class HttpApiClient {

    #region Fields

    private static readonly int DEFAULT_HTTP_CALL_TIMEOUT_SECONDS =
                                            ConfigurationData.Get<int>("HttpDefaultTimeout", 5);

    private readonly HttpClient httpClient = new HttpClient();

    #endregion Fields

    #region Constructors and parsers

    /// <summary>Initializes a Web API connector to a fixed server.</summary>
    /// <param name="baseAddress">The server's base address.</param>
    public HttpApiClient(string baseAddress): this(baseAddress,
                                              TimeSpan.FromSeconds(DEFAULT_HTTP_CALL_TIMEOUT_SECONDS)) {
      // no-op
    }


    /// <summary>Initializes a Web API connector to a fixed server.</summary>
    /// <param name="baseAddress">The server's base address.</param>
    public HttpApiClient(string baseAddress, TimeSpan timeout) {
      Assertion.AssertObject(baseAddress, nameof(baseAddress));
      Assertion.AssertObject(timeout, nameof(timeout));

      try {
        baseAddress = baseAddress.EndsWith("/") ? baseAddress : baseAddress + "/";

        httpClient.BaseAddress = new Uri(baseAddress);
        httpClient.Timeout = timeout;

        this.LoadDefaultHeaders();

      } catch (Exception e) {
        throw new WebApiClientException(WebApiClientException.Msg.UriParsingIssue, e, baseAddress);
      }
    }


    #endregion Constructors and parsers

    #region Public methods

    public bool IncludeAuthorizationHeader {
      get;
      set;
    } = true;



    public void SetTimeout(TimeSpan timeSpan) {
      Assertion.AssertObject(timeSpan, nameof(timeSpan));

      httpClient.Timeout = timeSpan;
    }


    public async Task<T> DeleteAsync<T>(string path, params object[] pars) {
      var response = await this.SendRequestAsync(HttpMethod.Delete,
                                                 String.Empty, path, pars)
                                .ConfigureAwait(false);

      return await this.ConvertHttpContentAsync<T>(response, path)
                       .ConfigureAwait(false);
    }


    public Task DeleteAsync(string path, params object[] pars) {
      return this.SendRequestAsync(HttpMethod.Delete, String.Empty,
                                   path, pars);
    }


    public async Task<T> GetAsync<T>(string path, params object[] pars) {
      var response = await this.SendRequestAsync(HttpMethod.Get, String.Empty, path, pars)
                               .ConfigureAwait(false);

      return await this.ConvertHttpContentAsync<T>(response, path)
                       .ConfigureAwait(false);
    }


    /// <summary>Sends a json POST request as an asynchronous operation without body.</summary>
    public async Task<T> PostAsync<T>(string path, params object[] pars) {
      var response = await this.SendRequestAsync(HttpMethod.Post, String.Empty, path, pars)
                                .ConfigureAwait(false);

      return await this.ConvertHttpContentAsync<T>(response, path)
                       .ConfigureAwait(false);
    }


    /// <summary>Sends a json POST request as an asynchronous operation
    /// returning a response of type T.</summary>
    public async Task<T> PostAsync<T>(object body, string path, params object[] pars) {
      var response = await this.SendRequestAsync(HttpMethod.Post, body, path, pars)
                               .ConfigureAwait(false);

      return await this.ConvertHttpContentAsync<T>(response, path)
                       .ConfigureAwait(false);
    }


    /// <summary>Sends a json PUT request as an asynchronous operation
    /// returning a response of type T.</summary>
    public async Task<T> PutAsync<T>(object body, string path, params object[] pars) {
      var response = await this.SendRequestAsync(HttpMethod.Put, body, path, pars)
                               .ConfigureAwait(false);

      return await this.ConvertHttpContentAsync<T>(response, path)
                       .ConfigureAwait(false);
    }

    #endregion Public methods

    #region Private methods

    private void CleanRequestHeaders() {
      this.RemoveAuthorizationHeader();
    }


    private async Task<T> ConvertHttpContentAsync<T>(HttpResponseMessage response, string path) {
      string scope = UtilityMethods.GetDataScopeFromPath(path);

      if (typeof(T) != typeof(HttpResponseMessage)) {
        if (!response.IsSuccessStatusCode) {
          var content = await response.Content.ReadAsStringAsync()
                                              .ConfigureAwait(false);

          throw new WebApiClientException(response, WebApiClientException.Msg.HttpNoSuccessStatusCode,
                                          response.StatusCode,
                                          $"{this.httpClient.BaseAddress}/{path}",
                                          content);
        }
      }

      if (scope.Length != 0) {
        var content = await response.Content.ReadAsStringAsync()
                                            .ConfigureAwait(false);

        return JsonObject.Parse(content).Get<T>(scope);

      } else if (typeof(T) == typeof(HttpResponseMessage)) {
        return (T) (object) response;

      } else if (typeof(T) == typeof(string)) {
        var content = await response.Content.ReadAsStringAsync()
                                            .ConfigureAwait(false);

        return (T) (object) content;

      } else if (typeof(T) == typeof(JsonObject)) {
        var content = await response.Content.ReadAsStringAsync()
                                            .ConfigureAwait(false);

        if (scope.Length != 0) {
          return (T) (object) JsonObject.Parse(content).Slice(scope);
        } else {
          return (T) (object) JsonObject.Parse(content);
        }

      } else {
        return await response.Content.ReadAsAsync<T>()
                             .ConfigureAwait(false);

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

      if (httpClient.DefaultRequestHeaders.Contains("ApplicationKey")) {
        httpClient.DefaultRequestHeaders.Remove("ApplicationKey");
      }
    }


    private Task<HttpResponseMessage> SendRequestAsync(HttpMethod method,  object body,
                                                       string path, object[] pars) {
      Assertion.AssertObject(path, "path");

      string fullPath = EmpiriaString.Format(path, pars);
      fullPath = UtilityMethods.RemoveDataScopeFromPath(fullPath);

      this.SetRequestHeaders();
      Task<HttpResponseMessage> response = this.InvokeMethodAsync(method, fullPath, body);

      this.CleanRequestHeaders();

      return response;
    }


    private void SetAuthorizationHeader() {
      this.RemoveAuthorizationHeader();

      if (ExecutionServer.IsAuthenticated) {
        httpClient.DefaultRequestHeaders.Add("Authorization",
                                             "bearer " + ExecutionServer.CurrentPrincipal.Session.Token);
      } else {
        httpClient.DefaultRequestHeaders.Add("ApplicationKey", ExecutionServer.ApplicationKey);
      }
    }


    private void SetRequestHeaders() {
      if (this.IncludeAuthorizationHeader) {
        this.SetAuthorizationHeader();
      } else {
        this.RemoveAuthorizationHeader();
      }
    }

    #endregion Private methods

  }  // class WebApiClient

}  // namespace Empiria.WebApi.Client
