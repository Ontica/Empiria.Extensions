/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Microservices             *
*  Namespace : Empiria.Microservices                            Assembly : Empiria.Microservices.dll         *
*  Type      : ServiceDirectoryController                       Pattern  : Web API Controller                *
*  Version   : 1.0                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Web api methods to get information about the web service directory.                           *
*                                                                                                            *
********************************* Copyright (c) 2016-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Web.Http;

using Empiria.WebApi;
using Empiria.WebApi.Models;

namespace Empiria.Microservices {

  /// <summary>Web api methods to get information about the web service directory.</summary>
  public class ServiceDirectoryController : WebApiController {

    #region Public APIs

    /// <summary>Gets a list of available web services for an authenticated user
    /// in the context of the client application.</summary>
    /// <returns>A list of services as instances of the type ServiceDirectoryItem.</returns>
    [HttpGet, AllowAnonymous]
    [Route("v1/system/service-directory")]
    public CollectionModel GetServiceDirectory() {
      try {
        var services = ServiceDirectoryItem.GetList();

        return new CollectionModel(base.Request, services);
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion Public APIs

  }  // class ServiceDirectoryController

}  // namespace Empiria.Microservices
