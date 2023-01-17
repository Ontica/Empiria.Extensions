/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parser                      *
*  Assembly : Empiria.Expressions.Tests.dll              Pattern   : Unit tests                              *
*  Type     : SyntaxTreeParserTests                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for the syntax tree parser.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Xunit;

namespace Empiria.Expressions.Tests.ExpressionsParsing {

  /// <summary>Test cases for the syntax tree parser.</summary>
  public class SyntaxTreeParserTests {

    #region Theories

    [Theory]
    [InlineData("1 + 1", 3)]
    [InlineData("(1 + 1)", 3)]
    public void Should_Parse_A_Simple_Tree(string expression, int nodesExpectedCount) {
      var tokenizer = new Tokenizer();

      FixedList<IToken> tokens = tokenizer.Tokenize(expression);

      var parser = new SyntaxTreeParser(tokens);

      SyntaxTree sut = parser.SyntaxTree();

      Assert.NotNull(sut);
      Assert.NotNull(sut.Root);
      Assert.Equal(nodesExpectedCount, sut.Root.Children.Count);
    }


    [Theory]
    [InlineData("1 + 1", "1 1 +")]
    [InlineData("(1 + 1)", "1 1 +")]
    [InlineData("(a + b) * (c - d)", "a b + c d - *")]
    [InlineData("-3 + SUM (a + b, 5, 7) - 8", "3 - a b + 5 7 SUM + 8 -")]
    [InlineData("ROUND(a + b, 4)", "a b + 4 ROUND")]
    [InlineData("ROUND((a + b) / c, 0)", "a b + c / 0 ROUND")]
    [InlineData("SUM(a + b)", "a b + SUM")]
    [InlineData("x - SUM(a, b, c)", "x a b c SUM -")]
    [InlineData("x + SUM(3000, a, b) - a", "x 3000 a b SUM + a -")]
    public void Should_Convert_To_Postfix_List(string expression, string expectedStream) {
      var tokenizer = new Tokenizer();

      FixedList<IToken> tokens = tokenizer.Tokenize(expression);

      var parser = new SyntaxTreeParser(tokens);

      FixedList<IToken> sut = parser.PostfixList();

      string sutAsString = ConvertToLexemeStream(sut);

      Assert.NotNull(sut);
      Assert.Equal(expectedStream, sutAsString);
    }


    private string ConvertToLexemeStream(FixedList<IToken> sut) {
      string temp = string.Empty;

      foreach (var token in sut) {
        temp += token.Lexeme + " ";
      }

      return temp.TrimEnd();
    }

    #endregion Theories

  }  // class SyntaxTreeParserTests

}  // namespace Empiria.Expressions.Tests.ExpressionsParsing
