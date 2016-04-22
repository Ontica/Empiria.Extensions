/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi.Models                            Assembly : Empiria.WebApi.dll                *
*  Type      : ExceptionData                                    Pattern  : Information Holder                *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Contains the data for an exception response.                                                  *
*                                                                                                            *
********************************* Copyright (c) 2014-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Net;
using System.Runtime.Serialization;

namespace Empiria.WebApi.Models {

  /// <summary>Contains the data for an exception response.</summary>
  [DataContract]
  internal class ExceptionData {

    #region Constructors and parsers

    public ExceptionData(Exception exception) {
      Assertion.AssertObject(exception, "exception");

      this.HttpStatusCode = (HttpStatusCode) ExceptionData.GetHttpStatusCode(exception);
      this.ErrorCode = this.GetErrorCode(exception);
      this.Source = this.GetErrorSource(exception);
      this.Message = this.GetErrorMessage(exception);
      this.Hint = this.GetHint(exception);
      if (exception is WebApiException) {
        this.Issues = ((WebApiException) exception).RequestIssues;
      } else {
        this.Issues = new string[0];
      }
    }

    #endregion Constructors and parsers

    #region Properties

    [DataMember(Name = "statusCode")]
    public HttpStatusCode HttpStatusCode {
      get;
      private set;
    }

    [DataMember(Name = "errorCode")]
    public string ErrorCode {
      get;
      private set;
    }

    [DataMember(Name = "errorSource")]
    public string Source {
      get;
      internal set;
    }

    [DataMember(Name = "errorMessage")]
    public string Message {
      get;
      private set;
    }

    [DataMember(Name = "errorHint")]
    public string Hint {
      get;
      internal set;
    }

    [DataMember(Name = "requestIssues")]
    public string[] Issues {
      get;
      internal set;
    }

    public bool IsInternalServerError {
      get {
        return (this.HttpStatusCode == System.Net.HttpStatusCode.InternalServerError);
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

      } else if (this.IsInternalServerError) {
        return "Please contact technical support with the requestId Guid on " +
               "hand in order to help them track the issue.";
      } else if (e is ResourceNotFoundException) {
        return "Please check each of the url parameters to point to valid resources.";

      } else {
        return "There is not hint defined for this kind of error.";
      }
    }

    static internal HttpErrorCode GetHttpStatusCode(Exception e) {
      if (e is WebApiException) {
        return ((WebApiException) e).ErrorCode;

      } else if (e is Security.SecurityException) {
        return HttpErrorCode.Unauthorized;

      } else if (e is ValidationException) {
        return HttpErrorCode.BadRequest;

      } else if (e is AssertionFailsException) {
        return HttpErrorCode.BadRequest;

      } else if (e is ResourceNotFoundException) {
        return HttpErrorCode.NotFound;

      } else if (e is ResourceConflictException) {
        return HttpErrorCode.Conflict;

      } else if (e is NotImplementedException) {
        return HttpErrorCode.NotImplemented;

      } else {
        return HttpErrorCode.InternalServerError;
      }
    }

    #endregion Methods

  }  // class ExceptionData

} // namespace Empiria.WebApi.Models
