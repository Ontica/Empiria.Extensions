/* Empiria Presentation Framework 2014 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Service-Oriented Framework               System   : Empiria Web Services              *
*  Namespace : Empiria.WebServices                              Assembly : Empiria.WebServices.dll           *
*  Type      : WebApiGlobal                                     Pattern  : Global ASP .NET Class             *
*  Version   : 6.0        Date: 23/Oct/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Defines the methods, properties, and events common to all application objects used by         *
*              Empiria ASP.NET Web Services platform.                                                        *
*                                                                                                            *
********************************* Copyright (c) 2003-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
//using System.Web.Http.Cors;

using Empiria.Security;

namespace Empiria.WebServices {

  public enum HttpErrorCode {
    BadRequest = HttpStatusCode.BadRequest,
    Unauthorized = HttpStatusCode.Unauthorized,
    MovedPermanently = HttpStatusCode.MovedPermanently,
    NotFound = HttpStatusCode.NotFound,
    InternalServerError = HttpStatusCode.InternalServerError,
  }

  public enum HttpOkResponseCode {
    OK = System.Net.HttpStatusCode.OK,
    Created = System.Net.HttpStatusCode.Created,
    NotModified = System.Net.HttpStatusCode.NotModified
  }

  /// <summary>Defines the methods, properties, and events common to all application objects used by
  /// Empiria ASP.NET Web Api Services platform.</summary>
  //[EnableCors("*", "*", "*")]
  public class WebApiController : ApiController {

    public WebApiController() {

    }

    #region Properties

    public bool IsSessionAlive {
      get { return (ExecutionServer.CurrentPrincipal != null); }
    }

    public new EmpiriaUser User {
      get { return EmpiriaUser.Current; }
    }

    #endregion Properties

    #region Methods

    //[HttpOptions, AllowAnonymous]
    //public HttpResponseMessage Options() {
    //  var response = new HttpResponseMessage();
    //  response.StatusCode = HttpStatusCode.OK;

    //  return response;
    //}

    protected void Assert(bool value, string exceptionText) {
      if (!value) {
        try {
          Empiria.Assertion.Assert(false, exceptionText);
        } catch (Exception e) {
          var response = base.Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
          throw new HttpResponseException(response);
        }
      }
    }

    protected void Assert(object value, string name) {
      if (value == null) {
        try {
          Empiria.Assertion.Assert(false, name);
        } catch (Exception e) {
          var response = base.Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
          throw new HttpResponseException(response);
        }
      }
    }

    protected void AssertHeader(string headerName) {
      if (base.Request.Headers.Contains(headerName)) {
        return;
      }
      var e = new WebServicesException(WebServicesException.Msg.RequestHeaderMissed, headerName);
      throw CreateHttpResponseException(e, HttpErrorCode.BadRequest);
    }

    protected void AssertValidModel() {
      if (!this.ModelState.IsValid) {
        //var errors = this.ModelState
        //      .Where(e => e.Value.Errors.Count > 0)
        //      .Select(e => new ValidationError {
        //        Name = e.Key,
        //        Message = e.Value.Errors.First().ErrorMessage,
        //      }).ToArray();

        //var response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
        //response.Content = new ObjectContent<ValidationError[]>(errors, new JsonMediaTypeFormatter());
        //throw new HttpResponseException(response);

        var response = base.Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, this.ModelState);
        throw new HttpResponseException(response);
      }
    }

    protected HttpResponseException CreateHttpResponseException(EmpiriaException e) {
      return CreateHttpResponseException(e, HttpErrorCode.InternalServerError);
    }

    protected HttpResponseException CreateHttpResponseException(EmpiriaException e, HttpErrorCode errorCode) {
      var response = base.Request.CreateErrorResponse((System.Net.HttpStatusCode) errorCode, e);
      e.Publish();
      return new HttpResponseException(response);
    }

    protected object ToJson<T>(T instance, Func<T, object> serializer) where T : IStorable {
      return serializer.Invoke(instance);
    }

    protected IQueryable ToJson<T>(FixedList<T> list, Func<T, object> serializer) where T : IStorable {
      object[] array = new object[list.Count];

      for (int i = 0; i < list.Count; i++) {
        array[i] = serializer.Invoke(list[i]);
      }
      return array.AsQueryable();
    }

    protected IQueryable ToJson<T>(IEnumerable<T> list, Func<T, object> serializer) where T : IStorable {
      List<object> array = new List<object>();
      foreach (T item in list) {
        array.Add(serializer.Invoke(item));
      }
      return array.AsQueryable();
    }

    static private string GetSourceMethodName() {
      MethodBase sourceMethod = new StackFrame(2).GetMethod();
      ParameterInfo[] methodPars = sourceMethod.GetParameters();

      string methodName = String.Format("{0}.{1}", sourceMethod.DeclaringType, sourceMethod.Name);
      methodName += "(";
      for (int i = 0; i < methodPars.Length; i++) {
        methodName += String.Format("{0}{1} {2}", (i != 0) ? ", " : String.Empty,
                                    methodPars[i].ParameterType.Name, methodPars[i].Name);
      }
      methodName += ")";

      return methodName;
    }

    #endregion Methods

  } // class WebApiController

} // namespace Empiria.WebServices
