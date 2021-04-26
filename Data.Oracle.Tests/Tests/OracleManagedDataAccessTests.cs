/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Oracle Data Handler                        Component : Test Helpers                            *
*  Assembly : Empiria.Data.Oracle.Tests.dll              Pattern   : Unit tests                              *
*  Type     : OracleManagedDataAccessTests               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Unit tests calling directly the Oracle Managed Data Access library (ODT) methods.              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using Xunit;

using System.Data;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace Empiria.Data.Handlers.Tests {

  /// <summary>Unit tests calling directly the Oracle Managed Data Access library (ODT) methods.</summary>
  public class OracleManagedDataAccessTests {

    #region Facts

    [Fact]
    public void Should_Get_A_DataReader_With_StoredProcedure_Returned_Rows() {
      string dataSource = DataOperation.Parse("dataSource").DataSource.Source;

      using (var connection = new OracleConnection(dataSource)) {

        using (var command = new OracleCommand("getUserSession", connection)) {
          command.CommandType = CommandType.StoredProcedure;

          command.Parameters.Add("SessionToken", OracleDbType.Varchar2,
                                 ParameterDirection.Input).Value = TestingConstants.SESSION_TOKEN;
          command.Parameters.Add("ReturnedRefCursor",
                                 OracleDbType.RefCursor).Direction = ParameterDirection.Output;

          connection.Open();

          command.ExecuteNonQuery();

          using (OracleDataReader reader = ((OracleRefCursor) command.Parameters["ReturnedRefCursor"].Value).GetDataReader()) {

            Assert.NotNull(reader);

            reader.Read();

            Assert.Equal(TestingConstants.SESSION_TOKEN, (string) reader["SessionToken"]);

          } // using reader

        } // using command

      } // using connection
    }


    [Fact]
    public void Should_Fill_A_DataTable_With_StoredProcedure_Returned_Rows() {
      string dataSource = DataOperation.Parse("dataSource").DataSource.Source;

      using (var connection = new OracleConnection(dataSource)) {

        using (var command = new OracleCommand("getUserSession", connection)) {
          command.CommandType = CommandType.StoredProcedure;

          command.Parameters.Add("SessionToken", OracleDbType.Varchar2, ParameterDirection.Input);
          command.Parameters["SessionToken"].Value = TestingConstants.SESSION_TOKEN;

          command.Parameters.Add("ReturnedRefCursor", OracleDbType.RefCursor);
          command.Parameters["ReturnedRefCursor"].Direction = ParameterDirection.Output;

          connection.Open();

          command.ExecuteNonQuery();

          using (var dataAdapter = new OracleDataAdapter(command)) {

            var dataTable = new DataTable("getUserSession");

            dataAdapter.Fill(dataTable);

            Assert.Equal(TestingConstants.SESSION_TOKEN, (string) dataTable.Rows[0]["SessionToken"]);

            dataTable.Dispose();

          }  // using dataAdapter

        } // using command

      } // using connection
    }

    #endregion Facts

  }  // class OracleManagedDataAccessTests

}  // namespace Empiria.Data.Handlers.Tests
