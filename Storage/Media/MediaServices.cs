/* Empiria Postings ******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                             Component : Media Management Services             *
*  Assembly : Empiria.Postings.dll                         Pattern   : Application Service                   *
*  Type     : MediaServices                                License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Application service used to handle media files operations.                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Specialized;
using System.IO;
using System.Web;

using Empiria.Json;
using Empiria.Security;


namespace Empiria.Storage {

/// <summary>Application service used to handle media files operations.</summary>
  static public class MediaServices {


    #region Public services

    public static MediaFile CreateMediaFile(HttpPostedFile file, NameValueCollection metadata) {
      MediaStorage storage = MediaStorage.Default;

      JsonObject fileData = GetFileDataAsJson(file, storage);

      string fullPath = Path.Combine(storage.Path, fileData.Get<string>("fileName"));

      file.SaveAs(fullPath);

      Metadata metadataObject = BuildMetadataFromNameValueCollection(metadata);

      var mediaFile = new MediaFile(fileData, metadataObject);

      mediaFile.Save();

      return mediaFile;
    }


    #endregion Public services


    #region Private methods


    static private Metadata BuildMetadataFromNameValueCollection(NameValueCollection data) {
      var form = HttpContext.Current.Request.Form;

      var json = new JsonObject();

      json.AddIfValue("title", data["title"]);
      json.AddIfValue("type", data["type"]);
      json.AddIfValue("summary", data["summary"]);
      json.AddIfValue("topics", data["topics"]);
      json.AddIfValue("tags", data["tags"]);
      json.AddIfValue("authors", data["authors"]);

      return Metadata.Parse(json);
    }


    static private string CreateStorageFileName(string originalFileName) {
      string extension = Path.GetExtension(originalFileName);

      return $"{Guid.NewGuid().ToString()}{extension}";
    }


    static private string GetFileContentHashCode(Stream stream) {
      byte[] array = UtilityMethods.StreamToArray(stream);

      string hash = Cryptographer.CreateHashCode(array);

      if (hash.Length > 128) {
        return hash.Substring(0, 128);
      } else {
        return hash;
      }
    }


    static private JsonObject GetFileDataAsJson(HttpPostedFile file, MediaStorage storage) {
      string fileName = CreateStorageFileName(file.FileName);
      string hashCode = GetFileContentHashCode(file.InputStream);

      var json = new JsonObject();

      json.Add("mediaType", file.ContentType);
      json.Add("length", file.ContentLength);

      json.Add("storageId", storage.Id);

      json.Add("originalFileName", file.FileName);
      json.Add("fileName", fileName);
      json.Add("hashCode", hashCode);


      return json;
    }


    #endregion Private methods


  }  // class MediaServices

} // namespace Empiria.Storage
