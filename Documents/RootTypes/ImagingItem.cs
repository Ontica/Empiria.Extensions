/* Empiria Extended Framework 2015 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                       System   : Document Management Services      *
*  Namespace : Empiria.Documents                                Assembly : Empiria.Documents.dll             *
*  Type      : ImagingItem                                      Pattern  : Empiria Object Type               *
*  Version   : 2.0        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Abstract class that represents an imaging folder or an image file.                            *
*                                                                                                            *
********************************* Copyright (c) 2009-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

using Empiria.Contacts;

namespace Empiria.Documents {

  /// <summary>Abstract class that represents an imaging folder or an image file.</summary>
  abstract public class ImagingItem : BaseObject {

    #region Constructors and parsers

    protected ImagingItem() {
      // Required by Empiria Framework.
    }

    static public ImagingItem Parse(int id) {
      return BaseObject.ParseId<ImagingItem>(id);
    }

    static public ImagingItem Empty {
      get {
        return BaseObject.ParseEmpty<ImagingItem>();
      }
    }

    #endregion Constructors and parsers

    #region Public properties

    [DataField("FilesCount")]
    public int FilesCount {
      get;
      private set;
    }

    [DataField("FilesTotalSize")]
    public int FilesTotalSize {
      get;
      private set;
    }

    [DataField("ImagingItemPath")]
    public string ItemPath {
      get;
      protected set;
    }

    #endregion Public properties

    #region Public methods

    abstract protected void GetFilesCounters(out int filesCount, out int totalSize);

    protected override void OnBeforeSave() {
      this.RecalculateData();
    }

    #endregion Public methods

    #region Private methods

    private void RecalculateData() {
      int filesCount = 0;
      int totalSize = 0;

      this.GetFilesCounters(out filesCount, out totalSize);

      this.FilesCount = filesCount;
      this.FilesTotalSize = totalSize;
    }

    #endregion Private methods

  } // class ImagingItem

} // namespace Empiria.Documents
