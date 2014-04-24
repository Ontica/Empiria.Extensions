/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework 2014                  System   : Document Management Services      *
*  Namespace : Empiria.Documents                                Assembly : Empiria.Documents.dll             *
*  Type      : FileType                                         Pattern  : Storage Item                      *
*  Version   : 5.5        Date: 25/Jun/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Describes the technology of a system file.                                                    *
*                                                                                                            *
********************************* Copyright (c) 2004-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/

namespace Empiria.Documents {

  /// <summary>Describes the technology of a system file.</summary>
  public class FileType : GeneralObject {

    #region Fields

    private const string thisTypeName = "ObjectType.GeneralObject.FileType";

    #endregion Fields

    #region Constructors and parsers

    public FileType()
      : base(thisTypeName) {

    }

    private FileType(string typeName)
      : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    static public FileType Parse(int id) {
      return BaseObject.Parse<FileType>(thisTypeName, id);
    }

    static public FileType Empty {
      get { return BaseObject.ParseEmpty<FileType>(thisTypeName); }
    }

    static public FileType Unknown {
      get { return BaseObject.ParseUnknown<FileType>(thisTypeName); }
    }

    static public FixedList<FileType> GetList() {
      return GeneralObject.ParseList<FileType>(thisTypeName);
    }

    #endregion Constructors and parsers

  } // class FileType

} // namespace Empiria.Documents