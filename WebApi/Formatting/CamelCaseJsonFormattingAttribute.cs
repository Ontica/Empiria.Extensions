/* Empiria Service-Oriented Architecture Framework ***********************************************************
*                                                                                                            *
*  Solution  : Empiria SOA Framework                            System   : Empiria Web Api Framework         *
*  Namespace : Empiria.WebApi.Formatting                        Assembly : Empiria.WebApi.dll                *
*  Type      : CamelCaseJsonFormattingAttribute                 Pattern  : Attribute class                   *
*  Version   : 1.0        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Place on a web API controller class to convert all responses to camel case.                   *
*                                                                                                            *
********************************* Copyright (c) 2014-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Empiria.WebApi.Formatting {

  /// <summary>Place on a web API controller class to convert all responses to camel case.</summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public class CamelCaseJsonFormattingAttribute : Attribute, IControllerConfiguration {

    public void Initialize(HttpControllerSettings controllerSettings,
                           HttpControllerDescriptor controllerDescriptor) {
      //remove the existing Json formatter as this is the global formatter and changing any
      //setting on it would effect other controllers too.
      controllerSettings.Formatters.Remove(controllerSettings.Formatters.JsonFormatter);

      JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
      formatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;

      var jsonSettings = new JsonSerializerSettings();

      jsonSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
      jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
      // Add System.Data.DataView serializer
      jsonSettings.Converters.Add(new Empiria.Json.DataViewConverter());
      jsonSettings.Converters.Add(new Empiria.Json.DataRowConverter());

      formatter.SerializerSettings = jsonSettings;

      controllerSettings.Formatters.Insert(0, formatter);
    }

  } // class PascalCaseAttribute


} // namespace Empiria.WebApi
