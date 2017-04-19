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

namespace Empiria.WebApi.Client {

  /// <summary>Describes a web service and provides a handler to invoke it.</summary>
  internal class ServiceHandler {

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


    /// <summary>The API which provides the service.</summary>
    public string Api {
      get;
      set;
    } = String.Empty;


    private WebApiServer _apiServer = null;
    private WebApiServer ApiServer {
      get {
        if (_apiServer == null) {
          _apiServer = WebApiServer.Parse(this.Api);
        }
        return _apiServer;
      }
    }

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
      var handler = this.ApiServer.GetHandler();

      return PrepareHandler(handler);
    }


    internal HttpApiClient PrepareHandler(HttpApiClient handler) {
      Assertion.AssertObject(handler, "handler");

      handler.IncludeAuthorizationHeader = this.IsProtected;

      return handler;
    }

    #endregion Methods

  } // class ServiceHandler

} // namespace Empiria.WebApi.Client
