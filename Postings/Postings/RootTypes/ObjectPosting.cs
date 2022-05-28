/* Empiria Postings ******************************************************************************************
*                                                                                                            *
*  Module   : Empiria Postings                             Component : Domain services                       *
*  Assembly : Empiria.Postings.dll                         Pattern   : Domain class                          *
*  Type     : ObjectPosting                                License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Handles information about a posting attached to an object.                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Contacts;
using Empiria.Json;
using Empiria.Security;
using Empiria.StateEnums;

namespace Empiria.Postings {

  /// <summary>Handles information about a posting attached to an object.</summary>
  public class ObjectPosting : BaseObject {

    private readonly string libraryBaseAddress =
                              ConfigurationData.GetString("Empiria.Governance", "DocumentsLibrary.BaseAddress");

    #region Constructors and parsers

    protected ObjectPosting() {
      // Required by Empiria Framework.
    }


    public ObjectPosting(string objectUID, JsonObject data) {
      Assertion.Require(objectUID, "objectUID");
      this.AssertIsValid(data);

      this.ObjectUID = objectUID;

      this.Load(data);
    }


    static internal ObjectPosting Parse(int id) {
      return BaseObject.ParseId<ObjectPosting>(id);
    }


    static public ObjectPosting Parse(string uid) {
      return BaseObject.ParseKey<ObjectPosting>(uid);
    }


    static public FixedList<ObjectPosting> GetList(string objectUID, string keywords = "") {
      Assertion.Require(objectUID, "objectUID");

      return PostingsData.GetObjectPostingsList(objectUID, keywords);
    }

    static public void UpdateAll() {
      var postings = BaseObject.GetList<ObjectPosting>();

      foreach (var posting in postings) {
        posting.Save();
      }
    }


    #endregion Constructors and parsers

    #region Public properties


    [DataField("ObjectUID")]
    public string ObjectUID {
      get;
      private set;
    } = String.Empty;


    internal JsonObject ExtensionData {
      get;
      private set;
    } = new JsonObject();


    [DataField("ControlNo")]
    public string Authors {
      get;
      private set;
    } = String.Empty;


    [DataField("Title")]
    public string Title {
      get;
      private set;
    } = String.Empty;


    [DataField("Body")]
    public string Body {
      get;
      private set;
    } = String.Empty;


    [DataField("Tags")]
    public string Tags {
      get;
      private set;
    } = String.Empty;


    [DataField("FileUrl")]
    public string FilePath {
      get;
      private set;
    } = String.Empty;


    public string FileUrl {
      get {
        if (this.FilePath.StartsWith("~/")) {
          return this.FilePath.Replace("~/", libraryBaseAddress + "/");
        } else {
          return this.FilePath;
        }
      }
    }


    [DataField("Updated")]
    public string Updated {
      get;
      private set;
    } = String.Empty;


    [DataField("OwnerId")]
    public Contact Owner {
      get;
      private set;
    }


    internal string Keywords {
      get {
        return EmpiriaString.BuildKeywords(this.Authors, this.Title, this.Tags, this.Body);
      }
    }


    [DataField("AccessMode", Default = AccessMode.Public)]
    public AccessMode AccessMode {
      get;
      private set;
    }


    [DataField("ParentId", Default = -1)]
    internal int ParentId {
      get;
      private set;
    } = -1;


    [DataField("PostingTime", Default = "DateTime.Now")]
    public DateTime Date {
      get;
      private set;
    } = DateTime.Now;


    [DataField("Status", Default = EntityStatus.Active)]
    public EntityStatus Status {
      get;
      private set;
    }

    public JsonObject SendTo {
      get {
        if (this.Tags.Contains("registered")) {
          return JsonObject.Parse(this.Tags);
        } else {
          return JsonObject.Empty;
        }
      }
    }


    #endregion Public properties

    #region Public methods

    public void Delete() {
      this.Status = EntityStatus.Deleted;

      this.Save();
    }


    protected override void OnSave() {
      if (this.IsNew) {
        this.Owner = EmpiriaUser.Current.AsContact();
      }
      PostingsData.WritePosting(this);
    }


    public void Update(JsonObject data) {
      Assertion.Require(data, "data");

      this.AssertIsValid(data);

      this.Load(data);

      this.Save();
    }

    #endregion Public methods

    #region Private methods

    private void AssertIsValid(JsonObject data) {
      Assertion.Require(data, "data");

    }


    private void Load(JsonObject data) {
      this.Title = data.Get<string>("title", this.Title);
      this.Body = data.Get<string>("body", this.Body);
      this.Tags = data.Get<string>("tags", this.Tags);
      this.Authors = data.Get<string>("authors", this.Authors);
      this.FilePath = data.Get<string> ("filePath", this.FilePath);
      if (data.Contains("sendTo")) {
        this.Tags = data.Slice("sendTo").ToString();
      }
    }

    #endregion Private methods

  }  // class ObjectPosting

}  // namespace Empiria.Postings
