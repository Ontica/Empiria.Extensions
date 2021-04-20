/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution : Empiria Extensions Framework                     System  : Data Access Library                 *
*  Assembly : Empiria.Data.Oracle.dll                          Pattern : Information Holder (with cache)     *
*  Type     : OracleParameterCache                             License : Please read LICENSE.txt file        *
*                                                                                                            *
*  Summary  : Wrapper of a static hash table that contains loaded Oracle stored-procedure parameters.        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace Empiria.Data.Handlers {

  /// <summary>Wrapper of a static hash table that contains loaded Oracle stored-procedure parameters.</summary>
  static internal class OracleParameterCache {

    #region Fields

    static private Dictionary<string, OracleParameter[]> parametersCache = new Dictionary<string, OracleParameter[]>();

    #endregion Fields

    #region Internal methods

    static internal OracleParameter[] GetParameters(string connectionString, string sourceName) {
      string hashKey = BuildHashKey(connectionString, sourceName);

      OracleParameter[] cachedParameters = null;
      if (!parametersCache.TryGetValue(hashKey, out cachedParameters)) {
        OracleParameter[] spParameters = DiscoverParameters(connectionString, sourceName);
        parametersCache[hashKey] = spParameters;
        cachedParameters = spParameters;
      }
      return CloneParameters(cachedParameters);
    }

    static internal OracleParameter[] GetParameters(string connectionString, string sourceName,
                                                    object[] parameterValues) {
      string hashKey = BuildHashKey(connectionString, sourceName);

      OracleParameter[] cachedParameters = null;
      if (!parametersCache.TryGetValue(hashKey, out cachedParameters)) {
        OracleParameter[] spParameters = DiscoverParameters(connectionString, sourceName);
        parametersCache[hashKey] = spParameters;
        cachedParameters = spParameters;
      }
      return CloneParameters(cachedParameters, parameterValues);
    }

    #endregion Internal methods

    #region Private methods

    static private string BuildHashKey(string connectionString, string sourceName) {
      return connectionString + ":" + sourceName;
    }

    static private OracleParameter[] CloneParameters(OracleParameter[] sourceParameters) {
      OracleParameter[] clonedParameters = new OracleParameter[sourceParameters.Length];

      for (int i = 0, j = sourceParameters.Length; i < j; i++) {
        clonedParameters[i] = (OracleParameter) ((ICloneable) sourceParameters[i]).Clone();
      }
      return clonedParameters;
    }

    static private OracleParameter[] CloneParameters(OracleParameter[] sourceParameters,
                                                     object[] parameterValues) {
      OracleParameter[] clonedParameters = new OracleParameter[sourceParameters.Length];

      for (int i = 0, j = sourceParameters.Length; i < j; i++) {
        clonedParameters[i] = (OracleParameter) ((ICloneable) sourceParameters[i]).Clone();
        clonedParameters[i].Value = parameterValues[i];
      }
      return clonedParameters;
    }

    static private OracleParameter[] DiscoverParameters(string connectionString, string sourceName) {
      OracleParameter[] discoveredParameters = null;
      using (OracleConnection connection = new OracleConnection(connectionString)) {

        OracleCommand command = new OracleCommand("qryDbQueryParameters", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("QueryName", OracleDbType.Varchar2, 64, ParameterDirection.Input);
        command.Parameters["QueryName"].Value = sourceName;
        OracleParameter output = command.Parameters.Add("getParameters", OracleDbType.RefCursor);
        connection.Open();
        command.ExecuteNonQuery();
        output.Direction = ParameterDirection.ReturnValue;

        OracleDataReader reader = ((OracleRefCursor) output.Value).GetDataReader();
        OracleParameter parameter;
        int i = 0;
        while (reader.Read()) {
          if (discoveredParameters == null) {
            discoveredParameters = new OracleParameter[(int)reader["ParameterCount"]];
          }
          parameter = new OracleParameter((string) reader["Name"], (int) reader["ParameterDbType"]);
          parameter.Direction = (ParameterDirection) reader["ParameterDirection"];
          if (reader["ParameterDefaultValue"] == DBNull.Value) {
            parameter.Value = reader["ParameterDefaultValue"];
          }
          discoveredParameters[i] = parameter;
          i++;
        }
        reader.Close();
      }
      return discoveredParameters;
    }

    #endregion Private methods

  } // class OracleParameterCache

} // namespace Empiria.Data.Handlers
