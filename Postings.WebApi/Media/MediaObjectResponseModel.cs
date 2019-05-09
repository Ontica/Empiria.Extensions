/* Empiria Postings ******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                             Component : Web services interface                *
*  Assembly : Empiria.Postings.WebApi.dll                  Pattern   : Response methods                      *
*  Type     : MediaObjectResponseModel                     License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Response static methods for MediaObject instances.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web;

namespace Empiria.Postings.Media.WebApi {

  /// <summary>Response static methods for MediaObject instances.</summary>
  static internal class MediaObjectResponseModel {


    static internal object ToResponse(this MediaObject mediaObject) {
      return new {
        uid = mediaObject.UID,
        mediaType = mediaObject.MediaType,
        length = mediaObject.Length,
        originalFileName = mediaObject.OriginalFileName,
        url = HttpUtility.UrlPathEncode(mediaObject.Url),
        postingTime = mediaObject.PostingTime,
        postedBy = new {
          uid = mediaObject.PostedBy.UID,
          name = mediaObject.PostedBy.Alias
        },
        status = mediaObject.Status
      };
    }


  }  // class MediaObjectResponseModel

}  // namespace Empiria.Postings.WebApi
