/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Media Management                           Component : Domain Layer                            *
*  Assembly : Empiria.Storage.dll                        Pattern   : Methods library                         *
*  Type     : UtilityMethods                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Utility methods library to manipulate streams and files.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.IO;

using Empiria.Security;

namespace Empiria.Storage {

  /// <summary>Utility methods library to manipulate streams and files.</summary>
  static internal class UtilityMethods {


    static internal string CalculateStreamHashCode(Stream stream) {
      stream.Position = 0;

      byte[] array = UtilityMethods.StreamToArray(stream);

      stream.Position = 0;

      string hash = Cryptographer.CreateHashCode(array);

      if (hash.Length > 128) {
        return hash.Substring(0, 128);
      } else {
        return hash;
      }
    }


    static internal string GenerateUniqueFileNameForStorage(string originalFileName) {
      string extension = Path.GetExtension(originalFileName);

      return $"{Guid.NewGuid()}{extension}";
    }


    static internal string GetFileFullName(MediaStorage storage, string fileName) {
      return Path.Combine(storage.Path, fileName);
    }


    static internal byte[] StreamToArray(Stream stream) {
      byte[] array = new byte[stream.Length];

      stream.Read(array, 0, (int) stream.Length);

      return array;
    }


  }  // class UtilityMethods

}  // namespace Empiria.Storage
