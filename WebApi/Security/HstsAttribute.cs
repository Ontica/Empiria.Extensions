/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api                                    Component : Security Services                       *
*  Assembly : Empiria.WebApi.dll                         Pattern   : Http filter attribute                   *
*  Type     : HstsAttribute                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Action filter attribute to implement HTTP Strict Transport Security (HSTS)                     *
*             and enforce HTTPS connections.                                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

using System.Web;
using System.Web.Http.Filters;
using Empiria.Security;

namespace Empiria.WebApi {

  ///<summary>Action filter attribute to implement HTTP Strict Transport Security (HSTS)
  /// and enforce HTTPS connections.</summary>
  public class HstsAttribute : ActionFilterAttribute {

    #region Properties

    internal int MaxAge {
      get;
    } = (int) TimeSpan.FromDays(365).TotalSeconds;


    internal bool IncludeSubDomains {
      get;
    } = true;


    internal bool Preload {
      get;
    } = false;

    #endregion Properties

    #region Methods

    public override void OnActionExecuted(HttpActionExecutedContext context) {

      if (context.Response == null) {
        base.OnActionExecuted(context);
        return;
      }

      if (!HttpContext.Current.Request.IsSecureConnection) {
        var exception = new SecurityException(SecurityException.Msg.HSTSRequired);

        var model = new ExceptionModel(context.Request, exception);

        context.Response = model.CreateResponse();
      }

      if (!context.Response.Headers.Contains("Strict-Transport-Security")) {
        context.Response.Headers.Add("Strict-Transport-Security", BuildHstsValue());
      }

      base.OnActionExecuted(context);
    }

    #endregion Methods

    #region Helpers

    private string BuildHstsValue() {
      string value = $"max-age={MaxAge}";

      if (IncludeSubDomains) {
        value += "; includeSubDomains";
      }

      if (Preload) {
        value += "; preload";
      }

      return value;
    }

    #endregion Helpers

  }  // class HstsAttribute

}  // namespace Empiria.WebApi
