/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                           Component : Domain Layer                            *
*  Assembly : Empiria.Storage.dll                        Pattern   : Service provider                        *
*  Type     : MediaFile                                  License   : Please read LICENSE.txt file            *
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

namespace Empiria.Storage {

  /// <summary>Represents a stored media object treated as a value type, so it must be related to other
  /// objects like metadata information holders or document entities.</summary>
  public class MediaFile : BaseObject, IProtected {

    #region Constructors and parsers

    protected MediaFile() {
      // Required by Empiria Framework
    }


    static internal T Create<T>(MediaStorage storage, MediaFileFields fields) where T : MediaFile {
      Assertion.Require(storage, "storage");
      Assertion.Require(fields, "fields");

      T mediaFile = Empiria.Reflection.ObjectFactory.CreateObject<T>();


      mediaFile.Storage = storage;
      mediaFile.LoadFields(fields);
      mediaFile.FileName = StorageUtilityMethods.GenerateUniqueFileNameForStorage(mediaFile.OriginalFileName);

      return mediaFile;
    }


    static public MediaFile Parse(int id) {
      return BaseObject.ParseId<MediaFile>(id);
    }


    static public MediaFile Parse(string uid) {
      return BaseObject.ParseKey<MediaFile>(uid);
    }

    static public MediaFile Empty {
      get {
        return BaseObject.ParseEmpty<MediaFile>();
      }
    }

    #endregion Constructors and parsers

    #region Properties

    [DataField("MediaContent")]
    public string MediaContent {
      get;
      private set;
    }

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


    [DataField("StorageId", Default = "Empiria.Storage.MediaStorage.Default")]
    public MediaStorage Storage {
      get;
      private set;
    }


    [DataField("FolderPath")]
    internal string FolderPath {
      get;
      private set;
    }


    [DataField("FileName")]
    internal string FileName {
      get;
      private set;
    }


    public string FullPath {
      get {
        return StorageUtilityMethods.GetFileFullName(this.Storage, this.FileName);
      }
    }


    [DataField("FileHashCode")]
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
      private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("PostedById")]
    public Contact PostedBy {
      get;
      private set;
    }


    [DataField("MediaFileStatus", Default = EntityStatus.Active)]
    public EntityStatus Status {
      get;
      private set;
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
          "StorageId", this.Storage.Id, "FolderName", this.FolderPath, "FileName", this.FileName,
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
      Assertion.Require(this.Status == EntityStatus.Active,
                       "MediaObject must be in 'Active' status.");

      this.Status = EntityStatus.Deleted;

      this.Save();
    }


    private void LoadFields(MediaFileFields fields) {
      this.MediaContent = fields.MediaContent;
      this.MediaType = fields.MediaType;
      this.Length = fields.MediaLength;
      this.OriginalFileName = fields.OriginalFileName;
      this.FolderPath = fields.FolderPath;
      this.HashCode = fields.FileHashCode;
    }


    protected override void OnSave() {
      if (base.IsNew) {
        this.PostedBy = ExecutionServer.CurrentContact;
        this.PostingTime = DateTime.Now;
      }
      MediaRepository.WriteMediaFile(this);
    }

    #endregion Methods

  }
}
