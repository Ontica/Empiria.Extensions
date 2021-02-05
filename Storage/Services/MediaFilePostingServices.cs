/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                           Component : Services Layer                          *
*  Assembly : Empiria.Storage.dll                        Pattern   : Component services                      *
*  Type     : MediaFilePostingServices                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Invokes methods on ProjectItemFile objects according to the current Http Request.              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;
using System.Web;

using Empiria.Json;
using Empiria.Postings;

namespace Empiria.Storage {

  /// <summary>Invokes methods on objects according to the current Http Request.</summary>
  static public class MediaFilePostingServices {

    #region Public methods

    static public Posting CreateMediaFilePosting(HttpRequest request,
                                                 BaseObject nodeObject,
                                                 string postingType) {
      Assertion.AssertObject(request, "request");
      Assertion.AssertObject(nodeObject, "nodeObject");
      Assertion.AssertObject(postingType, "postingType");

      EnsureIsValidRequest(request);

      var form = HttpContext.Current.Request.Form;

      var mediaFileCreator = new MediaFileCreator(request.Files[0], form);

      FormerMediaFile mediaFile = mediaFileCreator.CreateMediaFile();

      var posting = new Posting(postingType, nodeObject, mediaFile);

      if (form.AllKeys.Contains("extensionData")) {
        posting.ExtensionData = JsonObject.Parse(form["extensionData"]);
      }

      posting.Save();

      return posting;
    }


    static public Posting UpdateMediaFilePosting(HttpRequest request,
                                                 Posting posting) {
      throw new NotImplementedException();
    }


    #endregion Public methods

    #region Private methods

    static private void EnsureIsValidRequest(HttpRequest request) {
      Assertion.Assert(request.ContentType.StartsWith("multipart/form-data"),
                       $"Invalid request content type '{request.ContentType}'.");

      if (request.Files == null || request.Files.Count != 1) {
        Assertion.AssertFail($"Invalid request. There are no files and one was expected.");
      }
    }


    #endregion Private methods

  }  // class MediaFilePostingServices

}  // namespace Empiria.Storage
