/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Empiria I/O Services                *
*  Namespace : Empiria.IO                                     Assembly : Empiria.IO.dll                      *
*  Type      : RootFilesFolder                                Pattern  : Storage Item                        *
*  Version   : 6.8                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Describes a root folder or directory that serves as a start point of documents storage.       *
*                                                                                                            *
********************************* Copyright (c) 2004-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

using Empiria.Ontology;

namespace Empiria.Documents.IO {

  /// <summary>Describes a root folder or directory that serves as a start point of documents storage.</summary>
  public class RootFilesFolder : FilesFolder {

    #region Constructors and parsers

    private RootFilesFolder() {
      // Required by Empiria Framework.
    }

    static public new RootFilesFolder Empty {
      get { return BaseObject.ParseEmpty<RootFilesFolder>(); }
    }

    static public new RootFilesFolder Parse(int id) {
      return BaseObject.ParseId<RootFilesFolder>(id);
    }

    static public FilesFolderList GetRootFilesFolders() {
      return DocumentsData.GetFilesFoldersList(ObjectTypeInfo.Parse<RootFilesFolder>());
    }

    internal protected override IIdentifiable Reference {
      get { return FilesFolder.Empty; }
    }

    #endregion Constructors and parsers

  } // class RootFilesFolder

} // namespace Empiria.Documents.IO
