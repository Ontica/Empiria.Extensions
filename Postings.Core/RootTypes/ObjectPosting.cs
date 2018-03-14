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

using Empiria.Json;
using Empiria.Contacts;
using Empiria.Security;

namespace Empiria.Postings {

  /// <summary>Handles information about a posting attached to an object.</summary>
  public class ObjectPosting : BaseObject {

    #region Constructors and parsers

    protected ObjectPosting() {
      // Required by Empiria Framework.
    }


    public ObjectPosting(string objectType, string objectUID,
                         JsonObject data) {
      Assertion.AssertObject(objectType, "objectType");
      Assertion.AssertObject(objectUID, "objectUID");      
      
      this.AssertIsValid(data);

      this.ObjectType = objectType;
      this.ObjectUID = objectUID;

      this.Load(data);
    }


    static internal ObjectPosting Parse(int id) {
      return BaseObject.ParseId<ObjectPosting>(id);
    }


    static public ObjectPosting Parse(string uid) {
      return BaseObject.ParseKey<ObjectPosting>(uid);
    }


    static public FixedList<ObjectPosting> GetList(string objectType, string objectUID) {
      Assertion.AssertObject(objectType, "objectType");
      Assertion.AssertObject(objectUID, "objectUID");

      return PostingsData.GetObjectPostingsList(objectType, objectUID);
    }


    #endregion Constructors and parsers

    #region Public properties

    [DataField("UID")]
    public string UID {
      get;
      private set;
    } = String.Empty;


    [DataField("ObjectType")]
    public string ObjectType {
      get;
      private set;
    } = String.Empty;


    [DataField("ObjectUID")]
    public string ObjectUID {
      get;
      private set;
    } = String.Empty;


    internal JsonObject ExtensionData {
      get;
      private set;
    } = new JsonObject();


    [DataField("PostingText")]
    public string Text {
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
        return EmpiriaString.BuildKeywords(this.Text);
      }
    }


    [DataField("AccessMode", Default = PostingAccessMode.Public)]
    public PostingAccessMode AccessMode {
      get;
      private set;
    }


    [DataField("ParentId", Default = -1)]
    internal int ParentId {
      get;
      private set;
    }


    [DataField("PostingDate", Default = "DateTime.Now")]
    public DateTime Date {
      get;
      private set;
    }


    [DataField("Status", Default = ObjectStatus.Active)]
    internal ObjectStatus Status {
      get;
      private set;
    }

    #endregion Public properties

    #region Public methods

    public void Delete() {
      this.Status = ObjectStatus.Deleted;

      this.Save();
    }


    protected override void OnSave() {
      if (this.UID.Length == 0) {
        this.UID = EmpiriaString.BuildRandomString(6, 24);
        this.Owner = EmpiriaUser.Current.AsContact();
      }
      PostingsData.WritePosting(this);
    }


    public void Update(JsonObject data) {
      Assertion.AssertObject(data, "data");

      this.AssertIsValid(data);

      this.Load(data);

      this.Save();
    }

    #endregion Public methods

    #region Private methods

    private void AssertIsValid(JsonObject data) {
      Assertion.AssertObject(data, "data");

    }


    private void Load(JsonObject data) {
      this.Text = data.Get<string>("text", this.Text);
    }

    #endregion Private methods

  }  // class ObjectPosting

}  // namespace Empiria.Postings
