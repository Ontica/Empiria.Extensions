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

using Empiria.Expressions;

namespace Empiria.Tests.Expressions {

  /// <summary>Test cases for the syntax tree parser.</summary>
  public class SyntaxTreeParserTests {

    #region Theories

    [Theory]
    [InlineData("1 + 1", 3)]
    public void Should_Parse_A_Simple_Tree(string expression, int nodesExpectedCount) {
      FixedList<IToken> tokens = new Tokenizer().Tokenize(expression);

      var parser = new SyntaxTreeParser(tokens);

      SyntaxTree sut = parser.SyntaxTree();

      Assert.NotNull(sut);
      Assert.Equal(nodesExpectedCount, sut.Count);
    }

    #endregion Theories

  }  // class SyntaxTreeParserTests

}  // namespace Empiria.Tests.Expressions
