/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Empiria I/O Services                *
*  Namespace : Empiria.IO                                     Assembly : Empiria.IO.dll                      *
*  Type      : DocumentsData                                  Pattern  : Data Services Static Class          *
*  Version   : 6.8                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Provides database read and write methods for documents management.                            *
*                                                                                                            *
********************************* Copyright (c) 2004-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Data;

using Empiria.Contacts;
using Empiria.Data;
using Empiria.Documents.IO;
using Empiria.Ontology;

namespace Empiria.Documents {

  /// <summary>Provides database read and write methods for documents management</summary>
  static internal class DocumentsData {

    #region Public methods

    static public FilesFolderList GetChildFilesFoldersList(FilesFolder parent) {
      throw new NotImplementedException();

      //var operation = DataOperation.Parse("qryLRSChildFileFolders", parent.Id);

      //var list = DataReader.GetList<FilesFolder>(operation, (x) => BaseObject.ParseList<FilesFolder>(x));

      //return new FilesFolderList(list);
    }

    static public DataView GetFilesFolders(ObjectTypeInfo filesFolderTypeInfo, string filter, string sort) {
      throw new NotImplementedException();

      //return DataReader.GetDataView(DataOperation.Parse("qryLRSFileFoldersWithType", filesFolderTypeInfo.Id),
      //                                                                               filter, sort);
    }

    static public FilesFolderList GetFilesFoldersList(ObjectTypeInfo filesFolderTypeInfo) {
      throw new NotImplementedException();

      //var operation = DataOperation.Parse("qryLRSFileFoldersWithType", filesFolderTypeInfo.Id);

      //var list = DataReader.GetList<FilesFolder>(operation, (x) => BaseObject.ParseList<FilesFolder>(x));

      //return new FilesFolderList(list);
    }

    static public string GetFilesFoldersFilter(Contact filesFolderOwner, string keywords) {
      string filter = SearchExpression.ParseAndLike("FilesFolderKeywords", keywords);

      if (!filesFolderOwner.IsEmptyInstance) {
        if (filter.Length != 0) {
          filter += " AND ";
        }
        filter += "(FilesFolderOwnerId = " + filesFolderOwner.Id.ToString() + ")";
      }
      return filter;
    }

    #endregion Public methods

    #region Internal methods

    static internal void WriteFilesFolder(FilesFolder o) {
      DataOperation dataOperation = DataOperation.Parse("writeLRSFilesFolder", o.Id, o.GetEmpiriaType().Id,
                                o.Owner.Id, o.WebServer.Id, o.PhysicalPath, o.PhysicalRootPath, o.VirtualRootPath,
                                o.DisplayName, o.Tags, o.FileNameFilters, o.Keywords, o.ImpersonationToken,
                                o.SubFoldersCount, o.FilesCount, o.FilesTotalSize, o.Reference.Id, o.CapturedBy.Id,
                                o.ReviewedBy.Id, o.ApprovedBy.Id, o.CreationDate, o.LastUpdateDate,
                                o.ParentFolder.Id, (char) o.Status, String.Empty);

      DataWriter.Execute(dataOperation);
    }

    #endregion Internal methods

  } // class Documents

} // namespace Empiria.Documents.Data
