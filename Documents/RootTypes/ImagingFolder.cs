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
  public class ImagingFolder : ImagingItem {

    #region Constructors and parsers

    private ImagingFolder() {

    }

    static private readonly ImagingFolder _empty = BaseObject.ParseEmpty<ImagingFolder>();
    static public new ImagingFolder Empty {
      get {
        return _empty.Clone<ImagingFolder>();
      }
    }

    static public ImagingFolder TryParse(string absolutePath) {
      return BaseObject.TryParse<ImagingFolder>("ImagingItemPath = '" + absolutePath + "'");
    }

    #endregion Constructors and parsers

    #region Public properties

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

    #region Public methods

    protected sealed override void GetFilesCounters(out int filesCount, out int totalSize) {
      filesCount = 0;
      totalSize = 0;
    }

    #endregion Public methods

  }  // class ImagingFolder

}  // namespace Empiria.Documents
