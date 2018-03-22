/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Base controllers                      *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Web Api Controller                    *
*  Type     : LoggingController                            License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Contains web api methods for application log services.                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.Logging;
using Empiria.Security;

namespace Empiria.WebApi.Controllers {

  /// <summary>Contains web api methods for application log services.</summary>
  public class LoggingController : WebApiController {

    #region Public APIs

    /// <summary>Stores an array of log entries.</summary>
    /// <param name="logEntries">The non-empty array of LogEntryModel instances.</param>
    [HttpPost, AllowAnonymous]
    [Route("v1/logging")]
    public void PostLogEntryArray([FromBody] LogEntryModel[] logEntries) {
      try {
        ClientApplication clientApplication = base.GetClientApplication();

        var logTrail = new LogTrail(clientApplication);

        logTrail.Write(logEntries);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion Public APIs

  }  // class LoggingController

}  // namespace Empiria.WebApi.Controllers
