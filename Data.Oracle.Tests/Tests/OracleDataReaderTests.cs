/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Oracle Data Handler                        Component : Test Helpers                            *
*  Assembly : Empiria.Data.Oracle.Tests.dll              Pattern   : Unit tests                              *
*  Type     : OracleReadDataTests                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Unit tests for data reading operations using Empiria Oracle Data Handler.                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Xunit;

namespace Empiria.Data.Handlers.Tests {

  /// <summary>Unit tests for data reading operations using Empiria Oracle Data Handler.</summary>
  public class OracleReadDataTests {

    #region Facts

    [Fact]
    public void Should_CountRows() {
      var dataOperation = DataOperation.Parse(TestingConstants.DATA_READER_SQL);

      var oracleMethods = new OracleMethods();

      int value = oracleMethods.CountRows(dataOperation);

      Assert.True(value >= TestingConstants.DATA_SET_MINIMAL_ROWS_COUNT);
    }


    [Fact]
    public void Should_GetDataReader() {
      var dataOperation = DataOperation.Parse(TestingConstants.DATA_READER_SQL);

      var oracleMethods = new OracleMethods();

      var dataReader = oracleMethods.GetDataReader(dataOperation);

      Assert.NotNull(dataReader);
      Assert.True(dataReader.FieldCount > 0);
    }


    [Fact]
    public void Should_GetDataRow() {
      var dataOperation = DataOperation.Parse(TestingConstants.GET_INT_SCALAR_SQL);

      var oracleMethods = new OracleMethods();

      var dataRow = oracleMethods.GetDataRow(dataOperation);

      Assert.Equal(TestingConstants.SCALAR_VALUE, (long) dataRow[0]);
    }


    [Fact]
    public void Should_GetDataTable() {
      var dataOperation = DataOperation.Parse(TestingConstants.DATA_READER_SQL);

      var oracleMethods = new OracleMethods();

      var dataTable = oracleMethods.GetDataTable(dataOperation, "TestsTable");

      Assert.True(dataTable.Rows.Count >= TestingConstants.DATA_SET_MINIMAL_ROWS_COUNT);
    }


    [Fact]
    public void Should_GetDataView() {
      var dataOperation = DataOperation.Parse(TestingConstants.DATA_READER_SQL);

      var oracleMethods = new OracleMethods();

      var dataView = oracleMethods.GetDataView(dataOperation, string.Empty, string.Empty);

      Assert.True(dataView.Count >= TestingConstants.DATA_SET_MINIMAL_ROWS_COUNT);
    }


    [Fact]
    public void Should_GetFieldValue() {
      var dataOperation = DataOperation.Parse(TestingConstants.GET_INT_SCALAR_SQL);

      var oracleMethods = new OracleMethods();

      var value = oracleMethods.GetFieldValue(dataOperation, TestingConstants.GET_INT_SCALAR_FIELD_NAME);

      Assert.Equal(TestingConstants.SCALAR_VALUE, (long) value);
    }


    [Fact]
    public void Should_GetScalar() {
      var dataOperation = DataOperation.Parse(TestingConstants.GET_INT_SCALAR_SQL);

      var oracleMethods = new OracleMethods();

      var scalar = oracleMethods.GetScalar(dataOperation);

      Assert.Equal(TestingConstants.SCALAR_VALUE, scalar);
    }


    [Fact]
    public void Should_InvokeSelectCount() {
      var sql = $"SELECT COUNT(*) FROM {TestingConstants.DATA_READER_TABLE_NAME}";

      var dataOperation = DataOperation.Parse(sql);

      var oracleMethods = new OracleMethods();

      var dataRow = oracleMethods.GetDataRow(dataOperation);

      Assert.NotNull(dataRow);

      Assert.True(Convert.ToInt32(dataRow[0]) >= TestingConstants.DATA_SET_MINIMAL_ROWS_COUNT);
    }

    #endregion Facts

  }  // class OracleReadDataTests

}  // namespace Empiria.Data.Handlers.Tests
