/* Empiria Postings ******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                             Component : Web services interface                *
*  Assembly : Empiria.Postings.WebApi.dll                  Pattern   : Controller                            *
*  Type     : MediaController                              License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web API controller for media management.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web;

using Empiria.WebApi;

namespace Empiria.Postings.Media.WebApi {

  /// <summary>Web API controller for media management.</summary>
  public class MediaController : WebApiController {

    #region GET methods


    [HttpGet]
    [Route("v1/media/{mediaUID}")]
    public SingleObjectModel GetMedia(string mediaUID) {
      try {
        var mediaFile = MediaFile.Parse(mediaUID);

        return new SingleObjectModel(this.Request, mediaFile.ToResponse(),
                                     typeof(MediaFile).FullName);

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    #endregion GET methods


    #region UPDATE methods


    [HttpPost]
    [Route("v1/media/upload")]
    public SingleObjectModel UploadMedia() {
      try {
        var fullRequest = HttpContext.Current.Request;

        Assertion.Assert(fullRequest.ContentType.StartsWith("multipart/form-data"),
                         $"Invalid request content type '{fullRequest.ContentType}'.");

        if (this.Request.Content.IsFormData()) {
          Assertion.AssertFail($"Invalid request content type '{fullRequest.ContentType}'.");
        }

        if (fullRequest.Files == null || fullRequest.Files.Count != 1) {
          Assertion.AssertFail($"Invalid request. There are no files and one was expected.");
        }

        MediaFile mediaFile = MediaServices.CreateMediaFile(fullRequest.Files[0], fullRequest.Form);

        return new SingleObjectModel(this.Request, mediaFile.ToResponse(),
                                     typeof(MediaFile).FullName);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    #endregion UPDATE methods

  }  // class MediaController

}  // namespace Empiria.Postings.Media.WebApi
