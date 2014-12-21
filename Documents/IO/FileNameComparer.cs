/* Empiria Extended Framework 2015 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Document Management Services        *
*  Namespace : Empiria.Documents.IO                           Assembly : Empiria.Documents.dll               *
*  Type      : FileNameComparer                               Pattern  : Storage Item                        *
*  Version   : 6.0        Date: 04/Jan/2015                   License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Defines a file name comparision method for sort FileInfo objects array.                       *
*                                                                                                            *
********************************* Copyright (c) 2004-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
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
