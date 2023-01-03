/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Operator evaluator handler              *
*  Type     : OperatorHandler                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Performs operations with unitary and binary operators.                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Expressions.Execution {

  /// <summary>Performs operations with unitary and binary operators.</summary>
  internal class OperatorHandler : BaseEvaluatorHandler {

    private readonly IToken _operator;

    internal OperatorHandler(IToken @operator, IDictionary<string, object> data) {
      Assertion.Require(@operator, nameof(@operator));
      Assertion.Require(data, nameof(data));

      _operator = @operator;

      LoadData(data);
    }


    internal decimal Evaluate(IToken parameter) {
      if (_operator.Lexeme == "-") {
        return -1 * GetDecimal(parameter);
      }

      throw new NotImplementedException(
          $"Unitary operator '{_operator.Lexeme}' handler is not implemented.");
    }


    internal decimal Evaluate(IToken parameter1, IToken parameter2) {
      decimal x = GetDecimal(parameter1);
      decimal y = GetDecimal(parameter2);

      switch (_operator.Lexeme) {

        case "+":
          return x + y;

        case "-":
          return x - y;

        case "*":
          return x * y;

        case "/":
          return x / y;

        default:
          throw new NotImplementedException(
              $"Binary operator '{_operator.Lexeme}' handler is not implemented.");
      }
    }

  }  // class OperatorHandler

}  // namespace Empiria.Expressions.Execution
