/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Empiria Web API Services            *
*  Namespace : Empiria.WebApi.Client                          Assembly : Empiria.WebApi.Client.dll           *
*  Type      : WebApiServer                                   Pattern  : Information holder                  *
*  Version   : 1.1                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Describes a server that provides multiple web services and a handler to invoke them.          *
*                                                                                                            *
********************************* Copyright (c) 2004-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;

namespace Empiria.WebApi.Client {

  /// <summary>Describes a server that provides multiple web services and a handler to invoke them.</summary>
  internal class WebApiServer {

    #region Fields

    private static Dictionary<string, WebApiServer> cache = new Dictionary<string, WebApiServer>();

    private static readonly string DEFAULT_SERVER_UID =
                                        ConfigurationData.GetString("Default.WebApiServer");

    #endregion Fields

    #region Constructors and parsers

    private WebApiServer(string serverUID) {
      var configData = GetConfigurationData(serverUID);

      this.UID = serverUID;
      this.BaseAddress = configData;
    }

    private string GetConfigurationData(string serverUID) {
      return ConfigurationData.GetString("WebApiServer." + serverUID);
    }

    static public WebApiServer Parse(string serverUID) {
      Assertion.AssertObject(serverUID, "serverUID");

      serverUID = EmpiriaString.IsInList(serverUID, "*", "Default") ? DEFAULT_SERVER_UID : serverUID;

      if (!cache.ContainsKey(serverUID)) {
        WebApiServer apiServer = new WebApiServer(serverUID);
        cache.Add(serverUID, apiServer);
      }
      return cache[serverUID];
    }

    static public WebApiServer Default {
      get {
        return WebApiServer.Parse(DEFAULT_SERVER_UID);
      }
    }

    #endregion Constructors and parsers

    #region Properties

    public string UID {
      get;
    } = String.Empty;


    public string BaseAddress {
      get;
    } = String.Empty;


    public string[] AlternativeAddresses {
      get;
    } = new string[0];


    #endregion Properties

    #region Methods

    private HttpApiClient _handler = null;
    internal HttpApiClient GetHandler() {
      if (_handler == null) {
        _handler = new HttpApiClient(this.BaseAddress);
      }
      return _handler;
    }

    #endregion Methods

  } // class WebApiServer

} // namespace Empiria.WebApi.Client
