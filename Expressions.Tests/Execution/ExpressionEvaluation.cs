/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parser                      *
*  Assembly : Empiria.Expressions.Tests.dll              Pattern   : Unit tests                              *
*  Type     : ExpressionEvaluationTests                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for the syntax tree parser.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Xunit;

using System.Collections.Generic;

using Empiria.Expressions;

namespace Empiria.Tests.Expressions {

  /// <summary>Test cases for the syntax tree parser.</summary>
  public class ExpressionEvaluationTests {

    #region Theories

    [Theory]
    [InlineData("6 + 7", 13)]
    [InlineData("(13 + 12) - 5", 20)]
    public void Should_Evaluate_Arithmetical_Expressions(string textExpression, decimal result) {
      var expression = new Expression(textExpression);

      decimal sut = expression.Evaluate<decimal>();

      Assert.Equal(result, sut);
    }


    [Theory]
    [InlineData("a + b", 13)]
    [InlineData("(x + 3) - 5", 20)]
    [InlineData("30 + VALORIZAR(10 + 10)", 430)]
    [InlineData("VALORIZAR(a + b)", 260)]
    [InlineData("250 - VALORIZAR(b + a - 3) + VALORIZAR(10 - a)", 280)]
    [InlineData("DEUDORAS_MENOS_ACREEDORAS(1000, a, b)", -1)]
    [InlineData("DEUDORAS_MENOS_ACREEDORAS(2000, a, b)", 1)]
    [InlineData("100 + DEUDORAS_MENOS_ACREEDORAS(1000, a, b) + DEUDORAS_MENOS_ACREEDORAS(2000, a, b) + 50", 150)]
    [InlineData("x + DEUDORAS_MENOS_ACREEDORAS(3000, a, b) - a", 15)]
    public void Should_Evaluate_Variables(string textExpression, decimal result) {
      var expression = new Expression(textExpression);

      var data = new Dictionary<string, object>();

      data.Add("a", 6);
      data.Add("b", 7);
      data.Add("x", 22);

      decimal sut = expression.Evaluate<decimal>(data);

      Assert.Equal(result, sut);
    }

    #endregion Theories

  }  // class ExpressionEvaluationTests

}  // namespace Empiria.Tests.Expressions
