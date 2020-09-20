/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                           Component : Services Layer                          *
*  Assembly : Empiria.Storage.dll                        Pattern   : Component services                      *
*  Type     : MediaFileTemplateServices                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Services used to interact with media file templates.                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.IO;

namespace Empiria.Storage {

  /// <summary>Services used to interact with media file templates.</summary>
  static public class MediaFileTemplateServices {

    #region Public services

    static public FileInfo CreateDerivedCopy(string mediaTemplateUID) {
      var templateFile = MediaFileTemplate.Parse(mediaTemplateUID);

      return templateFile.CreateDerivedCopy();
    }


    #endregion Public services

  }  // class MediaFileTemplateServices

} // namespace Empiria.Storage
