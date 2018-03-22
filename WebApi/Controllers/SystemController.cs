/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Base controllers                      *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Web Api Controller                    *
*  Type     : SystemController                             License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web api methods to get and set system configuration settings.                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

namespace Empiria.WebApi.Controllers {

  /// <summary> Web api methods to get and set system configuration settings.</summary>
  public class SystemController : WebApiController {

    #region Public APIs

    /// <summary>Gets the Empiria license name.</summary>
    [HttpGet, AllowAnonymous]
    [Route("v1/system/license")]
    public SingleObjectModel GetLicense() {
      try {
        return new SingleObjectModel(base.Request, ExecutionServer.LicenseName);
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    [HttpGet, HttpPost, HttpPut, HttpDelete, HttpPatch, HttpHead, HttpOptions]
    [AllowAnonymous]
    public void Http404ErrorHandler() {
      throw new WebApiException(WebApiException.Msg.EndpointNotFound,
                                base.Request.RequestUri.AbsoluteUri);
    }

    #endregion Public APIs

  }  // class SystemController

}  // namespace Empiria.WebApi.Controllers
