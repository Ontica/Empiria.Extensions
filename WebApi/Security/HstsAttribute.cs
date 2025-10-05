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

using System.Net.Http;
using System.Net.Http.Headers;

using System.Web;
using System.Web.Http.Filters;

using Empiria.Security;

namespace Empiria.WebApi {

  ///<summary>Action filter attribute to implement HTTP Strict Transport Security (HSTS)
  /// and enforce HTTPS connections.</summary>
  public class HstsAttribute : ActionFilterAttribute {

    #region Methods

    public override void OnActionExecuted(HttpActionExecutedContext context) {

      if (context.Response == null) {
        base.OnActionExecuted(context);
        return;
      }

      if (!HttpContext.Current.Request.IsSecureConnection) {
        context.Response = BuildHstsRequiredResponse(context);
      }

      SetHstsHeader(context.Response.Headers);

      base.OnActionExecuted(context);
    }

    #endregion Methods

    #region Helpers

    private HttpResponseMessage BuildHstsRequiredResponse(HttpActionExecutedContext context) {
      var exception = new SecurityException(SecurityException.Msg.HSTSRequired);

      var model = new ExceptionModel(context.Request, exception);

      return model.CreateResponse();
    }


    private void SetHstsHeader(HttpResponseHeaders headers) {

      headers.Remove("Strict-Transport-Security");
      headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
    }

    #endregion Helpers

  }  // class HstsAttribute

}  // namespace Empiria.WebApi
