/* Empiria Foundation Framework 2014 *************************************************************************
*                                                                                                            *
*  Solution  : Empiria Foundation Framework                     System   : Data Convertion Services          *
*  Namespace : Empiria.Data.Convertion.Handlers                 Assembly : Empiria.Data.Convertion.dll       *
*  Type      : MySqlHandler                                     Pattern  : Static Class                      *
*  Version   : 6.0        Date: 23/Oct/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Performs MySQL data read and write bulk operations.                                           *
*                                                                                                            *
********************************* Copyright (c) 2007-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Data;

using MySql.Data.MySqlClient;

namespace Empiria.Data.Convertion.Handlers {

  static internal class MySqlHandler {

    #region Public methods

    //static internal string ExecuteInsert(string source, params object[] parameters) {
    //  string sql = "INSERT INTO " + source + " VALUES ";

    //  try {
    //    sql += "(" + ParameterFormatter(parameters[0]);
    //    for (int i = 1; i < parameters.Length; i++) {
    //      sql += ", " + ParameterFormatter(parameters[i]);
    //    }
    //    sql += ");";

    //    UpdateMySQLTable(sql);

    //    return sql;
    //  } catch (Exception e) {
    //    sql = "Ocurrió un problema al intentar ejecutar la instrucción MySQL :" +
    //          System.Environment.NewLine + sql + 
    //          System.Environment.NewLine + System.Environment.NewLine + e.ToString();
    //    return sql;
    //  }
    //}

    //static internal string ExecuteUpdate(string source, params object[] parameters) {
    //  string sql = "REPLACE INTO " + source + " VALUES ";
    //  try {
    //    sql += "(" + ParameterFormatter(parameters[0]);
    //    for (int i = 1; i < parameters.Length; i++) {
    //      sql += ", " + ParameterFormatter(parameters[i]);
    //    }
    //    sql += ");";

    //    UpdateMySQLTable(sql);
    //    return sql;
    //  } catch (Exception e) {
    //    sql = "Ocurrió un problema al intentar ejecutar la instrucción MySQL :" +
    //          System.Environment.NewLine + sql +
    //          System.Environment.NewLine + System.Environment.NewLine + e.ToString();
    //    return sql;
    //  }
    //}

    static internal int Execute(string dataSourceName, string queryString) {
      MySqlConnection connection = GetDataSourceConnection(dataSourceName);
      MySqlCommand command = new MySqlCommand(queryString, connection);

      int affectedRows = 0;
      try {
        command.CommandType = CommandType.Text;
        command.CommandTimeout = 7200; // waits all needed time
        connection.Open();
        affectedRows = command.ExecuteNonQuery();
      } catch {
        throw;
      } finally {
        connection.Dispose();
      }
      return affectedRows;
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
      MySqlConnection connection = GetDataSourceConnection(dataSourceName);
      MySqlCommand command = new MySqlCommand(queryString, connection);

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
      MySqlConnection connection = GetDataSourceConnection(dataSourceName);
      MySqlCommand command = new MySqlCommand(queryString, connection);

      DataTable dataTable = new DataTable(dataTableName);

      try {
        dataTable.Locale = System.Globalization.CultureInfo.InvariantCulture;
        command.CommandType = CommandType.Text;
        MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
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

    static internal string GetTableSchema(string dataSourceName, string queryString, string tableName, string xmlFilePath) {
      try {
        DataTable table = GetDataTable(dataSourceName, queryString, tableName);

        string fileName = xmlFilePath + tableName + ".xml.txt";

        table.WriteXmlSchema(fileName);

        return fileName;
      } catch (Exception exception) {
        throw new DataConvertionException(DataConvertionException.Msg.MySqlReadProblem, exception);
      }
    }

    static internal int UpdateTable(string dataSourceName, DataTable sourceTable, string targetTableName,
                                    int startRowIndex, int endRowIndex) {
      MySqlConnection connection = GetDataSourceConnection(dataSourceName);
      int affectedRows = 0;
      string sqlItem = String.Empty;
      try {
        connection.Open();

        for (int i = startRowIndex; i <= endRowIndex; i++) {
          sqlItem = "REPLACE INTO " + targetTableName + " VALUES ";
          sqlItem += "(" + ParameterFormatter(sourceTable.Rows[i][0]);
          for (int j = 1; j < sourceTable.Columns.Count; j++) {
            sqlItem += ", " + ParameterFormatter(sourceTable.Rows[i][j]);
          }  // j
          sqlItem += ");";
          MySqlCommand command = new MySqlCommand(sqlItem, connection);
          command.CommandType = CommandType.Text;
          command.CommandTimeout = 7200; // waits all needed time
          affectedRows += command.ExecuteNonQuery();
        } // i
      } catch (Exception exception) {
        throw new Exception("No pude ejecutar el siguiente comando en MySQL:" + System.Environment.NewLine + System.Environment.NewLine + sqlItem, exception);
      } finally {
        connection.Dispose();
      }
      return affectedRows;
    }

    #endregion Public methods

    #region Private methods

    static private MySqlConnection GetDataSourceConnection(string dataSourceName) {
      string connectionString = ConfigurationData.GetString("DS." + dataSourceName);

      return new MySqlConnection(connectionString);
    }

    static private string ParameterFormatter(object value) {
      if (value is string) {
        return "'" + (string) value + "'";
      } else if (value is DateTime) {
        return "\"" + ((DateTime) value).ToString("yyyy-MM-dd") + "\"";
      } else if (value is DBNull || value == DBNull.Value || value == null) {
        return "NULL";
      } else {
        return Convert.ToString(value);
      }
    }

    #endregion Private methods

  } // class MySqlHandler

} // namespace Empiria.Data.Convertion.Handlers
