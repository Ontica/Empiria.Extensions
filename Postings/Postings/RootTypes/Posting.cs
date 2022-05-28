/* Empiria Postings ******************************************************************************************
*                                                                                                            *
*  Module   : Empiria Postings                             Component : Domain services                       *
*  Assembly : Empiria.Postings.dll                         Pattern   : Information Holder                    *
*  Type     : Posting                                      License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Holds information about a posting, a qualified pair tuple of related base objects.             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Contacts;
using Empiria.Json;
using Empiria.Security;
using Empiria.StateEnums;

namespace Empiria.Postings {

  /// <summary>Holds information about a posting, a qualified pair tuple of related base objects.</summary>
  public class Posting : BaseObject {

    #region Constructors and parsers

    private Posting() {
      // Required by Empiria Framework.
    }


    public Posting(string postingType, BaseObject nodeObject, BaseObject postedItem) {
      Assertion.Require(postingType, "postingType");
      Assertion.Require(nodeObject, "nodeObject");
      Assertion.Require(postedItem, "postedItem");

      this.PostingType = postingType;
      this.NodeObjectUID = nodeObject.UID;
      this.PostedItemUID = postedItem.UID;
    }


    static public Posting Parse(int id) {
      return BaseObject.ParseId<Posting>(id);
    }


    static public Posting Parse(string uid) {
      return BaseObject.ParseKey<Posting>(uid);
    }

    #endregion Constructors and parsers


    #region Properties


    [DataField("PostingType")]
    public string PostingType {
      get;
      private set;
    }


    [DataField("NodeObjectUID")]
    public string NodeObjectUID {
      get;
      private set;
    }


    [DataField("PostedItemUID")]
    public string PostedItemUID {
      get;
      private set;
    }


    [DataField("PostingIndex")]
    public int Index {
      get;
      private set;
    } = 0;


    //[DataField("Keywords")]
    public string Keywords {
      get;
      private set;
    }


    [DataField("ExtData")]
    public JsonObject ExtensionData {
      get;
      set;
    }


    [DataField("PostingTime")]
    public DateTime PostingTime {
      get;
      private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("PostedById")]
    public Contact PostedBy {
      get;
      private set;
    }


    [DataField("PostingStatus", Default = EntityStatus.Active)]
    public EntityStatus Status {
      get;
      private set;
    }

    #endregion Properties


    #region Methods


    public void Delete() {
      this.Status = EntityStatus.Deleted;

      this.Save();
    }


    public T GetNodeObjectItem<T>() where T : BaseObject {
      return BaseObject.ParseKey<T>(this.NodeObjectUID);
    }


    public T GetPostedItem<T>() where T : BaseObject {
      return BaseObject.ParseKey<T>(this.PostedItemUID);
    }


    protected override void OnSave() {
      this.PostingTime = DateTime.Now;
      this.PostedBy = EmpiriaUser.Current.AsContact();

      PostingsData.WritePosting(this);
    }


    #endregion Methods

  } // class Posting

}  // namespace Empiria.Postings
