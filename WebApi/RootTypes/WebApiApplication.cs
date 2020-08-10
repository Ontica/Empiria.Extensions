using System;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;

using Empiria.Json;

namespace Empiria.WebApi {

  static internal class WebApiConfig {

    #region Public methods

    static internal void RegisterCallback(HttpConfiguration config) {

      // To enable CORS
      var cors = new EnableCorsAttribute("*", "*", "*");
      config.EnableCors(cors);

      // config.SuppressHostPrincipal();

      // To enable attribute routing
      config.MapHttpAttributeRoutes();


      // To configure convention-based routing
      WebApiConfig.RegisterWebApiRoutes(config);

    }

    #endregion Public methods

    #region Private methods

    static private void RegisterHttp404ErrorHandlerRoute(HttpRouteCollection routes) {
      routes.MapHttpRoute(
        name: "Error404Handler",
        routeTemplate: "{*url}",
        defaults: new {
          controller = "Security", action = "Http404ErrorHandler"
        }
      );
    }

    static private void RegisterWebApiRoutes(HttpConfiguration config) {
      WebApiConfig.RegisterHttp404ErrorHandlerRoute(config.Routes);
    }

    #endregion Private methods

  }  // class WebApiConfig

  public class WebApiApplication : WebApiGlobal {

    static public void Register() {
      RegisterGlobalHandlers(GlobalConfiguration.Configuration);

      GlobalConfiguration.Configure(WebApiConfig.RegisterCallback);

      RegisterFormatters(GlobalConfiguration.Configuration);

      RegisterGlobalFilters(GlobalConfiguration.Configuration);

    }


    protected override void Application_Start(object sender, EventArgs e) {
      base.Application_Start(sender, e);

      Register();
    }


    static private void RegisterFormatters(HttpConfiguration config) {
      var settings = new Newtonsoft.Json.JsonSerializerSettings();

      settings.Formatting = Newtonsoft.Json.Formatting.Indented;

      settings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

      settings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();


      // Empiria Json converters

      settings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
      settings.Converters.Add(new DateTimeConverter());
      settings.Converters.Add(new ValueObjectConverter());
      settings.Converters.Add(new DataViewConverter());
      settings.Converters.Add(new DataRowConverter());

      // Third party converters
      settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

      config.Formatters.JsonFormatter.SerializerSettings = settings;
      config.Formatters.Remove(config.Formatters.XmlFormatter);         // Remove Xml formatter
    }


    static private void RegisterGlobalHandlers(HttpConfiguration config) {

      config.MessageHandlers.Add(new AuditTrailHandler());

      config.Services.Replace(typeof(IExceptionHandler), new WebApiExceptionHandler());

      config.MessageHandlers.Add(new WebApiResponseHandler());

      // config.MessageHandlers.Add(new StorageContextHandler());

    }


    static private void RegisterGlobalFilters(HttpConfiguration config) {
      // Denies anonymous access to every controller without the AllowAnonymous attribute
      if (!ExecutionServer.IsPassThroughServer) {
        config.Filters.Add(new AuthorizeAttribute());
      }
    }

  }  // class WebApiApplication

}  // namespace Empiria.WebApi
