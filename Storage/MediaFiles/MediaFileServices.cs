/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                           Component : Services Layer                          *
*  Assembly : Empiria.Storage.dll                        Pattern   : Service provider                        *
*  Type     : MediaFileServices                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides services for media file object creation and reposition.                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.IO;
using System.Threading.Tasks;

using Empiria.Postings;
using Empiria.Services;

namespace Empiria.Storage {

  /// <summary>Provides services for media file object creation and reposition.</summary>
  public class MediaFileServices : Service {

    #region Constructors and parsers

    protected MediaFileServices() {
      // no-op
    }

    static public MediaFileServices ServiceInteractor() {
      return Service.CreateInstance<MediaFileServices>();
    }

    #endregion Constructors and parsers

    #region Services

    public async Task<T> CreateMediaFile<T>(MediaStorage storage,
                                            MediaFileFields fields,
                                            Stream inputStream) where T: MediaFile {
      Assertion.AssertObject(storage, "storage");
      Assertion.AssertObject(fields, "fields");
      Assertion.AssertObject(inputStream, "inputStream");

      fields.FileHashCode = UtilityMethods.CalculateStreamHashCode(inputStream);

      T mediaFile = MediaFile.Create<T>(storage, fields);

      using (FileStream outputStream = File.OpenWrite(mediaFile.FullPath)) {
        await inputStream.CopyToAsync(outputStream);
      }

      mediaFile.Save();

      return mediaFile;
    }

    public MediaFile GetMediaFile(string mediaFileUID) {
      return MediaFile.Parse(mediaFileUID);
    }


    public FixedList<T> GetRelatedMediaFiles<T>(BaseObject entity) where T : MediaFile {
      return PostingList.GetPostedItems<T>(entity);
    }


    public MediaStorage GetStorageFor(BaseObject node) {
      return MediaStorage.Default;
    }


    public void RemoveMediaFile(MediaFile mediaFile, BaseObject node) {
      var posting = PostingList.GetPosting(node, mediaFile);

      posting.Delete();
      mediaFile.Delete();

      File.Delete(mediaFile.FullPath);
    }


    public Task ReplaceMediaFile(MediaFile mediaFile, MediaFileFields fields,
                                 Stream fileStream) {
      throw new NotImplementedException();
    }


    public void RelateMediaFile(MediaFile mediaFile, BaseObject node,
                                string relationName) {
      var posting = new Posting(relationName, node, mediaFile);

      posting.Save();
    }


    #endregion Services

  }  // class MediaFileServices

} // namespace Empiria.Storage
