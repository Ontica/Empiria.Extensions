/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Libraries                               *
*  Assembly : Empiria.Expressions.Tests.dll              Pattern   : Unit tests                              *
*  Type     : LogicalLibraryTests                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for logical library involved expressions.                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Xunit;

using System.Collections.Generic;

namespace Empiria.Expressions.Tests.Libraries {

  /// <summary>Test cases for logical library involved expressions.</summary>
  public class LogicalLibraryTests {

    #region Theories

    [Theory]
    [InlineData("ALEATORIO()")]
    public void Should_Generate_Random(string textExpression) {
      var expression = new Expression(textExpression);

      _ = expression.Evaluate<bool>();

      Assert.True(true);
    }


    [Theory]
    [InlineData("SI(true, 10, 4)", 10)]
    [InlineData("SI(a < b, a, b)", 6.91)]
    public void Should_Evaluate_SI(string textExpression, decimal expected) {
      var expression = new Expression(textExpression);

      var data = new Dictionary<string, object> {
        { "a", 6.91 },
        { "b", 9.72 }
      };

      decimal sut = expression.Evaluate<decimal>(data);

      Assert.Equal(expected, sut);
    }

    #endregion Theories

  }  // class LogicalLibraryTests

}  // namespace Empiria.Expressions.Tests.Libraries
