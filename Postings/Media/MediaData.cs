/* Empiria Postings ******************************************************************************************
*                                                                                                            *
*  Module   : Empiria Postings                             Component : Domain services                       *
*  Assembly : Empiria.Postings.dll                         Pattern   : Data Service                          *
*  Type     : MediaData                                    License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Data read and write methods for media domain objects.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;

namespace Empiria.Postings.Media {

  /// <summary>Data read and write methods for media domain objects.</summary>
  static internal class MediaData {


    static internal void WriteMediaFile(MediaFile o) {
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


  }  // class MediaData

}  // namespace Empiria.Postings.Media
