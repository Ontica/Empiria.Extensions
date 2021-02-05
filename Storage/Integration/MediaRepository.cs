/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                           Component : Integration Layer                       *
*  Assembly : Empiria.Storage.dll                        Pattern   : Data service                            *
*  Type     : MediaRepository                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Data read and write methods for media objects.                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;

namespace Empiria.Storage {

  /// <summary>Data read and write methods for media domain objects.</summary>
  static internal class MediaRepository {


    static internal void WriteFormerMediaFile(FormerMediaFile o) {
      var meta = o.Metadata;

      var op = DataOperation.Parse("writeEXFMedia",
                                    o.Id, o.UID, o.MediaType, o.Length,
                                    o.OriginalFileName, o.Storage.Id, o.FileName, o.HashCode,
                                    meta.Title, meta.Type, meta.Summary,
                                    meta.Topics, meta.Tags, meta.Authors,
                                    meta.Keywords, o.ExtensionData.ToString(),
                                    o.PostingTime, o.PostedBy.Id, (char) o.Status,
                                    o.Integrity.GetUpdatedHashCode());

      DataWriter.Execute(op);
    }


    static internal void WriteMediaFile(MediaFile o) {
      var op = DataOperation.Parse("writeEXFMediaFile",
                                    o.Id, o.UID, o.GetEmpiriaType().Id, o.MediaContent, o.MediaType, o.Length,
                                    o.Storage.Id, o.FolderPath, o.FileName, o.HashCode, o.OriginalFileName,
                                    string.Empty, o.ExtensionData.ToString(),
                                    o.PostingTime, o.PostedBy.Id, (char) o.Status,
                                    o.Integrity.GetUpdatedHashCode());

      DataWriter.Execute(op);
    }

  }  // class MediaRepository

}  // namespace Empiria.Storage
