/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi.Models                            Assembly : Empiria.WebApi.dll                *
*  Type      : WebApiAuditTrail                                 Pattern  : Service provider                  *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Holds web request and response information and write them to an audit trail log.              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;

using Empiria.Json;
using Empiria.Security;

namespace Empiria.WebApi {

  /// <summary>Holds web request and response information and write them to an audit trail log.</summary>
  internal class WebApiAuditTrail : AuditTrail {

    #region Constructors and parsers

    public WebApiAuditTrail() : this(WebApiRequest.Current) {
      //no-op
    }

    private WebApiAuditTrail(WebApiRequest request): base(request, AuditTrailType.WebApiCall) {

    }

    #endregion Constructors and parsers

    #region Public methods

    public void Write(HttpRequestMessage request, HttpResponseMessage response) {
      Assertion.Require(request, nameof(request));
      Assertion.Require(response, nameof(response));

      this.SetRequest(request);
      this.SetResponse(response);
      base.Write();

      if (response.IsSuccessStatusCode) {
        object value;
        if (request.Properties.TryGetValue("operation", out value)) {
          EmpiriaLog.Operation(this.Operation, (string) value);
        } else {
          EmpiriaLog.Operation(this.Operation, string.Empty);
        }

      } else {
        EmpiriaLog.FailedOperationLog(this.Operation, new Exception(GetErrorResponseMessage(response)));
      }
    }


    internal void Write(HttpRequest request, string operationName,
                        HttpResponse response, Exception exception) {
      Assertion.Require(request, nameof(request));
      Assertion.Require(operationName, nameof(operationName));
      Assertion.Require(response, nameof(response));
      Assertion.Require(exception, nameof(exception));

      this.SetRequest(request, operationName);
      this.SetResponse(response, exception);

      base.Write();
    }

    #endregion Public methods

    #region Private methods

    private JsonObject GetOperationData(Uri requestUri) {
      var json = new JsonObject();

      json.Add("endpoint", requestUri.PathAndQuery);

      return json;
    }

    private string GetOperationName(HttpActionDescriptor actionDescriptor) {
      return actionDescriptor.ControllerDescriptor.ControllerType.Name + "." +
             actionDescriptor.ActionName;
    }

    private string GetOperationName(HttpRequestMessage request) {
      var actionDescriptor = request.GetActionDescriptor();

      if (actionDescriptor != null) {
        return this.GetOperationName(actionDescriptor);
      } else {
        return request.RequestUri.AbsolutePath;
      }
    }

    private int GetReturnedItemsCount(HttpResponseMessage response) {
      var content = response.Content as ObjectContent;

      if (content == null) {
        return 0;
      }
      if (typeof(IBaseResponseModel).IsAssignableFrom(content.ObjectType)) {
        return ((IBaseResponseModel) content.Value).DataItemsCount;
      } else if (typeof(System.Data.DataTable).IsAssignableFrom(content.ObjectType)) {
        return ((System.Data.DataTable) content.Value).Rows.Count;
      } else {
        return 1;
      }
    }


    private string GetErrorResponseMessage(HttpResponseMessage response) {
      var content = response.Content as ObjectContent;

      if (content != null && content.ObjectType == typeof(ExceptionModel)) {
        return ((ExceptionModel) content.Value).Exception.Message;
      }

      if (content != null && content.ObjectType == typeof(System.Web.Http.HttpError)) {
        return ((System.Web.Http.HttpError) content.Value).ExceptionMessage;

      } else {
        return "Ocurrió un problema que no pudo ser determinado.";
      }
    }

    private void SetErrorResponse(HttpResponseMessage response) {
      var content = response.Content as ObjectContent;

      if (content != null && content.ObjectType == typeof(ExceptionModel)) {
        JsonObject jsonObject = ((ExceptionModel) content.Value).GetAuditTrailData();
        base.SetResponse((int) response.StatusCode, 0, jsonObject);
        return;
      }

      string reason = "N/A";
      string source = "N/A";
      if (content != null && content.ObjectType == typeof(System.Web.Http.HttpError)) {
        var s = (System.Web.Http.HttpError) content.Value;
        reason = s.ExceptionMessage;
        source = s.ExceptionType;
      } else {
        reason = response.ReasonPhrase;
      }

      var json = new JsonObject();

      json.AddIfValue("statusCode", (int) response.StatusCode);
      json.AddIfValue("statusName", response.StatusCode.ToString());
      json.AddIfValue("reason", reason);
      json.AddIfValue("source", source);

      base.SetResponse((int) response.StatusCode, 0, json);
    }


    private void SetOkResponse(HttpResponseMessage response) {
      int returnedItems = this.GetReturnedItemsCount(response);

      base.SetResponse((int) response.StatusCode, returnedItems, JsonObject.Empty);
    }


    private void SetRequest(HttpRequestMessage request) {
      string eventTag = request.Method.Method;
      string operationName = this.GetOperationName(request);
      JsonObject operationData = this.GetOperationData(request.RequestUri);

      base.SetOperationInfo(eventTag, operationName, operationData);
    }


    private void SetRequest(HttpRequest request, string operationName) {
      string eventTag = request.HttpMethod;
      JsonObject operationData = this.GetOperationData(request.Url);

      base.SetOperationInfo(eventTag, operationName, operationData);
    }


    private void SetResponse(HttpResponseMessage response) {
      if (response.IsSuccessStatusCode) {
        this.SetOkResponse(response);
      } else {
        this.SetErrorResponse(response);
      }
    }

    private void SetResponse(HttpResponse response, Exception exception) {
      var json = new JsonObject();

      json.Add("statusCode", response.StatusCode);
      json.Add("statusName", response.Status.ToString());
      json.Add("reason", exception.Message);
      json.Add("source", exception.Source);

      base.SetResponse(response.StatusCode, 0, json);
    }

    #endregion Private methods

  } // class WebApiAuditTrail

} // namespace Empiria.WebApi
