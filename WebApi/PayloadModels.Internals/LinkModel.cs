/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Core Services                        Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Immutable data holder                 *
*  Type     : ExceptionModel                               License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Represents a web link. A link is a typed connection between two resources that are             *
*             identified by Internationalised Resource Identifiers (IRIs) according to the RFC 5988.         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Runtime.Serialization;

namespace Empiria.WebApi.Internals {

  /// <summary>Represents a web link. A link is a typed connection between two resources that are
  /// identified by Internationalised Resource Identifiers (IRIs) according to the RFC 5988.</summary>
  [DataContract]
  public class LinkModel {

    #region Constructors

    internal LinkModel(string url, LinkRelation relation, string method = "GET") :
      this(url, relation.ToString().ToLowerInvariant(), method) {
    }


    internal LinkModel(string url, string relation, string method = "GET") {
      Assertion.AssertObject(url, "url");
      Assertion.AssertObject(relation, "relation");
      Assertion.AssertObject(method, "method");

      this.Url = url;
      this.Relation = relation;
      this.Method = method.ToUpperInvariant();
    }

    #endregion Constructors

    #region Properties

    /// <summary>Url of the related link used for subsequent calls.</summary>
    [DataMember(Name = "href")]
    public string Url {
      get;
    }


   /// <summary>Link relation that describes how this link relates to the previous call.</summary>
    [DataMember(Name = "rel")]
    public string Relation {
      get;
    }


    /// <summary>Http method required for the related call.</summary>
    [DataMember(Name = "method")]
    public string Method {
      get;
    }

    #endregion Properties

  }  // class LinkModel

}  // namespace Empiria.WebApi.Internals
