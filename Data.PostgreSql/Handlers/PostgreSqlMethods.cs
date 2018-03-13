/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution : Empiria Extensions Framework                     System  : Data Access Library                 *
*  Assembly : Empiria.Data.PostgreSql.dll                      Pattern : Provider                            *
*  Type     : PostgreSqlMethods                                License : Please read LICENSE.txt file        *
*                                                                                                            *
*  Summary  : Empiria data handler to connect solutions to PostgreSQL databases.                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Data;
using System.EnterpriseServices;

using Npgsql;

namespace Empiria.Data.Handlers {

  /// <summary>Empiria data handler to connect solutions to PostgreSQL databases.</summary>
  internal class PostgreSqlMethods : IDataHandler {

    #region Internal methods

    public int AppendRows(IDbConnection connection, string tableName,
                          DataTable table, string filter) {
      int result = 0;
      string queryString = "SELECT * FROM " + tableName;
      if (!String.IsNullOrEmpty(filter)) {
        queryString += " WHERE " + filter;
      }
      using (connection) {
        connection.Open();
        NpgsqlTransaction transaction = ((NpgsqlConnection) connection).BeginTransaction();
        var dataAdapter = new NpgsqlDataAdapter();
        dataAdapter.SelectCommand = new NpgsqlCommand(queryString,
                                                      (NpgsqlConnection) connection,
                                                      transaction);

        var commandBuilder = new NpgsqlCommandBuilder(dataAdapter);

        dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
        dataAdapter.InsertCommand.Transaction = transaction;

        DataTable source = DataReader.GetDataTable(DataOperation.Parse(queryString));
        dataAdapter.Fill(source);

        for (int i = 0; i < table.Rows.Count; i++) {
          table.Rows[i].SetAdded();
          source.ImportRow(table.Rows[i]);
        }
        result = dataAdapter.Update(source);
        transaction.Commit();
        dataAdapter.Dispose();
      }
      return result;
    }


    public int CountRows(DataOperation operation) {
      var connection = new NpgsqlConnection(operation.DataSource.Source);
      var command = new NpgsqlCommand(operation.SourceName, connection);

      try {
        operation.PrepareCommand(command);

        var dataAdapter = new NpgsqlDataAdapter(command);

        var dataTable = new DataTable();

        dataTable.Locale = System.Globalization.CultureInfo.InvariantCulture;

        dataAdapter.Fill(dataTable);
        dataAdapter.Dispose();

        return dataTable.Rows.Count;

      } catch (Exception exception) {
        throw new EmpiriaDataException(EmpiriaDataException.Msg.CannotGetDataTable,
                                       exception,
                                       operation.SourceName, operation.ParametersToString());

      } finally {
        command.Parameters.Clear();
        connection.Dispose();
      }
    }


    public int Execute(DataOperation operation) {
      var connection = new NpgsqlConnection(operation.DataSource.Source);
      var command = new NpgsqlCommand(operation.SourceName, connection);

      try {
        operation.PrepareCommand(command);

        connection.Open();

        return command.ExecuteNonQuery();

      } catch (Exception exception) {
        throw new EmpiriaDataException(EmpiriaDataException.Msg.CannotExecuteActionQuery,
                                       exception,
                                       operation.SourceName, operation.ParametersToString());

      } finally {
        command.Parameters.Clear();
        connection.Dispose();
      }
    }


    public T Execute<T>(DataOperation operation) {
      var connection = new NpgsqlConnection(operation.DataSource.Source);
      var command = new NpgsqlCommand(operation.SourceName, connection);

      try {
        operation.PrepareCommand(command);

        connection.Open();

        object result = command.ExecuteScalar();

        if (result != null) {
          return (T) result;
        } else {
          throw new EmpiriaDataException(EmpiriaDataException.Msg.ActionQueryDoesntReturnAValue,
                                         operation.SourceName);
        }

      } catch (Exception exception) {
        throw new EmpiriaDataException(EmpiriaDataException.Msg.CannotExecuteActionQuery,
                                       exception,
                                       operation.SourceName, operation.ParametersToString());

      } finally {
        command.Parameters.Clear();
        connection.Dispose();
      }
    }


    public int Execute(IDbConnection connection, DataOperation operation) {
      var command = new NpgsqlCommand(operation.SourceName, (NpgsqlConnection) connection);

      try {
        operation.PrepareCommand(command);

        return command.ExecuteNonQuery();

      } catch (Exception exception) {
        throw new EmpiriaDataException(EmpiriaDataException.Msg.CannotExecuteActionQuery,
                                       exception,
                                       operation.SourceName, operation.ParametersToString());

      } finally {
        command.Parameters.Clear();
      }
    }


    public int Execute(IDbTransaction transaction, DataOperation operation) {
      var command = new NpgsqlCommand(operation.SourceName,
                                      (NpgsqlConnection) transaction.Connection,
                                      (NpgsqlTransaction) transaction);
      try {
        operation.PrepareCommand(command);

        return command.ExecuteNonQuery();

      } catch (Exception exception) {
        throw new EmpiriaDataException(EmpiriaDataException.Msg.CannotExecuteActionQuery,
                                       exception,
                                       operation.SourceName, operation.ParametersToString());

      } finally {
        command.Parameters.Clear();
      }
    }


    public IDataParameter[] GetParameters(string connectionString,
                                          string sourceName,
                                          object[] parameterValues) {
      return PostgreSqlParameterCache.GetParameters(connectionString, sourceName, parameterValues);
    }


    public byte[] GetBinaryFieldValue(DataOperation operation, string fieldName) {
      throw new NotImplementedException();
    }


    public IDbConnection GetConnection(string connectionString) {
      var connection = new NpgsqlConnection(connectionString);

      if (ContextUtil.IsInTransaction) {
        connection.EnlistTransaction((System.Transactions.Transaction) ContextUtil.Transaction);
      }

      return connection;
    }


    public IDataReader GetDataReader(DataOperation operation) {
      var connection = new NpgsqlConnection(operation.DataSource.Source);
      var command = new NpgsqlCommand(operation.SourceName, connection);

      try {
        operation.PrepareCommand(command);

        connection.Open();

        return command.ExecuteReader(CommandBehavior.CloseConnection);

      } catch (Exception exception) {
        throw new EmpiriaDataException(EmpiriaDataException.Msg.CannotGetDataReader,
                                       exception,
                                       operation.SourceName, operation.ParametersToString());

      } finally {
        command.Parameters.Clear();
        //Don't dipose the connection because this method returns a DataReader.
      }
    }


    public DataRow GetDataRow(DataOperation operation) {
      var connection = new NpgsqlConnection(operation.DataSource.Source);
      var command = new NpgsqlCommand(operation.SourceName, connection);

      try {
        operation.PrepareCommand(command);

        var dataAdapter = new NpgsqlDataAdapter(command);

        var dataTable = new DataTable(operation.SourceName);

        dataTable.Locale = System.Globalization.CultureInfo.InvariantCulture;

        dataAdapter.Fill(dataTable);
        dataAdapter.Dispose();

        if (dataTable.Rows.Count != 0) {
          return dataTable.Rows[0];
        } else {
          return null;
        }

      } catch (Exception exception) {
        throw new EmpiriaDataException(EmpiriaDataException.Msg.CannotGetDataTable,
                                       exception,
                                       operation.SourceName, operation.ParametersToString());

      } finally {
        command.Parameters.Clear();
        connection.Dispose();
      }
    }


    public DataTable GetDataTable(DataOperation operation, string dataTableName) {
      var connection = new NpgsqlConnection(operation.DataSource.Source);
      var command = new NpgsqlCommand(operation.SourceName, connection);

      try {
        operation.PrepareCommand(command);

        var dataAdapter = new NpgsqlDataAdapter(command);

        var dataTable = new DataTable(dataTableName);

        dataTable.Locale = System.Globalization.CultureInfo.InvariantCulture;

        dataAdapter.Fill(dataTable);
        dataAdapter.Dispose();

        return dataTable;

      } catch (Exception exception) {
        throw new EmpiriaDataException(EmpiriaDataException.Msg.CannotGetDataTable,
                                       exception,
                                       operation.SourceName, operation.ParametersToString());

      } finally {
        command.Parameters.Clear();
        connection.Dispose();
      }
    }


    public DataView GetDataView(DataOperation operation, string filter, string sort) {
      var connection = new NpgsqlConnection(operation.DataSource.Source);
      var command = new NpgsqlCommand(operation.SourceName, connection);

      try {
        operation.PrepareCommand(command);

        var dataAdapter = new NpgsqlDataAdapter(command);

        var dataTable = new DataTable(operation.SourceName);

        dataTable.Locale = System.Globalization.CultureInfo.InvariantCulture;

        dataAdapter.Fill(dataTable);
        dataAdapter.Dispose();

        return new DataView(dataTable, filter, sort,
                            DataViewRowState.CurrentRows);

      } catch (Exception exception) {
        throw new EmpiriaDataException(EmpiriaDataException.Msg.CannotGetDataView,
                                       exception,
                                       operation.SourceName, operation.ParametersToString());

      } finally {
        command.Parameters.Clear();
        connection.Dispose();
      }
    }


    public object GetFieldValue(DataOperation operation, string fieldName) {
      var connection = new NpgsqlConnection(operation.DataSource.Source);
      var command = new NpgsqlCommand(operation.SourceName, connection);

      try {
        operation.PrepareCommand(command);

        connection.Open();

        NpgsqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

        if (dataReader.Read()) {
          return dataReader[fieldName];
        } else {
          return null;
        }

      } catch (Exception exception) {
        throw new EmpiriaDataException(EmpiriaDataException.Msg.CannotGetFieldValue,
                                       exception,
                                       operation.SourceName, operation.ParametersToString(),
                                       fieldName);

      } finally {
        command.Parameters.Clear();
        connection.Dispose();
      }
    }


    public object GetScalar(DataOperation operation) {
      var connection = new NpgsqlConnection(operation.DataSource.Source);
      var command = new NpgsqlCommand(operation.SourceName, connection);

      try {
        operation.PrepareCommand(command);

        connection.Open();

        return command.ExecuteScalar();

      } catch (Exception exception) {
        throw new EmpiriaDataException(EmpiriaDataException.Msg.CannotGetScalar,
                                       exception,
                                       operation.SourceName, operation.ParametersToString());

      } finally {
        command.Parameters.Clear();
        connection.Dispose();
      }
    }

    #endregion Internal methods

  } // class PostgreSqlMethods

} // namespace Empiria.Data.Handlers
