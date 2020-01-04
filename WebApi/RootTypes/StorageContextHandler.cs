/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi                                   Assembly : Empiria.WebApi.dll                *
*  Type      : StorageContextHandler                            Pattern  : Http Message Handler              *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Message handler used to log every web api request and some info about their response.         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Empiria.WebApi {

  /// <summary>Message handler used to log every web api request and some info about their response.</summary>
  public class StorageContextHandler : DelegatingHandler {

    #region Methods

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                 CancellationToken cancellationToken) {

      // Thread.Sleep(2000);

      if (request.Method == HttpMethod.Get ||
          request.Method == HttpMethod.Head ||
          request.Method == HttpMethod.Options) {

        return await base.SendAsync(request, cancellationToken);
      }

      return await base.SendAsync(request, cancellationToken);

      //using (var context = StorageContext.Open()) {
      //  try {
      //    HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

      //    context.Update();

      //    context.Close();

      //    return response;

      //  } catch {
      //    throw;

      //  } finally {
      //    context.Dispose();

      //  }  // try
      //}  // using

    }

    #endregion Methods

  }  // class StorageContextHandler

}  // namespace Empiria.WebApi
