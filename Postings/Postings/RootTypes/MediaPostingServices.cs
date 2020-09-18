/* Empiria Postings ******************************************************************************************
*                                                                                                            *
*  Module   : Posting services                             Component : Web services interface                *
*  Assembly : Empiria.Postings.dll                         Pattern   : Application Service                   *
*  Type     : MediaPostingServices                         License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Invokes methods on ProjectItemFile objects according to the current Http Request.              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;
using System.Web;

using Empiria.Json;
using Empiria.Postings.Media;

namespace Empiria.Postings {

  /// <summary>Invokes methods on ProjectItemFile objects according to the current Http Request.</summary>
  public class MediaPostingServices {

    #region Public methods


    static public Posting CreateMediaPost(HttpRequest request, BaseObject nodeObject, string postingType) {
      Assertion.AssertObject(request, "request");
      Assertion.AssertObject(nodeObject, "nodeObject");
      Assertion.AssertObject(postingType, "postingType");

      EnsureIsValidRequest(request);

      var form = HttpContext.Current.Request.Form;

      MediaFile mediaFile = MediaServices.CreateMediaFile(request.Files[0], form);

      var posting = new Posting(postingType, nodeObject, mediaFile);

      if (form.AllKeys.Contains("extensionData")) {
        posting.ExtensionData = JsonObject.Parse(form["extensionData"]);
      }

      posting.Save();

      return posting;
    }


    static public Posting UpdateMediaPost(HttpRequest request, Posting posting) {
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

  }  // class MediaPostingServices

}  // namespace Empiria.Postings
