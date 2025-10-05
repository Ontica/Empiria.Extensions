/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Core Services                        Component : Security services                     *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Http Module                           *
*  Type     : DynamicCorsModule                            License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Http module that configures CORS response headers dynamically.                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

using System.Linq;
using System.Web;

namespace Empiria.WebApi {

  /// <summary>Http module that configures CORS response headers dynamically.</summary>
  public sealed class DynamicCorsModule : IHttpModule {

    static private readonly string[] _allowedHosts;

    #region Methods

    static DynamicCorsModule() {
      var allowedHosts = ConfigurationData.Get("AllowedHosts", string.Empty)
                                          .Split(';');

      _allowedHosts = allowedHosts.Select(x => EmpiriaString.TrimAll(x))
                                  .Where(x => !string.IsNullOrWhiteSpace(x))
                                  .ToArray();
    }


    public void Dispose() {
      // no-op
    }


    public void Init(HttpApplication context) {
      context.BeginRequest += OnContextBeginRequest;
    }

    #endregion Methods

    #region Helpers

    static private bool IsAllowed(string origin) {
      var uri = new Uri(origin);

      if (WebApiApplication.HSTS_ENABLED && uri.Scheme.ToLower() != "https") {
        return false;
      }

      if (_allowedHosts.Length == 0) {
        return true;
      }

      if (_allowedHosts.Contains(uri.Host, StringComparer.OrdinalIgnoreCase)) {
        return true;
      }

      return false;
    }


    static private void OnContextBeginRequest(object sender, EventArgs e) {

      var context = HttpContext.Current;

      string origin = context.Request.Headers["Origin"];

      if (string.IsNullOrWhiteSpace(origin)) {
        origin = TryDetermineRequestOrigin(context.Request);
      }

      if (string.IsNullOrWhiteSpace(origin) || !IsAllowed(origin)) {
        return;
      }

      SetDefaultHeaders(context.Response);
      context.Response.AddHeader("Access-Control-Allow-Origin", origin);
    }


    static private void SetDefaultHeaders(HttpResponse response) {

      response.Headers.Remove("Access-Control-Allow-Credentials");
      response.AddHeader("Access-Control-Allow-Credentials", "true");

      response.Headers.Remove("Access-Control-Allow-Methods");
      response.AddHeader("Access-Control-Allow-Methods",
                         "GET, POST, PUT, PATCH, DELETE, OPTIONS");

      response.Headers.Remove("Access-Control-Allow-Headers");
      response.AddHeader("Access-Control-Allow-Headers",
                         "Content-Type, Accept, Authorization, X-Requested-With");

      response.Headers.Remove("Access-Control-Max-Age");
      response.Headers.Add("Access-Control-Max-Age", "3600");
    }



    static private string TryDetermineRequestOrigin(HttpRequest request) {
      string origin = request.Headers["Referer"];

      if (string.IsNullOrEmpty(origin)) {
        return null;
      }

      var uri = new Uri(origin);

      string scheme = uri.Scheme.ToLower();

      if (WebApiApplication.HSTS_ENABLED && scheme != "https") {
        return null;
      }

      if (_allowedHosts.Length == 0) {
        return $"{scheme}://{uri.Host}";
      }

      if (_allowedHosts.Contains(uri.Host, StringComparer.OrdinalIgnoreCase)) {
        return $"{scheme}://{uri.Host}";
      }

      return null;
    }

    #endregion Helpers

  }  // class DynamicCorsModule

}  // namespace Empiria.WebApi
