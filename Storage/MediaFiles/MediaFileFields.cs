/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Storage.dll                        Pattern   : Input Data Holder                       *
*  Type     : MediaFileFields                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Data structure with information about a media file object.                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Storage {

  /// <summary>Data structure with information about a media file object.</summary>
  public class MediaFileFields {

    public string MediaContent {
      get; set;
    } = string.Empty;


    public string MediaType {
      get; set;
    } = string.Empty;


    public int MediaLength {
      get; set;
    } = -1;


    public string OriginalFileName {
      get; set;
    } = string.Empty;


    internal string FolderPath {
      get; set;
    } = string.Empty;


    internal string FileHashCode {
      get; set;
    } = string.Empty;


  }  // class MediaFileFields

}  // namespace Empiria.Storage
