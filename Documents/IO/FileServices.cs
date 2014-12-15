/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework 2014                System   : Document Management Services        *
*  Namespace : Empiria.Documents.IO                           Assembly : Empiria.Documents.dll               *
*  Type      : FileServices                                   Pattern  : Domain Service                      *
*  Version   : 2.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Empiria file I/O services.                                                                    *
*                                                                                                            *
********************************* Copyright (c) 2009-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Empiria.Contacts;
using Empiria.Json;
using Empiria.Security;

namespace Empiria.Documents.IO {

  /// <summary>Empiria file I/O services.</summary>
  static public class FileServices {

    #region Public properties

    #endregion Public properties

    #region Public methods

    static public void AssureDirectory(string targetDirectory) {
      if (!Directory.Exists(targetDirectory)) {
        Directory.CreateDirectory(targetDirectory);
        AuditTrail.WriteOperation("AssureDirectory", "CreateDirectory",
                                  new JsonRoot() { new JsonItem("folder", targetDirectory) } );
      }
    }

    static public void AssureDirectoryForFile(string fileName) {
      string directory = fileName.Substring(0, fileName.LastIndexOf('\\'));

      AssureDirectory(directory);
    }

    static public string[] GetFileNames(string rootPath, string fileNameFilter) {
      Assertion.AssertObject(rootPath, "rootPath");
      Assertion.AssertObject(fileNameFilter, "fileNameFilter");
      Assertion.Assert(Directory.Exists(rootPath),
                       new IOServicesException(IOServicesException.Msg.DirectoryNotFound, rootPath));

      fileNameFilter = fileNameFilter.Replace(';', '|');
      fileNameFilter = fileNameFilter.Replace(',', '|');

      string[] searchPatternArray = fileNameFilter.Split('|');

      var directory = new DirectoryInfo(rootPath);

      string[] filesArray = new string[0];
      for (int i = 0; i < searchPatternArray.Length; i++) {
        string[] temp = FileServices.GetFileNames(directory, searchPatternArray[i]);
        if (i != 0) {
          filesArray = filesArray.Concat(temp).ToArray();
        } else {
          filesArray = temp;
        }
      }
      Array.Sort(filesArray);
      return filesArray;
    }

    static public string MoveFileTo(FileInfo file, string destinationFolder) {
      destinationFolder = destinationFolder.TrimEnd('\\') + @"\";
      AssureDirectory(destinationFolder);

      string destinationFileFullName = destinationFolder + file.Name;
      file.MoveTo(destinationFileFullName);

      return destinationFileFullName;
    }

    static public string MoveFileTo(FileInfo file, string destinationFolder, string newFileName) {
      destinationFolder = destinationFolder.TrimEnd('\\') + @"\";
      AssureDirectory(destinationFolder);

      string destinationFileFullName = destinationFolder + newFileName;
      file.MoveTo(destinationFileFullName);

      return destinationFileFullName;
    }

    #endregion Public methods

    #region Private methods

    static private string[] GetFileNames(DirectoryInfo root, string fileNameFilter) {
      FileInfo[] files = root.GetFiles(fileNameFilter);
      if (files.Length != 0) {
        return files.Select((x) => x.FullName).ToArray();
      } else {
        return new string[0];
      }
    }

    #endregion Private methods

  } // class FileServices

} // namespace Empiria.Documents.IO
