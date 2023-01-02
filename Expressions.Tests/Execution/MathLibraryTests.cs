/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parser                      *
*  Assembly : Empiria.Expressions.Tests.dll              Pattern   : Unit tests                              *
*  Type     : MathLibraryTests                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for math library involved expressions.                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Xunit;

using System.Collections.Generic;

using Empiria.Expressions;

namespace Empiria.Tests.Expressions {

  /// <summary>Test cases for math library involved expressions.</summary>
  public class MathLibraryTests {

    #region Theories

    [Theory]
    [InlineData("ABS(-13)", 13)]
    [InlineData("ABS(a * b)", 67.1652)]
    [InlineData("ABS(-1 *(a * b))", 67.1652)]
    public void Should_Evaluate_Absolute_Values(string textExpression, decimal result) {
      var expression = new Expression(textExpression);

      var data = new Dictionary<string, object> {
        { "a", 6.91 },
        { "b", 9.72 }
      };

      decimal sut = expression.Evaluate<decimal>(data);

      Assert.Equal(result, sut);
    }


    [Theory]
    [InlineData("SUM(10, 5)", 15)]
    [InlineData("SUM(5 + 5, 5)", 15)]
    [InlineData("SUM(a, 5)", 9.91)]
    [InlineData("SUM(a, b)", 13.63)]
    public void Should_Evaluate_Sum(string textExpression, decimal result) {
      var expression = new Expression(textExpression);

      var data = new Dictionary<string, object> {
        { "a", 4.91 },
        { "b", 8.72 }
      };

      decimal sut = expression.Evaluate<decimal>(data);

      Assert.Equal(result, sut);
    }


    [Theory]
    [InlineData("ROUND(a, 0)", 7)]
    [InlineData("ROUND(a + b, 0)", 15)]
    [InlineData("ROUND(a + b, 1)", 14.7)]
    [InlineData("ROUND(a + b, 2)", 14.73)]
    [InlineData("ROUND(a * b / 2, 0)", 27)]
    public void Should_Evaluate_Rounds(string textExpression, decimal result) {
      var expression = new Expression(textExpression);

      var data = new Dictionary<string, object> {
        { "a", 6.9489 },
        { "b", 7.7813 }
      };

      decimal sut = expression.Evaluate<decimal>(data);

      Assert.Equal(result, sut);
    }

    #endregion Theories

  }  // class MathLibraryTests

}  // namespace Empiria.Tests.Expressions
