/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi                                   Assembly : Empiria.WebApi.dll                *
*  Type      : WebApiController                                 Pattern  : Base Controller                   *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Defines the methods, properties, and events common to all Web API controller types            *
*              used by Empiria ASP.NET Web API platform.                                                     *
*                                                                                                            *
********************************* Copyright (c) 2014-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Http;

using Empiria.Json;
using Empiria.Security;

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

    protected ClientApplication GetClientApplication() {
      if (ExecutionServer.IsAuthenticated) {
        return ExecutionServer.CurrentPrincipal.ClientApp;
      } else {
        return this.GetClientApplicationFromRequestHeader();
      }
    }


    public T GetFromBody<T>(object body, string itemPath) {
      Assertion.AssertObject(body, "body");
      Assertion.AssertObject(itemPath, "itemPath");

      var json = JsonObject.Parse(body);

      return json.Get<T>(itemPath);
    }


    protected List<string> GetModelStateErrorList() {
      List<string> exceptionList = new List<string>();

      foreach (var modelState in base.ModelState.Values) {
        foreach (var error in modelState.Errors) {
          exceptionList.Add(error.Exception.Message);
        }
      }

      return exceptionList;
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


    public T GetRequestHeader<T>(string requestHeaderName) {
      this.RequireHeader(requestHeaderName);

      string rawValue = base.Request.Headers.GetValues(requestHeaderName).FirstOrDefault();

      if (rawValue == null) {
        throw new WebApiException(WebApiException.Msg.NullHeaderValue, requestHeaderName);
      }

      try {
        return EmpiriaString.ConvertTo<T>(rawValue);
      } catch (Exception e) {
        throw new WebApiException(WebApiException.Msg.InvalidHeaderValue, e, requestHeaderName);
      }
    }


    public void RequireBody(object model) {
      if (model == null) {
        throw new WebApiException(WebApiException.Msg.BodyMissed);
      }
      if (!base.ModelState.IsValid) {

        List<string> exceptionList = this.GetModelStateErrorList();;

        throw new WebApiException(WebApiException.Msg.BadBody, exceptionList.ToArray());
      }
    }

    protected void RequireHeader(string headerName) {
      Assertion.AssertObject(headerName, "headerName");

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
      if (value == 0) {
        throw new WebApiException(WebApiException.Msg.ResourceMissed, resourceName);
      }
    }

    public WebApiException UnauthorizedResource(string resourceName, object value) {
      return new WebApiException(WebApiException.Msg.UnauthorizedResource, resourceName, value);
    }

    #endregion Public Methods

    #region Private methods

    private ClientApplication GetClientApplicationFromRequestHeader() {
      const string HEADER_NAME = "ApplicationKey";

      this.RequireHeader(HEADER_NAME);

      string clientApplicationKey = this.GetRequestHeader<string>(HEADER_NAME);

      return ClientApplication.ParseActive(clientApplicationKey);
    }

    #endregion Private methods

  } // class WebApiController

} // namespace Empiria.WebApi
