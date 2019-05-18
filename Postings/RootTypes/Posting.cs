/* Empiria Postings ******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                             Component : Media Domain Types                    *
*  Assembly : Empiria.Postings.dll                         Pattern   : Information Holder                    *
*  Type     : MediaPosting                                 License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Abstract type that represents a media posting attached to a source object.                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Contacts;
using Empiria.Json;
using Empiria.StateEnums;

namespace Empiria.Postings {

  /// <summary>Abstract type that represents a media-based metadata posting attached to a source object.</summary>
  public class Posting : BaseObject {

    #region Constructors and parsers


    public Posting(string postingType, BaseObject nodeObject, BaseObject postedItem) {
      Assertion.AssertObject(postingType, "postingType");
      Assertion.AssertObject(nodeObject, "nodeObject");
      Assertion.AssertObject(postedItem, "postedItem");

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


    static public FixedList<T> GetList<T>(BaseObject nodeObject,
                                          string postingType) where T: BaseObject {
      Assertion.AssertObject(nodeObject, "nodeObject");
      Assertion.AssertObject(postingType, "postingType");

      return PostingsData.GetPostingsList<T>(nodeObject, postingType);
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


    [DataField("ExtData")]
    protected internal JsonObject ExtensionData {
      get;
      private set;
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
      PostingsData.WritePosting(this);
    }


    #endregion Methods

  } // class Posting

}  // namespace Empiria.Postings
