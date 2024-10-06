/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management              Component : Domain Layer                        *
*  Assembly : Empiria.Artifacts.dll                          Pattern   : DTO                                 *
*  Type     : DeploymentID                                   License   : Please read LICENSE.txt file        *
*                                                                                                            *
*  Summary  : Contains data that represents and identifies a client or server application                    *
*             running in a given environment.                                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Artifacts {

  /// <summary>Contains data that represents and identifies a client or server application
  /// running in a given environment.</summary>
  public class DeploymentID {

    #region Fields

    static public readonly string RequestHeaderName = "Empiria-DevOps-DeploymentID";

    #endregion Fields

    #region Properties

    /// <summary>Uniquely identifies a client or server application.</summary>
    public string Application {
      get;
      set;
    }

    /// <summary>Uniquely identifies an execution evironment.
    /// An environment could execute multiple applications.</summary>
    public string Environment {
      get;
      set;
    }

    /// <summary>Uniquely identifies the application's or solution's customer.
    /// The same license can be used in many environments.</summary>
    public string License {
      get;
      set;
    }

    #endregion Properties

    #region Methods

    public void AssertValid() {
      Assertion.Require(this.Application, "application");
      Assertion.Require(this.Environment, "environment");
      Assertion.Require(this.License, "license");
    }

    #endregion Methods

  }  // class DeploymentID

} // namespace Empiria.Artifacts
