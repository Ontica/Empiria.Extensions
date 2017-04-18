/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Empiria Web API Services            *
*  Namespace : Empiria.WebApi.Client                          Assembly : Empiria.WebApi.Client.dll           *
*  Type      : UtilityMethods                                 Pattern  : Information holder                  *
*  Version   : 1.2                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : General utility methods for use inside the WebApi.Client component.                           *
*                                                                                                            *
********************************* Copyright (c) 2016-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

namespace Empiria.WebApi.Client {

  /// <summary>General utility methods for use inside the WebApi.Client component.</summary>
  static internal class UtilityMethods {

    #region Methods

    static internal string BuildDataScopeParameter(string path) {
      string dataScope = String.Empty;

      if (path.Contains("::")) {
        dataScope = path.Substring(path.LastIndexOf("::"));
      }
      return dataScope;
    }


    static internal string GetDataScopeFromPath(string path) {
      string dataScope = String.Empty;

      if (path.Contains("::")) {
        dataScope = path.Substring(path.LastIndexOf("::") + 2);
      }
      return dataScope;
    }


    static internal string RemoveDataScopeFromPath(string path) {
      if (path.Contains("::")) {
        return path.Remove(path.LastIndexOf("::"));
      }
      return path;
    }

    #endregion Methods

  } // class UtilityMethods

} // namespace Empiria.WebApi.Client
