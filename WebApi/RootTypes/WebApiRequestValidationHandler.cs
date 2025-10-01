/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Core Services                        Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Http Message Handler                  *
*  Type     : WebApiRequestValidationHandler               License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Message handler used to validate Web API requests.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Empiria.Security;

namespace Empiria.WebApi {

  /// <summary>Message handler used to validate Web API requests.</summary>
  public class WebApiRequestValidationHandler : DelegatingHandler {

    #region Override

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                 CancellationToken cancellationToken) {

      await request.Content.LoadIntoBufferAsync();

      if (request.Content.IsFormData() || request.Content.IsMimeMultipartContent()) {
        return await base.SendAsync(request, cancellationToken);
      }

      var body = await request.Content.ReadAsStringAsync();

      if (!EmpiriaString.IsUnsafe(body)) {
        return await base.SendAsync(request, cancellationToken);
      }

      EmpiriaLog.Critical("Dangerous input detected in request body: " + body);

      var exception = new SecurityException(SecurityException.Msg.UnsafeInput);

      var model = new ExceptionModel(request, exception);

      return model.CreateResponse();
    }

    #endregion Override

  }  // class WebApiRequestValidationHandler

}  // namespace Empiria.WebApi
