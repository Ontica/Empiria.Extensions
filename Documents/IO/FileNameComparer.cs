/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework 2014                System   : Document Management Services        *
*  Namespace : Empiria.Documents.IO                           Assembly : Empiria.Documents.dll               *
*  Type      : FileNameComparer                               Pattern  : Storage Item                        *
*  Version   : 5.5        Date: 25/Jun/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Defines a file name comparision method for sort FileInfo objects array.                       *
*                                                                                                            *
********************************* Copyright (c) 2009-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System.Collections;
using System.IO;

namespace Empiria.Documents.IO {

  /// <summary>Defines a file name comparision method for sort FileInfo objects array.</summary>
  public class FileNameComparer : IComparer {

    #region Constructors and parsers

    public FileNameComparer() {

    }

    #endregion Constructors and parsers

    #region Public methods

    int IComparer.Compare(object x, object y) {
      Assertion.RequireObject(x, "x");
      Assertion.RequireObject(y, "y");
      Assertion.Require(x is FileInfo, new DocumentsException(DocumentsException.Msg.FileInfoInstanceExpected,
                                                              x.GetType().ToString()));
      Assertion.Require(y is FileInfo, new DocumentsException(DocumentsException.Msg.FileInfoInstanceExpected,
                                                              y.GetType().ToString()));

      return ((FileInfo) x).Name.ToLowerInvariant().CompareTo(((FileInfo) y).Name.ToLowerInvariant());
    }

    #endregion Public methods

  } // class FileNameComparer

} // namespace Empiria.Documents.IO