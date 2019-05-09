/* Empiria Postings ******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                             Component : Media Domain Types                    *
*  Assembly : Empiria.Postings.dll                         Pattern   : Information Holder                    *
*  Type     : MediaObject                                  License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Represents a stored media object treated as a value type, so it must be related to             *
*             other objects like metadata information holders or document entities.                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Contacts;
using Empiria.Json;
using Empiria.Security;
using Empiria.StateEnums;

namespace Empiria.Postings.Media {

  /// <summary>Represents a stored media object treated as a value type, so it must be related to
  /// other objects like metadata information holders or document entities.</summary>
  public class MediaObject : BaseObject, IProtected {

    #region Constructors and parsers


    private MediaObject() {
      // Required by Empiria Framework
    }


    internal MediaObject(JsonObject data) {
      this.LoadData(data);
    }


    static public MediaObject Parse(string uid) {
      return BaseObject.ParseKey<MediaObject>(uid);
    }


    #endregion Constructors and parsers

    #region Properties


    [DataField("MediaType")]
    public string MediaType {
      get;
      private set;
    }


    [DataField("MediaLength")]
    public int Length {
      get;
      private set;
    }


    [DataField("OriginalFileName")]
    public string OriginalFileName {
      get;
      private set;
    }



    [DataField("StorageId", Default = "Empiria.Postings.Media.MediaStorage.Default")]
    public MediaStorage Storage {
      get;
      private set;
    }


    [DataField("FileName")]
    public string FileName {
      get;
      private set;
    }


    [DataField("MediaHashCode")]
    public string HashCode {
      get;
      private set;
    }


    [DataField("ExtData")]
    protected internal JsonObject ExtensionData {
      get;
      private set;
    }


    [DataField("PostingTime")]
    public DateTime PostingTime {
      get;
      protected set;
    } = ExecutionServer.DateMaxValue;


    [DataField("PostedById")]
    public Contact PostedBy {
      get;
      protected set;
    }


    [DataField("MediaStatus", Default = EntityStatus.Active)]
    public EntityStatus Status {
      get;
      protected set;
    }


    public string Url {
      get {
        return $"{this.Storage.Url}/{this.FileName}";
      }
    }

    #endregion Properties


    #region Integrity protection members


    int IProtected.CurrentDataIntegrityVersion {
      get {
        return 1;
      }
    }


    object[] IProtected.GetDataIntegrityFieldValues(int version) {
      if (version == 1) {
        return new object[] {
          1, "MediaId", this.Id, "MediaUID", this.UID, "MediaType", this.MediaType,
          "MediaLength", this.Length, "OriginalFileName", this.OriginalFileName,
          "StorageId", this.Storage.Id, "FileName", this.FileName,
          "MediaHashCode", this.HashCode, "ExtData", this.ExtensionData.ToString(),
          "PostingTime", this.PostingTime, "PostedById", this.PostedBy.Id,
          "MediaStatus", (char) this.Status
        };
      }
      throw new SecurityException(SecurityException.Msg.WrongDIFVersionRequested, version);
    }


    private IntegrityValidator _validator = null;
    public IntegrityValidator Integrity {
      get {
        if (_validator == null) {
          _validator = new IntegrityValidator(this);
        }
        return _validator;
      }
    }


    #endregion Integrity protection members


    #region Methods


    internal void Delete() {
      Assertion.Assert(this.Status == EntityStatus.Active,
                       "MediaObject must be in 'Active' status.");

      this.Status = EntityStatus.Deleted;

      this.Save();
    }


    private void LoadData(JsonObject data) {
      this.MediaType = data.Get<string>("mediaType");
      this.Length = data.Get<int>("length");
      this.Storage = data.Get<MediaStorage>("storageId");
      this.OriginalFileName = data.Get<string>("originalFileName");
      this.FileName = data.Get<string>("fileName");
      this.HashCode = data.Get<string>("hashCode");
    }


    protected override void OnSave() {
      if (base.IsNew) {
        this.PostedBy = EmpiriaUser.Current.AsContact();
        this.PostingTime = DateTime.Now;
      }
      MediaData.WriteMediaObject(this);
    }


    #endregion Methods


  }  // class MediaObject

} // namespace Empiria.Postings.Media
