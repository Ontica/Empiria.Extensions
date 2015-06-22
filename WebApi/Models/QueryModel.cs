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

    static internal QueryModel Parse(HttpRequestMessage request) {
      Assertion.AssertObject(request, "request");

      return new QueryModel(request);
    }

    #region Public properties

    [DataMember(Name = "skip")]
    public int Skip {
      get {
        return 0;
      }
    }

    [DataMember(Name = "top")]
    public int Top {
      get {
        return 100;
      }
    }

    [DataMember(Name = "filter", EmitDefaultValue=false)]
    [DefaultValue("")]
    public string Filter {
      get {
        return "the-odata-filter";
      }
    }

    [DataMember(Name = "format", EmitDefaultValue = false)]
    [DefaultValue("")]
    public string Format {
      get {
        return "the-odata-format";
      }
    }

    [DataMember(Name = "orderBy", EmitDefaultValue = false)]
    [DefaultValue("")]
    public string OrderBy {
      get {
        return "the-odata-orderBy";
      }
    }

    [DataMember(Name = "select", EmitDefaultValue = false)]
    [DefaultValue("")]
    public string Select {
      get {
        return "the-odata-select";
      }
    }

    #endregion Public properties

  }  // class QueryModel

} // namespace Empiria.WebApi.Models
