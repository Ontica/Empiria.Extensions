/* Empiria Service-Oriented Architecture Framework ***********************************************************
*                                                                                                            *
*  Solution  : Empiria SOA Framework                            System   : Empiria Web Api Framework         *
*  Namespace : Empiria.WebApi                                   Assembly : Empiria.WebApi.dll                *
*  Type      : WebApiException                                  Pattern  : Exception Class                   *
*  Version   : 1.0        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : The exception that is thrown when a web api call fails.                                       *
*                                                                                                            *
********************************* Copyright (c) 2014-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Reflection;

using Empiria.WebApi.Models;

namespace Empiria.WebApi {

  /// <summary>The exception that is thrown when a web api call fails.</summary>
  public sealed class WebApiException : EmpiriaException {

    public enum Msg {
      AuthenticationHeaderMissed,
      BadAuthenticationHeaderFormat,
      BadRequest,
      BodyMissed,
      EndpointNotFound,
      InvalidHttpMethod,
      RequestHeaderMissed,
      ResourceMissed,
      ResourceNotFound,
    }

    static private string resourceBaseName = "Empiria.WebApi.RootTypes.WebApiExceptionMsg";

    private Msg message = Msg.BadRequest;

    #region Constructors and parsers

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
        switch (message) {

          case Msg.AuthenticationHeaderMissed:
          case Msg.BadAuthenticationHeaderFormat:
            return "Please review the format and value of the 'Authorization' " +
                   "request header.";

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
            return "Please check each of the url parameters to point to valid resources.";

          default:
            return "There was an internal or unknown error in the call.";
        }
      }
    }

    public HttpErrorCode ErrorCode {
      get {
        switch (message) {

          case Msg.AuthenticationHeaderMissed:
            return HttpErrorCode.Unauthorized;

          case Msg.BadAuthenticationHeaderFormat:
            return HttpErrorCode.BadRequest;

          case Msg.BadRequest:
            return HttpErrorCode.BadRequest;

          case Msg.BodyMissed:
            return HttpErrorCode.BadRequest;

          case Msg.EndpointNotFound:
            return HttpErrorCode.NotFound;

          case Msg.InvalidHttpMethod:
            return HttpErrorCode.MethodNotAllowed;

          case Msg.RequestHeaderMissed:
            return HttpErrorCode.BadRequest;

          case Msg.ResourceMissed:
            return HttpErrorCode.BadRequest;

          case Msg.ResourceNotFound:
            return HttpErrorCode.NotFound;

          default:
            throw Assertion.AssertNoReachThisCode();
        }
      }
    }

    static private string GetMessage(Msg message, params object[] args) {
      return GetResourceMessage(message.ToString(), resourceBaseName, Assembly.GetExecutingAssembly(), args);
    }

    #endregion Methods


  } // class WebApiException

} // namespace Empiria.WebApi
