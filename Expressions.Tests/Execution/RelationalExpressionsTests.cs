/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parser                      *
*  Assembly : Empiria.Expressions.Tests.dll              Pattern   : Unit tests                              *
*  Type     : RelationalExpressionsTests                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for relational expressions evaluation.                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Xunit;

using Empiria.Expressions;

namespace Empiria.Tests.Expressions {

  /// <summary>Test cases for relational expressions evaluation.</summary>
  public class RelationalExpressionsTests {

    #region Theories


    [Theory]
    [InlineData("1 == 1", true)]
    [InlineData("-3.29 == -3.29", true)]
    [InlineData("-4 == 4", false)]
    [InlineData("4 == -4", false)]
    public void Should_Evaluate_Equal_Expressions(string textExpression, bool expected) {
      var expression = new Expression(textExpression);

      bool sut = expression.Evaluate<bool>();

      Assert.Equal(expected, sut);
    }


    [Theory]
    [InlineData("2 > 1", true)]
    [InlineData("3.0001 > 3", true)]
    [InlineData("-5 >= -10", true)]
    [InlineData("2 >= 2", true)]
    [InlineData("5 >= 5.0001", false)]
    [InlineData("-5 >= -4", false)]
    [InlineData("5 >= -3", false)]
    public void Should_Evaluate_Greater_Than_Expressions(string textExpression, bool expected) {
      var expression = new Expression(textExpression);

      bool sut = expression.Evaluate<bool>();

      Assert.Equal(expected, sut);
    }


    [Theory]
    [InlineData("6 < 7", true)]
    [InlineData("-5 < 0", true)]
    [InlineData("1 < 1", false)]
    [InlineData("0 <= 1.78", true)]
    [InlineData("-1.05 <= -2.34", false)]
    public void Should_Evaluate_Less_Than_Expressions(string textExpression, bool expected) {
      var expression = new Expression(textExpression);

      bool sut = expression.Evaluate<bool>();

      Assert.Equal(expected, sut);
    }


    [Theory]
    [InlineData("1 != 2", true)]
    [InlineData("1 != 1", false)]
    [InlineData("3.05 != 3.050001", true)]
    [InlineData("-4 <> 4", true)]
    [InlineData("-3 != -4", true)]
    public void Should_Evaluate_Not_Equal_Expressions(string textExpression, bool expected) {
      var expression = new Expression(textExpression);

      bool sut = expression.Evaluate<bool>();

      Assert.Equal(expected, sut);
    }

    #endregion Theories

  }  // class RelationalExpressionsTests

}  // namespace Empiria.Tests.Expressions
