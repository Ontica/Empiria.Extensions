/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Empiria I/O Services                *
*  Namespace : Empiria.IO                                     Assembly : Empiria.IO.dll                      *
*  Type      : IOServicesException                            Pattern  : Exception Class                     *
*  Version   : 6.5                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : The exception that is thrown when a file system problem occurs.                               *
*                                                                                                            *
********************************* Copyright (c) 2004-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Reflection;

namespace Empiria.Documents.IO {

  /// <summary>The exception that is thrown when a file system problem occurs.</summary>
  [Serializable]
  public sealed class IOServicesException : EmpiriaException {

    public enum Msg {
      DirectoryNotFound,

      CantCopyToNoneEmptyDirectory,
      CantDeleteInUseFolder,
      CantDeleteReferencedFilesFolder,
      CantParseFilesFolder,

      FileInfoInstanceExpected,
    }

    static private string resourceBaseName = "Empiria.Documents.RootTypes.DocumentsExceptionMsg";

    #region Constructors and parsers

    /// <summary>Initializes a new instance of IOServicesException class with a specified error
    /// message.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public IOServicesException(Msg message, params object[] args) :
                               base(message.ToString(), GetMessage(message, args)) {

    }

    /// <summary>Initializes a new instance of IOServicesException class with a specified error
    ///  message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="innerException">This is the inner exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public IOServicesException(Msg message, Exception innerException, params object[] args) :
                               base(message.ToString(), GetMessage(message, args), innerException) {

    }

    #endregion Constructors and parsers

    #region Private methods

    static private string GetMessage(Msg message, params object[] args) {
      return GetResourceMessage(message.ToString(), resourceBaseName, Assembly.GetExecutingAssembly(), args);
    }

    #endregion Private methods

  } // class IOServicesException

} // namespace Empiria.Documents.IO
