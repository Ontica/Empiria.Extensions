﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi                                   Assembly : Empiria.WebApi.dll                *
*  Type      : HttpOkResponseCode                               Pattern  : Enumeration                       *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Restricts the Http OK status codes to a minimal set for use as Empiria web api responses.     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Net;

namespace Empiria.WebApi {

  /// <summary>Restricts the Http OK status codes to a minimal set for use as
  /// Empiria web api responses.</summary>
  public enum HttpOkResponseCode {

    /// <summary>
    /// Equivalent to HTTP status 200. Empiria.WebApi.HttpOkResponseCode.OK indicates that
    /// the request succeeded and that the requested information is in the response.
    /// This is the most common status code to receive.
    /// </summary>
    OK = HttpStatusCode.OK,

    /// <summary>
    ///  Equivalent to HTTP status 201. Empiria.Net.HttpOkResponseCode.Created indicates that
    ///  the request resulted in a new resource created before the response was sent.
    /// </summary>
    Created = HttpStatusCode.Created,

  }  // enum HttpOkResponseCode

} // namespace Empiria.WebApi
