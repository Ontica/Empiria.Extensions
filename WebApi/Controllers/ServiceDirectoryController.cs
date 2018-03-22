/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Base controllers                      *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Web Api Controller                    *
*  Type     : ServiceDirectoryController                   License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web api methods to get information about the web service directory.                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.Security;

namespace Empiria.WebApi.Controllers {

  /// <summary>Web api methods to get information about the web service directory.</summary>
  public class ServiceDirectoryController : WebApiController {

    #region Public APIs

    /// <summary>Gets a list of available web services in the context of the client application.</summary>
    /// <returns>A list of services as instances of the type ServiceDirectoryItem.</returns>
    [HttpGet, AllowAnonymous]
    [Route("v1/system/service-directory")]
    public CollectionModel GetServiceDirectory() {
      try {
        ClientApplication clientApplication = base.GetClientApplication();

        var endpointsList = EndpointConfig.GetList(clientApplication);

        return new CollectionModel(base.Request, endpointsList);
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion Public APIs

  }  // class ServiceDirectoryController

}  // namespace Empiria.WebApi.Controllers
