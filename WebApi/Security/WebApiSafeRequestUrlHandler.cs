/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Core Services                        Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Http Message Handler                  *
*  Type     : WebApiSafeRequestUrlHandler                  License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Message handler used to guarantee safe URLs in Web API requests.                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Empiria.Security;

namespace Empiria.WebApi {

  /// <summary>Message handler used to guarantee safe URLs in Web API requests.</summary>
  public class WebApiSafeRequestUrlHandler : DelegatingHandler {

    #region Override

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                 CancellationToken cancellationToken) {

      var query = request.RequestUri.Query;

      if (EmpiriaString.IsSafe(query)) {
        return await base.SendAsync(request, cancellationToken);
      }

      EmpiriaLog.Critical($"Possible dangerous input detected in web api query. " +
                          $"The request was not processed: {query}");

      var exception = new SecurityException(SecurityException.Msg.UnsafeInput);

      var model = new ExceptionModel(request, exception);

      return model.CreateResponse();
    }

    #endregion Override

  }  // class WebApiSafeRequestUrlHandler

}  // namespace Empiria.WebApi
