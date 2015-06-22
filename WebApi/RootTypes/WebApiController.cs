/* Empiria Service-Oriented Architecture Framework ***********************************************************
*                                                                                                            *
*  Solution  : Empiria SOA Framework                            System   : Empiria Web Api Framework         *
*  Namespace : Empiria.WebApi                                   Assembly : Empiria.WebApi.dll                *
*  Type      : WebApiController                                 Pattern  : Base Controller                   *
*  Version   : 1.0        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Defines the methods, properties, and events common to all Web API controller types            *
*              used by Empiria ASP.NET Web API platform.                                                     *
*                                                                                                            *
********************************* Copyright (c) 2014-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using Empiria.Security;
using Empiria.WebApi.Models;

namespace Empiria.WebApi {

  /// <summary>Defines the methods, properties, and events common to all Web API controller
  ///types used by Empiria ASP.NET Web API platform.</summary>
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

    protected HttpResponseException CreateHttpException(Exception exception) {
      ExceptionModel model = new ExceptionModel(base.Request, exception);

      var response = model.CreateResponse();

      return new HttpResponseException(response);
    }

    protected HttpResponseException CreateHttpException(HttpErrorCode errorCode, Exception exception) {
      ExceptionModel model = new ExceptionModel(base.Request, errorCode, exception);

      var response = model.CreateResponse();

      return new HttpResponseException(response);
    }

    protected NameValueCollection GetQueryStringAsCollection() {
      string queryString = Uri.UnescapeDataString(base.Request.RequestUri.Query);
      char itemsSeparator = '&';
      char valuesSeparator = '=';

      string[] stringItems = queryString.Split(itemsSeparator);
      NameValueCollection pars = new NameValueCollection(stringItems.Length);

      for (int i = 0; i < stringItems.Length; i++) {
        pars.Add(stringItems[i].Split(valuesSeparator)[0], stringItems[i].Split(valuesSeparator)[1]);
      }
      return pars;
    }

    public void RequireBody(IDataModel model) {
      if (model == null) {
        throw CreateHttpException(HttpErrorCode.BadRequest,
                                  new WebApiException(WebApiException.Msg.BodyMissed, model.GetType().FullName));
      }
      model.Validate();
    }

    protected void RequireHeader(string headerName) {
      if (base.Request.Headers.Contains(headerName)) {
        return;
      }
      throw CreateHttpException(HttpErrorCode.BadRequest,
                                new WebApiException(WebApiException.Msg.RequestHeaderMissed, headerName));
    }

    public void RequireResource(string value, string resourceName) {
      if (String.IsNullOrWhiteSpace(value)) {
        throw CreateHttpException(HttpErrorCode.BadRequest,
                                  new WebApiException(WebApiException.Msg.ResourceMissed, resourceName));
      }
    }

    #endregion Public Methods

    #region Private Methods

    private HttpResponseException WebApiModelStateException(ModelStateDictionary modelStateDictionary) {
      var response = base.Request.CreateErrorResponse(HttpStatusCode.BadRequest, this.ModelState);

      return new HttpResponseException(response);
    }

    #endregion Private Methods

  } // class WebApiController

} // namespace Empiria.WebApi
