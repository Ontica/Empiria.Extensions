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

namespace Empiria.WebApi {

  /// <summary>The exception that is thrown when a web api call fails.</summary>
  public sealed class WebApiException : EmpiriaException {

    public enum Msg {
      BadRequest,
      BodyMissed,
      EndpointNotFound,
      InvalidHttpMethod,
      RequestHeaderMissed,
      ResourceMissed,
      ResourceNotFound,
    }

    static private string resourceBaseName = "Empiria.WebApi.RootTypes.WebApiExceptionMsg";

    #region Constructors and parsers

    /// <summary>Initializes a new instance of WebApiException class with a specified error
    /// message.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public WebApiException(Msg message, params object[] args)
                                        : base(message.ToString(), GetMessage(message, args)) {

    }

    /// <summary>Initializes a new instance of WebApiException class with a specified error
    ///  message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="exception">This is the inner exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public WebApiException(Msg message, Exception exception, params object[] args)
                                        : base(message.ToString(), GetMessage(message, args), exception) {
    }

    #endregion Constructors and parsers

    #region Private methods

    static private string GetMessage(Msg message, params object[] args) {
      return GetResourceMessage(message.ToString(), resourceBaseName, Assembly.GetExecutingAssembly(), args);
    }

    #endregion Private methods

  } // class WebApiException

} // namespace Empiria.WebApi
