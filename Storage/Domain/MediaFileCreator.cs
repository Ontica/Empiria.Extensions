/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                           Component : Domain Layer                            *
*  Assembly : Empiria.Storage.dll                        Pattern   : Service provider                        *
*  Type     : MediaFileCreator                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Creates a media file.                                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Specialized;
using System.Web;

using Empiria.Json;

namespace Empiria.Storage {

  /// <summary>Creates a media file.</summary>
  internal class MediaFileCreator {

    #region Constructors and parsers

    internal MediaFileCreator(HttpPostedFile postedFile, NameValueCollection metadataValues) {
      this.PostedFile = postedFile;

      this.Metadata = BuildMetadataFromNameValueCollection(metadataValues);
    }

    #endregion Constructors and parsers


    #region Properties

    private MediaStorage Storage {
      get {
        return MediaStorage.Default;
      }
    }


    private HttpPostedFile PostedFile {
      get;
    }


    private Metadata Metadata {
      get;
    }


    #endregion Properties

    #region Public methods


    public FormerMediaFile CreateMediaFile() {
      JsonObject fileData = GetFileDataAsJson();

      string fileFullName = StorageUtilityMethods.GetFileFullName(this.Storage, fileData.Get<string>("fileName"));

      this.PostedFile.SaveAs(fileFullName);

      var mediaFile = new FormerMediaFile(fileData, this.Metadata);

      mediaFile.Save();

      return mediaFile;
    }


    #endregion Public methods

    #region Private methods

    static private Metadata BuildMetadataFromNameValueCollection(NameValueCollection data) {
      var json = new JsonObject();

      json.AddIfValue("title", data["title"]);
      json.AddIfValue("type", data["type"]);
      json.AddIfValue("summary", data["summary"]);
      json.AddIfValue("topics", data["topics"]);
      json.AddIfValue("tags", data["tags"]);
      json.AddIfValue("authors", data["authors"]);

      return Metadata.Parse(json);
    }


    private JsonObject GetFileDataAsJson() {
      string fileName = StorageUtilityMethods.GenerateUniqueFileNameForStorage(this.PostedFile.FileName);
      string hashCode = StorageUtilityMethods.CalculateStreamHashCode(this.PostedFile.InputStream);

      var json = new JsonObject();

      json.Add("mediaType", this.PostedFile.ContentType);
      json.Add("length", this.PostedFile.ContentLength);

      json.Add("storageId", this.Storage.Id);

      json.Add("originalFileName", this.PostedFile.FileName);
      json.Add("fileName", fileName);
      json.Add("hashCode", hashCode);

      return json;
    }


    #endregion Private methods

  }  // class MediaFileCreator

} // namespace Empiria.Storage
