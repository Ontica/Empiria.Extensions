/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Scripts Execution                       *
*  Assembly : Empiria.Expressions.Tests.dll              Pattern   : Unit tests                              *
*  Type     : AssignmentExecutionTests                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for assignment statements execution.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Xunit;

using System.Collections.Generic;

namespace Empiria.Expressions.Tests.ScriptsExecution {

  /// <summary>Test cases for assignment statements execution.</summary>
  public class AssignmentExecutionTests {

    #region Theories

    [Theory]
    [InlineData("a := 5;", "a", 5)]
    [InlineData("b := 7.5 + 5;", "b", 12.5)]
    public void Should_Assign_Constants(string scriptText, string resultVariable, decimal resultValue) {
      var script = new Script(scriptText);

      var sut = new Dictionary<string, object> {
        { "a", 3 },
        { "b", 10 }
      };

      script.Execute(sut);

      Assert.Equal(resultValue, sut[resultVariable]);
    }


    [Theory]
    [InlineData("a := 5; b := a + 3; a := 2;", "b", 8)]
    public void Should_Execute_Assign_Scripts(string scriptText, string resultVariable, decimal resultValue) {
      var script = new Script(scriptText);

      var sut = new Dictionary<string, object> {
        { "a", 0 },
        { "b", 0 }
      };

      script.Execute(sut);

      Assert.Equal(resultValue, sut[resultVariable]);
    }


    [Theory]
    [InlineData("b := a; a := 0;", "b", 3)]
    public void Should_Execute_Assign_Scripts2(string scriptText, string resultVariable, decimal resultValue) {
      var script = new Script(scriptText);

      var sut = new Dictionary<string, object> {
        { "a", 3m },
        { "b", 0m }
      };

      script.Execute(sut);

      Assert.Equal(resultValue, sut[resultVariable]);
    }


    [Theory]
    [InlineData("b := a; a := 0;", "b", 3)]
    public void Should_Execute_Assign_Scripts3(string scriptText, string resultVariable, decimal resultValue) {
      var script = new Script(scriptText);

      var three = 3m;
      var zero = 0m;

      var sut = new Dictionary<string, object> {
        { "a", three },
        { "b", zero }
      };

      script.Execute(sut);

      Assert.Equal(resultValue, sut[resultVariable]);
    }

    #endregion Theories

  }  // class AssignmentExecutionTests

}  // namespace Empiria.Expressions.Tests.ScriptsExecution
