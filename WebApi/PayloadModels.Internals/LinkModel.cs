/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Information Holder                    *
*  Type     : LinkModel                                    License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Represents a web link. A link is a typed connection between two resources that are identified  *
*             by Internationalised Resource Identifiers (IRIs) according to the RFC 5988.                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Runtime.Serialization;

namespace Empiria.WebApi.Internals {

  /// <summary>Represents a web link. A link is a typed connection between two resources that are identified
  /// by Internationalised Resource Identifiers (IRIs) according to the RFC 5988.</summary>
  [DataContract]
  public class LinkModel {

    #region Constructors and parsers

    internal LinkModel(string url, LinkRelation relation) :
      this(url, relation.ToString().ToLowerInvariant(), GetLinkRelationMethod(relation)) {
    }


    internal LinkModel(string url, string relation, string method = "GET") {
      Assertion.AssertObject(url, "url");
      Assertion.AssertObject(relation, "relation");
      Assertion.AssertObject(method, "method");

      this.Url = url;
      this.Relation = relation;
      this.Method = method.ToUpperInvariant();
    }

    #endregion Constructors and parsers

    #region Private properties

    /// <summary>Url of the related link used for subsequent calls.</summary>
   [DataMember(Name = "href")]
    public string Url {
      get;
      private set;
    }


   /// <summary>Link relation that describes how this link relates to the previous call.</summary>
    [DataMember(Name = "rel")]
    public string Relation {
      get;
      private set;
    }


    /// <summary>Http method required for the related call.</summary>
    [DataMember(Name = "method")]
    public string Method {
      get;
      private set;
    }

    #endregion Private properties

    #region Private methods

    static private string GetLinkRelationMethod(LinkRelation relation) {
      switch (relation) {
        case LinkRelation.Append:
          return "POST";
        case LinkRelation.Edit:
          return "PUT";
        case LinkRelation.Delete:
          return "DELETE";
        default:
          return "GET";
      }
    }

    #endregion Private methods

  }  // class LinkModel

}  // namespace Empiria.WebApi.Internals
