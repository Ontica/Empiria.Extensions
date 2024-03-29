﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Base controllers                      *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Web Api Controller                    *
*  Type     : TestsController                              License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary   : Web api methods used to test clients and proxies calls.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

namespace Empiria.WebApi.Controllers {

  /// <summary> Web api methods to get and set system configuration settings.</summary>
  public class TestsController : WebApiController {

    #region Public APIs

    [HttpGet, AllowAnonymous]
    [Route("v1/tests/anonymous")]
    public SingleObjectModel TestAnonymous() {
      try {
        var data = "This is the hello world message.";

        return new SingleObjectModel(base.Request, data);
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    [HttpGet]
    [Route("v1/tests/secure-data")]
    public SingleObjectModel TestSecureData() {
      try {
        var data = "This is some secured data.";

        return new SingleObjectModel(base.Request, data);
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    [HttpGet]
    [Route("v1/tests/get-resource/{resourceId}")]
    public SingleObjectModel TestGetValidResource(int resourceId) {
      try {
        if (resourceId % 2 != 0) {
          throw new ResourceNotFoundException("TestResourceNotFound",
                                              "Resource with id = '{0}' was not found.", resourceId);
        }
        return new SingleObjectModel(base.Request, resourceId);
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost]
    [Route("v1/tests/post-without-body")]
    public SingleObjectModel TestPostWithoutBody() {
      try {
        var data = "This is some data from a POST without a body.";

        return new SingleObjectModel(base.Request, data);
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    [HttpPost]
    [Route("v1/tests/post-with-body")]
    public SingleObjectModel TestPostWithBody([FromBody] object body) {
      try {
        Assertion.Require(body, "Body can't be null or empty.");

        return new SingleObjectModel(base.Request, body);
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion Public APIs

  }  // class TestsController

}  // namespace Empiria.WebApi.Controllers
