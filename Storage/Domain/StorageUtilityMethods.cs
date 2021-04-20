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
  static public class StorageUtilityMethods {


    static public string CalculateStreamHashCode(Stream stream) {
      stream.Position = 0;

      byte[] array = StreamToArray(stream);

      stream.Position = 0;

      string hash = Cryptographer.CreateHashCode(array);

      if (hash.Length > 128) {
        return hash.Substring(0, 128);
      } else {
        return hash;
      }
    }


    static public string CombinePaths(string path1, string path2) {
      return Path.Combine(path1, path2);
    }


    static public string GenerateUniqueFileNameForStorage(string originalFileName) {
      string extension = Path.GetExtension(originalFileName);

      return $"{Guid.NewGuid()}{extension}";
    }


    static public string GetFileFullName(MediaStorage storage, string fileName) {
      return Path.Combine(storage.Path, fileName);
    }


    static public byte[] StreamToArray(Stream stream) {
      byte[] array = new byte[stream.Length];

      stream.Read(array, 0, (int) stream.Length);

      return array;
    }


  }  // class UtilityMethods

}  // namespace Empiria.Storage
