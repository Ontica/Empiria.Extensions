/* Empiria Postings ******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                             Component : Media Domain Types                    *
*  Assembly : Empiria.Postings.dll                         Pattern   : Information Holder                    *
*  Type     : Metadata                                     License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Holds media metadata information.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

namespace Empiria.Storage {

  /// <summary>Holds media metadata information.</summary>
  public class Metadata  {

    #region Constructors and parsers


    private Metadata() {
      // Required by Empiria Framework.
    }


    static public Metadata Parse(JsonObject data) {
      Assertion.AssertObject(data, "data");

      var metadata = new Metadata();

      metadata.Title = data.GetClean("title", metadata.Title);
      metadata.Type = data.GetClean("type", metadata.Type);
      metadata.Summary = data.GetClean("summary", metadata.Summary);
      metadata.Topics = data.GetClean("topics", metadata.Topics);
      metadata.Tags = data.GetClean("tags", metadata.Tags);
      metadata.Authors = data.GetClean("authors", metadata.Authors);

      return metadata;
    }


    static public Metadata Empty {
      get {
        return new Metadata();
      }
    }


    #endregion Constructors and parsers


    #region Properties


    [DataField("Title")]
    public string Title {
      get;
      private set;
    } = String.Empty;


    [DataField("Type")]
    public string Type {
      get;
      private set;
    } = String.Empty;


    [DataField("Summary")]
    public string Summary {
      get;
      private set;
    } = String.Empty;


    [DataField("Topics")]
    public string Topics {
      get;
      private set;
    } = String.Empty;


    [DataField("Tags")]
    public string Tags {
      get;
      private set;
    } = String.Empty;


    [DataField("Authors")]
    public string Authors {
      get;
      private set;
    } = String.Empty;


    public virtual string Keywords {
      get {
        return EmpiriaString.BuildKeywords(this.Title, this.Type, this.Topics,
                                           this.Tags, this.Authors, this.Summary);
      }
    }


    #endregion Properties

  } // class Metadata

}  // namespace Empiria.Storage
