/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Core Services                        Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Response Model                        *
*  Type     : ExceptionModel                               License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : The data model returned on exception responses.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Net.Http;
using System.Runtime.Serialization;

using Empiria.Json;

using Empiria.WebApi.Internals;

namespace Empiria.WebApi {

  /// <summary>The data model returned on exception responses.</summary>
  [DataContract]
  internal class ExceptionModel : BaseResponseModel<ExceptionData> {

    #region Constructor

    internal ExceptionModel(HttpRequestMessage request, Exception exception)
                          : base(request,
                                 GetResponseModelStatus(exception),
                                 GetExceptionData(exception, request),
                                 "Empiria.ExceptionData") {
      this.Exception = exception;
    }

    #endregion Constructor

    #region Properties

    public Exception Exception {
      get;
    }


    public override LinksCollectionModel Links {
      get {
        return new LinksCollectionModel(this);
      }
    }

    #endregion Properties

    #region Public methods

    internal HttpResponseMessage CreateResponse() {
      return base.Request.CreateResponse<ExceptionModel>(this.Data.HttpStatusCode, this);
    }


    internal JsonObject GetAuditTrailData() {
      var json = new JsonObject();

      json.Add("statusCode", (int) this.Data.HttpStatusCode);
      json.Add("errorCode", this.Data.ErrorCode);
      json.Add("errorSource", this.Data.Source);
      json.Add("errorMessage", this.Data.Message);
      json.Add("errorHint", this.Data.Hint);
      if (this.Data.Issues.Length != 0) {
        json.Add("issues", this.Data.Issues);
      }
      if (this.Data.HttpStatusCode == System.Net.HttpStatusCode.InternalServerError) {
        json.Add("exception", this.Exception);
        if (this.Exception.InnerException != null) {
          json.Add("innerException", this.Exception.InnerException);
        }
        if (this.Exception.StackTrace != null) {
          json.Add("stackTrace", this.Exception.StackTrace);
        }
      }
      return json;
    }

    #endregion Methods

    #region Helpers

    static private string GetErrorSource(HttpRequestMessage request) {
      Assertion.Require(request, "request");

      var actionDescriptor = request.GetActionDescriptor();

      if (actionDescriptor != null) {
        return actionDescriptor.ControllerDescriptor.ControllerType.Name + "." +
               actionDescriptor.ActionName;
      } else {
        return request.RequestUri.AbsoluteUri;
      }
    }


    static private ExceptionData GetExceptionData(Exception exception, HttpRequestMessage request) {
      Assertion.Require(exception, "exception");
      Assertion.Require(request, "request");

      ExceptionData exceptionData;

      if (ExecutionServer.IsDevelopmentServer &&
          ExceptionData.GetHttpStatusCode(exception) == HttpErrorCode.InternalServerError) {
        exceptionData = new ExceptionExtendedData(exception);
      } else {
        exceptionData = new ExceptionData(exception);
      }

      exceptionData.Source = ExceptionModel.GetErrorSource(request);

      return exceptionData;
    }


    static private ResponseStatus GetResponseModelStatus(Exception exception) {
      Assertion.Require(exception, "exception");

      if (exception is WebApiException) {
        return ResponseStatus.Invalid_Request;

      } else if (exception is AssertionFailsException) {
        return ResponseStatus.Invalid_Request;

      } else if (exception is ValidationException) {
        return ResponseStatus.Invalid_Request;

      } else if (exception is ResourceNotFoundException) {
        return ResponseStatus.Invalid_Request;

      } else if (exception is ResourceConflictException) {
        return ResponseStatus.Invalid_Request;

      } else if (exception is ServiceException) {
        return ResponseStatus.Unavailable;

      } else if (exception is Security.SecurityException) {
        return ResponseStatus.Denied;

      } else if (exception is NotImplementedException) {
        return ResponseStatus.Unavailable;

      } else {
        return ResponseStatus.Error;
      }
    }

    #endregion Helpers

  }  // class ExceptionModel

} // namespace Empiria.WebApi
