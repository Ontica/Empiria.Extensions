/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi                                   Assembly : Empiria.WebApi.dll                *
*  Type      : WebApiResponseHandler                            Pattern  : Http Message Handler              *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Message handler used to control Web API authentication.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Empiria.WebApi {

  /// <summary>Message handler used to control Web API authentication.</summary>
  public class WebApiResponseHandler : DelegatingHandler {

    #region Public methods

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                 CancellationToken cancellationToken) {
      var response = await base.SendAsync(request, cancellationToken);

      if (!response.IsSuccessStatusCode) {
        return this.WrapResponseException(request, response);
      } else {
        return this.WrapResponse(request, response);
      }
    }

    #endregion Public methods

    #region Private methods

    private HttpResponseMessage BadRequest400Handler(HttpRequestMessage request) {
      var e = new WebApiException(WebApiException.Msg.BadRequest,
                                  request.Method.Method, request.RequestUri.AbsoluteUri);
      var model = new ExceptionModel(request, e);

      return model.CreateResponse();
    }


    private HttpResponseMessage MethodNotAllowed405Handler(HttpRequestMessage request) {
      var e = new WebApiException(WebApiException.Msg.InvalidHttpMethod,
                                  request.Method.Method, request.RequestUri.AbsoluteUri);
      var model = new ExceptionModel(request, e);

      return model.CreateResponse();
    }


    private HttpResponseMessage NotFound404Handler(HttpRequestMessage request) {
      var e = new WebApiException(WebApiException.Msg.EndpointNotFound,
                                  request.RequestUri.AbsoluteUri);
      var model = new ExceptionModel(request, e);

      return model.CreateResponse();
    }


    private HttpResponseMessage Unauthorized401Handler(HttpRequestMessage request) {
      var e = new WebApiException(WebApiException.Msg.AuthenticationHeaderMissed);
      var model = new ExceptionModel(request, e);

      return model.CreateResponse();
    }


    private HttpResponseMessage WrapResponse(HttpRequestMessage request,
                                             HttpResponseMessage response) {
      var content = response.Content as ObjectContent;

      if (content == null) {
        return response;
      }

      // If is already a base response model (or base payload)
      if (typeof(IBaseResponseModel).IsAssignableFrom(content.ObjectType)) {
        return response;
      }

      if (typeof(ICollection).IsAssignableFrom(content.ObjectType)) {
        var model = new CollectionModel(request, (ICollection) content.Value);

        return request.CreateResponse(model);

      } else if (typeof(DataTable).IsAssignableFrom(content.ObjectType)) {
        var model = new CollectionModel(request, (DataTable) content.Value);

        return request.CreateResponse(model);

      } else {
        var model = new SingleObjectModel(request, content.Value);

        return request.CreateResponse(model);

      }
    }


    private HttpResponseMessage WrapResponseException(HttpRequestMessage request,
                                                      HttpResponseMessage response) {
      var content = response.Content as ObjectContent;

      if (content == null) {
        return response;
      }

      // If is already a base response model (or base payload)
      if (typeof(IBaseResponseModel).IsAssignableFrom(content.ObjectType)) {
        return response;
      }

      switch(response.StatusCode) {
        case HttpStatusCode.BadRequest:
          return this.BadRequest400Handler(request);

        case HttpStatusCode.Unauthorized:
          return this.Unauthorized401Handler(request);

        case HttpStatusCode.NotFound:
          return this.NotFound404Handler(request);

        case HttpStatusCode.MethodNotAllowed:
          // All 405 Errors are catched and transformed here.
          // There is no way to catch them using a special controller/action.
          //supportedMethods = request.GetActionDescriptor().SupportedHttpMethods;
          return this.MethodNotAllowed405Handler(request);

        default:
          return response;
      }
    }

    #endregion Private methods

  }  // class WebApiResponseHandler

}  // namespace Empiria.WebApi
