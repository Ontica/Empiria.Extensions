/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Client                             Component : Services Layer                          *
*  Assembly : Empiria.WebApi.Client.dll                  Pattern   : Service provider                        *
*  Type     : WebApiClient                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides methods to inkove web services using the Empiria Web API infrastructure.              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Threading.Tasks;

using Empiria.Collections;
using Empiria.Security;

namespace Empiria.WebApi.Client {

  /// <summary>Provides methods to inkove web services using the Empiria Web API infrastructure.</summary>
  public class WebApiClient : IWebApiClient {

    #region Fields

    private static EmpiriaDictionary<string, WebApiClient> _instances =
                                                          new EmpiriaDictionary<string, WebApiClient>(8);

    private static readonly object _locker = new object();

    private readonly WebApiServer _webApiServer;
    private readonly HttpApiClient _handler;

    #endregion Fields


    #region Constructors and parsers

    private WebApiClient(string webApiServerName) {
      _webApiServer = WebApiServer.Parse(webApiServerName);
      _handler = new HttpApiClient(_webApiServer.BaseAddress);

      _handler.AddHeader("User-Agent", ExecutionServer.SystemName);

      Authenticate();
    }


    static public WebApiClient GetInstance(string webApiServerName) {
      Assertion.Require(webApiServerName, nameof(webApiServerName));

      if (_instances.ContainsKey(webApiServerName)) {
        return _instances[webApiServerName];
      }

      lock (_locker) {
        if (_instances.ContainsKey(webApiServerName)) {
          return _instances[webApiServerName];
        }
        var webApiClient = new WebApiClient(webApiServerName);

        _instances.Insert(webApiServerName, webApiClient);

        return webApiClient;
      }  // lock
    }

    #endregion Constructors and parsers

    #region Methods

    public async Task DeleteAsync(string path, params object[] pars) {
      try {

        await _handler.DeleteAsync(path, pars);

      } catch (WebApiClientException e) {
        if (e.IsUnauthorized) {
          Reauthenticate();
        }
        await _handler.DeleteAsync(path, pars);
      }
    }


    public async Task<T> DeleteAsync<T>(string path, params object[] pars) {
      try {

        return await _handler.DeleteAsync<T>(path, pars);

      } catch (WebApiClientException e) {

        if (e.IsUnauthorized) {
          Reauthenticate();
        }

        return await _handler.DeleteAsync<T>(path, pars);
      }
    }


    public async Task<T> GetAsync<T>(string path, params object[] pars) {
      try {

        return await _handler.GetAsync<T>(path, pars);

      } catch (WebApiClientException e) {

        if (e.IsUnauthorized) {
          Reauthenticate();
        }

        return await _handler.GetAsync<T>(path, pars);
      }
    }


    public async Task<T> PostAsync<T>(string path, params object[] pars) {
      try {

        return await _handler.PostAsync<T>(path, pars);

      } catch (WebApiClientException e) {

        if (e.IsUnauthorized) {
          Reauthenticate();
        }

        return await _handler.PostAsync<T>(path, pars);
      }
    }


    public async Task<T> PostAsync<T>(object body, string path, params object[] pars) {
      try {

        return await _handler.PostAsync<T>(body, path, pars);

      } catch (WebApiClientException e) {

        if (e.IsUnauthorized) {
          Reauthenticate();
        }

        return await _handler.PostAsync<T>(body, path, pars);
      }
    }


    public async Task<T> PutAsync<T>(object body, string path, params object[] pars) {
      try {

        return await _handler.PutAsync<T>(body, path, pars);

      } catch (WebApiClientException e) {

        if (e.IsUnauthorized) {
          Reauthenticate();
        }

        return await _handler.PutAsync<T>(body, path, pars);
      }
    }


    public void SetTimeout(TimeSpan timeSpan) {
      _handler.SetTimeout(timeSpan);
    }

    #endregion Methods

    #region Helpers

    private void Authenticate() {
      _handler.RemoveHeader("Authorization");
      _handler.AddHeader("ApplicationKey", _webApiServer.Credentials.AppKey);

      var credentials = new {
        userID = _webApiServer.Credentials.UserID,
      };

      string loginToken = _handler.PostAsync<string>(credentials, "v3/security/login-token::data").Result;

      var credentials2 = new {
        userID = _webApiServer.Credentials.UserID,
        password = EncryptUserPassword(_webApiServer.Credentials.Password, loginToken)
      };

      string accessToken = _handler.PostAsync<string>(credentials2, "v3/security/login::data/access_token").Result;

      _handler.AddHeader("Authorization", $"bearer {accessToken}");
      _handler.RemoveHeader("ApplicationKey");
    }


    private string EncryptUserPassword(string password, string loginToken) {
      string encryptedPassword = Cryptographer.GetSHA256(password);

      return Cryptographer.GetSHA256($"{encryptedPassword}{loginToken}");
    }


    private void Reauthenticate() {
      _handler.RemoveHeader("Authorization");

      lock (_locker) {
        if (_handler.ContainsHeader("Authorization")) {
          return;
        }
        Authenticate();
      }
    }

    #endregion Helpers

  }  // class WebApiClient

}  // namespace Empiria.WebApi.Client
