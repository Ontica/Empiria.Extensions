/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Oracle Data Handler                        Component : Test Helpers                            *
*  Assembly : Empiria.Data.Oracle.Tests.dll              Pattern   : Testing constants                       *
*  Type     : TestingConstants                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides testing constants.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Data.Handlers.Tests {

  /// <summary>Provides testing constants.</summary>
  static public class TestingConstants {

    static readonly internal string DATA_READER_SQL = ConfigurationData.GetString("DATA_READER_SQL");

    static readonly internal string GET_INT_SCALAR_SQL = ConfigurationData.GetString("GET_INT_SCALAR_SQL");

    static readonly internal string GET_INT_SCALAR_FIELD_NAME =
                                                  ConfigurationData.GetString("GET_INT_SCALAR_FIELD_NAME");

    static readonly internal long SCALAR_VALUE = ConfigurationData.GetInteger("SCALAR_VALUE");

    static readonly internal int DATA_SET_MINIMAL_ROWS_COUNT =
                                              ConfigurationData.GetInteger("DATA_SET_MINIMAL_ROWS_COUNT");


  }  // class TestingConstants

}  // namespace Empiria.Data.Handlers.Tests
