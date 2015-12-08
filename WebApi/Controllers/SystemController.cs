﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.Core.WebApi                              Assembly : Empiria.WebApi.dll                *
*  Type      : SystemController                                 Pattern  : Web API Controller                *
*  Version   : 2.0                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Web api methods to get and set system configuration settings.                                 *
*                                                                                                            *
********************************* Copyright (c) 2009-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Web.Http;

using Empiria.WebApi;
using Empiria.WebApi.Models;

namespace Empiria.Core.WebApi {

  /// <summary> Web api methods to get and set system configuration settings.</summary>
  public class SystemController : WebApiController {

    #region Public APIs

    [HttpGet, AllowAnonymous]
    [Route("v1/system/license")]
    public SingleObjectModel GetLicense() {
      try {
        return new SingleObjectModel(base.Request, ExecutionServer.LicenseName);
      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion Public APIs

  }  // class SystemController

}  // namespace Empiria.Core.WebApi