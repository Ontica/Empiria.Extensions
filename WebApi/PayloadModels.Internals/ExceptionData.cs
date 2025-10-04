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
using Empiria.Json;

namespace Empiria.WebApi.Internals {

  /// <summary>Contains the Data part used to build an ExceptionResponseModel.</summary>
  [DataContract]
  internal class ExceptionData {

    #region Constructor

    internal ExceptionData(Exception exception) {
      Assertion.Require(exception, nameof(exception));

      HttpStatusCode = (HttpStatusCode) GetHttpStatusCode(exception);
      ErrorCode = GetErrorCode(exception);
      Source = GetErrorSource(exception);
      Message = exception.Message;
      Hint = GetHint(exception);
      Issues = GetIssues(exception);
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
        return (HttpStatusCode == HttpStatusCode.InternalServerError ||
                HttpStatusCode == HttpStatusCode.ServiceUnavailable);
      }
    }

    #endregion Properties

    #region Methods

    private string GetErrorCode(Exception e) {
      string temp = e.GetType().Name;

      if (e is EmpiriaException empiriaEx) {
        temp = $"{temp}.{empiriaEx.ExceptionTag}";
      }

      return temp;
    }


    private string GetErrorSource(Exception exception) {
      if (exception.TargetSite != null) {
        return exception.TargetSite.DeclaringType.FullName + "." + exception.TargetSite.Name;
      } else {
        return exception.GetType().FullName;
      }
    }


    private string GetHint(Exception e) {
      if (e is WebApiException webApiEx) {
        return webApiEx.Hint;

      } else if (e is ServiceException) {
        return "Please contact the system administrator to check that all connections " +
               "and services are running.";

      } else if (e is ResourceNotFoundException) {
        return "Please check each of the url parameters to point to valid resources.";

      } else if (e is ValidationException) {
        return "Please check the request's body because it has wrong data.";

      } else if (e is JsonDataException) {
        return "Please check the request's body because the JSON payload is invalid.";

      } else if (e is Ontology.OntologyException) {
        return "Please contact the system administrator because there are " +
               "system configuration or database referential integrity issues.";

      } else if (e is Security.SecurityException securityEx) {

        if (securityEx.DenyService) {
          return "The access was denied because lack of compliance with security polices.";
        }

        return "Please check the request's body because the server detected " +
               "content that could represent a security concern.";

      } else if (this.IsInternalServerError) {
        return "Please contact technical support with the requestId Guid on " +
               "hand in order to help them track the issue.";

      } else {
        return "There is no hint defined for this kind of error.";
      }
    }


    static internal HttpErrorCode GetHttpStatusCode(Exception e) {
      if (e is WebApiException webApiEx) {
        return webApiEx.ErrorCode;

      } else if (e is Security.SecurityException securityEx) {

        if (securityEx.DenyService) {
          return HttpErrorCode.Unauthorized;
        }

        return HttpErrorCode.UnprocessableContent;

      } else if (e is ValidationException) {
        return HttpErrorCode.BadRequest;

      } else if (e is JsonDataException) {
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
      if (exception is WebApiException webApiEx) {
        return webApiEx.RequestIssues;
      } else {
        return new string[0];
      }
    }

    #endregion Methods

  }  // class ExceptionData

} // namespace Empiria.WebApi.Internals
