/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Core Services                        Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Immutable data holder                 *
*  Type     : ExceptionData                                License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Contains the Data part used to build an ExceptionResponseModel.                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Net;
using System.Runtime.Serialization;

namespace Empiria.WebApi.Internals {

  /// <summary>Contains the Data part used to build an ExceptionResponseModel.</summary>
  [DataContract]
  internal class ExceptionData {

    #region Constructor

    internal ExceptionData(Exception exception) {
      Assertion.Require(exception, "exception");

      this.HttpStatusCode = (HttpStatusCode) GetHttpStatusCode(exception);
      this.ErrorCode = GetErrorCode(exception);
      this.Source = GetErrorSource(exception);
      this.Message = GetErrorMessage(exception);
      this.Hint = GetHint(exception);
      this.Issues = GetIssues(exception);
    }

    #endregion Constructor

    #region Properties

    public HttpStatusCode HttpStatusCode {
      get;
    }


    [DataMember(Name = "statusCode")]
    public int StatusCode {
      get {
        return (int) this.HttpStatusCode;
      }
    }


    [DataMember(Name = "errorCode")]
    public string ErrorCode {
      get;
    }


    [DataMember(Name = "errorSource")]
    public string Source {
      get; internal set;
    }


    [DataMember(Name = "errorMessage")]
    public string Message {
      get;
    }


    [DataMember(Name = "errorHint")]
    public string Hint {
      get;
    }


    [DataMember(Name = "requestIssues")]
    public string[] Issues {
      get;
    }


    public bool IsInternalServerError {
      get {
        return (this.HttpStatusCode == HttpStatusCode.InternalServerError ||
                this.HttpStatusCode == HttpStatusCode.ServiceUnavailable);
      }
    }

    #endregion Properties

    #region Methods

    private string GetErrorCode(Exception e) {
      string temp = e.GetType().Name;
      if (e is EmpiriaException) {
        temp += "." + ((EmpiriaException) e).ExceptionTag;
      }
      return temp;
    }


    private string GetErrorMessage(Exception exception) {
      if (!this.IsInternalServerError) {
        return exception.Message;
      } else if (ExecutionServer.IsDevelopmentServer) {
        return exception.Message;
      } else {
        return "We are sorry. Something was wrong processing the request.";
      }
    }


    private string GetErrorSource(Exception exception) {
      if (exception.TargetSite != null) {
        return exception.TargetSite.DeclaringType.FullName + "." + exception.TargetSite.Name;
      } else {
        return exception.GetType().FullName;
      }
    }


    private string GetHint(Exception e) {
      if (e is WebApiException) {
        return ((WebApiException) e).Hint;

      } else if (e is ResourceNotFoundException) {
        return "Please check each of the url parameters to point to valid resources.";

      } else if (e is ServiceException) {
        return "Please contact the system administrator to check all connections and services.";

      } else if (this.IsInternalServerError) {
        return "Please contact technical support with the requestId Guid on " +
               "hand in order to help them track the issue.";

      } else {
        return "There is not hint defined for this kind of error.";
      }
    }


    static internal HttpErrorCode GetHttpStatusCode(Exception e) {
      if (e is WebApiException) {
        return ((WebApiException) e).ErrorCode;

      } else if (e is Security.SecurityException securityEx) {

        if (securityEx.DenyService) {
          return HttpErrorCode.Unauthorized;
        }

        return HttpErrorCode.UnprocessableContent;

      } else if (e is ValidationException) {
        return HttpErrorCode.BadRequest;

      } else if (e is Json.JsonDataException) {
        return HttpErrorCode.BadRequest;

      } else if (e is AssertionFailsException) {
        return HttpErrorCode.BadRequest;

      } else if (e is ResourceNotFoundException) {
        return HttpErrorCode.NotFound;

      } else if (e is ServiceException) {
        return HttpErrorCode.ServiceUnavailable;

      } else if (e is Ontology.OntologyException) {
        return HttpErrorCode.NotFound;

      } else if (e is ResourceConflictException) {
        return HttpErrorCode.Conflict;

      } else if (e is NotImplementedException) {
        return HttpErrorCode.NotImplemented;

      } else {
        return HttpErrorCode.InternalServerError;
      }
    }


    static private string[] GetIssues(Exception exception) {
      if (exception is WebApiException) {
        return ((WebApiException) exception).RequestIssues;
      } else {
        return new string[0];
      }
    }

    #endregion Methods

  }  // class ExceptionData

} // namespace Empiria.WebApi.Internals
