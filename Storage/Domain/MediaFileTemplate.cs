/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                           Component : Domain Layer                            *
*  Assembly : Empiria.Storage.dll                        Pattern   : Information holder                      *
*  Type     : MediaFileTemplate                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : A media file that is used as a template to get derived copies of the same file type.           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.IO;

namespace Empiria.Storage {

  /// <summary>A media file that is used as a template to get derived copies of the same file type.</summary>
  internal class MediaFileTemplate : MediaFile {

    #region Constructors and parsers

    protected MediaFileTemplate() {
      // Required by Empiria Framework
    }


    static public new MediaFileTemplate Parse(string uid) {
      return BaseObject.ParseKey<MediaFileTemplate>(uid);
    }

    #endregion Constructors and parsers

    #region Methods

    public FileInfo CreateDerivedCopy() {
      var derivedCopyFileName = DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss-") + base.OriginalFileName;

      MediaStorage copiesStorage = MediaStorage.Default;

      string derivedCopyFullName = Path.Combine(copiesStorage.Path, derivedCopyFileName);

      File.Copy(this.FullName, derivedCopyFullName, true);


      return new FileInfo(derivedCopyFullName);
    }


    #endregion Methods

  }  // class MediaFileTemplate

}  // namespace Empiria.Storage
