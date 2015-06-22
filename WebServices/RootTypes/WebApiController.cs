/* Empiria Presentation Framework 2015 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Service-Oriented Framework               System   : Empiria Web Services              *
*  Namespace : Empiria.WebServices                              Assembly : Empiria.WebServices.dll           *
*  Type      : WebApiController                                 Pattern  : API Controller                    *
*  Version   : 6.5        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Base class for all API controllers for Empiria applications and systems.                      *
*                                                                                                            *
********************************* Copyright (c) 2003-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

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

  /// <summary>Base class for all API controllers for Empiria applications and systems.</summary>
  public abstract class WebApiController : ApiController {

    protected WebApiController() {

    }

    #region Properties

    public bool IsSessionAlive {
      get {
        return (ExecutionServer.CurrentPrincipal != null);
      }
    }

    public new EmpiriaUser User {
      get {
        return EmpiriaUser.Current;
      }
    }

    #endregion Properties

    #region Public Methods

    protected void Assert(bool value, string exceptionText) {
      if (!value) {
        try {
          Assertion.Assert(false, exceptionText);
        } catch (Exception e) {
          throw WebApiException(HttpErrorCode.BadRequest, e);
        }
      }
    }

    protected void AssertHeader(string headerName) {
      if (base.Request.Headers.Contains(headerName)) {
        return;
      }
      throw WebApiException(HttpErrorCode.BadRequest,
                            new WebServicesException(WebServicesException.Msg.RequestHeaderMissed, headerName));
    }

    protected void AssertValue(object value, string name) {
      if (value == null) {
        throw WebApiException(HttpErrorCode.BadRequest,
                              new WebServicesException(WebServicesException.Msg.RequestValueMissed, name));
      }
    }

    protected void AssertValidModel() {
      if (!this.ModelState.IsValid) {
        throw WebApiException(this.ModelState);
      }
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

    protected HttpResponseException WebApiException(HttpErrorCode httpErrorCode, Exception exception) {
      var response = base.Request.CreateErrorResponse((System.Net.HttpStatusCode) httpErrorCode,
                                                      exception.Message);
      Messaging.Publisher.Publish(exception);

      return new HttpResponseException(response);
    }

    #endregion Public Methods

    #region Private Methods

    private HttpResponseException WebApiException(ModelStateDictionary modelStateDictionary) {
      var response = base.Request.CreateErrorResponse(HttpStatusCode.BadRequest, this.ModelState);

      var exception = new HttpResponseException(response);
      Messaging.Publisher.Publish(exception);

      return exception;
    }

    #endregion Private Methods

  } // class WebApiController

} // namespace Empiria.WebServices
