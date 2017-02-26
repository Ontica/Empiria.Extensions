/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Empiria I/O Services                *
*  Namespace : Empiria.IO                                     Assembly : Empiria.IO.dll                      *
*  Type      : FileNameComparer                               Pattern  : Storage Item                        *
*  Version   : 6.8                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Defines a file name comparision method for sort FileInfo objects array.                       *
*                                                                                                            *
********************************* Copyright (c) 2004-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
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
      Assertion.AssertObject(x, "x");
      Assertion.AssertObject(y, "y");
      Assertion.Assert(x is FileInfo, new DocumentsException(DocumentsException.Msg.FileInfoInstanceExpected,
                                                             x.GetType().ToString()));
      Assertion.Assert(y is FileInfo, new DocumentsException(DocumentsException.Msg.FileInfoInstanceExpected,
                                                             y.GetType().ToString()));

      return ((FileInfo) x).Name.ToLowerInvariant().CompareTo(((FileInfo) y).Name.ToLowerInvariant());
    }

    #endregion Public methods

  } // class FileNameComparer

} // namespace Empiria.Documents.IO
