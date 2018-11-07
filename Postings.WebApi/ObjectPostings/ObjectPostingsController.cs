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

namespace Empiria.Postings.WebApi {

  /// <summary>Web API controller for object postings.</summary>
  public class ObjectPostingsController : WebApiController {

    #region GET methods

    [HttpGet]
    [Route("v1/postings/{objectUID}")]
    public CollectionModel GetObjectPostingsList(string objectUID) {
      try {

        FixedList<ObjectPosting> postingsList = ObjectPosting.GetList(objectUID);

        return new CollectionModel(this.Request, postingsList.ToResponse(),
                                   typeof(ObjectPosting).FullName);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }



    [HttpGet]
    [Route("v1/postings/{objectUID}/{postingUID}")]
    public SingleObjectModel GetPosting(string objectUID, string postingUID) {
      try {

        var posting = ObjectPosting.Parse(postingUID);

        Assertion.Assert(posting.ObjectUID == objectUID,
                         $"posting.UID '{postingUID}' is not related with object.UID = '{objectUID}'.");

        return new SingleObjectModel(this.Request, posting.ToResponse(),
                                     typeof(ObjectPosting).FullName);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    #endregion GET methods

    #region UPDATE methods

    [HttpPost]
    [Route("v1/postings/{objectUID}")]
    public SingleObjectModel CreateObjectPosting(string objectUID,
                                                 [FromBody] object body) {
      try {
        base.RequireBody(body);

        var bodyAsJson = JsonObject.Parse(body);

        var posting = new ObjectPosting(objectUID, bodyAsJson);

        posting.Save();

        return new SingleObjectModel(this.Request, posting.ToResponse(),
                                     typeof(ObjectPosting).FullName);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPut, HttpPatch]
    [Route("v1/postings/{objectUID}/{postingUID}")]
    public SingleObjectModel UpdateObjectPosting(string objectUID, string postingUID,
                                                 [FromBody] object body) {
      try {
        base.RequireBody(body);

        var bodyAsJson = JsonObject.Parse(body);

        var posting = ObjectPosting.Parse(postingUID);

        posting.Update(bodyAsJson);

        return new SingleObjectModel(this.Request, posting,
                                     typeof(ObjectPosting).FullName);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpDelete]
    [Route("v1/postings/{objectUID}/{postingUID}")]
    public CollectionModel DeleteObjectPosting(string objectUID, string postingUID) {
      try {

        var posting = ObjectPosting.Parse(postingUID);

        posting.Delete();

        return this.GetObjectPostingsList(objectUID);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion UPDATE methods

  }  // class ObjectPostingsController

}  // namespace Empiria.Postings.WebApi
