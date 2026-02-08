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
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;

using Empiria.Json;
using Empiria.Security;
using Empiria.Storage;

namespace Empiria.WebApi {


  public class BulkOperationCommand {

    public string[] Items {
      get; set;
    }

  }  // class BulkOperationCommand



  public class MessageFields {

    public string Message {
      get; set;
    } = "No se indicó el motivo.";

  }  // class MessageFields



  /// <summary>Defines the methods, properties, and events common to all Web API controller
  ///types used by Empiria ASP.NET Web API platform.</summary>
  public class WebApiController : ApiController {

    static public readonly bool IsPassThroughServer = ConfigurationData.Get("IsPassThroughServer", false);


    public WebApiController() {
      // no-op
    }

    #region Public Methods


    protected string GetApplicationKeyFromHeader() {
      return this.GetRequestHeader<string>("ApplicationKey");
    }


    protected HttpResponseException CreateHttpException(Exception exception) {
      if (exception is HttpResponseException ex1) {
        return ex1;
      }

      ExceptionModel model = new ExceptionModel(base.Request, exception);

      var response = model.CreateResponse();

      return new HttpResponseException(response);
    }


    protected IClientApplication GetClientApplication() {
      if (ExecutionServer.IsAuthenticated) {
        return ExecutionServer.CurrentPrincipal.ClientApp;
      } else {
        return this.GetClientApplicationFromRequestHeader();
      }
    }


    protected string GetClientIpAddress() {
      var request = this.Request;

      if (request.Properties.ContainsKey("MS_HttpContext")) {
        return ((HttpContextWrapper) request.Properties["MS_HttpContext"]).Request.UserHostAddress;

      } else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name)) {
        var prop = (RemoteEndpointMessageProperty) request.Properties[RemoteEndpointMessageProperty.Name];

        return prop.Address;

      } else if (HttpContext.Current != null) {
        return HttpContext.Current.Request.UserHostAddress;

      } else {
        return String.Empty;
      }
    }

    protected string GetFormDataFromHttpRequest(string fieldName) {
      Assertion.Require(fieldName, nameof(fieldName));

      var httpRequest = HttpContext.Current.Request;

      Assertion.Require(httpRequest, "httpRequest");

      var form = httpRequest.Form;

      Assertion.Require(form, "The request must be of type 'multipart/form-data'.");

      Assertion.Require(form[fieldName], $"'{fieldName}' form field is required");

      return form[fieldName];
    }


    protected T GetFormDataFromHttpRequest<T>(string fieldName) where T : new() {
      string json = GetFormDataFromHttpRequest(fieldName);

      var instance = new T();

      return JsonConverter.Merge<T>(json, instance);
    }


    protected T GetFromBody<T>(object body, string itemPath) {
      Assertion.Require(body, "body");
      Assertion.Require(itemPath, "itemPath");

      var json = JsonObject.Parse(body);

      return json.Get<T>(itemPath);
    }


    protected JsonObject GetJsonFromBody(object body) {
      this.RequireBody(body);

      return JsonObject.Parse(body);
    }


    protected InputFile GetInputFileFromHttpRequest() {
      return GetInputFileFromHttpRequest(string.Empty);
    }


    protected InputFile GetInputFileFromHttpRequest(string applicationContentType) {
      var httpRequest = HttpContext.Current.Request;

      Assertion.Require(httpRequest, "httpRequest");
      Assertion.Require(httpRequest.Files.Count == 1,
                        "The request does not have the file to be imported.");

      HttpPostedFile file = httpRequest.Files[0];

      return new InputFile(
        file.InputStream,
        file.ContentType,
        file.FileName,
        applicationContentType
      );
    }


    protected InputFileCollection GetInputFilesFromHttpRequest(string applicationContentType) {
      Assertion.Require(applicationContentType, nameof(applicationContentType));

      var httpRequest = HttpContext.Current.Request;

      Assertion.Require(httpRequest, nameof(httpRequest));

      Assertion.Require(httpRequest.Files.Count > 0,
                        "The request does not have any file to be imported.");

      var files = new InputFileCollection();

      foreach (string key in httpRequest.Files.Keys) {
        HttpPostedFile file = httpRequest.Files[key];

        var inputFile = new InputFile(
            file.InputStream,
            file.ContentType,
            file.FileName,
            applicationContentType);

        files.Insert(key, inputFile);
      }

      return files;
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

        List<string> exceptionList = this.GetModelStateErrorList();

        throw new WebApiException(WebApiException.Msg.BadBody, exceptionList.ToArray());
      }
    }


    protected void RequireHeader(string headerName) {
      Assertion.Require(headerName, "headerName");

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


    protected void SetOperation(string operation) {
      base.Request.Properties.Add("operation", operation);
    }

    public WebApiException UnauthorizedResource(string resourceName, object value) {
      return new WebApiException(WebApiException.Msg.UnauthorizedResource, resourceName, value);
    }

    #endregion Public Methods

    #region Private methods

    private IClientApplication GetClientApplicationFromRequestHeader() {
      const string HEADER_NAME = "ApplicationKey";

      this.RequireHeader(HEADER_NAME);

      string clientApplicationKey = this.GetRequestHeader<string>(HEADER_NAME);

      return AuthenticationService.AuthenticateClientApp(clientApplicationKey);
    }

    #endregion Private methods

  } // class WebApiController

} // namespace Empiria.WebApi
