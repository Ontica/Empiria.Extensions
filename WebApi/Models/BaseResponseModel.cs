/* Empiria Service-Oriented Architecture Framework ***********************************************************
*                                                                                                            *
*  Solution  : Empiria SOA Framework                            System   : Empiria Web Api Framework         *
*  Namespace : Empiria.WebApi.Models                            Assembly : Empiria.WebApi.dll                *
*  Type      : BaseResponseModel                                Pattern  : Web Api Response Model            *
*  Version   : 1.0        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Abstract class that handles a uniform and consistent web api response. Special responses      *
*              should be implemented through derived types of this generic type.                             *
*                                                                                                            *
********************************* Copyright (c) 2014-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections;
using System.Data;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Empiria.WebApi.Models {

  public enum ResponseStatus {
    Ok,
    Ok_No_Data,
    Denied,
    Over_Limit,
    Unavailable,
    Invalid_Request,
    Error,
  }

  /// <summary>Abstract class that handles a uniform and consistent web api response. Special responses
  /// should be implemented through derived types of this generic type.</summary>
  /// <typeparam name="T">The type of the data contained in the response.</typeparam>
  [DataContract]
  public abstract class BaseResponseModel<T> : IBaseResponseModel {

    #region Constructors and parsers

    protected BaseResponseModel(HttpRequestMessage request, T data, string typeName = "") {
      Assertion.AssertObject(request, "request");
      Assertion.AssertObject(data, "data");
      Assertion.Assert(typeName != null, "typeName can't be null");

      this.Request = request;
      this.TypeName = this.GetTypeName(data, typeName);
      this.Data = data;
      this.DataItemsCount = this.GetReturnedItems(data);
      this.Status = this.GetStatus(this.DataItemsCount);
    }

    protected BaseResponseModel(HttpRequestMessage request, ResponseStatus status,
                                T data, string typeName = "") {
      Assertion.AssertObject(request, "request");
      Assertion.AssertObject(data, "data");
      Assertion.Assert(typeName != null, "typeName can't be null");

      this.Request = request;
      this.Status = status.ToString().ToLowerInvariant();
      this.TypeName = typeof(T).FullName;
      this.Data = data;
      this.DataItemsCount = this.GetReturnedItems(data);
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

    private int GetReturnedItems(T data) {
      if (data is ICollection) {
        return ((ICollection) this.Data).Count;
      } else {
        return 1;
      }
    }

    private string GetTypeName(object data, string typeName) {
      if (typeName.Length != 0) {
        return typeName;
      }
      if (data is DataView) {
        string tableName = ((DataView) data).Table.TableName;

        return tableName;
      }
      return data.GetType().FullName;
    }

    #endregion Constructors and parsers

    #region Public properties

    [DataMember(Name = "status", Order=0)]
    public string Status {
      get;
      private set;
    }

    [DataMember(Name = "type", Order = 1)]
    public string TypeName {
      get;
      private set;
    }

    [DataMember(Name = "version", Order = 2)]
    public virtual string Version {
      get {
        return "1.0";
      }
    }

    [DataMember(Name = "dataItems", Order = 3)]
    public virtual int DataItemsCount {
      get;
      private set;
    }

    public HttpRequestMessage Request {
      get;
      private set;
    }

    [DataMember(Name = "requestId", Order = 4)]
    public Guid RequestId {
      get {
        return WebApiRequest.Current.Guid;
      }
    }

    [DataMember(Name = "links", Order = 5)]
    public abstract LinksCollectionModel Links {
      get;
    }

    [DataMember(Name = "data", Order = 100)]
    public T Data {
      get;
      private set;
    }

    #endregion Public properties

  }  // class BaseResponseModel

} // namespace Empiria.WebApi.Models
