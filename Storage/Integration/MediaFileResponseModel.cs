/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                           Component : Integration Layer                       *
*  Assembly : Empiria.Storage.dll                        Pattern   : Response methods                        *
*  Type     : MediaFileResponseModel                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Response static methods for MediaFile instances.                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web;

using System.Collections;
using System.Collections.Generic;

namespace Empiria.Storage {

  /// <summary>Response static methods for MediaFile instances.</summary>
  static public class MediaFileResponseModel {


    static public ICollection ToResponse(this IList<FormerMediaFile> list) {
      ArrayList array = new ArrayList(list.Count);

      foreach (var item in list) {
        var itemResponse = item.ToResponse();

        array.Add(itemResponse);
      }
      return array;
    }


    static public object ToResponse(this FormerMediaFile mediaFile) {
      var metadata = mediaFile.Metadata;

      return new {
        uid = mediaFile.UID,
        name = metadata.Title.Length != 0 ? metadata.Title : mediaFile.OriginalFileName,
        mediaType = mediaFile.MediaType,
        length = mediaFile.Length,
        originalFileName = mediaFile.OriginalFileName,
        url = HttpUtility.UrlPathEncode(mediaFile.Url),
        postingTime = mediaFile.PostingTime,
        postedBy = new {
          uid = mediaFile.PostedBy.UID,
          name = mediaFile.PostedBy.ShortName
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

}  // namespace Empiria.Storge
