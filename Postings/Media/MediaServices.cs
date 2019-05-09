/* Empiria Postings ******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                             Component : Media Management Services             *
*  Assembly : Empiria.Postings.dll                         Pattern   : Application Service                   *
*  Type     : MediaServices                                License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web API controller for object postings.                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.IO;
using System.Web;

using Empiria.Json;
using Empiria.Security;

using Empiria.Documents.IO;

namespace Empiria.Postings.Media {

  static public class MediaServices {

    #region Public services


    static public MediaObject Upload(HttpPostedFile file) {
      MediaStorage storage = MediaStorage.Default;

      string fileName = CreateStorageFileName(file.FileName);
      string hashCode = GetFileContentHashCode(file.InputStream);

      var json = new JsonObject();

      json.Add("mediaType", file.ContentType);
      json.Add("length", file.ContentLength);
      json.Add("storageId", storage.Id);
      json.Add("originalFileName", file.FileName);
      json.Add("fileName", fileName);
      json.Add("hashCode", hashCode);


      string fullPath = Path.Combine(storage.Path, fileName);

      file.SaveAs(fullPath);

      MediaObject mediaObject = new MediaObject(json);

      mediaObject.Save();

      return mediaObject;
    }


    #endregion Public services


    #region Private methods


    static private string CreateStorageFileName(string originalFileName) {
      string extension = Path.GetExtension(originalFileName);

      return $"{Guid.NewGuid().ToString()}{extension}";
    }


    static private string GetFileContentHashCode(Stream stream) {
      byte[] array = FileServices.StreamToArray(stream);



      string hash = Cryptographer.CreateHashCode(array);

      EmpiriaLog.Debug(array.Length.ToString() + "//" + hash);

      if (hash.Length > 128) {
        return hash.Substring(0, 128);
      } else {
        return hash;
      }
    }


    #endregion Private methods


  }  // class MediaServices

} // namespace Empiria.Postings.Media
