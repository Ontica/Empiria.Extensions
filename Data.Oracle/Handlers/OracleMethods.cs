﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution : Empiria Extensions Framework                     System  : Data Access Library                 *
*  Assembly : Empiria.Data.Oracle.dll                          Pattern : Provider                            *
*  Type     : OracleMethods                                    License : Please read LICENSE.txt file        *
*                                                                                                            *
*  Summary  : Empiria data handler to connect solutions to Oracle databases.                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Data;
using System.Data.OracleClient;
using System.EnterpriseServices;

namespace Empiria.Data.Handlers {

  /// <summary>Empiria data handler to connect solutions to Oracle databases.</summary>
  internal class OracleMethods : IDataHandler {

    #region Internal methods

    public int AppendRows(IDbConnection connection, string tableName,
                          DataTable table, string filter) {
      throw new NotImplementedException();
    }


    public int CountRows(DataOperation operation) {
      var connection = new OracleConnection(operation.DataSource.Source);
      var command = new OracleCommand(operation.SourceName, connection);

      try {
        operation.PrepareCommand(command);

        var dataAdapter = new OracleDataAdapter(command);

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
      var connection = new OracleConnection(operation.DataSource.Source);
      var command = new OracleCommand(operation.SourceName, connection);

      try {
        operation.PrepareCommand(command);

        connection.Open();

        if (ContextUtil.IsInTransaction) {
          connection.EnlistDistributedTransaction((System.EnterpriseServices.ITransaction) ContextUtil.Transaction);
        }

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
      var connection = new OracleConnection(operation.DataSource.Source);
      var command = new OracleCommand(operation.SourceName, connection);

      try {
        operation.PrepareCommand(command);

        connection.Open();

        if (ContextUtil.IsInTransaction) {
          connection.EnlistDistributedTransaction((System.EnterpriseServices.ITransaction) ContextUtil.Transaction);
        }

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
      var command = new OracleCommand(operation.SourceName, (OracleConnection) connection);

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
      var command = new OracleCommand(operation.SourceName,
                                      (OracleConnection) transaction.Connection,
                                      (OracleTransaction) transaction);
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
      return OracleParameterCache.GetParameters(connectionString, sourceName, parameterValues);
    }


    public byte[] GetBinaryFieldValue(DataOperation operation, string fieldName) {
      var reader = (OracleDataReader) this.GetDataReader(operation);

      if (reader.Read()) {
        OracleBinary blob = reader.GetOracleBinary(reader.GetOrdinal(fieldName));

        return blob.Value;

      } else {
        return null;

      }
    }


    public IDbConnection GetConnection(string connectionString) {
      var connection = new OracleConnection(connectionString);

      if (ContextUtil.IsInTransaction) {
        connection.EnlistDistributedTransaction((System.EnterpriseServices.ITransaction) ContextUtil.Transaction);
      }

      return connection;
    }


    public IDataReader GetDataReader(DataOperation operation) {
      var connection = new OracleConnection(operation.DataSource.Source);
      var command = new OracleCommand(operation.SourceName, connection);

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
      var connection = new OracleConnection(operation.DataSource.Source);
      var command = new OracleCommand(operation.SourceName, connection);

      try {
        operation.PrepareCommand(command);

        var dataAdapter = new OracleDataAdapter(command);

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
      var connection = new OracleConnection(operation.DataSource.Source);
      var command = new OracleCommand(operation.SourceName, connection);

      try {
        operation.PrepareCommand(command);

        var dataAdapter = new OracleDataAdapter(command);

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
      var connection = new OracleConnection(operation.DataSource.Source);
      var command = new OracleCommand(operation.SourceName, connection);

      try {
        operation.PrepareCommand(command);

        var dataAdapter = new OracleDataAdapter(command);

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
      var connection = new OracleConnection(operation.DataSource.Source);
      var command = new OracleCommand(operation.SourceName, connection);

      try {
        operation.PrepareCommand(command);

        connection.Open();

        OracleDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

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
      var connection = new OracleConnection(operation.DataSource.Source);
      var command = new OracleCommand(operation.SourceName, connection);

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

  } // class OracleMethods

} // namespace Empiria.Data.Handlers