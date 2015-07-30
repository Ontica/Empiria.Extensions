/* Empiria Service-Oriented Architecture Framework ***********************************************************
*                                                                                                            *
*  Solution  : Empiria SOA Framework                            System   : Empiria Web Api Framework         *
*  Namespace : Empiria.WebApi.Models                            Assembly : Empiria.WebApi.dll                *
*  Type      : QueryModel                                       Pattern  : Information Holder                *
*  Version   : 1.0        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Holds the configuration data of a query following the OData standard.                         *
*                                                                                                            *
********************************* Copyright (c) 2014-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Empiria.WebApi.Models {

  /// <summary>Holds the configuration data of a query following the OData standard.</summary>
  [DataContract]
  public class QueryModel {

    private HttpRequestMessage request;

    public QueryModel(HttpRequestMessage request) {
      this.request = request;
    }

    #region Public properties

    [DataMember(Name = "filter", EmitDefaultValue=false)]
    [DefaultValue("")]
    public string Filter {
      get {
        return this.GetQueryStringValue<string>("$filter", String.Empty);
      }
    }

    [DataMember(Name = "orderBy", EmitDefaultValue = false)]
    [DefaultValue("")]
    public string OrderBy {
      get {
        return this.GetQueryStringValue<string>("$orderBy", String.Empty);
      }
    }

    [DataMember(Name = "select", EmitDefaultValue = false)]
    [DefaultValue("")]
    public string Select {
      get {
        return this.GetQueryStringValue<string>("$select", String.Empty);
      }
    }

    #endregion Public properties

    #region Public methods

    protected T GetQueryStringValue<T>(string fieldName, T defaultValue) {
      string value = request.RequestUri.ParseQueryString().Get(fieldName);
      value = EmpiriaString.TrimAll(value);
      if (String.IsNullOrWhiteSpace(value)) {
        return defaultValue;
      } else {
        return (T) Convert.ChangeType(value, typeof(T));
      }
    }

    #endregion Public methods

  }  // class QueryModel

} // namespace Empiria.WebApi.Models
