/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Empiria Web API Services            *
*  Namespace : Empiria.WebApi.Client                          Assembly : Empiria.WebApi.Client.dll           *
*  Type      : ServiceHandler                                 Pattern  : Information holder                  *
*  Version   : 1.2                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Describes a web service and provides a handler to invoke it.                                  *
*                                                                                                            *
********************************* Copyright (c) 2016-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
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

    internal ServiceHandler() {

    }

    #endregion Constructors and parsers

    #region Properties

    /// <summary>Unique ID string for the service descriptor.</summary>
    public string UID {
      get;
      set;
    } = String.Empty;


    /// <summary>The base address of the http endpoint.</summary>
    public string BaseAddress {
      get;
      set;
    } = String.Empty;


    public string Path {
      get;
      set;
    } = String.Empty;


    public string[] Parameters {
      get;
      set;
    } = new string[0];


    public string Method {
      get;
      set;
    } = "GET";


    public string Description {
      get;
      set;
    } = String.Empty;


    public bool IsProtected {
      get;
      set;
    } = true;


    public string[] Headers {
      get;
      set;
    } = new string[0];


    public string PayloadDataField {
      get;
      set;
    } = "data";


    public string PayloadType {
      get;
      set;
    } = String.Empty;


    public string ResponseDataType {
      get;
      set;
    } = String.Empty;


    #endregion Properties

    #region Methods

    internal HttpApiClient GetHandler() {
      string serverUID = this.BaseAddress;

      if (!cache.ContainsKey(serverUID)) {
        lock (locker) {
          if (!cache.ContainsKey(serverUID)) {
            var handler = new HttpApiClient(this.BaseAddress);

            cache.Add(serverUID, handler);
          }
        }
      }
      return this.PrepareHandler(cache[serverUID]);
    }


    internal HttpApiClient PrepareHandler(HttpApiClient handler) {
      Assertion.AssertObject(handler, "handler");

      handler.IncludeAuthorizationHeader = this.IsProtected;

      return handler;
    }

    #endregion Methods

  } // class ServiceHandler

} // namespace Empiria.WebApi.Client
