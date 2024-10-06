/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management          Component : Test Helpers                            *
*  Assembly : Empiria.Artifacts.Tests.dll                Pattern   : Testing constants                       *
*  Type     : TestingConstants                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides testing constants.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Tests.Artifacts {

  /// <summary>Provides testing constants.</summary>
  static public class TestingConstants {

    static internal string SOFTWARE_PRODUCT_UID => ConfigurationData.GetString("SOFTWARE_PRODUCT_UID");

  }  // class TestingConstants

}  // namespace Empiria.Tests.Artifacts
