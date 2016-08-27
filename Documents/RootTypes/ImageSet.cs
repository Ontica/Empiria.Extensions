/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Empiria I/O Services                *
*  Namespace : Empiria.IO                                     Assembly : Empiria.IO.dll                      *
*  Type      : ImagingFolder                                  Pattern  : Empiria Object Type                 *
*  Version   : 6.7                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : A folder to store imaging content.                                                            *
*                                                                                                            *
********************************* Copyright (c) 2009-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.IO;

namespace Empiria.Documents {

  /// <summary>A folder to store imaging content.</summary>
  public class ImageSet : ImagingItem {

    #region Constructors and parsers

    protected ImageSet() {

    }

    static public new ImageSet Parse(int id) {
      return BaseObject.ParseId<ImageSet>(id);
    }

    #endregion Constructors and parsers

    #region Public properties

    [DataField("BaseFolderId")]
    public ImagingFolder BaseFolder {
      get;
      protected set;
    }

    private DirectoryInfo _directoryInfo = null;
    public DirectoryInfo Directory {
      get {
        if (_directoryInfo == null) {
          _directoryInfo = new DirectoryInfo(this.FullPath);
        }
        return _directoryInfo;
      }
    }

    public virtual string FullPath {
      get {
        return this.ItemPath.Replace("~", this.BaseFolder.ItemPath)
                             .ToUpperInvariant();
      }
    }

    public virtual string UrlRelativePath {
      get {
        return this.ItemPath.Replace("~", this.BaseFolder.UrlRelativePath)
                            .Replace('\\', '/') + '/';
      }
    }

    private string[] _imagesFileNamesArray = null;
    public string[] ImagesNamesArray {
      get {
        if (_imagesFileNamesArray == null) {
          _imagesFileNamesArray = this.GetImagesFileNamesArray();
        }
        return _imagesFileNamesArray;
      }
    }

    #endregion Public properties

    #region Public methods

    protected virtual string[] GetImagesFileNamesArray() {
      FileInfo[] files = this.Directory.GetFiles(this.BaseFolder.FileExtension);

      base.FilesCount = files.Length;

      string[] array = new string[files.Length];
      for (int i = 0; i < files.Length; i++) {
        array[i] = files[i].Name.ToUpperInvariant();
      }
      return array;
    }

    #endregion Public methods

  }  // class ImageSet

}  // namespace Empiria.Documents
