/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Oracle Data Handler                        Component : Data Access Library                     *
*  Assembly : Empiria.Data.Oracle.dll                    Pattern   : Data Handler                            *
*  Type     : OracleMethods                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Data handler used to connect Empiria-based solutions to Oracle databases.                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Data;

using System.EnterpriseServices;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace Empiria.Data.Handlers {

  /// <summary>Data handler used to connect Empiria-based solutions to Oracle databases.</summary>
  public class OracleMethods : IDataHandler {

    #region Internal methods

    public int AppendRows(IDbConnection connection, string tableName,
                          DataTable table, string filter) {
      throw new NotImplementedException();
    }


    public int CountRows(DataOperation operation) {
      var dataTable = GetDataTable(operation, String.Empty);

      return dataTable.Rows.Count;
    }


    public int Execute(DataOperation operation) {
      var connection = new OracleConnection(operation.DataSource.Source);
      var command = new OracleCommand(operation.SourceName, connection);

      try {
        operation.PrepareCommand(command);

        TryOpenConnection(connection);

        if (ContextUtil.IsInTransaction) {
          connection.EnlistDistributedTransaction((System.EnterpriseServices.ITransaction) ContextUtil.Transaction);
        }

        return command.ExecuteNonQuery();

      } catch (ServiceException) {
        throw;

      } catch (Exception exception) {
        throw new EmpiriaDataException(EmpiriaDataException.Msg.CannotExecuteActionQuery,
                                       exception, operation.SourceName, operation.ParametersToString());

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

        TryOpenConnection(connection);

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

      } catch (ServiceException) {
        throw;

      } catch (Exception exception) {
        throw new EmpiriaDataException(EmpiriaDataException.Msg.CannotExecuteActionQuery,
                                       exception, operation.SourceName, operation.ParametersToString());

      } finally {
        command.Parameters.Clear();
        connection.Dispose();
      }
    }


    public int Execute(IDbConnection connection, DataOperation operation) {
      var command = new OracleCommand(operation.SourceName, (OracleConnection) connection);

      try {
        operation.PrepareCommand(command);

        TryOpenConnection((OracleConnection) connection);

        return command.ExecuteNonQuery();

      } catch (ServiceException) {
        throw;

      } catch (Exception exception) {
        throw new EmpiriaDataException(EmpiriaDataException.Msg.CannotExecuteActionQuery,
                                       exception, operation.SourceName, operation.ParametersToString());

      } finally {
        command.Parameters.Clear();
      }
    }


    public int Execute(IDbTransaction transaction, DataOperation operation) {
      var command = new OracleCommand(operation.SourceName,
                                      (OracleConnection) transaction.Connection);

      try {
        operation.PrepareCommand(command);

        TryOpenConnection((OracleConnection) transaction.Connection);

        return command.ExecuteNonQuery();

      } catch (ServiceException) {
        throw;

      } catch (Exception exception) {
        throw new EmpiriaDataException(EmpiriaDataException.Msg.CannotExecuteActionQuery,
                                       exception, operation.SourceName, operation.ParametersToString());

      } finally {
        command.Parameters.Clear();
      }
    }


    public byte[] GetBinaryFieldValue(DataOperation operation, string fieldName) {
      byte[] value = new byte[0];

      var reader = (OracleDataReader) this.GetDataReader(operation);

      if (reader.Read()) {
        OracleBinary blob = reader.GetOracleBinary(reader.GetOrdinal(fieldName));

        value = blob.Value;
      }

      reader.Close();

      return value;
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

        TryOpenConnection(connection);

        return command.ExecuteReader(CommandBehavior.CloseConnection);

      } catch (ServiceException) {
        throw;

      } catch (Exception exception) {
        throw new EmpiriaDataException(EmpiriaDataException.Msg.CannotGetDataReader,
                                       exception, operation.SourceName, operation.ParametersToString());

      } finally {
        command.Parameters.Clear();
        // Do not dipose the connection because this method returns a DataReader.
      }
    }


    public DataRow GetDataRow(DataOperation operation) {
      DataTable dataTable = GetDataTable(operation, operation.SourceName);

      if (dataTable.Rows.Count != 0) {
        return dataTable.Rows[0];
      } else {
        return null;
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

        TryOpenConnection(connection);

        dataAdapter.Fill(dataTable);

        dataAdapter.Dispose();

        return dataTable;

      } catch (ServiceException) {
        throw;

      } catch (Exception exception) {
        throw new EmpiriaDataException(EmpiriaDataException.Msg.CannotGetDataTable,
                                       exception, operation.SourceName, operation.ParametersToString());

      } finally {
        command.Parameters.Clear();
        connection.Dispose();
      }
    }


    public DataView GetDataView(DataOperation operation, string filter, string sort) {
      DataTable dataTable = GetDataTable(operation, operation.SourceName);

      return new DataView(dataTable, filter, sort, DataViewRowState.CurrentRows);
    }


    public object GetFieldValue(DataOperation operation, string fieldName) {
      var connection = new OracleConnection(operation.DataSource.Source);
      var command = new OracleCommand(operation.SourceName, connection);

      try {
        operation.PrepareCommand(command);

        TryOpenConnection(connection);

        OracleDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

        if (dataReader.Read()) {
          return dataReader[fieldName];
        } else {
          return null;
        }

      } catch (ServiceException) {
        throw;

      } catch (Exception exception) {
        throw new EmpiriaDataException(EmpiriaDataException.Msg.CannotGetFieldValue,
                                       exception, operation.SourceName, operation.ParametersToString(),
                                       fieldName);

      } finally {
        command.Parameters.Clear();
        connection.Dispose();
      }
    }


    public IDataParameter[] GetParameters(string source, string name, object[] values) {
      return OracleParameterCache.GetParameters(source, name, values);
    }


    public object GetScalar(DataOperation operation) {
      var connection = new OracleConnection(operation.DataSource.Source);
      var command = new OracleCommand(operation.SourceName, connection);

      try {
        operation.PrepareCommand(command);

        TryOpenConnection(connection);

        return command.ExecuteScalar();

      } catch (ServiceException) {
        throw;

      } catch (Exception exception) {
        throw new EmpiriaDataException(EmpiriaDataException.Msg.CannotGetScalar,
                                       exception, operation.SourceName, operation.ParametersToString());

      } finally {
        command.Parameters.Clear();
        connection.Dispose();
      }
    }


    static internal void TryOpenConnection(OracleConnection connection) {
      try {
        connection.Open();

      } catch (Exception innerException) {
        throw new ServiceException("DATABASE_SERVER_CONNECTION_FAILED",
          "No se pudo hacer una conexión del sistema con la base de datos. " +
          "Puede ser que el servidor de base de datos no esté disponible, " +
          "o que la conexión entre el servidor de base de datos " +
          "y el servidor de aplicaciones se haya perdido.",
          innerException);
      }
    }

    #endregion Internal methods

  } // class OracleMethods

} // namespace Empiria.Data.Handlers
