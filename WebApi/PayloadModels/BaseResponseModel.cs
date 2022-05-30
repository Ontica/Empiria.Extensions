/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Core Services                        Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Base Response Model                   *
*  Type     : BaseResponseModel<T>                         License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Abstract class that handles a uniform and consistent web api response. Special responses       *
*             should be implemented through derived types of this generic type.                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Collections;
using System.Data;
using System.Net.Http;
using System.Runtime.Serialization;

using Empiria.WebApi.Internals;

namespace Empiria.WebApi {

  /// <summary>Abstract class that handles a uniform and consistent web api response. Special responses
  /// should be implemented through derived types of this generic type.</summary>
  /// <typeparam name="T">The type of the data contained in the response.</typeparam>
  [DataContract]
  public abstract class BaseResponseModel<T> : IBaseResponseModel {

    #region Constructors and parsers

    protected BaseResponseModel(HttpRequestMessage request, T data) {
      this.Initialize(request, data, typeof(T).FullName);
    }


    protected BaseResponseModel(HttpRequestMessage request, T data, string typeName) {
      this.Initialize(request, data, typeName);
    }


    protected BaseResponseModel(HttpRequestMessage request, ResponseStatus status, T data) {
      this.Initialize(request, data, typeof(T).FullName);
      this.Status = status.ToString().ToLowerInvariant();
    }


    protected BaseResponseModel(HttpRequestMessage request, ResponseStatus status,
                                T data, string typeName) {
      this.Initialize(request, data, typeName);
      this.Status = status.ToString().ToLowerInvariant();
    }


    #endregion Constructors and parsers

    #region Public properties

    [DataMember(Name = "status", Order = 0)]
    public string Status {
      get;
      private set;
    }


    [DataMember(Name = "dataType", Order = 1)]
    public string TypeName {
      get;
      private set;
    }


    [DataMember(Name = "payloadType", Order = 2)]
    public string PayloadType {
      get {
        return this.GetType().Name;
      }
    }


    [DataMember(Name = "version", Order = 3)]
    public virtual string Version {
      get {
        return "1.0";
      }
    }


    [DataMember(Name = "dataItems", Order = 4)]
    public virtual int DataItemsCount {
      get;
      private set;
    }


    public HttpRequestMessage Request {
      get;
      private set;
    }


    [DataMember(Name = "requestId", Order = 5)]
    public Guid RequestId {
      get {
        return WebApiRequest.Current.Guid;
      }
    }


    [DataMember(Name = "links", Order = 6)]
    public abstract LinksCollectionModel Links {
      get;
    }


    [DataMember(Name = "data", Order = 100)]
    public T Data {
      get;
      private set;
    }

    #endregion Public properties

    #region Methods

    private int GetReturnedItems(T data) {
      if (data is ICollection) {
        return ((ICollection) this.Data).Count;
      } else {
        return 1;
      }
    }


    private string GetStatus(int dataItemsCount) {
      ResponseStatus status;

      if (dataItemsCount > 0) {
        status = ResponseStatus.Ok;
      } else {
        status = ResponseStatus.Ok_No_Data;
      }
      return status.ToString().ToLowerInvariant();
    }


    private string GetTypeName(object data, string typeName) {
      string temp = String.Empty;

      if (typeName.Length != 0) {
        temp = typeName;

      } else if (data is IIdentifiable) {
        temp = data.GetType().FullName;

      } else if (data is DataView) {
        temp = ((DataView) data).Table.TableName;

      } else if (data.GetType().IsGenericType &&
                 data.GetType().GetGenericTypeDefinition() == typeof(FixedList<>)) {
        temp = data.GetType().GenericTypeArguments[0].FullName;

      } else {
        temp = data.GetType().FullName;

      }

      if (ExecutionServer.IsSpecialLicense) {
        temp = temp.Replace("Empiria", ExecutionServer.LicenseName);
      }

      return temp;
    }


    private void Initialize(HttpRequestMessage request, T data, string typeName) {
      Assertion.Require(request, "request");
      Assertion.Require(data, "data");
      Assertion.Require(typeName != null, "typeName can't be null");

      this.Request = request;
      this.TypeName = this.GetTypeName(data, typeName);
      this.RefreshData(data);
    }


    internal void RefreshData(T newData) {
      Assertion.Require(newData, "newData");

      this.Data = newData;
      this.DataItemsCount = this.GetReturnedItems(newData);
      this.Status = this.GetStatus(this.DataItemsCount);
    }

    #endregion Methods

  }  // class BaseResponseModel

} // namespace Empiria.WebApi
