/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Operator evaluator handler              *
*  Type     : RelationalOperatorHandler                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Performs relational operations with binary operators.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Expressions.Execution {

  /// <summary> Performs relational operations with binary operators.</summary>
  internal class RelationalOperatorHandler : OperatorHandler {

    internal RelationalOperatorHandler(IToken @operator,
                                         IDictionary<string, object> data) : base(@operator, data) {
      //no-op
    }


    protected override internal object Evaluate(IToken parameter) {
      throw new NotImplementedException(
          $"Unitary relational operator '{base.Operator.Lexeme}' handler is not implemented.");
    }


    protected override internal object Evaluate(IToken parameter1, IToken parameter2) {
      decimal x = GetDecimal(parameter1);
      decimal y = GetDecimal(parameter2);

      switch (base.Operator.Lexeme) {

        case "<":
          return x < y;

        case ">":
          return x > y;

        case "<=":
          return x <= y;

        case ">=":
          return x >= y;

        case "==":
          return x == y;

        case "!=":
        case "<>":
          return x != y;

        default:
          throw new NotImplementedException(
              $"Binary relational operator '{base.Operator.Lexeme}' handler is not implemented.");
      }
    }

  }  // class RelationalOperatorHandler

}  // namespace Empiria.Expressions.Execution
