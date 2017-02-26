/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Empiria I/O Services                *
*  Namespace : Empiria.IO                                     Assembly : Empiria.IO.dll                      *
*  Type      : ImagingItem                                    Pattern  : Empiria Object Type                 *
*  Version   : 6.8                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Abstract class that represents an imaging folder or an image file.                            *
*                                                                                                            *
********************************* Copyright (c) 2009-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using Empiria.Json;

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
      protected set;
    }

    [DataField("ImagingItemPath")]
    public string ItemPath {
      get;
      protected set;
    }

    [DataField("ImagingItemExtData")]
    public JsonObject ImagingItemExtData {
      get;
      protected set;
    }

    #endregion Public properties

  } // class ImagingItem

} // namespace Empiria.Documents
