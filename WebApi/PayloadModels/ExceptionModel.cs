/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Payload Models                        *
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

  [DataContract]
  internal class ExceptionModel : BaseResponseModel<ExceptionData> {

    #region Constructors and parsers

    internal ExceptionModel(HttpRequestMessage request, Exception exception)
                          : base(request, ExceptionModel.GetResponseModelStatus(exception),
                                 ExceptionModel.GetExceptionData(exception, request),
                                 "Empiria.ExceptionData") {
      this.Exception = exception;
    }

    #endregion Constructors and parsers

    #region Public properties

    public Exception Exception {
      get;
      private set;
    }


    public override LinksCollectionModel Links {
      get {
        var links = new LinksCollectionModel(this);

        //links.Add("http://empiria.ws/documentation/errors/348712", LinkRelation.Help);

        return links;
      }
    }

    #endregion Public properties

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

    #endregion Public methods

    #region Private methods

    static private string GetErrorSource(HttpRequestMessage request) {
      Assertion.AssertObject(request, "request");

      var actionDescriptor = request.GetActionDescriptor();

      if (actionDescriptor != null) {
        return actionDescriptor.ControllerDescriptor.ControllerType.Name + "." +
               actionDescriptor.ActionName;
      } else {
        return request.RequestUri.AbsoluteUri;
      }
    }


    static private ExceptionData GetExceptionData(Exception exception, HttpRequestMessage request) {
      Assertion.AssertObject(exception, "exception");
      Assertion.AssertObject(request, "request");

      ExceptionData exceptionData = null;
      if (ExecutionServer.IsDevelopmentServer &&
          ExceptionData.GetHttpStatusCode(exception) == HttpErrorCode.InternalServerError) {
        exceptionData = new ExceptionExtendedData(exception);
      }  else {
        exceptionData = new ExceptionData(exception);
      }
      exceptionData.Source = ExceptionModel.GetErrorSource(request);

      return exceptionData;
    }


    static private ResponseStatus GetResponseModelStatus(HttpErrorCode errorCode) {
      switch (errorCode) {
        case HttpErrorCode.BadRequest:
          return ResponseStatus.Invalid_Request;

        case HttpErrorCode.Forbidden:
          return ResponseStatus.Over_Limit;

        case HttpErrorCode.InternalServerError:
          return ResponseStatus.Error;

        case HttpErrorCode.MethodNotAllowed:
          return ResponseStatus.Invalid_Request;

        case HttpErrorCode.NotFound:
          return ResponseStatus.Invalid_Request;

        case HttpErrorCode.NotImplemented:
          return ResponseStatus.Unavailable;

        case HttpErrorCode.ServiceUnavailable:
          return ResponseStatus.Unavailable;

        case HttpErrorCode.Unauthorized:
          return ResponseStatus.Denied;

        default:
          throw Assertion.AssertNoReachThisCode();
      }
    }


    static private ResponseStatus GetResponseModelStatus(Exception exception) {
      Assertion.AssertObject(exception, "exception");

      if (exception is WebApiException) {
        return ResponseStatus.Invalid_Request;

      } else if (exception is Security.SecurityException) {
        return ResponseStatus.Denied;

      } else if (exception is ValidationException) {
        return ResponseStatus.Invalid_Request;

      } else if (exception is ResourceNotFoundException) {
        return ResponseStatus.Invalid_Request;

      } else if (exception is ResourceConflictException) {
        return ResponseStatus.Invalid_Request;

      } else if (exception is AssertionFailsException) {
        return ResponseStatus.Invalid_Request;

      } else if (exception is NotImplementedException) {
        return ResponseStatus.Unavailable;

      } else {
        return ResponseStatus.Error;
      }
    }

    #endregion Private methods

  }  // class ExceptionModel

} // namespace Empiria.WebApi
