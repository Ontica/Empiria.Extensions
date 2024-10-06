/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management              Component : Domain Layer                        *
*  Assembly : Empiria.Artifacts.dll                          Pattern   : Information holder                  *
*  Type     : License                                        License   : Please read LICENSE.txt file        *
*                                                                                                            *
*  Summary  : Contains information about a customer's Empiria solution license.                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.Data;

using Empiria.Json;

namespace Empiria.Artifacts {

  /// <summary>Contains information about a customer's Empiria solution license.</summary>
  [DataModel("ECMLicenses", "LicenseId")]
  public class License : BaseObjectLite {

    #region Constructors and parsers

    private License() {

    }


    static public License Parse(int id) {
      return BaseObjectLiteFactory.Parse<License>(id);
    }


    static public License Parse(string licenseNumber) {
      Assertion.Require(licenseNumber, "licenseNumber");

      return BaseObjectLiteFactory.ParseWithFilter<License>($"LicenseNumber = '{licenseNumber}'");
    }


    protected override void OnLoadObjectData(DataRow row) {
      this.LicenseName = (string) row["LicenseName"];
      this.LicenseNumber = (string) row["LicenseNumber"];
      this.Customer = (string) row["Customer"];
      this.LoadSecurityValues((string) row["SecurityValues"]);
    }

    #endregion Constructors and parsers

    #region Properties

    public string Customer {
      get;
      private set;
    }


    public string LicenseName {
      get;
      private set;
    }


    public string LicenseNumber {
      get;
      private set;
    }


    public byte[] Key {
      get;
      private set;
    }


    public byte[] IV {
      get;
      private set;
    }


    public byte[] LKey {
      get;
      private set;
    }

    #endregion Properties

    #region Private methods

    private void LoadSecurityValues(string jsonString) {
      var json = JsonObject.Parse(jsonString);

      this.IV = json.GetList<byte>("IV").ToArray();
      this.Key = json.GetList<byte>("Key").ToArray();
      this.LKey = json.GetList<byte>("LKey").ToArray();
    }

    #endregion Private methods

  }  // class License

} // namespace Empiria.Artifacts
