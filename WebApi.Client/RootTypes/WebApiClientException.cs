﻿/* Empiria Extensions ****************************************************************************************
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

namespace Empiria.WebApi {

  /// <summary>The exception that is thrown when a web api client call fails.</summary>
  public sealed class WebApiClientException : EmpiriaException {

    public enum Msg {

      HttpNoSuccessStatusCode,

      UndefinedServiceUIDOrEndpoint,

      UriParsingIssue,
    }


    static private string resourceBaseName = "Empiria.WebApi.Client.RootTypes.WebApiClientExceptionMsg";


    #region Constructors and parsers

    /// <summary>Initializes a new instance of WebApiClientException class with a specified error
    /// message.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public WebApiClientException(HttpResponseMessage response, Msg message, params object[] args)
                           : base(message.ToString(), GetMessage(message, args)) {
      this.Response = response;
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


    static private string GetMessage(Msg message, params object[] args) {
      return GetResourceMessage(message.ToString(), resourceBaseName, Assembly.GetExecutingAssembly(), args);
    }

    #endregion Methods

  } // class WebApiClientException

} // namespace Empiria.WebApi
