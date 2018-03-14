/* Empiria Postings ******************************************************************************************
*                                                                                                            *
*  Module   : Empiria Postings                             Component : Web services interface                *
*  Assembly : Empiria.Postings.WebApi.dll                  Pattern   : Controller                            *
*  Type     : ObjectPostingsController                     License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web API controller for object postings.                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.Json;
using Empiria.WebApi;
using Empiria.WebApi.Models;

namespace Empiria.Postings.WebApi {

  /// <summary>Web API controller for object postings.</summary>
  public class ObjectPostingsController : WebApiController {

    #region GET methods

    [HttpGet]
    [Route("v1/postings/{objectType}/{objectUID}")]
    public CollectionModel GetObjectPostings(string objectType, string objectUID) {
      try {

        FixedList<ObjectPosting> postingsList = ObjectPosting.GetList(objectType, objectUID);

        return new CollectionModel(this.Request, postingsList.ToResponse(),
                                   typeof(ObjectPosting).FullName);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion GET methods

    #region UPDATE methods

    [HttpPost]
    [Route("v1/postings/{objectType}/{objectUID}")]
    public SingleObjectModel CreateObjectPosting(string objectType, string objectUID,
                                                 [FromBody] object body) {
      try {
        base.RequireBody(body);

        var bodyAsJson = JsonObject.Parse(body);

        var posting = new ObjectPosting(objectType, objectUID, bodyAsJson);

        posting.Save();

        return new SingleObjectModel(this.Request, posting.ToResponse(),
                                     typeof(ObjectPosting).FullName);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpDelete]
    [Route("v1/postings/{objectType}/{objectUID}/{postingUID}")]
    public CollectionModel DeleteObjectPosting(string objectType, string objectUID,
                                               string postingUID) {
      try {

        var posting = ObjectPosting.Parse(postingUID);

        posting.Delete();

        return this.GetObjectPostings(objectType, objectUID);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion UPDATE methods

  }  // class ObjectPostingsController

}  // namespace Empiria.Postings.WebApi
