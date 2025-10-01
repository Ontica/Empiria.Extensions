﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi                                   Assembly : Empiria.WebApi.dll                *
*  Type      : HttpErrorCode                                    Pattern  : Enumeration                       *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Restricts the Http error status codes to a minimal set for use as Empiria web api responses.  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.Net;

namespace Empiria.WebApi {

  /// <summary>Restricts the Http error status codes to a minimal set for use as
  /// Empiria web api responses.</summary>
  public enum HttpErrorCode {

    /// <summary>
    /// Equivalent to HTTP status 400. Empiria.WebApi.HttpErrorCode.BadRequest indicates
    /// that the request could not be understood by the server.
    /// Empiria.WebApi.Net.HttpErrorCode.BadRequest is sent when no other error is applicable,
    /// or if the exact error is unknown or does not have its own error code.
    /// </summary>
    BadRequest = HttpStatusCode.BadRequest,

    /// <summary>
    /// Equivalent to HTTP status 401. Empiria.WebApi.HttpErrorCode.Unauthorized indicates
    /// that the requested resource requires authentication. The WWW-Authenticate header contains
    /// the details of how to perform the authentication.
    /// </summary>
    Unauthorized = HttpStatusCode.Unauthorized,

    /// <summary>
    /// Equivalent to HTTP status 403. Empiria.WebApi.HttpErrorCode.Forbidden indicates
    /// that the server refuses to fulfill the request.
    /// </summary>
    Forbidden = HttpStatusCode.Forbidden,

    ///<summary>
    /// Equivalent to HTTP status 404. Empiria.WebApi.HttpErrorCode.NotFound indicates
    /// that the requested resource does not exist on the server.
    /// </summary>
    NotFound = HttpStatusCode.NotFound,

    /// <summary>
    /// Equivalent to HTTP status 405. Empiria.WebApi.HttpErrorCode.MethodNotAllowed indicates
    /// that the request method (e.g, POST or GET) is not allowed on the requested resource.
    /// </summary>
    MethodNotAllowed = HttpStatusCode.MethodNotAllowed,


    /// <summary>Equivalent to HTTP status 409. Empiria.WebApi.HttpErrorCode.Conflict indicates
    /// that the request could not be carried out because of a conflict on the server.
    /// </summary>
    Conflict = HttpStatusCode.Conflict,


    /// <summary>Equivalent to HTTP status 422. Empiria.WebApi.HttpErrorCode.UnprocessableContent
    /// indicates that the request is well-formed and syntactically correct, but could not
    /// be processed, e.g. due to dangerous inputs or semantical errors.
    /// </summary>
    UnprocessableContent = 422,

    /// <summary>
    /// Equivalent to HTTP status 500. Empiria.WebApi.HttpErrorCode.InternalServerError indicates
    /// that a generic error has occurred on the server.
    /// </summary>
    InternalServerError = HttpStatusCode.InternalServerError,

    /// <summary>
    /// Equivalent to HTTP status 501. Empiria.WebApi.HttpErrorCode.NotImplemented indicates
    /// that the server does not support the requested function.
    /// </summary>
    NotImplemented = HttpStatusCode.NotImplemented,

    /// <summary>Equivalent to HTTP status 503. Empiria.WebApi.HttpErrorCode.ServiceUnavailable indicates
    /// that the server is temporarily unavailable, usually due to high load or maintenance.
    /// </summary>
    ServiceUnavailable = HttpStatusCode.ServiceUnavailable,

  }  // enum HttpErrorCode

} // namespace Empiria.WebApi
