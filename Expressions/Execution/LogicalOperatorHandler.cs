/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Operator evaluator handler              *
*  Type     : LogicalOperatorHandler                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Performs logical operations with binary and unary operators.                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Expressions.Execution {

  /// <summary>Performs logical operations with binary and unary operators.</summary>
  internal class LogicalOperatorHandler : OperatorHandler {

    internal LogicalOperatorHandler(IToken @operator,
                                    IDictionary<string, object> data) : base(@operator, data) {
      //no-op
    }


    protected override internal object Evaluate(IToken parameter) {
      bool x = GetBoolean(parameter);

      switch (base.Operator.Lexeme) {

        case "!":
        case "NOT":
          return !x;

        default:

          throw new NotImplementedException(
            $"Unary logical operator '{base.Operator.Lexeme}' handler is not implemented.");
      }
    }


    protected override internal object Evaluate(IToken parameter1, IToken parameter2) {
      bool x = GetBoolean(parameter1);
      bool y = GetBoolean(parameter2);

      switch (base.Operator.Lexeme) {

        case "&&":
        case "AND":
          return x && y;

        case "||":
        case "OR":
          return x || y;

        default:
          throw new NotImplementedException(
              $"Binary logical operator '{base.Operator.Lexeme}' handler is not implemented.");
      }
    }

  }  // class LogicalOperatorHandler

}  // namespace Empiria.Expressions.Execution
