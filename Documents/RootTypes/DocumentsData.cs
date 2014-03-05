/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework 2014                  System   : Document Management Services      *
*  Namespace : Empiria.Documents.IO                             Assembly : Empiria.Documents.dll             *
*  Type      : DocumentsData                                    Pattern  : Data Services Static Class        *
*  Version   : 5.5        Date: 28/Mar/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Provides database read and write methods for documents management.                            *
*                                                                                                            *
********************************* Copyright (c) 1999-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Data;

using Empiria.Contacts;
using Empiria.Data;
using Empiria.Documents.IO;
using Empiria.Ontology;

namespace Empiria.Documents {

  /// <summary>Provides database read and write methods for documents management</summary>
  static public class DocumentsData {

    #region Public methods

    static public FilesFolderList GetChildFilesFoldersList(FilesFolder parent) {
      return ConvertToFilesFolderList(GetChildFilesFolders(parent));
    }

    static public DataView GetChildFilesFolders(FilesFolder parent) {
      return DataReader.GetDataView(DataOperation.Parse("qryLRSChildFileFolders", parent.Id));
    }

    static public DataView GetFilesFolders(ObjectTypeInfo filesFolderTypeInfo) {
      return DataReader.GetDataView(DataOperation.Parse("qryLRSFileFoldersWithType", filesFolderTypeInfo.Id));
    }

    static public DataView GetFilesFolders(ObjectTypeInfo filesFolderTypeInfo, string filter, string sort) {
      return DataReader.GetDataView(DataOperation.Parse("qryLRSFileFoldersWithType", filesFolderTypeInfo.Id),
                                                                                     filter, sort);
    }

    static public FilesFolderList GetFilesFoldersList(ObjectTypeInfo filesFolderTypeInfo) {
      DataView foldersDataView = DocumentsData.GetFilesFolders(filesFolderTypeInfo);

      FilesFolderList list = new FilesFolderList();

      for (int i = 0; i < foldersDataView.Count; i++) {
        FilesFolder filesFolder = FilesFolder.Parse(foldersDataView[i].Row);

        list.Add(filesFolder);
      }
      return list;
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

    static public DataTable GetObjectNotes(int referenceId) {
      return DataReader.GetDataTable(DataOperation.Parse("qryFacilityNotes", referenceId));
    }

    #endregion Public methods

    #region Internal methods

    static internal int WriteFilesFolder(FilesFolder o) {
      DataOperation dataOperation = DataOperation.Parse("writeLRSFilesFolder", o.Id, o.ObjectTypeInfo.Id,
                                o.OwnerId, o.WebServer.Id, o.PhysicalPath, o.PhysicalRootPath, o.VirtualRootPath,
                                o.DisplayName, o.Tags, o.FileNameFilters, o.Keywords, o.ImpersonationToken,
                                o.SubFoldersCount, o.FilesCount, o.FilesTotalSize, o.ReferenceId, o.CapturedById,
                                o.ReviewedById, o.ApprovedById, o.CreationDate, o.LastUpdateDate,
                                o.ParentFilesFolderId, (char) o.Status, o.FilesIntegrityHashCode,
                                o.RecordIntegrityHashCode);
      return DataWriter.Execute(dataOperation);
    }

    static internal int WriteNote(int noteId, int noteTypeId, string noteDirection, int sourceContactId,
                                 int targetContactId, int referenceId, int objectId,
                                 DateTime noteTime, string noteText, int responseTypeId,
                                 string responseMedia, DateTime responseTime, string responseText,
                                 int parentNoteId, DateTime postingTime, int postedById, string noteStatus) {
      DataOperation dataOperation = DataOperation.Parse("writeEOSNote", noteId, noteTypeId, noteDirection, sourceContactId,
                                                        targetContactId, referenceId, objectId, noteTime, noteText,
                                                        responseTypeId, responseMedia, responseTime, responseText,
                                                        parentNoteId, postingTime, postedById, noteStatus);
      return DataWriter.Execute(dataOperation);
    }

    #endregion Internal methods

    #region Private methods

    static private FilesFolderList ConvertToFilesFolderList(DataView dataView) {
      FilesFolderList filesFolderList = new FilesFolderList();

      for (int i = 0; i < dataView.Count; i++) {
        filesFolderList.Add(FilesFolder.Parse(dataView[i].Row));
      }

      return filesFolderList;
    }

    //static private int GetNextNoteId() {
    //  return DataWriter.CreateId("EOSNotes");
    //}

    #endregion Private methods

  } // class Documents

} // namespace Empiria.Documents.Data