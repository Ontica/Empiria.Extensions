/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework 2014                  System   : Document Management Services      *
*  Namespace : Empiria.Documents                                Assembly : Empiria.Documents.dll             *
*  Type      : FileType                                         Pattern  : Storage Item                      *
*  Version   : 6.0        Date: 23/Oct/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Describes the technology of a system file.                                                    *
*                                                                                                            *
********************************* Copyright (c) 2004-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/

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
      return GeneralObject.ParseList<FileType>();
    }

    #endregion Constructors and parsers

  } // class FileType

} // namespace Empiria.Documents
