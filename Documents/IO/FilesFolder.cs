/* Empiria® Extended Framework 2014 **************************************************************************
*                                                                                                            *
*  Solution  : Empiria® Extended Framework 2014                 System   : Document Management Services      *
*  Namespace : Empiria.Documents.IO                             Assembly : Empiria.Documents.dll             *
*  Type      : FilesFolder                                      Pattern  : Empiria Object Type               *
*  Date      : 28/Mar/2014                                      Version  : 5.5     License: CC BY-NC-SA 4.0  *
*                                                                                                            *
*  Summary   : Asbtract class that provides read and write operations on operating system directories.       *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2014. **/
using System;
using System.Collections;
using System.Data;
using System.IO;

using Empiria.Ontology;
using Empiria.Security;

namespace Empiria.Documents.IO {

  /// <summary>Enumerates the different folder status.</summary>
  public enum FilesFolderStatus {
    Pending = 'P',
    Opened = 'O',
    Suspended = 'S',
    Closed = 'C',
    Deleted = 'X',
    Obsolete = 'Z'
  }

  /// <summary>Asbtract class that provides read and write operations on operating system directories.</summary>
  public abstract class FilesFolder : BaseObject {

    #region Fields

    private const string thisTypeName = "ObjectType.FilesFolder";

    private int ownerId = -1;
    private WebServer webServer = null;
    private string physicalPath = String.Empty;
    private string physicalRootPath = String.Empty;
    private string virtualRootPath = String.Empty;
    private string displayName = String.Empty;
    private string tags = String.Empty;
    private string fileNameFilters = String.Empty;
    private string keywords = String.Empty;
    private string impersonationToken = String.Empty;
    private int subFoldersCount = 0;
    private int filesCount = 0;
    private int filesTotalSize = 0;
    private int referenceId = -1;
    private int capturedById = -1;
    private int reviewedById = -1;
    private int approvedById = -1;
    private DateTime creationDate = DateTime.Now;
    private DateTime lastUpdateDate = DateTime.Now;
    private int parentFilesFolderId = -1;
    private FilesFolder parentFilesFolder = null;
    private FilesFolderStatus status = FilesFolderStatus.Pending;
    private string filesIntegrityHashCode = String.Empty;
    private string recordIntegrityHashCode = String.Empty;

    private FileInfo[] filesCache = null;

    #endregion Fields

    #region Constructors and parsers

    public FilesFolder()
      : base(thisTypeName) {

    }

    protected FilesFolder(string typeName)
      : base(typeName) {
      // Empiria Object Type pattern classes always has this constructor. Don't delete
    }

    static public FilesFolder Parse(int id) {
      return BaseObject.Parse<FilesFolder>(thisTypeName, id);
    }

    static internal FilesFolder Parse(DataRow dataRow) {
      return BaseObject.Parse<FilesFolder>(thisTypeName, dataRow);
    }

    static public FilesFolder Empty {
      get { return BaseObject.ParseEmpty<FilesFolder>(thisTypeName); }
    }

    static public FilesFolderList CreateAllFromPath(ObjectTypeInfo filesFolderTypeInfo,
                                                    FilesFolder parentFilesFolder, string path) {
      FilesFolderList filesFolderList = new FilesFolderList();

      FilesFolder rootFilesFolder = LoadFromPath(filesFolderTypeInfo, parentFilesFolder, path);

      filesFolderList.Add(rootFilesFolder);

      DirectoryInfo[] subdirectories = rootFilesFolder.GetDirectories();

      for (int i = 0; i < subdirectories.Length; i++) {
        FilesFolderList childsFolderList = CreateAllFromPath(filesFolderTypeInfo, parentFilesFolder, subdirectories[i].FullName);
        for (int j = 0; j < childsFolderList.Count; j++) {
          filesFolderList.Add(childsFolderList[j]);
        }
      }
      return filesFolderList;
    }

    static public DirectoryInfo GetDirectoryInfo(string directoryKey) {
      string path = Empiria.ConfigurationData.GetString("FilesFolder." + directoryKey);
      if (Directory.Exists(path)) {
        return new DirectoryInfo(path);
      } else {
        return null;
      }
    }

    static public bool IsEmpty(string path) {
      DirectoryInfo dir = new DirectoryInfo(path);

      int length = dir.GetFiles().Length;
      length += dir.GetDirectories().Length;

      return (length == 0);
    }

    static private FilesFolder LoadFromPath(ObjectTypeInfo filesFolderTypeInfo, FilesFolder parentFilesFolder, string path) {
      FilesFolder filesFolder = BaseObject.Create<FilesFolder>(filesFolderTypeInfo);

      filesFolder.ownerId = parentFilesFolder.OwnerId;
      filesFolder.webServer = WebServer.Current;
      filesFolder.physicalRootPath = parentFilesFolder.PhysicalRootPath;
      filesFolder.virtualRootPath = parentFilesFolder.VirtualRootPath;
      filesFolder.impersonationToken = parentFilesFolder.ImpersonationToken;
      filesFolder.fileNameFilters = parentFilesFolder.FileNameFilters;
      filesFolder.tags = parentFilesFolder.Tags;
      filesFolder.keywords = parentFilesFolder.Keywords;
      filesFolder.referenceId = -1;
      filesFolder.capturedById = -1;
      filesFolder.reviewedById = -1;
      filesFolder.approvedById = -1;

      filesFolder.SetDirectoryInfo(path);

      filesFolder.parentFilesFolderId = parentFilesFolder.Id;
      filesFolder.parentFilesFolder = parentFilesFolder;
      filesFolder.status = FilesFolderStatus.Pending;

      return filesFolder;
    }

    #endregion Constructors and parsers

    #region Public fields

    public int ApprovedById {
      get { return approvedById; }
      set { approvedById = value; }
    }

    public int CapturedById {
      get { return capturedById; }
      set { capturedById = value; }
    }

    public int SubFoldersCount {
      get { return subFoldersCount; }
    }

    public DateTime CreationDate {
      get { return creationDate; }
    }

    public string DisplayName {
      get { return displayName; }
    }

    public string FileNameFilters {
      get { return fileNameFilters; }
      protected set { fileNameFilters = value; }
    }

    public int FilesCount {
      get { return filesCount; }
    }

    public int FilesTotalSize {
      get { return filesTotalSize; }
    }

    public string ImpersonationToken {
      get { return impersonationToken; }
      protected set { impersonationToken = value; }
    }

    public DateTime LastUpdateDate {
      get { return lastUpdateDate; }
    }

    public int OwnerId {
      get { return ownerId; }
      set { ownerId = value; }
    }

    public FilesFolder ParentFilesFolder {
      get {
        if (parentFilesFolder == null) {
          parentFilesFolder = FilesFolder.Parse(this.parentFilesFolderId);
        }
        return parentFilesFolder;
      }
    }

    public int ParentFilesFolderId {
      get { return parentFilesFolderId; }
      set {
        parentFilesFolderId = value;
        parentFilesFolder = null;
      }
    }

    public string PhysicalPath {
      get { return physicalPath; }
    }

    public string PhysicalRootPath {
      get { return physicalRootPath; }
      protected set { physicalRootPath = value; }
    }

    public int ReviewedById {
      get { return reviewedById; }
      protected set { reviewedById = value; }
    }

    public FilesFolderStatus Status {
      get { return status; }
      protected set { status = value; }
    }

    public string Tags {
      get { return tags; }
      set { tags = value; }
    }

    public WebServer WebServer {
      get { return webServer; }
    }

    public string VirtualRootPath {
      get { return virtualRootPath; }
      protected set { virtualRootPath = value; }
    }

    #endregion Public fields

    #region Internal and protected fields

    internal string FilesIntegrityHashCode {
      get { return filesIntegrityHashCode; }
      set { filesIntegrityHashCode = value; }
    }

    internal string Keywords {
      get { return keywords; }
    }

    internal string RecordIntegrityHashCode {
      get { return recordIntegrityHashCode; }
      set { recordIntegrityHashCode = value; }
    }

    internal protected int ReferenceId {
      get { return referenceId; }
      protected set { referenceId = value; }
    }

    protected void ResetStatistics() {
      this.creationDate = ExecutionServer.DateMinValue;
      this.lastUpdateDate = ExecutionServer.DateMinValue;
      this.filesCache = null;
      this.subFoldersCount = 0;
      this.filesCount = 0;
      this.filesTotalSize = 0;
      this.filesIntegrityHashCode = String.Empty;
      this.recordIntegrityHashCode = String.Empty;
    }

    #endregion Internal and protected fields

    #region Protected methods

    public void CloneInto(FilesFolder clone) {
      System.Data.DataRow dataRow = this.GetDataRow();
      clone.ImplementsLoadObjectData(dataRow);
    }

    protected void CopyFrom(DirectoryInfo sourceFolder) {
      this.physicalPath = this.PhysicalPath.TrimEnd('\\') + @"\" + sourceFolder.Name;
      this.displayName = sourceFolder.Name;

      using (ImpersonationContext context = ImpersonationContext.Open(this.impersonationToken)) {
        DirectoryInfo targetDirectory = null;
        if (!Directory.Exists(this.physicalPath)) {
          targetDirectory = Directory.CreateDirectory(this.physicalPath);
        } else if (!IsEmpty(this.physicalPath)) {
          throw new DocumentsException(DocumentsException.Msg.CantCopyToNoneEmptyDirectory, this.physicalPath);
        }
        foreach (FileInfo file in sourceFolder.GetFiles()) {
          file.CopyTo(targetDirectory.FullName.TrimEnd('\\') + @"\" + file.Name);
        }
      }
      UpdateStatistics();
    }

    protected void DeleteFileAtIndex(int fileIndex) {
      FileInfo[] files = this.GetFiles();
      using (ImpersonationContext context = ImpersonationContext.Open(this.ImpersonationToken)) {
        files[fileIndex].Delete();
      }
    }

    protected DirectoryInfo[] GetDirectories() {
      DirectoryInfo[] directoriesArray = null;
      using (ImpersonationContext context = ImpersonationContext.Open(this.impersonationToken)) {
        DirectoryInfo directoryInfo = new DirectoryInfo(this.physicalPath);

        directoriesArray = directoryInfo.GetDirectories();
      }
      return directoriesArray;
    }

    protected FileInfo[] GetFiles() {
      if (filesCache == null) {
        string[] searchPatternArray = this.FileNameFilters.Split('|');

        if (searchPatternArray.Length == 1) {
          filesCache = GetFiles(searchPatternArray[0]);
        } else {
          ArrayList filesArray = new ArrayList();
          for (int i = 0; i < searchPatternArray.Length; i++) {
            filesArray.AddRange(GetFiles(searchPatternArray[i]));
          }
          filesArray.Sort(new FileNameComparer());

          filesCache = (FileInfo[]) filesArray.ToArray(typeof(FileInfo));
        }
      }
      return filesCache;
    }

    protected string MapPath(FileInfo file) {
      string fileName = file.FullName.Replace(this.PhysicalRootPath, this.VirtualRootPath);

      return fileName.Replace(@"\", @"/");
    }

    #endregion Protected methods

    #region Public methods

    protected void Refresh() {
      filesCache = null;
    }

    public FilesFolderList GetChilds() {
      return DocumentsData.GetChildFilesFoldersList(this);
    }

    public void Delete() {
      if (this.referenceId != -1) {
        throw new DocumentsException(DocumentsException.Msg.CantDeleteReferencedFilesFolder, this.displayName);
      }
      if (this.Status == FilesFolderStatus.Opened || this.Status == FilesFolderStatus.Closed) {
        throw new DocumentsException(DocumentsException.Msg.CantDeleteInUseFolder, this.displayName);
      }
      this.filesCount = 0;
      this.filesTotalSize = 0;
      this.subFoldersCount = 0;
      this.status = FilesFolderStatus.Deleted;
      base.Save();
    }

    public void UpdateStatistics() {
      this.filesCache = null;
      this.subFoldersCount = this.GetDirectories().Length;
      FileInfo[] files = this.GetFiles();
      this.filesCount = files.Length;
      this.filesTotalSize = this.CalculateFilesSize(files);
      this.filesIntegrityHashCode = String.Empty;
      this.recordIntegrityHashCode = String.Empty;
      if (this.tags.Length == 0) {
        this.tags = this.ParentFilesFolder.Tags;
      }
    }

    public void UpdateFilesCount() {
      DirectoryInfo directoryInfo = new DirectoryInfo(this.physicalPath);

      if (directoryInfo.Exists) {
        FileInfo[] files = this.GetFiles();

        this.filesIntegrityHashCode = files.Length.ToString();
      } else {
        this.filesIntegrityHashCode = "NO EXISTE";
      }
    }

    #endregion Public methods

    #region Private methods

    private int CalculateFilesSize(FileInfo[] files) {
      long size = 0;
      using (ImpersonationContext context = ImpersonationContext.Open(this.impersonationToken)) {
        for (int i = 0; i < files.Length; i++) {
          size += files[i].Length;
        }
      }
      return (int) (size / 1024L);
    }

    private FileInfo[] GetFiles(string fileNameFilter) {
      FileInfo[] fileInfoArray = null;
      using (ImpersonationContext context = ImpersonationContext.Open(this.impersonationToken)) {
        DirectoryInfo directoryInfo = new DirectoryInfo(this.physicalPath);

        fileInfoArray = directoryInfo.GetFiles(fileNameFilter);
      }
      return fileInfoArray;
    }

    protected override void ImplementsSave() {
      this.keywords = EmpiriaString.BuildKeywords(this.displayName, this.tags);

      DocumentsData.WriteFilesFolder(this);
    }

    protected override void ImplementsLoadObjectData(DataRow row) {
      this.ownerId = (int) row["FilesFolderOwnerId"];
      this.webServer = WebServer.Parse((int) row["WebServerId"]);
      this.physicalPath = (string) row["PhysicalPath"];
      this.physicalRootPath = (string) row["PhysicalRootPath"];
      this.virtualRootPath = (string) row["VirtualRootPath"];
      this.displayName = (string) row["FilesFolderDisplayName"];
      this.tags = (string) row["FilesFolderTags"];
      this.fileNameFilters = (string) row["FileNameFilters"];
      this.keywords = (string) row["FilesFolderKeywords"];
      this.impersonationToken = (string) row["ImpersonationToken"];
      this.subFoldersCount = (int) row["SubFoldersCount"];
      this.filesCount = (int) row["FilesCount"];
      this.filesTotalSize = (int) row["FilesTotalSize"];
      this.referenceId = (int) row["ReferenceId"];
      this.capturedById = (int) row["CapturedById"];
      this.reviewedById = (int) row["ReviewedById"];
      this.approvedById = (int) row["ApprovedById"];
      this.creationDate = (DateTime) row["CreationDate"];
      this.lastUpdateDate = (DateTime) row["LastUpdateDate"];
      this.parentFilesFolderId = (int) row["ParentFilesFolderId"];
      this.status = (FilesFolderStatus) Convert.ToChar(row["FilesFolderStatus"]); ;
      this.filesIntegrityHashCode = (string) row["FilesIntegrityHashCode"];
      this.recordIntegrityHashCode = (string) row["FilesFolderRIHC"];
    }

    private void SetDirectoryInfo(string path) {
      using (ImpersonationContext context = ImpersonationContext.Open(this.impersonationToken)) {
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        this.physicalPath = directoryInfo.FullName;
        this.displayName = directoryInfo.Name;
        this.creationDate = directoryInfo.CreationTime;
        this.lastUpdateDate = directoryInfo.LastAccessTime;
      }
    }

    #endregion Private methods

  } // class FilesFolder

} // namespace Empiria.Documents.IO