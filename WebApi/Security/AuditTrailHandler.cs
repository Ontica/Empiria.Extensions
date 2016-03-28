/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi                                   Assembly : Empiria.WebApi.dll                *
*  Type      : AuditTrailHandler                                Pattern  : Http Message Handler              *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Message handler used to log every web api request and some info about their response.         *
*                                                                                                            *
********************************* Copyright (c) 2014-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Empiria.WebApi {

  /// <summary>Message handler used to log every web api request and some info about their response.</summary>
  public class AuditTrailHandler : DelegatingHandler {

    #region Methods

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                 CancellationToken cancellationToken) {
      var auditTrailLog = new WebApiAuditTrail();

      HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

      auditTrailLog.Write(request, response);

      return response;
    }

    #endregion Methods

  }  // class AuditTrailHandler

}  // namespace Empiria.WebApi
