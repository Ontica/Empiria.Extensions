/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Information holder                      *
*  Type     : AssignmentHandler                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Assignment statement handler that assigns an expression result to a variable.                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using Empiria.Expressions.Execution;

namespace Empiria.Expressions {

  /// <summary>Assignment statement handler that assigns an expression result to a variable.</summary>
  internal class AssignmentHandler : IStatement {

    private readonly IToken _leftToken;
    private readonly ExpressionEvaluator _rightExpression;

    public AssignmentHandler(IToken leftToken, ExpressionEvaluator rightExpression) {
      Assertion.Require(leftToken, nameof(leftToken));
      Assertion.Require(rightExpression, nameof(rightExpression));

      _leftToken = leftToken;
      _rightExpression = rightExpression;
    }


    public void Execute(IDictionary<string, object> data) {
      if (!data.ContainsKey(_leftToken.Lexeme)) {
        data.Add(_leftToken.Lexeme, new object());
      }

      data[_leftToken.Lexeme] = _rightExpression.Evaluate(data);
    }

  }  // class AssignmentHandler

}  // namespace Empiria.Expressions
