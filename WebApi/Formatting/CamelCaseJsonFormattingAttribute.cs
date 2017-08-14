/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi.Formatting                        Assembly : Empiria.WebApi.dll                *
*  Type      : CamelCaseJsonFormattingAttribute                 Pattern  : Attribute class                   *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Place on a web API controller class to convert all responses to camelCase.                    *
*                                                                                                            *
********************************* Copyright (c) 2014-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Empiria.Json;

namespace Empiria.WebApi.Formatting {

  /// <summary>Place on a web API controller class to convert all responses to camelCase.</summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public class CamelCaseJsonFormattingAttribute : Attribute, IControllerConfiguration {

    private bool browserFriendly = false;

    public CamelCaseJsonFormattingAttribute(bool browserFriendly = false) {
      this.browserFriendly = browserFriendly;
    }

    public void Initialize(HttpControllerSettings controllerSettings,
                           HttpControllerDescriptor controllerDescriptor) {
      //remove the existing Json formatter as this is the global formatter and changing any
      //setting on it would effect other controllers too.
      controllerSettings.Formatters.Remove(controllerSettings.Formatters.JsonFormatter);

      JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
      formatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;

      // Support browser output
      if (browserFriendly) {
        var jsonContentTypeHeader = new MediaTypeHeaderValue("text/html");
        formatter.SupportedMediaTypes.Add(jsonContentTypeHeader);
      }

      var jsonSettings = new JsonSerializerSettings();

      jsonSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
      jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

      // Add Empiria System.Data.DataView and System.Data.DataRow serializers
      jsonSettings.Converters.Add(new DataViewConverter());
      jsonSettings.Converters.Add(new DataRowConverter());

      formatter.SerializerSettings = jsonSettings;

      controllerSettings.Formatters.Insert(0, formatter);
    }

  } // class CamelCaseJsonFormattingAttribute

} // namespace Empiria.WebApi.Formatting
