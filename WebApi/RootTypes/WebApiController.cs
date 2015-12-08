﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi                                   Assembly : Empiria.WebApi.dll                *
*  Type      : WebApiController                                 Pattern  : Base Controller                   *
*  Version   : 1.0                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Defines the methods, properties, and events common to all Web API controller types            *
*              used by Empiria ASP.NET Web API platform.                                                     *
*                                                                                                            *
********************************* Copyright (c) 2014-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Specialized;
using System.Web.Http;

using Empiria.WebApi.Models;

namespace Empiria.WebApi {

  /// <summary>Defines the methods, properties, and events common to all Web API controller
  ///types used by Empiria ASP.NET Web API platform.</summary>
  public abstract class WebApiController : ApiController {

    protected WebApiController() {

    }

    #region Public Methods

    protected HttpResponseException CreateHttpException(Exception exception) {
      if (exception is HttpResponseException) {
        return (HttpResponseException) exception;
      }

      ExceptionModel model = new ExceptionModel(base.Request, exception);

      var response = model.CreateResponse();

      return new HttpResponseException(response);
    }

    protected NameValueCollection GetQueryStringAsCollection() {
      string queryString = Uri.UnescapeDataString(base.Request.RequestUri.Query);
      char itemsSeparator = '&';
      char valuesSeparator = '=';

      string[] stringItems = queryString.Split(itemsSeparator);
      NameValueCollection pars = new NameValueCollection(stringItems.Length);

      for (int i = 0; i < stringItems.Length; i++) {
        pars.Add(stringItems[i].Split(valuesSeparator)[0], stringItems[i].Split(valuesSeparator)[1]);
      }
      return pars;
    }

    public void RequireBody(object model) {
      if (model == null) {
        throw new WebApiException(WebApiException.Msg.BodyMissed);
      }
    }

    public void RequireBody(IDataModel model) {
      if (model == null) {
        throw new WebApiException(WebApiException.Msg.BodyMissed);
      }
      model.AssertValid();
    }

    protected void RequireHeader(string headerName) {
      if (!base.Request.Headers.Contains(headerName)) {
        throw new WebApiException(WebApiException.Msg.RequestHeaderMissed, headerName);
      }
    }

    public void RequireResource(string value, string resourceName) {
      if (String.IsNullOrWhiteSpace(value)) {
        throw new WebApiException(WebApiException.Msg.ResourceMissed, resourceName);
      }
    }

    public void RequireResource(int value, string resourceName) {
      if (value == 0 || value == -1 || value == -2) {
        throw new WebApiException(WebApiException.Msg.ResourceMissed, resourceName);
      }
    }

    #endregion Public Methods

  } // class WebApiController

} // namespace Empiria.WebApi