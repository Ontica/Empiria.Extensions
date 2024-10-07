/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Oracle Data Handler                        Component : Test Helpers                            *
*  Assembly : Empiria.Data.Oracle.Tests.dll              Pattern   : Integration tests                       *
*  Type     : OracleEmpiriaCoreIntegrationTests          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Integration tests between Oracle and Empiria Core Framework.                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Xunit;

namespace Empiria.Data.Handlers.Tests {

  /// <summary>Integration tests between Oracle and Empiria Core Framework.</summary>
  public class OracleEmpiriaCoreIntegrationTests {

    #region Facts

    [Fact]
    public void Should_Create_And_Remove_One_Record() {
      var dataOperation = DataOperation.Parse("SELECT COUNT(*) FROM COF_CUENTA");

      var startCount = DataReader.GetScalar<decimal>(dataOperation);

      dataOperation = DataOperation.Parse("INSERT INTO COF_CUENTA VALUES(99999, 99999, 99999)");

      DataWriter.Execute(dataOperation);

      dataOperation = DataOperation.Parse("SELECT COUNT(*) FROM COF_CUENTA");

      var endCount = DataReader.GetScalar<decimal>(dataOperation);

      Assert.Equal(startCount + 1, endCount);

      dataOperation = DataOperation.Parse("DELETE FROM COF_CUENTA WHERE ID_CUENTA=99999 AND ID_MAYOR=99999 AND ID_CUENTA_ESTANDAR = 99999");

      DataWriter.Execute(dataOperation);

      dataOperation = DataOperation.Parse("SELECT COUNT(*) FROM COF_CUENTA");

      endCount = DataReader.GetScalar<decimal>(dataOperation);

      Assert.Equal(startCount, endCount);
    }


    [Fact]
    public void Should_Log_An_Event() {
      var dataOperation = DataOperation.Parse("SELECT COUNT(*) FROM LogEntries");

      var entriesCount = DataReader.GetScalar<decimal>(dataOperation);

      EmpiriaLog.Info("Logging a text message");

      Assert.Equal(entriesCount + 1, DataReader.GetScalar<decimal>(dataOperation));
    }

    #endregion Facts

  }  // class OracleEmpiriaCoreIntegrationTests

}  // namespace Empiria.Data.Handlers.Tests
