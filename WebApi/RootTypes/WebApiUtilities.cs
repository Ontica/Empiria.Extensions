/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Core Services                        Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Static Library                        *
*  Type     : WebApiUtilities                              License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Internal static class with general purpose methods used by web api core services.              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Net.Http;
using System.Web;

using Empiria.Json;

namespace Empiria.WebApi {

  /// <summary>Internal static class with general purpose methods used by web api core services.</summary>
  static internal class WebApiUtilities {

    #region Public methods

    /// <summary>Creates a HttpRequestMessage object from a HttpRequest instance.</summary>
    /// <param name="request">The HttpRequest instance to convert to HttpRequestMessage.</param>
    static internal HttpRequestMessage CreateHttpRequestMessage(HttpRequest request) {
      var httpMethod = new HttpMethod(request.HttpMethod);

      return new HttpRequestMessage(httpMethod, request.Url);
    }


    /// <summary>Returns a standard Json object representation of an exception.</summary>
    /// <param name="exception">The exception to return in the standarized Json object.</param>
    /// <param name="request">The request where the exception was generated.</param>
    static internal JsonObject GetExceptionAsJsonObject(Exception exception, HttpRequest request) {
      HttpRequestMessage requestMessage = WebApiUtilities.CreateHttpRequestMessage(request);

      var model = new ExceptionModel(requestMessage, exception);

      return JsonObject.Parse(model);
    }


    static internal string GetUserHostAddress() {
      if (HttpContext.Current != null && HttpContext.Current.Request != null) {
        return HttpContext.Current.Request.UserHostAddress;
      } else {
        return "0.0.0.0";
      }
    }


    static internal string TryGetRequestHeader(string headerName) {
      var request = HttpContext.Current.Request;

      return request.Headers[headerName];
    }

    #endregion Public methods

  }  // class WebApiUtilities

} // namespace Empiria.WebApi
