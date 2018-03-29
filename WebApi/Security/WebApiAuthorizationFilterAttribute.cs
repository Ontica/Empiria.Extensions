/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Security                                     Component : Web Api Security Services             *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Http Filter Attribute                 *
*  Type     : WebApiAuthorizationFilterAttribute           License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : ASP .NET Web API filter to handle authorization using Empiria security claims.                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using Empiria.Security;
using Empiria.Security.Claims;

namespace Empiria.WebApi {

  /// <summary>ASP .NET Web API filter to handle authorization using Empiria security claims.</summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
  public class WebApiAuthorizationFilterAttribute : AuthorizationFilterAttribute {

    #region Constructors and parsers

    public WebApiAuthorizationFilterAttribute(WebApiClaimType claimType, string claimValue) {
      Assertion.AssertObject(claimValue, "claimValue");

      this.ClaimType = claimType;
      this.ClaimValue = claimValue;
    }

    #endregion Constructors and parsers

    #region Properties

    public WebApiClaimType ClaimType {
      get;
      private set;
    }

    public string ClaimValue {
      get;
      private set;
    }

    #endregion Properties

    #region Methods

    public override void OnAuthorization(HttpActionContext actionContext) {
      base.OnAuthorization(actionContext);

      switch (this.ClaimType) {

        case WebApiClaimType.ClientApp_Controller:
          ClaimsService.EnsureClaim(ClientApplication.Current,
                                    Security.Claims.ClaimType.WebApiController,
                                    this.ClaimValue,
                                    "The API key doesn't have execution permissions over this controller.");
          return;

        case WebApiClaimType.ClientApp_Method:
          ClaimsService.EnsureClaim(ClientApplication.Current,
                                    Security.Claims.ClaimType.WebApiMethod,
                                    this.ClaimValue,
                                    "The API key doesn't have execution permissions over this method.");
          return;

        case WebApiClaimType.User_Role:
          ClaimsService.EnsureClaim(EmpiriaUser.Current,
                                    Security.Claims.ClaimType.UserRole,
                                    this.ClaimValue,
                    $"This functionality can be executed only by users playing the role '{this.ClaimValue}'.");
          return;

        default:
          throw Assertion.AssertNoReachThisCode();
      }

    }

    #endregion Methods

  }  // class WebApiAuthorizationFilterAttribute

}  // namespace Empiria.WebApi
