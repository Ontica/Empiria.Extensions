/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management              Component : Domain Layer                        *
*  Assembly : Empiria.Artifacts.dll                          Pattern   : Root aggregate                      *
*  Type     : Deployment                                     License   : Please read LICENSE.txt file        *
*                                                                                                            *
*  Summary  : Holds information about an Empiria application deployment for an specific environment and      *
*             customer. Deployment is the root aggregate of the Empiria DevOps domain model.                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Data;

using Empiria.Data;

namespace Empiria.Artifacts {

  /// <summary>Holds information about an Empiria application deployment for an specific environment and
  /// customer. Deployment is the root aggregate of the Empiria DevOps domain model.</summary>
  [DataModel("ECMDeployments", "DeploymentId")]
  public class Deployment : BaseObjectLite {

    #region Constructors and parsers

    private Deployment() {

    }

    static public Deployment Parse(DeploymentID deploymentID) {
      Assertion.Require(deploymentID, "deploymentID");

      var dataOperation = DataOperation.Parse("getECMDeploymentWithIDValues",
                                              deploymentID.Application, deploymentID.Environment,
                                              deploymentID.License);

      return BaseObjectLiteFactory.Parse<Deployment>(dataOperation);
    }


    protected override void OnLoadObjectData(DataRow row) {
      this.Application = Application.Parse((int) row["ApplicationId"]);
      this.Environment = Environment.Parse((int) row["EnvironmentId"]);
      this.License = License.Parse((int) row["LicenseId"]);

      this.Name = (string) row["DeploymentName"];
      this.Description = (string) row["Description"];
    }

    #endregion Constructors and parsers

    #region Properties

    public string Name {
      get;
      private set;
    }


    public string Description {
      get;
      private set;
    }


    /// <summary>Uniquely identifies a client or server application.</summary>
    internal Application Application {
      get;
      private set;
    }


    /// <summary>Uniquely identifies an execution evironment. An environment could execute
    /// multiple applications belonging to the same software solution.</summary>
    public Environment Environment {
      get;
      private set;
    }


    /// <summary>Uniquely identifies the application's or solution's customer.
    /// The same customer or license can be related to many execution environments.</summary>
    internal License License {
      get;
      private set;
    }

    #endregion Properties

    #region Methods

    #endregion Methods

  }  // class Deployment

} // namespace Empiria.Artifacts
