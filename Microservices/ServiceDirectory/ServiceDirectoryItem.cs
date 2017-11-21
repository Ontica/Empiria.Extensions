/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Microservices             *
*  Namespace : Empiria.Microservices                            Assembly : Empiria.Microservices.dll         *
*  Type      : ServiceDirectoryItem                           Pattern  : General Object                      *
*  Version   : 1.0                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Describes a service endpoint used to invoke the web API from a client app.                    *
*                                                                                                            *
********************************* Copyright (c) 2016-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;
using Empiria.Security;

namespace Empiria.Microservices {

  /// <summary>Describes a service endpoint used to invoke the web API from a client app.</summary>
  internal class ServiceDirectoryItem : GeneralObject {

    #region Constructors and parsers

    private ServiceDirectoryItem() {
      // Required by Empiria Framework.
    }

    /// <summary>Parses an Http endpoint given its numerical id.</summary>
    static public ServiceDirectoryItem Parse(int id) {
      return BaseObject.ParseId<ServiceDirectoryItem>(id);
    }

    /// <summary>Gets the full list of available Http API endpoints.</summary>
    static public FixedList<ServiceDirectoryItem> GetList(ClientApplication clientApplication) {
      var list = BaseObject.GetList<ServiceDirectoryItem>();

      list = ServiceDirectoryItem.GetFilteredListForClientApplication(list, clientApplication);

      return list.ToFixedList();
    }

    private static List<ServiceDirectoryItem> GetFilteredListForClientApplication(List<ServiceDirectoryItem> list,
                                                                                  ClientApplication clientApplication) {
      var defaultWebApiAddress = clientApplication.WebApiAddresses.Find((x) => x.Name == "Default");

      Assertion.AssertObject(defaultWebApiAddress.Name,
                             "ClientApplication doesn't have a default web api server address.");

      foreach (var item in list) {
        var itemWebApiName = item.ApiName;

        if (itemWebApiName == "*") {
          item.BaseAddress = defaultWebApiAddress.Value;

        } else {
          var apiAddress = clientApplication.WebApiAddresses.Find((x) => x.Name == itemWebApiName);

          if (apiAddress.Name != null) {
            item.BaseAddress = apiAddress.Value;
          } else {
            item.BaseAddress = defaultWebApiAddress.Value;
          }

        }
      }
      return list;
    }

    #endregion Constructors and parsers

    #region Properties

    /// <summary>Unique ID string for the Http Endpoint.</summary>
    public string UID {
      get {
        return base.NamedKey;
      }
    }

    /// <summary>The base address of the http endpoint.</summary>
    public string BaseAddress {
      get;
      private set;
    }

    /// <summary>The Http relative endpoint that provides the service.</summary>
    [DataField(ExtensionDataFieldName + ".path")]
    public string Path {
      get;
      private set;
    }

    /// <summary>Array with the path parameters.</summary>
    //[DataField(ExtensionDataFieldName + ".parameters", Default = "HttpEndpoint.DefaultHeaders")]
    public string[] Parameters {
      get;
      private set;
    } = new string[0];


    /// <summary>HTTP method used to invoke the call.
    /// Posible return values are GET, POST, PUT, PATCH or DELETE.</summary>
    [DataField(ExtensionDataFieldName + ".method", Default = "GET")]
    public string Method {
      get;
      private set;
    }


    /// <summary>Description of the service provided by the endpoint.</summary>
    public string Description {
      get {
        return base.Name;
      }
    }


    /// <summary>Indicates if the service must be requested with a valid Authorization header.</summary>
    [DataField(ExtensionDataFieldName + ".isProtected", Default = true)]
    public bool IsProtected {
      get;
      private set;
    }


    /// <summary>Array with any additional request headers.</summary>
    //[DataField(ExtensionDataFieldName + ".headers", Default = "HttpEndpoint.DefaultHeaders")]
    public string[] Headers {
      get;
      private set;
    } = new string[0];


    /// <summary>The response's payload type.</summary>
    [DataField(ExtensionDataFieldName + ".payloadType")]
    public string PayloadType {
      get;
      private set;
    }


    /// <summary>The response's payload data file name.</summary>
    [DataField(ExtensionDataFieldName + ".payloadDataField", Default = "data")]
    public string PayloadDataField {
      get;
      private set;
    }


    /// <summary>Array with any additional request headers.</summary>
    [DataField(ExtensionDataFieldName + ".responseDataType")]
    public string ResponseDataType {
      get;
      private set;
    }


    /// <summary>The API unique identificator</summary>
    [DataField(ExtensionDataFieldName + ".api")]
    private string ApiName {
      get;
      set;
    }

    #endregion Properties

  } // class ServiceDirectoryItem

} // namespace Empiria.Microservices
