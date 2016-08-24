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
          _directoryInfo = new DirectoryInfo(base.ItemPath);
        }
        return _directoryInfo;
      }
    }

    public string FileExtensions {
      get;
      private set;
    }

    public string FullPath {
      get {
        return this.Directory.FullName;
      }
    }

    public string Name {
      get {
        return this.Directory.Name;
      }
    }

    #endregion Public properties

  }  // class ImageSet

}  // namespace Empiria.Documents
