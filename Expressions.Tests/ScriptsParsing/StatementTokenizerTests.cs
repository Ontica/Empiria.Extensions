/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Scripts Parsing                         *
*  Assembly : Empiria.Expressions.Tests.dll              Pattern   : Unit tests                              *
*  Type     : StatementTokenizerTests                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for the Tokenizer that converts an statement into streams of tokenized lexemes.     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Xunit;

namespace Empiria.Expressions.Tests.ScriptsParsing {

  /// <summary>est cases for the Tokenizer that converts an statement into streams of tokenized lexemes.</summary>
  public class StatementTokenizerTests {

    #region Theories

    [Theory]
    [InlineData("a := 5;", 4)]
    [InlineData("a := 7.5 + 2.5;", 6)]
    [InlineData("a := a + b - 8;", 8)]
    public void Should_Tokenize_Assignment_Statements(string statement,
                                                      int lexemesExpectedCount) {
      var scanner = new Scanner();

      FixedList<string> sut = scanner.Scan(statement);

      Assert.Equal(lexemesExpectedCount, sut.Count);

      Assert.All(sut, x => Assert.NotNull(x));
      Assert.All(sut, x => Assert.Contains(x, statement));
    }

    #endregion Theories

  }  // class StatementTokenizerTests

}  // namespace Empiria.Expressions.Tests.ScriptsParsing
