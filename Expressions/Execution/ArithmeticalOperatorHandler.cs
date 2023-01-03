/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Operator evaluator handler              *
*  Type     : ArithmeticalOperatorHandler                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Performs arithmetical operations with unitary and binary operators.                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Expressions.Execution {

  /// <summary>Performs arithmetical operations with unitary and binary operators.</summary>
  internal class ArithmeticalOperatorHandler : OperatorHandler {

    internal ArithmeticalOperatorHandler(IToken @operator,
                                         IDictionary<string, object> data) : base(@operator, data) {
      //no-op
    }


    protected override internal object Evaluate(IToken parameter) {
      if (base.Operator.Lexeme == "-") {
        return -1 * GetDecimal(parameter);
      }

      throw new NotImplementedException(
          $"Unitary arithmetical operator '{base.Operator.Lexeme}' handler is not implemented.");
    }


    protected override internal object Evaluate(IToken parameter1, IToken parameter2) {
      decimal x = GetDecimal(parameter1);
      decimal y = GetDecimal(parameter2);

      switch (base.Operator.Lexeme) {

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
              $"Binary arithmetical operator '{base.Operator.Lexeme}' handler is not implemented.");
      }
    }

  }  // class ArithmeticalOperatorHandler

}  // namespace Empiria.Expressions.Execution
