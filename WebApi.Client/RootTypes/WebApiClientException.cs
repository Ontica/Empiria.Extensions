/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Client                             Component : Services Layer                          *
*  Assembly : Empiria.WebApi.Client.dll                  Pattern   : Empiria Exception                       *
*  Type     : WebApiClientException                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : The exception that is thrown when a web api client call fails.                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Net.Http;
using System.Reflection;

using Empiria.Json;

namespace Empiria.WebApi {

  /// <summary>The exception that is thrown when a web api client call fails.</summary>
  public sealed class WebApiClientException : EmpiriaException {

    public enum Msg {

      HttpNoSuccessStatusCode,

      RemoteServerException,

      UndefinedServiceUIDOrEndpoint,

      UriParsingIssue,
    }


    static private string resourceBaseName = "Empiria.WebApi.Client.RootTypes.WebApiClientExceptionMsg";


    #region Constructors and parsers

    /// <summary>Initializes a new instance of WebApiClientException class with a specified error
    /// message.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public WebApiClientException(HttpResponseMessage response, string responseContent,
                                 Msg message, params object[] args)
                           : base(message.ToString(), GetReponseContentMessage(responseContent, message, args)) {
      this.Response = response;
      this.ResponseContent = responseContent;
    }


    /// <summary>Initializes a new instance of WebApiClientException class with a specified error
    /// message.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public WebApiClientException(Msg message, params object[] args)
                           : base(message.ToString(), GetMessage(message, args)) {

    }

    /// <summary>Initializes a new instance of WebApiClientException class with a specified error
    ///  message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="innerException">This is the inner exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public WebApiClientException(Msg message, Exception innerException, params object[] args)
                          : base(message.ToString(), GetMessage(message, args), innerException) {

    }

    public WebApiClientException(Msg message, WebApiClientException innerException)
                          : base(message.ToString(), GetMessage(message, innerException), innerException) {
      this.Response = innerException.Response;
      this.ResponseContent = innerException.ResponseContent;
    }

    #endregion Constructors and parsers

    #region Methods

    public bool IsUnauthorized {
      get {
        if (Response == null) {
          return false;
        }
        return Response.StatusCode == System.Net.HttpStatusCode.Unauthorized;
      }
    }


    public HttpResponseMessage Response {
      get;
    }


    public string ResponseContent {
      get;
    }


    public JsonObject TryGetResponseContentAsJson() {
      return TryGetResponseContentAsJson(ResponseContent);
    }

    #endregion Methods

    #region Helpers

    static private string GetMessage(Msg message, params object[] args) {

      return GetResourceMessage(message.ToString(), resourceBaseName, Assembly.GetExecutingAssembly(), args);
    }


    static private string GetMessage(Msg message, WebApiClientException innerException) {

      return GetReponseContentMessage(innerException.ResponseContent, message);
    }


    static private string GetReponseContentMessage(string responseContent,
                                                   Msg message, params object[] args) {
      string msg = GetMessage(message, args);

      JsonObject json = TryGetResponseContentAsJson(responseContent);

      if (json == null) {
        return msg;
      }
      if (json.Contains("data/errorMessage")) {
        return $"{msg}: {json.Get<string>("data/errorMessage")}";
      } else {
        return msg;
      }
    }


    static private JsonObject TryGetResponseContentAsJson(string content) {
      if (content == null) {
        return null;
      }
      try {
        return JsonConverter.ToJsonObject(content);
      } catch {
        return null;
      }
    }

    #endregion Helpers

  } // class WebApiClientException

} // namespace Empiria.WebApi
