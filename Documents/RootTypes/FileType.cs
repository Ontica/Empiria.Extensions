/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Empiria I/O Services                *
*  Namespace : Empiria.IO                                     Assembly : Empiria.IO.dll                      *
*  Type      : FileType                                       Pattern  : Storage Item                        *
*  Version   : 6.8                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Describes the technology of a system file.                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Documents {

  /// <summary>Describes the technology of a system file.</summary>
  public class FileType : GeneralObject {

    #region Constructors and parsers

    private FileType() {
      // Required by Empiria Framework.
    }

    static public FileType Parse(int id) {
      return BaseObject.ParseId<FileType>(id);
    }

    static public FileType Empty {
      get { return BaseObject.ParseEmpty<FileType>(); }
    }

    static public FileType Unknown {
      get { return BaseObject.ParseUnknown<FileType>(); }
    }

    static public FixedList<FileType> GetList() {
      return GeneralObject.GetList<FileType>();
    }

    #endregion Constructors and parsers

  } // class FileType

} // namespace Empiria.Documents
