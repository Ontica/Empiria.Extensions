/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parser                      *
*  Assembly : Empiria.Expressions.Tests.dll              Pattern   : Unit tests                              *
*  Type     : ArithmeticalExpressionsTests               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for arithmetical expressions evaluation.                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Xunit;

using System.Collections.Generic;

using Empiria.Expressions;

namespace Empiria.Tests.Expressions {

  /// <summary>Test cases for arithmetical expressions evaluation.</summary>
  public class ArithmeticalExpressionsTests {

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
    public void Should_Evaluate_Variables(string textExpression, decimal result) {
      var expression = new Expression(textExpression);

      var data = new Dictionary<string, object> {
        { "a", 6 },
        { "b", 7 },
        { "x", 22 }
      };

      decimal sut = expression.Evaluate<decimal>(data);

      Assert.Equal(result, sut);
    }

    #endregion Theories

  }  // class ArithmeticalExpressionsTests

}  // namespace Empiria.Tests.Expressions
