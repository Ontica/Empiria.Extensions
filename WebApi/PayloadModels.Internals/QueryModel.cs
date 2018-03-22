/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Information Holder                    *
*  Type     : QueryModel                                   License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Holds configuration data of a query following the OData standard.                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Empiria.WebApi.Internals {

  /// <summary>Holds configuration data of a query following the OData standard.</summary>
  [DataContract]
  public class QueryModel {

    private HttpRequestMessage request;

    internal QueryModel(HttpRequestMessage request) {
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

} // namespace Empiria.WebApi.Internals
