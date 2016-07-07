/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Data Convertion Services          *
*  Namespace : Empiria.Data.Convertion                          Assembly : Empiria.Data.Convertion.dll       *
*  Type      : DataConvertionException                          Pattern  : Exception Class                   *
*  Version   : 6.7                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Performs data convertion from MySQL to SQLServer database tables rows.                        *
*                                                                                                            *
********************************* Copyright (c) 2007-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Data;
using System.Runtime.Remoting.Messaging;

using Empiria.Data.Convertion.Handlers;

namespace Empiria.Data.Convertion {

  public class DataConvertionEngine {

    #region Delegates

    private delegate int ReplaceDataDelegate(string sourceTableName, string targetTableName,
                                             string sourceQueryString, string targetEmptyQueryString,
                                             string targetDeleteQueryString);

    private delegate int ReplaceDataSetDelegate(string dataSetName);

    #endregion

    #region Fields

    static private readonly DataConvertionEngine instance = new DataConvertionEngine();  // singleton element

    static private string logText = String.Empty;
    static private string dataErrorLogText = String.Empty;
    private string sourceDataSourceName = String.Empty;
    private string targetDataSourceName = String.Empty;
    private DataTechnology sourceDataSourceTechnology = DataTechnology.SqlServer;
    private DataTechnology targetDataSourceTechnology = DataTechnology.SqlServer;

    private readonly string logFilePath = ConfigurationData.GetString("Convertion.LogFilePath");

    private IAsyncResult asyncResult = null;

    private bool isRunning = false;   // semaphore

    #endregion Fields

    #region Constructors and parsers

    private DataConvertionEngine() {
      // Singleton pattern needs private constructor
    }

    static public DataConvertionEngine GetInstance() {
      return instance;
    }

    #endregion Constructors and parsers

    #region Public methods

    public void Initalize(string sourceDataSourceName, string targetDataSourceName) {
      if (isRunning) {
        throw new DataConvertionException(DataConvertionException.Msg.ConverterIsRunning);
      }
      this.sourceDataSourceName = sourceDataSourceName;
      this.targetDataSourceName = targetDataSourceName;

      sourceDataSourceTechnology = GetDataSourceTechnology(sourceDataSourceName);
      targetDataSourceTechnology = GetDataSourceTechnology(targetDataSourceName);
    }

    public int Execute(string[] queryString) {
      WriteLog(String.Empty);
      WriteLog("Started at: " + DateTime.Now.ToLongTimeString());
      WriteLog(String.Empty);

      int counter = 0;
      for (int i = 0; i < queryString.Length; i++) {
        counter += Execute(queryString[i]);
      }

      WriteLog(String.Empty);
      WriteLog("Finished at: " + DateTime.Now.ToLongTimeString());
      isRunning = false;
      WriteLogToDisk();
      return counter;
    }

    public int ExecuteOne(string queryString) {
      WriteLog(String.Empty);
      WriteLog("Started at: " + DateTime.Now.ToLongTimeString());
      WriteLog(String.Empty);

      int counter = 0;
      counter += Execute(queryString);

      WriteLog(String.Empty);
      WriteLog("Finished at: " + DateTime.Now.ToLongTimeString());
      isRunning = false;
      WriteLogToDisk();
      return counter;
    }

    public int GetSourceIntegerValue(string queryString) {
      switch (sourceDataSourceTechnology) {
        case DataTechnology.SqlServer:
          return SqlHandler.GetIntegerValue(sourceDataSourceName, queryString);
        case DataTechnology.MySql:
          return MySqlHandler.GetIntegerValue(sourceDataSourceName, queryString);
        case DataTechnology.Odbc:
          return OdbcHandler.GetIntegerValue(sourceDataSourceName, queryString);
        default:
          throw new EmpiriaDataException(EmpiriaDataException.Msg.InvalidDatabaseTechnology);
      }
    }

    public object GetSourceFieldValue(string queryString) {
      switch (sourceDataSourceTechnology) {
        case DataTechnology.SqlServer:
          return SqlHandler.GetValue(sourceDataSourceName, queryString);
        case DataTechnology.MySql:
          return MySqlHandler.GetValue(sourceDataSourceName, queryString);
        case DataTechnology.Odbc:
          return OdbcHandler.GetValue(sourceDataSourceName, queryString);
        default:
          throw new EmpiriaDataException(EmpiriaDataException.Msg.InvalidDatabaseTechnology);
      }
    }

    public DataRow GetSourceDataRow(string queryString, string tableName) {
      switch (sourceDataSourceTechnology) {
        case DataTechnology.SqlServer:
          return SqlHandler.GetDataRow(sourceDataSourceName, queryString, tableName);
        case DataTechnology.MySql:
          return MySqlHandler.GetDataRow(sourceDataSourceName, queryString, tableName);
        case DataTechnology.Odbc:
          return OdbcHandler.GetDataRow(sourceDataSourceName, queryString, tableName);
        default:
          throw new EmpiriaDataException(EmpiriaDataException.Msg.InvalidDatabaseTechnology);
      }
    }

    public DataTable GetSourceDataTable(string queryString, string tableName) {
      switch (sourceDataSourceTechnology) {
        case DataTechnology.SqlServer:
          return SqlHandler.GetDataTable(sourceDataSourceName, queryString, tableName);
        case DataTechnology.MySql:
          return MySqlHandler.GetDataTable(sourceDataSourceName, queryString, tableName);
        case DataTechnology.Odbc:
          return OdbcHandler.GetDataTable(sourceDataSourceName, queryString, tableName);
        default:
          throw new EmpiriaDataException(EmpiriaDataException.Msg.InvalidDatabaseTechnology);
      }
    }

    public DataTable GetTargetDataTable(string queryString, string tableName) {
      switch (targetDataSourceTechnology) {
        case DataTechnology.SqlServer:
          return SqlHandler.GetDataTable(targetDataSourceName, queryString, tableName);
        case DataTechnology.MySql:
          return MySqlHandler.GetDataTable(targetDataSourceName, queryString, tableName);
        case DataTechnology.Odbc:
          return OdbcHandler.GetDataTable(targetDataSourceName, queryString, tableName);
        default:
          throw new EmpiriaDataException(EmpiriaDataException.Msg.InvalidDatabaseTechnology);
      }
    }

    public DataRow GetTargetDataRow(string queryString, string tableName) {
      switch (targetDataSourceTechnology) {
        case DataTechnology.SqlServer:
          return SqlHandler.GetDataRow(targetDataSourceName, queryString, tableName);
        case DataTechnology.MySql:
          return MySqlHandler.GetDataRow(targetDataSourceName, queryString, tableName);
        case DataTechnology.Odbc:
          return OdbcHandler.GetDataRow(targetDataSourceName, queryString, tableName);
        default:
          throw new EmpiriaDataException(EmpiriaDataException.Msg.InvalidDatabaseTechnology);
      }
    }

    public int GetTargetIntegerValue(string queryString) {
      switch (targetDataSourceTechnology) {
        case DataTechnology.SqlServer:
          return SqlHandler.GetIntegerValue(targetDataSourceName, queryString);
        case DataTechnology.MySql:
          return MySqlHandler.GetIntegerValue(targetDataSourceName, queryString);
        case DataTechnology.Odbc:
          return OdbcHandler.GetIntegerValue(targetDataSourceName, queryString);
        default:
          throw new EmpiriaDataException(EmpiriaDataException.Msg.InvalidDatabaseTechnology);
      }
    }

    public object GetTargetFieldValue(string queryString) {
      switch (targetDataSourceTechnology) {
        case DataTechnology.SqlServer:
          return SqlHandler.GetValue(targetDataSourceName, queryString);
        case DataTechnology.MySql:
          return MySqlHandler.GetValue(targetDataSourceName, queryString);
        case DataTechnology.Odbc:
          return OdbcHandler.GetValue(targetDataSourceName, queryString);
        default:
          throw new EmpiriaDataException(EmpiriaDataException.Msg.InvalidDatabaseTechnology);
      }
    }

    public bool IsRunning() {
      return isRunning;
    }

    public void ReplaceDataSet(string setName) {
      if (isRunning) {
        throw new DataConvertionException(DataConvertionException.Msg.ConverterIsRunning);
      }
      WriteLog(String.Empty);
      WriteLog("Started at: " + DateTime.Now.ToLongTimeString());
      WriteLog(String.Empty);

      asyncResult = BeginReplaceDataSet(setName, EndReplaceDataSet);

      isRunning = true;
    }

    public int ReplaceDataSetSync(string setName) {
      //if (isRunning) {
      //  throw new DataConvertionException(DataConvertionException.Msg.ConverterIsRunning);
      //}
      //isRunning = true;
      WriteLog(String.Empty);
      WriteLog("Started at: " + DateTime.Now.ToLongTimeString());
      WriteLog(String.Empty);

      int result = DoReplaceDataSet(setName);
      //isRunning = false;
      WriteLogToDisk();

      return result;
    }

    public void ReplaceData(string sourceTableName, string targetTableName,
                            string sourceQueryString, string targetEmptyQueryString,
                            string targetDeleteQueryString) {
      if (isRunning) {
        throw new DataConvertionException(DataConvertionException.Msg.ConverterIsRunning);
      }

      WriteLog(String.Empty);
      WriteLog("Started at: " + DateTime.Now.ToLongTimeString());
      WriteLog(String.Empty);

      asyncResult = BeginReplaceData(sourceTableName, targetTableName, sourceQueryString,
                                     targetEmptyQueryString, targetDeleteQueryString, EndReplaceData);
      isRunning = true;
    }

    public void ReplaceDataSync(string sourceTableName, string targetTableName,
                                string sourceQueryString, string targetEmptyQueryString,
                                string targetDeleteQueryString) {
      if (isRunning) {
        throw new DataConvertionException(DataConvertionException.Msg.ConverterIsRunning);
      }
      isRunning = true;
      WriteLog(String.Empty);
      WriteLog("Started at: " + DateTime.Now.ToLongTimeString());
      WriteLog(String.Empty);

      DoReplaceData(sourceTableName, targetTableName, sourceQueryString,
                    targetEmptyQueryString, targetDeleteQueryString);

      WriteLog(String.Empty);
      WriteLog("Finished at: " + DateTime.Now.ToLongTimeString());

      WriteLogToDisk();
      isRunning = false;
    }

    #endregion Public methods

    #region Private and internal methods

    internal void TranslateData(DataTable source, DataTable target, int startRowIndex, int endRowIndex) {
      //target.BeginLoadData();
      for (int i = startRowIndex; i <= endRowIndex; i++) {
        try {
          target.Rows.Add(source.Rows[i].ItemArray);
        } catch {
          WriteDataErrorLog(source.TableName, i, source.Rows[i]);
        }
      }
      //target.EndLoadData();
    }

    private IAsyncResult BeginReplaceData(string sourceTableName, string targetTableName, string sourceQueryString,
                                          string targetEmptyQueryString, string targetDeleteQueryString,
                                          AsyncCallback callback) {
      ReplaceDataDelegate replaceDataDelegate = new ReplaceDataDelegate(DoReplaceData);

      return replaceDataDelegate.BeginInvoke(sourceTableName, targetTableName, sourceQueryString,
                                             targetEmptyQueryString, targetDeleteQueryString, callback, null);
    }

    private IAsyncResult BeginReplaceDataSet(string dataSetName, AsyncCallback callback) {
      ReplaceDataSetDelegate replaceDataSetDelegate = new ReplaceDataSetDelegate(DoReplaceDataSet);

      return replaceDataSetDelegate.BeginInvoke(dataSetName, callback, null);
    }

    private void EndReplaceData(IAsyncResult asyncResult) {
      ReplaceDataDelegate replaceDataDelegate = (ReplaceDataDelegate) ((AsyncResult) asyncResult).AsyncDelegate;

      replaceDataDelegate.EndInvoke(asyncResult);

      WriteLog(String.Empty);
      WriteLog("Finished at: " + DateTime.Now.ToLongTimeString());
      isRunning = false;
      WriteLogToDisk();
    }

    private void EndReplaceDataSet(IAsyncResult asyncResult) {
      ReplaceDataSetDelegate replaceDataSetDelegate = (ReplaceDataSetDelegate) ((AsyncResult) asyncResult).AsyncDelegate;

      replaceDataSetDelegate.EndInvoke(asyncResult);

      WriteLog(String.Empty);
      WriteLog("Finished at: " + DateTime.Now.ToLongTimeString());
      isRunning = false;
      WriteLogToDisk();
    }

    private int DoReplaceData(string sourceTableName, string targetTableName,
                              string sourceQueryString, string targetEmptyQueryString,
                              string targetDeleteQueryString) {
      try {
        DataTable sourceTable = GetSourceDataTable(sourceQueryString, sourceTableName);
        WriteLog(String.Empty);
        WriteLog("Se leyeron " + sourceTable.Rows.Count.ToString("N0") + " registros de la tabla " + sourceTableName);

        int maxCount = sourceTable.Rows.Count;
        int incrementSize = 0;
        if (this.targetDataSourceTechnology == DataTechnology.SqlServer ||
            this.targetDataSourceTechnology == DataTechnology.Oracle) {
          incrementSize = 100000;
        } else {
          incrementSize = 5000;
        }

        int increments = (maxCount / incrementSize) + 1;
        if (maxCount == 0) {
          increments = 0;
        }

        int counter = 0;
        Execute(targetDeleteQueryString);
        for (int i = 0; i < increments; i++) {
          WriteLog("Conversión incremental " + ((int) (i + 1)).ToString() + " de " + increments.ToString());
          int startRow = i * incrementSize;
          int endRow = ((i + 1) * incrementSize) - 1;
          if ((i + 1) == increments) {  // Is last increment
            endRow = startRow + (maxCount % incrementSize) - 1;
          }
          counter += UpdateTable(sourceTable, targetTableName, targetEmptyQueryString, startRow, endRow);
        }

        WriteLog("Se convirtieron en total " + counter.ToString("N0") + " registros de la tabla " + sourceTableName);

        DataTable targetTable = GetTargetDataTable("SELECT * FROM " + sourceTableName, sourceTableName);

        WriteLog("Se escribieron en total " + targetTable.Rows.Count.ToString("N0") + " registros en la tabla destino " + sourceTableName);
        if (sourceTable.Rows.Count == targetTable.Rows.Count) {
          WriteLog("Los registros de la tabla " + sourceTableName + " coinciden en número en las dos bases de datos.");
        } else {
          WriteLog("NOTA: La tabla " + sourceTableName + " en la base de datos destino finalizó con " +
                   (sourceTable.Rows.Count - targetTable.Rows.Count).ToString() + " registros menos, mismos que NO pudieron convertirse");
          WriteLog("Ver el detalle con los errores encontrados en la conversión al final de este archivo");
        }

        WriteLog(String.Empty);

        return counter;
      } catch (Exception exception) {
        isRunning = false;
        WriteLog("NOTA: Ver el detalle con los errores encontrados en la conversión al final de este archivo");
        WriteDataErrorLog(String.Empty);
        WriteDataErrorLog("Ocurrió un problema en la conversión de la tabla: " + sourceTableName + ":");
        WriteDataErrorLog(exception.ToString());
        WriteDataErrorLog(String.Empty);
        WriteDataErrorLog(String.Empty);
        WriteDataErrorLog("Finished at: " + DateTime.Now.ToLongTimeString());
        return -1;
      }
    }

    private int DoReplaceDataSet(string dataSetName) {

      WriteLog(String.Empty);
      WriteLog("Conversión del conjunto de datos: " + dataSetName);
      string config = ConfigurationData.GetString("Empiria.Data.Convertion.DataConverter", "Convertion.Set." + dataSetName);
      string[] dataSetSources = config.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

      string dataSource = String.Empty;
      string sourceSQL = String.Empty;
      string deleteFilter = String.Empty;
      string emptyFilter = String.Empty;

      int result = 0;
      for (int i = 0; i < dataSetSources.Length; i++) {

        try {
          dataSource = dataSetSources[i].Split('@')[0].Trim();
          sourceSQL = dataSetSources[i].Split('@')[1].Trim();
          emptyFilter = dataSetSources[i].Split('@')[2].Trim();
          if (dataSetSources[i].Length == 4) {
            deleteFilter = dataSetSources[i].Split('@')[3].Trim();
          } else {
            deleteFilter = dataSetSources[i].Split('@')[1].Trim();
          }

          if (sourceSQL.Length != 0 && !sourceSQL.Contains("SELECT")) {
            sourceSQL = "SELECT * FROM " + dataSource + " WHERE " + sourceSQL;
          } else if (sourceSQL.Length == 0) {
            sourceSQL = "SELECT * FROM " + dataSource;
          }
          if (emptyFilter.Length != 0 && !emptyFilter.Contains("SELECT")) {
            emptyFilter = "SELECT * FROM " + dataSource + " WHERE " + emptyFilter;
          } else if (emptyFilter.Length == 0) {
            emptyFilter = "SELECT * FROM " + dataSource;
          }
          if (deleteFilter.Length != 0) {
            deleteFilter = "DELETE FROM " + dataSource + " WHERE " + deleteFilter;
          } else {
            deleteFilter = "TRUNCATE TABLE " + dataSource;
          }
          result += DoReplaceData(dataSource, dataSource, sourceSQL, emptyFilter, deleteFilter);
        } catch (Exception exception) {
          isRunning = false;
          WriteDataErrorLog(String.Empty);
          WriteDataErrorLog("Ocurrió un problema al convertir el conjunto de datos: " + dataSetName);
          WriteDataErrorLog(exception.ToString());
          WriteDataErrorLog(String.Empty);
          WriteDataErrorLog(String.Empty);
        } // try
      } // for
      return result;
    }

    private int Execute(string queryString) {
      int affectedRows = 0;
      switch (targetDataSourceTechnology) {
        case DataTechnology.SqlServer:
          affectedRows = SqlHandler.Execute(targetDataSourceName, queryString);
          break;
        case DataTechnology.MySql:
          affectedRows = MySqlHandler.Execute(targetDataSourceName, queryString);
          break;
        case DataTechnology.Odbc:
          affectedRows = OdbcHandler.Execute(targetDataSourceName, queryString);
          break;
        default:
          throw new EmpiriaDataException(EmpiriaDataException.Msg.InvalidDatabaseTechnology);
      }
      WriteLog("Se afectaron " + affectedRows.ToString() + " registros, a través del comando: " + queryString);

      return affectedRows;
    }

    private int UpdateTable(DataTable source, string targetTableName, string targetEmptyQueryString,
                            int startRowIndex, int endRowIndex) {

      int affectedRows = 0;
      switch (targetDataSourceTechnology) {
        case DataTechnology.SqlServer:
          DataTable target = GetTargetDataTable(targetEmptyQueryString, targetTableName);
          affectedRows = SqlHandler.UpdateTable(targetDataSourceName, source, target,
                                                targetEmptyQueryString, startRowIndex, endRowIndex);
          if (target.HasErrors) {
            WriteDataErrorLog(target);
          }
          break;
        case DataTechnology.MySql:
          affectedRows = MySqlHandler.UpdateTable(targetDataSourceName, source, targetTableName,
                                                  startRowIndex, endRowIndex);
          break;
        case DataTechnology.Odbc:
          Tuple<int, string> returnValue = OdbcHandler.UpdateTable(targetDataSourceName, source, targetTableName,
                                                                   startRowIndex, endRowIndex);
          if (returnValue.Item2.Length != 0) {
            WriteDataErrorLog(returnValue.Item2);
          }
          affectedRows = returnValue.Item1;
          break;
        default:
          throw new EmpiriaDataException(EmpiriaDataException.Msg.InvalidDatabaseTechnology);
      }

      WriteLog("Se agregaron " + affectedRows.ToString() + " registros a la tabla " + targetTableName);

      return affectedRows;
    }

    private void WriteDataErrorLog(string text) {
      dataErrorLogText += text + System.Environment.NewLine;
    }

    private void WriteDataErrorLog(string dataTable, int index, DataRow row) {
      string errString = "Se detectaron errores al convertir los datos del registro " +
                          index.ToString("N0") + " de la tabla " + dataTable + ":";
      errString += System.Environment.NewLine;

      errString += "Registro: [";
      object[] columns = row.ItemArray;
      for (int i = 0; i < columns.Length; i++) {
        errString += (i != 0) ? "," : "" + columns[i].ToString();
      }
      errString += "]";
      errString += System.Environment.NewLine;

      WriteDataErrorLog(errString);
    }

    private void WriteDataErrorLog(DataTable table) {
      DataRow[] rowsWithError = table.GetErrors();
      WriteDataErrorLog(String.Empty);
      WriteDataErrorLog(String.Empty);
      WriteDataErrorLog("Se detectaron errores al importar los registros de la tabla " + table.TableName);
      WriteDataErrorLog(String.Empty);
      for (int i = 0; i < rowsWithError.Length; i++) {
        string errorMsg = "Error en registro [";
        for (int j = 0; j < table.Columns.Count; j++) {
          errorMsg += Convert.ToString(rowsWithError[i][j]) + (j == table.Columns.Count - 1 ? "" : ",");
        }
        errorMsg += "]";
        WriteDataErrorLog(errorMsg);
        WriteDataErrorLog(rowsWithError[i].RowError);
        DataColumn[] errorColumns = rowsWithError[i].GetColumnsInError();
        for (int j = 0; j < errorColumns.Length; j++) {
          WriteDataErrorLog("Columna: " + errorColumns[j].ColumnName + ", " +
                            "Valor = " + Convert.ToString(rowsWithError[i][j]) + ", " +
                            "Error = " + rowsWithError[i].GetColumnError(j));
        }
      }
      WriteDataErrorLog(String.Empty);
    }

    private void WriteLog(string text) {
      logText += text + System.Environment.NewLine;
    }

    private DataTechnology GetDataSourceTechnology(string dataSourceName) {
      int value = ConfigurationData.GetInteger("DS.Tech." + dataSourceName);

      return (DataTechnology) value;
    }

    private void WriteLogToDisk() {
      string message = "Tarea de conversión entre bases de datos";
      message += System.Environment.NewLine;

      message += logText;

      message += System.Environment.NewLine;
      message += System.Environment.NewLine;

      if (dataErrorLogText.Length == 0) {
        message += "No se detectaron errores en la conversión";
      } else {
        message += "***************************************************" + System.Environment.NewLine;
        message += "      Errores encontrados en la conversión" + System.Environment.NewLine;
        message += "***************************************************" + System.Environment.NewLine;
        message += System.Environment.NewLine;
        message += dataErrorLogText;
      }

      System.IO.File.WriteAllText(logFilePath + "data.convertion." +
                                  DateTime.Now.ToString("yyyy-MM-dd-HH.mm.ss.ffff") + ".log",
                                  message);

      dataErrorLogText = String.Empty;
      logText = String.Empty;
    }

    #endregion Private and internal methods

  } // class DataConvertionEngine

} // namespace Empiria.Data.Convertion
