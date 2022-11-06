/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parser                      *
*  Assembly : Empiria.Expressions.Tests.dll              Pattern   : Unit tests                              *
*  Type     : TokenizerTests                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for the Tokenizer, our lexical analyzer (a.k.a scanner or lexer).                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Xunit;

using Empiria.Expressions;

namespace Empiria.Tests.Expressions {

  /// <summary>Test cases for expression parsing.</summary>
  public class TokenizerTests {

    #region Theories

    [Theory]
    [InlineData("1 + 1", 3)]
    public void ShouldS(string expression, int tokensCount) {

      var tokenizer = new Tokenizer();

      FixedList<IExpressionToken> sut = tokenizer.Tokenize(expression);

      Assert.Equal(sut.Count, tokensCount);
    }

    #endregion Theories

  }  // class TokenizerTests

}  // namespace Empiria.Tests.Expressions
