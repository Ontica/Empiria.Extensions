/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Web Api Core Services                     Component : Core Types                               *
*  Assembly : Empiria.WebApi.dll                        Pattern   : WebApiGlobal Type                        *
*  Type     : WebApiApplication                         License   : Please read LICENSE.txt file             *
*                                                                                                            *
*  Summary  : Empiria HttpApplication object for Http WebApi services                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

using Empiria.Json;

namespace Empiria.WebApi {

  /// <summary>Empiria HttpApplication object for Http WebApi services.</summary>
  public class WebApiApplication : WebApiGlobal {

    #region Fields

    static internal readonly bool HSTS_ENABLED = ConfigurationData.Get("Hsts.Enabled", true);

    #endregion Fields

    #region Methods

    protected override void Application_Start(object sender, EventArgs e) {
      base.Application_Start(sender, e);

      ConfigureSecuritySettings();

      ExecutionServer.Preload();

      Register();
    }


    static public void Register() {
      RegisterGlobalHandlers(GlobalConfiguration.Configuration);

      GlobalConfiguration.Configure(WebApiConfig.RegisterCallback);

      RegisterFormatters(GlobalConfiguration.Configuration);

      RegisterGlobalFilters(GlobalConfiguration.Configuration);
    }

    #endregion Methods

    #region Helpers

    static private void ConfigureSecuritySettings() {
      if (HSTS_ENABLED) {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
      }
    }


    static private void RegisterFormatters(HttpConfiguration config) {
      Newtonsoft.Json.JsonSerializerSettings settings = JsonConverter.JsonSerializerDefaultSettings();

      config.Formatters.JsonFormatter.SerializerSettings = settings;
    }


    static private void RegisterGlobalHandlers(HttpConfiguration config) {

      config.MessageHandlers.Add(new AuditTrailHandler());

      config.MessageHandlers.Add(new WebApiResponseHandler());

      config.MessageHandlers.Add(new WebApiSafeRequestBodyHandler());

      // config.MessageHandlers.Add(new StorageContextHandler());

      config.Services.Replace(typeof(IExceptionHandler), new WebApiExceptionHandler());
    }


    static private void RegisterGlobalFilters(HttpConfiguration config) {

      // Denies anonymous access to every controller without the AllowAnonymous attribute
      if (!ExecutionServer.IsPassThroughServer) {
        config.Filters.Add(new AuthorizeAttribute());
      }

      if (HSTS_ENABLED) {
        config.Filters.Add(new HstsAttribute());
      }
    }

    #endregion Helpers

  }  // class WebApiApplication



  static internal class WebApiConfig {

    #region Methods

    static internal void RegisterCallback(HttpConfiguration config) {

      // To enable attribute routing
      config.MapHttpAttributeRoutes();


      // To configure convention-based routing
      RegisterWebApiRoutes(config);

    }

    #endregion Methods

    #region Helpers

    static private void RegisterHttp404ErrorHandlerRoute(HttpRouteCollection routes) {
      routes.MapHttpRoute(
        name: "Error404Handler",
        routeTemplate: "{*url}",
        defaults: new {
          controller = "Security",
          action = "Http404ErrorHandler"
        }
      );
    }

    static private void RegisterWebApiRoutes(HttpConfiguration config) {
      RegisterHttp404ErrorHandlerRoute(config.Routes);
    }

    #endregion Helpers

  }  // class WebApiConfig

}  // namespace Empiria.WebApi
