/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Oracle Data Handler                        Component : Test Helpers                            *
*  Assembly : Empiria.Data.Oracle.Tests.dll              Pattern   : Unit tests                              *
*  Type     : OracleDataOperationTests                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Unit tests for data reading operations using DataOperation through Oracle Data Handler.        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using Xunit;

namespace Empiria.Data.Handlers.Tests {

  /// <summary>Unit tests for data reading operations using DataOperation through Oracle Data Handler.</summary>
  public class OracleDataOperationTests {

    #region Facts

    [Fact]
    public void Should_Read_StoredProcedure_Parameters() {
      var dataOperation = DataOperation.Parse(TestingConstants.DATA_READER_SQL);

      var oracleMethods = new OracleMethods();

      var parameterValues = new object[] { TestingConstants.SESSION_TOKEN };

      var pars = oracleMethods.GetParameters(dataOperation.DataSource.Source,
                                            "getUserSession", parameterValues);

      Assert.Equal(2, pars.Length);
    }


    [Fact]
    public void Should_Call_StoredProc_Using_DataOp_and_GetDataReader() {
      var dataOperation = DataOperation.Parse("getUserSession", TestingConstants.SESSION_TOKEN);

      var oracleMethods = new OracleMethods();

      using (var dataReader = oracleMethods.GetDataReader(dataOperation)) {
        Assert.NotNull(dataReader);

        dataReader.Read();

        Assert.Equal(TestingConstants.SESSION_TOKEN, dataReader["SessionToken"]);
      }
    }


    [Fact]
    public void Should_Call_StoredProc_Using_DataOp_and_GetDataRow() {
      var dataOperation = DataOperation.Parse("getUserSession", TestingConstants.SESSION_TOKEN);

      var oracleMethods = new OracleMethods();

      var dataRow = oracleMethods.GetDataRow(dataOperation);

      Assert.NotNull(dataRow);
      Assert.Equal(TestingConstants.SESSION_TOKEN, dataRow["SessionToken"]);
    }


    [Fact]
    public void Should_Call_StoredProc_Using_DataOp_and_GetDataTable() {
      var dataOperation = DataOperation.Parse("getUserSession", TestingConstants.SESSION_TOKEN);

      var oracleMethods = new OracleMethods();

      var dataTable = oracleMethods.GetDataTable(dataOperation, "getUserSession");

      Assert.NotNull(dataTable);
      Assert.Equal(1, dataTable.Rows.Count);
      Assert.Equal(TestingConstants.SESSION_TOKEN, dataTable.Rows[0]["SessionToken"]);
    }


    [Fact]
    public void Should_Call_StoredProc_Using_DataOp_and_GetDataView() {
      var dataOperation = DataOperation.Parse("getUserSession", TestingConstants.SESSION_TOKEN);

      var oracleMethods = new OracleMethods();

      var dataView = oracleMethods.GetDataView(dataOperation, string.Empty, string.Empty);

      Assert.NotEmpty(dataView);
      Assert.True(dataView.Count == 1);
      Assert.Equal(TestingConstants.SESSION_TOKEN, dataView[0]["SessionToken"]);
    }


    [Fact]
    public void Should_Call_StoredProc_Using_DataOp_and_GetFieldValue() {
      var dataOperation = DataOperation.Parse("getUserSession", TestingConstants.SESSION_TOKEN);

      var oracleMethods = new OracleMethods();

      var fieldValue = oracleMethods.GetFieldValue(dataOperation, "SessionToken");

      Assert.NotNull(fieldValue);
      Assert.Equal(TestingConstants.SESSION_TOKEN, fieldValue);
    }


    #endregion Facts

  }  // class OracleDataOperationTests

}  // namespace Empiria.Data.Handlers.Tests
