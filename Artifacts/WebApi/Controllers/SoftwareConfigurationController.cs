/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management          Component : Web Api                                 *
*  Assembly : Empiria.Artifacts.WebApi.dll               Pattern   : Web Api Controller                      *
*  Type     : SoftwareConfigurationController            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web methods for software deployment configuration.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Net.Http;
using System.Web.Http;

using Empiria.WebApi;

namespace Empiria.Artifacts.WebApi {

  /// <summary>Web methods for software deployment configuration.</summary>
  public class SoftwareConfigurationController : WebApiController {

    #region Web Apis

    /// <summary>Creates a serial number for a specific physical server.</summary>
    /// <param name="hardwareCode">The code assigned to the physical hardware.</param>
    /// <remarks>An Empiria-DevOps-DeploymentID request header must be supplied.</remarks>
    [HttpPost, AllowAnonymous]
    [Route("v1/artifacts/serial-number")]
    public SingleObjectModel CreateSerialNumber([FromBody] string hardwareCode) {
      try {
        base.RequireBody(hardwareCode);

        Deployment deployment = GetDeployment(this.Request);

        var cryptographer = new DevOpsCryptographer(deployment);

        string serialNumber = cryptographer.CreateSerialNumber(hardwareCode);

        return new SingleObjectModel(this.Request, serialNumber);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpGet, AllowAnonymous]
    [Route("v1/artifacts/app/{applicationUID}")]
    public SingleObjectModel GetApplication([FromUri] string applicationUID) {
      try {
        var app = Application.Parse(applicationUID);

        return new SingleObjectModel(this.Request, app);

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpGet, AllowAnonymous]
    [Route("v1/artifacts/environment/{environmentUID}")]
    public SingleObjectModel GetEnvironment([FromUri] string environmentUID) {
      try {
        var environment = Environment.Parse(environmentUID);

        return new SingleObjectModel(this.Request, environment);

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpGet, AllowAnonymous]
    [Route("v1/artifacts/license/{licenseUID}")]
    public SingleObjectModel GetLicense([FromUri] string licenseUID) {
      try {
        var license = License.Parse(licenseUID);

        return new SingleObjectModel(this.Request, license);

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    /// <summary>Gets a protected string encrypted according to the caller rules.</summary>
    /// <param name="plainText">The string to be encrypted.</param>
    /// <remarks>An Empiria-DevOps-DeploymentID request header must be supplied.</remarks>
    [HttpPost, AllowAnonymous]
    [Route("v1/artifacts/protect-string")]
    public SingleObjectModel ProtectString([FromBody] string plainText) {
      try {
        base.RequireBody(plainText);

        Deployment deployment = GetDeployment(this.Request);

        var cryptographer = new DevOpsCryptographer(deployment);

        string encryptedValue = cryptographer.Encrypt(plainText);

        return new SingleObjectModel(this.Request, encryptedValue);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion Web Apis

    #region Helpers

    private Deployment GetDeployment(HttpRequestMessage request) {
      DeploymentID deploymentID = base.GetRequestHeader<DeploymentID>(DeploymentID.RequestHeaderName);

      return Deployment.Parse(deploymentID);
    }

    #endregion Helpers

  }  // class SoftwareConfigurationController

}  // namespace Empiria.Artifacts.WebApi
