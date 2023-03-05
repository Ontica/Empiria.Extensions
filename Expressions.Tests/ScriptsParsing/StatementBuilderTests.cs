/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Scripts Parsing                         *
*  Assembly : Empiria.Expressions.Tests.dll              Pattern   : Unit tests                              *
*  Type     : StatementBuilderTests                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for scripts statements builder.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Xunit;

namespace Empiria.Expressions.Tests.ScriptsParsing {

  /// <summary>Test cases for scripts statements builder.</summary>
  public class StatementBuilderTests {

    #region Theories

    [Theory]
    [InlineData("a := 5;", 1)]
    [InlineData("a := 5; b := 7;", 2)]
    [InlineData("; a := 5; ; b := 7;;", 2)]
    public void Should_Build_Assignments(string script,
                                         int expectedStatementsCount) {
      var builder = new StatementBuilder(script);

      FixedList<Statement> sut = builder.Build();

      Assert.Equal(expectedStatementsCount, sut.Count);

      Assert.All(sut, statement => Assert.NotNull(statement));
      Assert.All(sut, statement => Assert.Contains(statement.ToString(), script));
    }


    #endregion Theories

  }  // class StatementBuilderTests

}  // namespace Empiria.Expressions.Tests.ScriptsParsing
