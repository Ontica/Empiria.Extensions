/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Empiria I/O Services                *
*  Namespace : Empiria.IO                                     Assembly : Empiria.IO.dll                      *
*  Type      : FileServices                                   Pattern  : Domain Service                      *
*  Version   : 6.7                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Empiria file I/O services.                                                                    *
*                                                                                                            *
********************************* Copyright (c) 2009-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Linq;
using System.IO;

using Empiria.Json;

namespace Empiria.Documents.IO {

  /// <summary>Empiria file I/O services.</summary>
  static public class FileServices {

    #region Public properties

    #endregion Public properties

    #region Public methods

    static public void AssureDirectory(string targetDirectory) {
      if (!Directory.Exists(targetDirectory)) {
        Directory.CreateDirectory(targetDirectory);
        FileAuditTrail.WriteOperation("AssureDirectory", "CreateDirectory",
                                      new JsonObject() { new JsonItem("folder", targetDirectory) });
      }
    }

    static public void AssureDirectoryForFile(string fileName) {
      string directory = fileName.Substring(0, fileName.LastIndexOf('\\'));

      AssureDirectory(directory);
    }

    public static void DeleteEmptyDirectories(string rootPath) {
      string[] paths = Directory.GetDirectories(rootPath);

      foreach (string path in paths) {
        DeleteWhenIsEmpty(path);
      }
    }

    static public void DeleteWhenIsEmpty(string folderPath) {
      if (DirectoryIsEmpty(folderPath)) {
        Directory.Delete(folderPath, false);
      }
    }

    static public bool DirectoryIsEmpty(string folderPath) {
      return (Directory.EnumerateFileSystemEntries(folderPath).Any() == false);
    }

    static public FileInfo[] GetFiles(string rootPath, string fileNameFilter) {
      Assertion.AssertObject(rootPath, "rootPath");
      Assertion.AssertObject(fileNameFilter, "fileNameFilter");
      Assertion.Assert(Directory.Exists(rootPath),
                       new IOServicesException(IOServicesException.Msg.DirectoryNotFound, rootPath));

      fileNameFilter = fileNameFilter.Replace(';', '|');
      fileNameFilter = fileNameFilter.Replace(',', '|');

      string[] searchPatternArray = fileNameFilter.Split('|');

      var directory = new DirectoryInfo(rootPath);

      FileInfo[] filesArray = new FileInfo[0];
      for (int i = 0; i < searchPatternArray.Length; i++) {
        FileInfo[] temp = directory.GetFiles(searchPatternArray[i]);
        if (i != 0) {
          filesArray = filesArray.Concat(temp).ToArray();
        } else {
          filesArray = temp;
        }
      }
      Array.Sort(filesArray, (x, y) => x.Name.CompareTo(y.Name));
      return filesArray;
    }

    static public string[] GetFileNames(string rootPath, string fileNameFilter) {
      FileInfo[] files = GetFiles(rootPath, fileNameFilter);

      return files.Select((x) => x.FullName).ToArray();
    }

    /// <summary>Moves a file to another folder. When the folder does not exist it is created.</summary>
    static public string MoveFileTo(FileInfo file, string destinationFolder) {
      destinationFolder = destinationFolder.TrimEnd('\\') + @"\";
      AssureDirectory(destinationFolder);

      string destinationFileFullName = destinationFolder + file.Name;
      file.MoveTo(destinationFileFullName);

      return destinationFileFullName;
    }

    /// <summary>Moves a file to another folder and also change it's name.
    ///When the folder does not exist it is created.</summary>
    static public string MoveFileTo(FileInfo file, string destinationFolder, string newFileName) {
      destinationFolder = destinationFolder.TrimEnd('\\') + @"\";
      AssureDirectory(destinationFolder);

      string destinationFileFullName = destinationFolder + newFileName;
      file.MoveTo(destinationFileFullName);

      return destinationFileFullName;
    }

    #endregion Public methods

  } // class FileServices

} // namespace Empiria.Documents.IO
