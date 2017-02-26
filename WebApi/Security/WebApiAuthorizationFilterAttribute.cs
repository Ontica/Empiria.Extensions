/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi.Models                            Assembly : Empiria.WebApi.dll                *
*  Type      : WebApiAuthorizationFilterAttribute               Pattern  : Http Filter                       *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : ASP .NET Web API filter to handle authorization using Empiria security claims.                *
*                                                                                                            *
********************************* Copyright (c) 2014-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using Empiria.Security;

namespace Empiria.WebApi {

  // Defines Web API security claims for client applications and users
  public enum WebApiClaimType {
    ClientApp_Controller,
    ClientApp_Method,
    User_Role,
  }

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
          ClientApplication.Current.AssertClaim(SecurityClaimType.WebApiController, this.ClaimValue,
                                 "The API key doesn't have execution permissions over this controller.");
          return;

        case WebApiClaimType.ClientApp_Method:
          ClientApplication.Current.AssertClaim(SecurityClaimType.WebApiMethod, this.ClaimValue,
                                 "The API key doesn't have execution permissions over this method.");
          return;

        case WebApiClaimType.User_Role:
          EmpiriaPrincipal.Current.AssertClaim(SecurityClaimType.UserRole, this.ClaimValue,
                   String.Format("This functionality can be executed only by users playing the role '{0}'.",
                                  this.ClaimValue));
          return;

        default:
          throw Assertion.AssertNoReachThisCode();
      }
    }

    #endregion Methods

  }  // class WebApiAuthorizationFilterAttribute

}  // namespace Empiria.WebApi
