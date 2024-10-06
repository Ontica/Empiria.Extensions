/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management              Component : Domain Layer                        *
*  Assembly : Empiria.Artifacts.dll                          Pattern   : Information Holder                  *
*  Type     : Environment                                    License   : Please read LICENSE.txt file        *
*                                                                                                            *
*  Summary  : Represents a physical executable context or environment where software solutions runs.         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Data;

namespace Empiria.Artifacts {

  /// <summary>Represents a physical executable context or environment where software solutions runs.</summary>
  [DataModel("ECMEnvironments", "EnvironmentId")]
  public class Environment : BaseObjectLite {

    #region Constructors and parsers

    private Environment() {

    }


    static public Environment Parse(int id) {
      return BaseObjectLiteFactory.Parse<Environment>(id);
    }


    static public Environment Parse(string serialNumber) {
      Assertion.Require(serialNumber, "serialNumber");

      return BaseObjectLiteFactory.ParseWithFilter<Environment>($"SerialNumber = '{serialNumber}'");
    }


    protected override void OnLoadObjectData(DataRow row) {
      this.SerialNumber = (string) row["SerialNumber"];
      this.Name = (string) row["EnvironmentName"];
      this.Description = (string) row["Description"];
      this.Owner = (string) row["Owner"];
    }

    #endregion Constructors and parsers

    #region Properties

    [DataField("SerialNumber")]
    public string SerialNumber {
      get;
      private set;
    }


    [DataField("Name")]
    public string Name {
      get;
      private set;
    }


    [DataField("Description")]
    public string Description {
      get;
      private set;
    }


    [DataField("Owner")]
    public string Owner {
      get;
      private set;
    }

    #endregion Properties

  }  // class Environment

} // namespace Empiria.Artifacts
