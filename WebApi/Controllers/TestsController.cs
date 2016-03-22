/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.Core.WebApi                              Assembly : Empiria.WebApi.dll                *
*  Type      : TestsController                                  Pattern  : Web API Controller                *
*  Version   : 2.0                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Web api methods used to test clients and proxies calls.                                       *
*                                                                                                            *
********************************* Copyright (c) 2009-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Web.Http;

using Empiria.WebApi;
using Empiria.WebApi.Models;

namespace Empiria.Core.WebApi {

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
        Assertion.AssertObject(body, "Body can't be null or empty.");

        return new SingleObjectModel(base.Request, body);
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion Public APIs

  }  // class TestsController

}  // namespace Empiria.Core.WebApi
