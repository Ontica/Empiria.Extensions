/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework 2014                  System   : Document Management Services      *
*  Namespace : Empiria.Documents.IO                             Assembly : Empiria.Documents.dll             *
*  Type      : DocumentsException                               Pattern  : Empiria Exception Class           *
*  Version   : 5.5        Date: 25/Jun/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : The exception that is thrown when a user interface process problem occurs.                    *
*                                                                                                            *
********************************* Copyright (c) 2009-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Reflection;

namespace Empiria.Documents {

  /// <summary>The exception that is thrown when a user interface process problem occurs.</summary>
  [Serializable]
  public sealed class DocumentsException : EmpiriaException {

    public enum Msg {
      CantCopyToNoneEmptyDirectory,
      CantDeleteInUseFolder,
      CantDeleteReferencedFilesFolder,
      CantParseFilesFolder,
      DirectoryNotFound,
      FileInfoInstanceExpected,
    }

    static private string resourceBaseName = "Empiria.Documents.RootTypes.DocumentsExceptionMsg";

    #region Constructors and parsers

    /// <summary>Initializes a new instance of PresentationException class with a specified error 
    /// message.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public DocumentsException(Msg message, params object[] args)
      : base(message.ToString(), GetMessage(message, args)) {

    }

    /// <summary>Initializes a new instance of PresentationException class with a specified error
    ///  message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="innerException">This is the inner exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public DocumentsException(Msg message, Exception innerException, params object[] args)
      : base(message.ToString(), GetMessage(message, args), innerException) {

    }

    #endregion Constructors and parsers

    #region Private methods

    static private string GetMessage(Msg message, params object[] args) {
      return GetResourceMessage(message.ToString(), resourceBaseName, Assembly.GetExecutingAssembly(), args);
    }

    #endregion Private methods

  } // class DocumentsException

} // namespace Empiria.Documents
