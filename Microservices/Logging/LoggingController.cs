/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Microservices             *
*  Namespace : Empiria.Microservices                            Assembly : Empiria.Microservices.dll         *
*  Type      : LoggingController                                Pattern  : Web API Controller                *
*  Version   : 1.0                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Contains web api methods for application log services.                                        *
*                                                                                                            *
********************************** Copyright(c) 2016-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Web.Http;

using Empiria.Logging;
using Empiria.Security;
using Empiria.WebApi;

namespace Empiria.Microservices {

  /// <summary>Contains web api methods for application log services.</summary>
  public class LoggingController : WebApiController {

    #region Public APIs

    /// <summary>Stores an array of log entries.</summary>
    /// <param name="apiKey">The client application key.</param>
    /// <param name="logEntries">The non-empty array of LogEntryModel instances.</param>
    [HttpPost, AllowAnonymous]
    [Route("v1/logging/{apiKey}")]
    public void PostLogEntryArray(string apiKey, [FromBody] LogEntryModel[] logEntries) {
      try {
        base.RequireResource(apiKey, "apiKey");
        base.RequireBody(logEntries);
        Assertion.Assert(logEntries.Length > 0,
                         "Request body must contain a non-empty LogEntry array.");

        var clientApp = ClientApplication.ParseActive(apiKey);
        var logTrail = new LogTrail(clientApp);

        logTrail.Write(logEntries);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion Public APIs

  }  // class LoggingController

}  // namespace Empiria.Microservices
