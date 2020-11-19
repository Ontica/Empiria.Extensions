/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                           Component : Services Layer                          *
*  Assembly : Empiria.Storage.dll                        Pattern   : Component services                      *
*  Type     : MediaFileTemplateServices                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Services used to interact with media file templates.                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.IO;

namespace Empiria.Storage {

  /// <summary>Services used to interact with media file templates.</summary>
  static public class MediaFileTemplateServices {

    #region Public services

    static public FileInfo CreateFileFromTemplate(string mediaTemplateUID) {
      var templateFile = MediaFileTemplate.Parse(mediaTemplateUID);

      return templateFile.CreateDerivedCopy();
    }


    static public string GeFileUrl(FileInfo fileInfo) {
      MediaStorage copiesStorage = MediaStorage.Default;

      return $"{copiesStorage.Url}/{fileInfo.Name}";
    }


    #endregion Public services

  }  // class MediaFileTemplateServices

} // namespace Empiria.Storage
