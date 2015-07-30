/* Empiria Service-Oriented Architecture Framework ***********************************************************
*                                                                                                            *
*  Solution  : Empiria SOA Framework                            System   : Empiria Web Api Framework         *
*  Namespace : Empiria.WebApi.Models                            Assembly : Empiria.WebApi.dll                *
*  Type      : ExceptionModel                                   Pattern  : Web Api Response Model            *
*  Version   : 1.0        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Contains the data for an exception response.                                                  *
*                                                                                                            *
********************************* Copyright (c) 2014-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http.ExceptionHandling;

using Empiria.Json;

namespace Empiria.WebApi.Models {

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

      json.Add(new JsonItem("statusCode", (int) this.Data.HttpStatusCode));
      json.Add(new JsonItem("errorCode", this.Data.ErrorCode));
      json.Add(new JsonItem("errorSource", this.Data.Source));
      json.Add(new JsonItem("errorMessage", this.Data.Message));
      json.Add(new JsonItem("errorHint", this.Data.Hint));
      if (this.Data.Issues.Length != 0) {
        json.Add(new JsonItem("issues", this.Data.Issues));
      }
      if (this.Data.HttpStatusCode == System.Net.HttpStatusCode.InternalServerError) {
        json.Add(new JsonItem("exception", this.Exception));
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

      var exceptionData = new ExceptionData(exception);
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

} // namespace Empiria.WebApi.Models
