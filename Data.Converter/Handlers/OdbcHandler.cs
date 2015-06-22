/* Empiria Foundation Framework 2015 *************************************************************************
*                                                                                                            *
*  Solution  : Empiria Foundation Framework                     System   : Data Convertion Services          *
*  Namespace : Empiria.Data.Convertion.Handlers                 Assembly : Empiria.Data.Convertion.dll       *
*  Type      : OdbcHandler                                      Pattern  : Static Class                      *
*  Version   : 6.5        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Performs Odbc data read and write bulk operations.                                            *
*                                                                                                            *
********************************* Copyright (c) 2007-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Data;
using System.Data.Odbc;

namespace Empiria.Data.Convertion.Handlers {

  static internal class OdbcHandler {

    #region Internal methods

    static internal int Execute(string dataSourceName, string queryString) {
      OdbcConnection connection = GetDataSourceConnection(dataSourceName);
      OdbcCommand command = new OdbcCommand(queryString, connection);

      int affectedRows = 0;
      try {
        command.CommandType = CommandType.Text;
        command.CommandTimeout = 7200; // waits all needed time
        connection.Open();
        affectedRows = command.ExecuteNonQuery();
      } catch (Exception exception) {
        throw exception;
      } finally {
        connection.Dispose();
      }
      return affectedRows;
    }

    static internal string ExecuteInsert(string dataSourceName, string source, params object[] parameters) {
      string sql = "INSERT INTO " + source + " VALUES ";

      try {
        sql += "(" + ParameterFormatter(parameters[0]);
        for (int i = 1; i < parameters.Length; i++) {
          sql += ", " + ParameterFormatter(parameters[i]);
        }
        sql += ");";

        Execute(dataSourceName, sql);

        return sql;
      } catch (Exception e) {
        sql = "Ocurrió un problema al intentar ejecutar la instrucción MySQL :" +
              System.Environment.NewLine + sql +
              System.Environment.NewLine + System.Environment.NewLine + e.ToString();
        return sql;
      }
    }

    static internal string ExecuteUpdate(string dataSourceName, string source, params object[] parameters) {
      string sql = "REPLACE INTO " + source + " VALUES ";
      try {
        sql += "(" + ParameterFormatter(parameters[0]);
        for (int i = 1; i < parameters.Length; i++) {
          sql += ", " + ParameterFormatter(parameters[i]);
        }
        sql += ");";

        Execute(dataSourceName, sql);
        return sql;
      } catch (Exception e) {
        sql = "Ocurrió un problema al intentar ejecutar la instrucción MySQL :" +
              System.Environment.NewLine + sql +
              System.Environment.NewLine + System.Environment.NewLine + e.ToString();
        return sql;
      }
    }

    static internal int GetIntegerValue(string dataSourceName, string queryString) {
      object value = GetValue(dataSourceName, queryString);
      if (value != DBNull.Value) {
        return Convert.ToInt32(value);
      } else {
        return 0;
      }
    }

    static internal object GetValue(string dataSourceName, string queryString) {
      OdbcConnection connection = GetDataSourceConnection(dataSourceName);
      OdbcCommand command = new OdbcCommand(queryString, connection);

      object returnValue = 0;
      try {
        command.CommandType = CommandType.Text;
        connection.Open();
        returnValue = command.ExecuteScalar();
      } catch (Exception exception) {
        throw new DataConvertionException(DataConvertionException.Msg.MySqlReadProblem, exception);
      } finally {
        connection.Dispose();
      }
      return returnValue;
    }

    static internal DataTable GetDataTable(string dataSourceName, string queryString, string dataTableName) {
      OdbcConnection connection = GetDataSourceConnection(dataSourceName);
      OdbcCommand command = new OdbcCommand(queryString, connection);

      DataTable dataTable = new DataTable(dataTableName);

      try {
        dataTable.Locale = System.Globalization.CultureInfo.InvariantCulture;
        command.CommandType = CommandType.Text;
        OdbcDataAdapter dataAdapter = new OdbcDataAdapter(command);
        dataAdapter.Fill(dataTable);
      } catch (Exception exception) {
        throw exception;
      } finally {
        connection.Dispose();
      }
      return dataTable;
    }

    static internal DataRow GetDataRow(string dataSourceName, string queryString, string tableName) {
      DataTable table = GetDataTable(dataSourceName, queryString, tableName);
      if (table.Rows.Count != 0) {
        return table.Rows[0];
      } else {
        return null;
      }
    }

    static internal string GetTableSchema(string dataSourceName, string queryString, string tableName,
                                          string xmlFilePath) {
      try {
        DataTable table = GetDataTable(dataSourceName, queryString, tableName);

        string fileName = xmlFilePath + tableName + ".xml.txt";

        table.WriteXmlSchema(fileName);

        return fileName;
      } catch (Exception exception) {
        throw new DataConvertionException(DataConvertionException.Msg.MySqlReadProblem, exception);
      }
    }

    static internal Tuple<int, string> UpdateTable(string dataSourceName, DataTable sourceTable, string targetTableName,
                                                    int startRowIndex, int endRowIndex) {
      OdbcConnection connection = GetDataSourceConnection(dataSourceName);
      int affectedRows = 0;
      string sqlItem = String.Empty;
      string sqlError = String.Empty;
      try {
        connection.Open();

        for (int i = startRowIndex; i <= endRowIndex; i++) {
          sqlItem = "REPLACE INTO " + targetTableName + " VALUES ";
          sqlItem += "(" + ParameterFormatter(sourceTable.Rows[i][0]);
          for (int j = 1; j < sourceTable.Columns.Count; j++) {
            sqlItem += ", " + ParameterFormatter(sourceTable.Rows[i][j]);
          }  // j
          sqlItem += ");";
          try {
            OdbcCommand command = new OdbcCommand(sqlItem, connection);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 7200; // waits all needed time
            affectedRows += command.ExecuteNonQuery();
          } catch (Exception exception) {
            sqlError += "No pude ejecutar el siguiente comando en MySQL:" +
                         System.Environment.NewLine + System.Environment.NewLine + sqlItem +
                         System.Environment.NewLine + System.Environment.NewLine + exception.Message +
                         System.Environment.NewLine;
          }
        }
        return new Tuple<int, string>(affectedRows, sqlError);
      } catch (Exception exception) {
        throw exception;
      } finally {
        connection.Dispose();
      }
    }

    //static internal int UpdateTable(string dataSourceName, DataTable sourceTable, DataTable targetTable,
    //                                string targetEmptyQueryString, int startRowIndex, int endRowIndex) {
    //  int affectedRows = 0;
    //  using (SqlConnection connection = GetDataSourceConnection(dataSourceName)) {
    //    connection.Open();
    //    SqlTransaction transaction = connection.BeginTransaction();
    //    SqlDataAdapter dataAdapter = new SqlDataAdapter();
    //    dataAdapter.ContinueUpdateOnError = true;
    //    dataAdapter.SelectCommand = new SqlCommand(targetEmptyQueryString, connection, transaction);
    //    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

    //    dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
    //    dataAdapter.InsertCommand.Transaction = transaction;
    //    dataAdapter.Fill(targetTable);
    //    DataConverter.GetInstance().TranslateData(sourceTable, targetTable, startRowIndex, endRowIndex);
    //    affectedRows = dataAdapter.Update(targetTable);
    //    transaction.Commit();
    //  }
    //  return affectedRows;
    //}

    #endregion Internal methods

    #region Private methods

    static private OdbcConnection GetDataSourceConnection(string dataSourceName) {
      string connectionString = ConfigurationData.GetString("DS." + dataSourceName);

      return new OdbcConnection(connectionString);
    }

    static private string ParameterFormatter(object value) {
      if (value is string) {
        return "\"" + ((string) value).Replace('\"', '\'') + "\"";
      } else if (value is DateTime) {
        return "\"" + ((DateTime) value).ToString("yyyy-MM-dd") + "\"";
      } else if (value is DBNull || value == DBNull.Value || value == null) {
        return "NULL";
      } else {
        return Convert.ToString(value);
      }
    }

    #endregion Private methods

  } // class OdbcHandler

} // namespace Empiria.Data.Convertion.Handlers
