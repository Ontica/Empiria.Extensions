/* Empiria Extended Framework 2015 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Document Management Services        *
*  Namespace : Empiria.Documents.IO                           Assembly : Empiria.Documents.dll               *
*  Type      : FileAuditTrail                                 Pattern  : Service provider                    *
*  Version   : 2.0        Date: 25/Jun/2015                   License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Audit trail services for file system operations.                                              *
*                                                                                                            *
********************************* Copyright (c) 2009-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Empiria.Contacts;
using Empiria.Json;
using Empiria.Security;

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

} // namespace Empiria.Documents.IO
