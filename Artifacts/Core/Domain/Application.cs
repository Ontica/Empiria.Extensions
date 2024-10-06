/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management              Component : Domain Layer                        *
*  Assembly : Empiria.Artifacts.dll                          Pattern : Information holder                    *
*  Type     : Application                                    License : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Represents an executable client or server software application.                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Data;

namespace Empiria.Artifacts {

  /// <summary>Represents an executable client or server software application.</summary>
  [DataModel("ECMApplications", "ApplicationId")]
  public class Application : BaseObjectLite {

    #region Constructors and parsers

    private Application() {

    }


    static internal Application Parse(int id) {
      return BaseObjectLiteFactory.Parse<Application>(id);
    }


    static public Application Parse(string applicationKey) {
      Assertion.Require(applicationKey, "applicationKey");

      return BaseObjectLiteFactory.ParseWithFilter<Application>($"ApplicationKey = '{applicationKey}'");
    }


    protected override void OnLoadObjectData(DataRow row) {
      this.AppKey = (string) row["ApplicationKey"];
      this.Name = (string) row["ApplicationName"];
      this.Solution = (string) row["Solution"];
      this.Description = (string) row["Description"];
    }

    #endregion Constructors and parsers

    #region Properties

    [DataField("ApplicationKey")]
    public string AppKey {
      get;
      private set;
    }


    [DataField("ApplicationName")]
    public string Name {
      get;
      private set;
    }

    [DataField("Solution")]
    public string Solution {
      get;
      private set;
    }


    [DataField("Description")]
    public string Description {
      get;
      private set;
    }


    #endregion Properties

    #region Methods

    #endregion Methods

  }  // class Application

} // namespace Empiria.Artifacts
