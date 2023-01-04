/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parser                      *
*  Assembly : Empiria.Expressions.Tests.dll              Pattern   : Unit tests                              *
*  Type     : ScannerTests                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for the Scanner, first stage of the tokenizer that builds lexemes streams.          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Xunit;

using Empiria.Expressions;

namespace Empiria.Tests.Expressions {

  /// <summary>Test cases for the Scanner, first stage of the tokenizer that builds lexemes streams.</summary>
  public class ScannerTests {

    #region Theories

    [Theory]
    [InlineData("1", 1)]
    [InlineData("1 + 1", 3)]
    [InlineData("1+1", 3)]
    [InlineData("+1", 2)]
    [InlineData("-1", 2)]
    [InlineData("-1 * 3", 4)]
    [InlineData("6 / -10", 4)]
    [InlineData("+1 - 1", 4)]
    [InlineData("-1 + 2 - 1 / 5", 8)]
    [InlineData("-143463.12 + 12.000 - 11.20 / 5", 8)]
    public void Should_Scan_Simple_Arithmetic_Expressions(string expression,
                                                          int lexemesExpectedCount) {
      var scanner = new Scanner();

      FixedList<string> sut = scanner.Scan(expression);

      Assert.Equal(lexemesExpectedCount, sut.Count);
      Assert.All(sut, x => Assert.NotNull(x));
      Assert.All(sut, x => Assert.Contains(x, expression));
    }


    [Theory]
    [InlineData("true", 1)]
    [InlineData("false", 1)]
    [InlineData("!P", 2)]
    [InlineData("!(P, Q)", 6)]
    [InlineData("P && !Q", 4)]
    [InlineData("P AND Q", 3)]
    [InlineData("P || !Q", 4)]
    [InlineData("!!Q", 3)]
    [InlineData("NOT P && NOT Q", 5)]
    [InlineData("P OR Q", 3)]
    [InlineData("(!P && !Q) || (P && Q)", 13)]
    [InlineData("((P && !(!Q || !R)))", 14)]
    public void Should_Scan_Logical_Expressions(string expression,
                                                int lexemesExpectedCount) {
      var scanner = new Scanner();

      FixedList<string> sut = scanner.Scan(expression);

      Assert.All(sut, x => Assert.NotNull(x));
      Assert.All(sut, x => Assert.Contains(x, expression));
      Assert.Equal(lexemesExpectedCount, sut.Count);
    }


    [Theory]
    [InlineData("X == Y", 3)]
    [InlineData("X > Y", 3)]
    [InlineData("x < Y", 3)]
    [InlineData("x != Y", 3)]
    [InlineData("x<=y && y<=z", 7)]
    [InlineData("(valor_1==y||valor2!=z)&& x >=z", 13)]
    [InlineData("(x ==y || x!= z)", 9)]
    public void Should_Scan_Relational_Expressions(string expression,
                                                   int lexemesExpectedCount) {
      var scanner = new Scanner();

      FixedList<string> sut = scanner.Scan(expression);

      Assert.All(sut, x => Assert.NotNull(x));
      Assert.All(sut, x => Assert.Contains(x, expression));
      Assert.Equal(lexemesExpectedCount, sut.Count);
    }


    [Theory]
    [InlineData("(1)", 3)]
    [InlineData("-(+1)", 5)]
    [InlineData("(3 + 5, VALOR)", 7)]
    [InlineData("5 + (4 - 3 * (4 + 8) )", 13)]
    [InlineData("RANDOM()", 3)]
    [InlineData("ORGANIZE(3, 5)", 6)]
    [InlineData("ANOTAR(valor1, valor2)", 6)]
    [InlineData("SUM(VALOR_1,MONEDA_2,-456.12,MONEDA_2)   ", 11)]
    [InlineData("   ABS(SUM( -456.12,VALOR_1 ,MONEDA_2 )", 11)]
    [InlineData("AB(-SUM(0.98123,categoria_1,categoria_2)", 11)]
    [InlineData("ABS   (-SUM (-x, 456.12,-y) ", 13)]
    [InlineData(" (SUM(SUM( -456.12, (VALOR_1 + MONEDA_2))))", 16)]
    [InlineData("ROUND(a + b, 4)", 8)]
    public void Should_Scan_Expressions_With_Parenthesis(string expression,
                                                         int lexemesExpectedCount) {
      var scanner = new Scanner();

      FixedList<string> sut = scanner.Scan(expression);

      Assert.All(sut, x => Assert.NotNull(x));
      Assert.All(sut, x => Assert.Contains(x, expression));
      Assert.Equal(lexemesExpectedCount, sut.Count);
    }

    #endregion Theories

  }  // class ScannerTests

}  // namespace Empiria.Tests.Expressions
