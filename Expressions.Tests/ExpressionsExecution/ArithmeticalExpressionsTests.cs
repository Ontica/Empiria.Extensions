/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Execution                   *
*  Assembly : Empiria.Expressions.Tests.dll              Pattern   : Unit tests                              *
*  Type     : ArithmeticalExpressionsTests               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for arithmetical expressions evaluation.                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Xunit;

using System.Collections.Generic;

namespace Empiria.Expressions.Tests.ExpressionsExecution {

  /// <summary>Test cases for arithmetical expressions evaluation.</summary>
  public class ArithmeticalExpressionsTests {

    #region Theories

    [Theory]
    [InlineData("6 + 7", 13)]
    [InlineData("6.3 - 7.3", -1)]
    [InlineData("1 - 0.9890997", 0.0109003)]
    [InlineData("5 * 4 - 3", 17)]
    [InlineData("3 + 5 * 4", 23)]
    [InlineData("3 + 5 * 4 - 1", 22)]
    [InlineData("(13 + 12) - 5", 20)]
    public void Should_Evaluate_Arithmetical_Expressions(string textExpression, decimal result) {
      var expression = new Expression(textExpression);

      decimal sut = expression.Evaluate<decimal>();

      Assert.Equal(result, sut);
    }


    [Theory]
    [InlineData("0", 0)]
    [InlineData("3", 3)]
    [InlineData("-1", -1)]
    [InlineData("-1.000000", -1)]
    [InlineData("0.45001", 0.45001)]
    [InlineData("-0.45001", -0.45001)]
    public void Should_Evaluate_Decimal_Constants(string textExpression, decimal expected) {
      var expression = new Expression(textExpression);

      object sut = expression.Evaluate<object>();

      Assert.Equal(expected, sut);
    }


    [Theory]
    [InlineData("a", 6)]
    [InlineData("a + b", 13)]
    [InlineData("(x + 3) - 5", 20)]
    public void Should_Evaluate_Variables(string textExpression, decimal result) {
      var expression = new Expression(textExpression);

      var data = new Dictionary<string, object> {
        { "a", 6m },
        { "b", 7m },
        { "x", 22m }
      };

      decimal sut = expression.Evaluate<decimal>(data);

      Assert.Equal(result, sut);
    }

    #endregion Theories

  }  // class ArithmeticalExpressionsTests

}  // namespace Empiria.Expressions.Tests.ExpressionsExecution
