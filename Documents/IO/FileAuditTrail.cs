/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Empiria I/O Services                *
*  Namespace : Empiria.IO                                     Assembly : Empiria.IO.dll                      *
*  Type      : FileAuditTrail                                 Pattern  : Service provider                    *
*  Version   : 6.7                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Audit trail services for file system operations.                                              *
*                                                                                                            *
********************************* Copyright (c) 2009-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

using Empiria.Json;

namespace Empiria.Documents.IO {

  /// <summary>Audit trail services for file system operations.</summary>
  static public class FileAuditTrail {

    #region Public methods

    public static void WriteOperation(string p1, string p2, JsonObject jsonObject) {
      throw new NotImplementedException();
    }

    public static void WriteException(string p1, string p2, JsonObject jsonObject) {
      throw new NotImplementedException();
    }

    #endregion Public methods

  } // class FileServices

} // namespace Empiria.IO
