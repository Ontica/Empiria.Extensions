/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Core Services                        Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Exception Class                       *
*  Type     : WebApiException                              License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Exceptions that can be thrown inside web api methods.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Reflection;

namespace Empiria.WebApi {

  /// <summary>Exceptions that can be thrown inside web api methods.</summary>
  public sealed class WebApiException : EmpiriaException {

    public enum Msg {
      AuthenticationHeaderMissed,
      BadAuthenticationHeaderFormat,
      BadRequest,
      BadBody,
      BodyMissed,
      EndpointNotFound,
      InvalidHeaderValue,
      InvalidHttpMethod,
      NullHeaderValue,
      RequestHeaderMissed,
      ResourceMissed,
      ResourceNotFound,
      UnauthorizedResource,
      UnprocessableContent,
    }

    static private string resourceBaseName = "Empiria.WebApi.RootTypes.WebApiExceptionMsg";

    private readonly Msg message;

    #region Constructors and parsers

    /// <summary>Initializes a new instance of WebApiException class with a specified error
    /// message.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="requestIssues">An array with a list of request issues.</param>
    public WebApiException(Msg message, string[] requestIssues)
                           : base(message.ToString(), GetMessage(message, null)) {
      this.message = message;
      this.RequestIssues = requestIssues;
    }

    /// <summary>Initializes a new instance of WebApiException class with a specified error
    /// message.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public WebApiException(Msg message, params object[] args)
                           : base(message.ToString(), GetMessage(message, args)) {
      this.message = message;
    }

    /// <summary>Initializes a new instance of WebApiException class with a specified error
    ///  message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="exception">This is the inner exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public WebApiException(Msg message, Exception exception, params object[] args)
                           : base(message.ToString(), GetMessage(message, args), exception) {
      this.message = message;
    }

    #endregion Constructors and parsers

    #region Methods

    public string Hint {
      get {
        switch (this.message) {

          case Msg.AuthenticationHeaderMissed:
          case Msg.BadAuthenticationHeaderFormat:
            return "Please review the format and value of the 'Authorization' " +
                   "request header.";

          case Msg.BadBody:
            return "Please check the request body and their values. " +
                   "Assure that every required field was supplied and " +
                   "review them against typos.";

          case Msg.BadRequest:
            return "Please check the request required parameters and their values. " +
                   "Also review the structure and values of the body content.";

          case Msg.BodyMissed:
            return "Please send the request body structure as described " +
                   "in the API documentation.";

          case Msg.EndpointNotFound:
            return "Please check the request to point to a valid resource. " +
                   "Assure that every required parameter was supplied and " +
                   "review them against typos.";

          case Msg.InvalidHttpMethod:
            return "Please check the list of valid methods using an " +
                   "OPTIONS request to the same endpoint.";

          case Msg.RequestHeaderMissed:
            return "Please review the request and include on it the " +
                   "missed required header.";

          case Msg.ResourceMissed:
            return "Please review the endpoint url structure according to " +
                   "what is described in the API documentation.";

          case Msg.ResourceNotFound:
            return "Please check that each of the url parameters point to a valid resource.";

          case Msg.UnauthorizedResource:
            return "Please check that each of the url parameters point to an unrestricted access resource.";

          default:
            return "There was an internal or unknown error in the call.";
        }
      }
    }

    public HttpErrorCode ErrorCode {
      get {
        switch (message) {

          case Msg.AuthenticationHeaderMissed:
          case Msg.UnauthorizedResource:

            return HttpErrorCode.Unauthorized;

          case Msg.BadAuthenticationHeaderFormat:
          case Msg.BadBody:
          case Msg.ResourceMissed:
          case Msg.BodyMissed:
          case Msg.BadRequest:
          case Msg.RequestHeaderMissed:

            return HttpErrorCode.BadRequest;

          case Msg.UnprocessableContent:

            return HttpErrorCode.UnprocessableContent;

          case Msg.EndpointNotFound:
          case Msg.ResourceNotFound:

            return HttpErrorCode.NotFound;

          case Msg.InvalidHttpMethod:
            return HttpErrorCode.MethodNotAllowed;

          default:
            throw Assertion.EnsureNoReachThisCode($"Unhandled message '{message}'.");
        }
      }
    }

    public string[] RequestIssues {
      get;
      private set;
    } = new string[0];


    static private string GetMessage(Msg message, params object[] args) {
      return GetResourceMessage(message.ToString(), resourceBaseName, Assembly.GetExecutingAssembly(), args);
    }

    #endregion Methods


  } // class WebApiException

} // namespace Empiria.WebApi
