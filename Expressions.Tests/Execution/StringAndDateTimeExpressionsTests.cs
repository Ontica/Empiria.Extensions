/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parser                      *
*  Assembly : Empiria.Expressions.Tests.dll              Pattern   : Unit tests                              *
*  Type     : StringAndDateTimeExpressionsTests          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for the evaluation of date time and string expressions.                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Xunit;

using Empiria.Expressions;

namespace Empiria.Tests.Expressions {

  /// <summary>Test cases for the evaluation of datetime and string expressions.</summary>
  public class StringAndDateTimeExpressionsTests {

    #region Theories


    [Theory]
    [InlineData("'2022-01-09'", 2022, 01, 09)]
    [InlineData("'1978-12-14'", 1978, 12, 14)]
    [InlineData("'31/03/1970'", 1970, 03, 31)]
    [InlineData("'25/06/2012'", 2012, 06, 25)]
    public void Should_Evaluate_Date_Constants(string textExpression,
                                               int year, int month, int day) {
      var expression = new Expression(textExpression);

      object sut = expression.Evaluate<object>();

      var expected = new DateTime(year, month, day);

      Assert.Equal(expected, sut);
    }


    [Theory]
    [InlineData("'Hola'", "Hola")]
    [InlineData("'Hola mundo'", "Hola mundo")]
    [InlineData("'SAT-2321-A X'", "SAT-2321-A X")]
    [InlineData("'ABC/78'", "ABC/78")]
    [InlineData("'     SAT-   2321-A X     '", "     SAT-   2321-A X     ")]
    [InlineData("'A + B + C - (D + FG) * 5 '", "A + B + C - (D + FG) * 5 ")]
    public void Should_Evaluate_String_Constants(string textExpression, string expected) {
      var expression = new Expression(textExpression);

      object sut = expression.Evaluate<object>();

      Assert.Equal(expected, sut);
    }

    #endregion Theories

  }  // class StringAndDateTimeExpressionsTests

}  // namespace Empiria.Tests.Expressions
