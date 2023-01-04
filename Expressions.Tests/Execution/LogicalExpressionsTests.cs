/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parser                      *
*  Assembly : Empiria.Expressions.Tests.dll              Pattern   : Unit tests                              *
*  Type     : LogicalExpressionsTests                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for logical expressions evaluation.                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Xunit;

using Empiria.Expressions;

namespace Empiria.Tests.Expressions {

  /// <summary>Test cases for logical expressions evaluation.</summary>
  public class LogicalExpressionsTests {

    #region Theories

    [Theory]
    [InlineData("(1 < 3) AND (3 <= 5)", true)]
    [InlineData("(1 == 3) AND (3 <= 5)", false)]
    [InlineData("-1.2 < 3.2 AND 3.2 <= 5", true)]
    [InlineData("true AND 3.006 <= 3.006", true)]
    public void Should_Evaluate_And_Expressions(string textExpression, bool expected) {
      var expression = new Expression(textExpression);

      bool sut = expression.Evaluate<bool>();

      Assert.Equal(expected, sut);
    }


    [Theory]
    [InlineData("true", true)]
    [InlineData("false", false)]
    [InlineData("true AND true", true)]
    [InlineData("false AND true", false)]
    [InlineData("false OR true", true)]
    [InlineData("true || false", true)]
    [InlineData("false OR false", false)]
    [InlineData("false || true", true)]
    [InlineData("!false", true)]
    [InlineData("NOT false", true)]
    [InlineData("!true", false)]
    [InlineData("NOT true", false)]
    public void Should_Evaluate_Logical_Constants(string textExpression, bool expected) {
      var expression = new Expression(textExpression);

      bool sut = expression.Evaluate<bool>();

      Assert.Equal(expected, sut);
    }

    #endregion Theories

  }  // class LogicalExpressionsTests

}  // namespace Empiria.Tests.Expressions
