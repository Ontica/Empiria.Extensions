/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Client                             Component : Services Layer                          *
*  Assembly : Empiria.WebApi.Client.dll                  Pattern   : Service provider                        *
*  Type     : HttpApiClient                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides general purpose web api client methods using Http.                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Empiria.Json;

namespace Empiria.WebApi.Client {

  /// <summary>Provides general purpose web api client methods using Http.</summary>
  public class HttpApiClient {

    #region Fields

    private static readonly int DEFAULT_HTTP_CALL_TIMEOUT_SECONDS =
                                            ConfigurationData.Get<int>("HttpDefaultTimeout", 400);

    private readonly HttpClient httpClient = new HttpClient();

    #endregion Fields

    #region Constructors and parsers

    static HttpApiClient() {
      JsonConvert.DefaultSettings = () => Json.JsonConverter.JsonSerializerDefaultSettings();
    }

    /// <summary>Initializes a Web API connector to a fixed server.</summary>
    /// <param name="baseAddress">The server's base address.</param>
    public HttpApiClient(string baseAddress): this(baseAddress,
                                              TimeSpan.FromSeconds(DEFAULT_HTTP_CALL_TIMEOUT_SECONDS)) {
      // no-op
    }


    /// <summary>Initializes a Web API connector to a fixed server.</summary>
    /// <param name="baseAddress">The server's base address.</param>
    public HttpApiClient(string baseAddress, TimeSpan timeout) {
      Assertion.Require(baseAddress, nameof(baseAddress));
      Assertion.Require(timeout, nameof(timeout));

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

    #region Methods

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


    public void AddHeader(string headerName, string value) {
      Assertion.Require(headerName, nameof(headerName));
      Assertion.Require(value, nameof(value));

      if (ContainsHeader(headerName)) {
        RemoveHeader(headerName);
      }
      httpClient.DefaultRequestHeaders.Add(headerName, value);
    }


    public void Authenticate() {
      Assertion.Require(ExecutionServer.IsAuthenticated, "User is not authenticated.");

      AddHeader("Authorization", $"bearer {ExecutionServer.CurrentPrincipal.Session.Token}");
    }


    internal bool ContainsHeader(string headerName) {
      Assertion.Require(headerName, nameof(headerName));

      return httpClient.DefaultRequestHeaders.Contains(headerName);
    }


    public void RemoveHeader(string headerName) {
      Assertion.Require(headerName, nameof(headerName));

      httpClient.DefaultRequestHeaders.Remove(headerName);
    }


    public void SetTimeout(TimeSpan timeSpan) {
      Assertion.Require(timeSpan.TotalSeconds >= 5, "timeSpan too low");

      httpClient.Timeout = timeSpan;
    }

    #endregion Methods

    #region Helpers

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
        throw Assertion.EnsureNoReachThisCode($"Http method handler not defined for '{method.Method}'.");
      }
    }


    private void LoadDefaultHeaders() {
      httpClient.DefaultRequestHeaders.Clear();
      httpClient.DefaultRequestHeaders.Accept.Clear();
      httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }


    private Task<HttpResponseMessage> SendRequestAsync(HttpMethod method, object body,
                                                       string path, object[] pars) {
      Assertion.Require(path, "path");

      string fullPath = EmpiriaString.Format(path, pars);
      fullPath = UtilityMethods.RemoveDataScopeFromPath(fullPath);

      Task<HttpResponseMessage> response = this.InvokeMethodAsync(method, fullPath, body);

      return response;
    }

    #endregion Helpers

  }  // class WebApiClient

}  // namespace Empiria.WebApi.Client
