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
    /// <param name="logEntries">The non-empty array of LogEntryModel instances.</param>
    [HttpPost, AllowAnonymous]
    [Route("v1/logging")]
    public void PostLogEntryArray(string apiKey, [FromBody] LogEntryModel[] logEntries) {
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

}  // namespace Empiria.Microservices
