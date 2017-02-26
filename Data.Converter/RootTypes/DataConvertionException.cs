/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Data Convertion Services          *
*  Namespace : Empiria.Data.Convertion                          Assembly : Empiria.Data.Convertion.dll       *
*  Type      : DataConvertionException                          Pattern  : Exception Class                   *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : The exception that is thrown when a data access operation fails.                              *
*                                                                                                            *
********************************* Copyright (c) 2007-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Empiria.Data.Convertion {

  /// <summary>The exception that is thrown when a data convertion operation fails.</summary>
  [Serializable]
  public sealed class DataConvertionException : EmpiriaException {

    public enum Msg {
      ConverterIsRunning,
      MySqlReadProblem,
      MySqlWriteProblem,
      OdbcReadProblem,
      OdbcWriteProblem,
      OleDbReadProblem,
      OleDbWriteProblem,
      OracleReadProblem,
      OracleWriteProblem,
    }

    static private string resourceBaseName = "Empiria.Data.Convertion.RootTypes.DataConvertionExceptionMsg";

    #region Constructors and parsers

    /// <summary>Initializes a new instance of DataConvertionException class with a specified error
    /// message.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public DataConvertionException(Msg message, params object[] args)
      : base(message.ToString(), GetMessage(message, args)) {
      try {
        base.Publish();
      } catch {
        // no-op
      }
    }

    /// <summary>Initializes a new instance of DataConvertionException class with a specified error
    ///  message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="exception">This is the inner exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public DataConvertionException(Msg message, Exception exception,
                                   params object[] args)
      : base(message.ToString(), GetMessage(message, args), exception) {
      try {
        base.Publish();
      } catch {
        // no-op
      }
    }

    public DataConvertionException(SerializationInfo info, StreamingContext context)
      : base(info, context) {

    }

    #endregion Constructors and parsers

    #region Private methods

    static private string GetMessage(Msg message, params object[] args) {
      return GetResourceMessage(message.ToString(), resourceBaseName, Assembly.GetExecutingAssembly(), args);
    }

    #endregion Private methods

  } // class EmpiriaDataException

} // namespace Empiria.Data
