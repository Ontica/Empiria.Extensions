/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Scripts Parsing                         *
*  Assembly : Empiria.Expressions.Tests.dll              Pattern   : Unit tests                              *
*  Type     : ScriptsScannerTests                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for scripts scanning.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Xunit;

using System.Collections.Generic;

namespace Empiria.Expressions.Tests.ScriptsParsing {

  /// <summary>Test cases for scripts scanning.</summary>
  public class ScriptsScannerTests {

    #region Theories

    [Theory]
    [InlineData("a := 5;", 4)]
    public void Should_Scan_Assignment_Statements(string statement,
                                                  int lexemesExpectedCount) {
      var scanner = new Scanner();

      FixedList<string> sut = scanner.Scan(statement);

      Assert.Equal(lexemesExpectedCount, sut.Count);
      Assert.All(sut, x => Assert.NotNull(x));
      Assert.All(sut, x => Assert.Contains(x, statement));
    }

    #endregion Theories

  }  // class ScriptsScannerTests

}  // namespace Empiria.Expressions.Tests.ScriptsParsing
