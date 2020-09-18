/* Empiria Postings ******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                             Component : Media Domain Types                    *
*  Assembly : Empiria.Postings.dll                         Pattern   : Information Holder                    *
*  Type     : MediaStorage                                 License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Represents a media storage unit that contain media objects.                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.StateEnums;

namespace Empiria.Postings.Media {

  /// <summary>Represents a media storage unit that contain media objects.</summary>
  public class MediaStorage : BaseObject {

    #region Fields

    static private readonly string DefaultMediaStorage = ConfigurationData.GetString("Default.MediaStorage");

    #endregion Fields

    #region Constructors and parsers

    private MediaStorage() {
      // Required by Empiria Framework
    }


    static public MediaStorage Parse(int id) {
      return BaseObject.ParseId<MediaStorage>(id);
    }


    static public MediaStorage Parse(string uid) {
      return BaseObject.ParseKey<MediaStorage>(uid);
    }


    static public MediaStorage Default {
      get {
        return Parse(DefaultMediaStorage);
      }
    }


    #endregion Constructors and parsers

    #region Properties


    [DataField("ObjectName")]
    public string FolderName {
      get;
      private set;
    }


    [DataField("ObjectExtData.Path")]
    public string Path {
      get;
      private set;
    }


    [DataField("ObjectExtData.Url")]
    public string Url {
      get;
      private set;
    }


    [DataField("ObjectStatus", Default = EntityStatus.Active)]
    public EntityStatus Status {
      get;
      protected set;
    }


    #endregion Properties

    #region Methods


    internal void Delete() {
      Assertion.Assert(this.Status == EntityStatus.Active,
                       "MediaStorage must be in 'Active' status.");

      this.Status = EntityStatus.Deleted;

      this.Save();
    }


    protected override void OnSave() {
      throw new NotImplementedException();
    }


    #endregion Methods

  }  // class MediaStorage

} // namespace Empiria.Postings.Media
