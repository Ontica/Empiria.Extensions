﻿/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parser                      *
*  Assembly : Empiria.Expressions.Tests.dll              Pattern   : Unit tests                              *
*  Type     : LogicalLibraryTests                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for logical library involved expressions.                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Xunit;

using System.Collections.Generic;

using Empiria.Expressions;

namespace Empiria.Tests.Expressions {

  /// <summary>Test cases for logical library involved expressions.</summary>
  public class LogicalLibraryTests {

    #region Theories

    [Theory]
    [InlineData("SI(true, 10, 4)", 10)]
    [InlineData("SI(a < b, a, b)", 6.91)]
    public void Should_Evaluate_SI(string textExpression, decimal result) {
      var expression = new Expression(textExpression);

      var data = new Dictionary<string, object> {
        { "a", 6.91 },
        { "b", 9.72 }
      };

      decimal sut = expression.Evaluate<decimal>(data);

      Assert.Equal(result, sut);
    }

    #endregion Theories

  }  // class LogicalLibraryTests

}  // namespace Empiria.Tests.Expressions
