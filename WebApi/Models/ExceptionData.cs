/* Empiria Service-Oriented Architecture Framework ***********************************************************
*                                                                                                            *
*  Solution  : Empiria SOA Framework                            System   : Empiria Web Api Framework         *
*  Namespace : Empiria.WebApi.Models                            Assembly : Empiria.WebApi.dll                *
*  Type      : ExceptionData                                    Pattern  : Information Holder                *
*  Version   : 1.0        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Contains the data for an exception response.                                                  *
*                                                                                                            *
********************************* Copyright (c) 2014-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Net;
using System.Runtime.Serialization;

using Empiria.Json;

namespace Empiria.WebApi.Models {

  /// <summary>Contains the data for an exception response.</summary>
  [DataContract]
  internal class ExceptionData {

    #region Constructors and parsers

    public ExceptionData(Exception e) : this(ExceptionData.GetHttpStatusCode(e), e) {

    }

    public ExceptionData(HttpErrorCode statusCode, Exception exception) {
      this.HttpStatusCode = (HttpStatusCode) statusCode;
      this.ErrorCode = this.GetErrorCode(exception);
      this.Source = this.GetErrorSource(exception);
      this.Message = exception.Message;
      this.Hint = "This will be the text to help developers.";
      this.Issues = new string[0];
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

    #endregion Properties

    #region Methods

    private string GetErrorSource(Exception exception) {
      if (exception.TargetSite != null) {
        return exception.TargetSite.DeclaringType.FullName + "." + exception.TargetSite.Name;
      } else {
        return exception.GetType().FullName;
      }
    }

    private string GetErrorCode(Exception e) {
      string temp = e.GetType().Name;
      if (e is EmpiriaException) {
        temp += "." + ((EmpiriaException) e).ExceptionTag;
      }
      return temp;
    }

    static private HttpErrorCode GetHttpStatusCode(Exception e) {
      if (e is UnauthorizedException) {
        return HttpErrorCode.Forbidden;
      } else if (e is Security.SecurityException) {
        return HttpErrorCode.Unauthorized;
      } else if (e is ValidationException) {
        return HttpErrorCode.BadRequest;
      } else if (e is ResourceNotFoundException) {
        return HttpErrorCode.NotFound;
      } else if (e is NotImplementedException) {
        return HttpErrorCode.NotImplemented;
      } else {
        return HttpErrorCode.InternalServerError;
      }
    }

    #endregion Methods

  }  // class ExceptionData

} // namespace Empiria.WebApi.Models
