/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Client                             Component : Tests Helpers                           *
*  Assembly : Empiria.WebApi.Client.Tests.dll            Pattern   : Testing constants                       *
*  Type     : TestingConstants                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides testing constants for WebApi.Client components.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Tests {

  /// <summary>Provides testing constants for WebApi.Client components.</summary>
  static internal class TestingConstants {

    static public string WEB_API_SERVER_NAME => ConfigurationData.GetString("Testing.WebApiServerName");

  }  // class TestingConstants

}  // namespace Empiria.Tests
