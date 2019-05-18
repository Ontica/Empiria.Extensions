/* Empiria Postings ******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                             Component : Media Management Services             *
*  Assembly : Empiria.Postings.dll                         Pattern   : Interfacer                            *
*  Type     : MediaFileResponseModel                       License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Response static methods for MediaFile instances.                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web;

using System.Collections;
using System.Collections.Generic;

namespace Empiria.Postings.Media {

  /// <summary>Response static methods for MediaObject instances.</summary>
  static public class MediaFileResponseModel {


    static public ICollection ToResponse(this IList<MediaFile> list) {
      ArrayList array = new ArrayList(list.Count);

      foreach (var item in list) {
        var itemResponse = item.ToResponse();

        array.Add(itemResponse);
      }
      return array;
    }


    static public object ToResponse(this MediaFile mediaFile) {
      var metadata = mediaFile.Metadata;

      return new {
        uid = mediaFile.UID,
        mediaType = mediaFile.MediaType,
        length = mediaFile.Length,
        originalFileName = mediaFile.OriginalFileName,
        url = HttpUtility.UrlPathEncode(mediaFile.Url),
        postingTime = mediaFile.PostingTime,
        postedBy = new {
          uid = mediaFile.PostedBy.UID,
          name = mediaFile.PostedBy.Alias
        },
        status = mediaFile.Status,

        metadata = new {
          title = metadata.Title,
          type = metadata.Type,
          summary = metadata.Summary,
          topics = metadata.Topics,
          tags = metadata.Tags,
          authors = metadata.Authors,
        }
      };
    }


  }  // class MediaFileResponseModel

}  // namespace Empiria.Postings.Media