/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Oracle Data Handler                        Component : Test Helpers                            *
*  Assembly : Empiria.Data.Oracle.Tests.dll              Pattern   : Integration tests                       *
*  Type     : OracleEmpiriaCoreIntegrationTests          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Integration tests between Oracle and Empiria Core Framework.                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Xunit;

using Empiria.Security;
using Empiria.Services.Authentication;

using Empiria.Tests;

namespace Empiria.Data.Handlers.Tests {

  /// <summary>Integration tests between Oracle and Empiria Core Framework.</summary>
  public class OracleEmpiriaCoreIntegrationTests {

    #region Initialization

    public OracleEmpiriaCoreIntegrationTests() {
      TestsCommonMethods.Authenticate();
    }

    #endregion Initialization

    #region Facts

    [Fact]
    public void Should_Create_A_User_Session() {
      var fields = new AuthenticationFields {
        AppKey = "dev_SICOFIN_Backend_JEcz5AaKxuST8KG8zYQgQFJEtQehjCssHZBJ4Jws",
        IpAddress = "192.168.1.1",
        UserID = "José Manuel Cota",
        Password = "d1Jkstra@0308"
      };

      using (var usecases = AuthenticationUseCases.UseCaseInteractor()) {
        var token = usecases.GenerateAuthenticationToken(fields);

        Assert.NotNull(token);

        var protectedPassword = Cryptographer.GetSHA256(fields.Password);
        protectedPassword = Cryptographer.GetSHA256(protectedPassword + token);

        fields.Password = protectedPassword;

        var principal = usecases.Authenticate(fields);

        Assert.NotNull(principal);
      }
    }


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
