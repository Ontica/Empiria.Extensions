/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Operator evaluator handler              *
*  Type     : OperatorHandler                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Base abstract class for all operator handlers.                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Expressions.Execution {

  /// <summary>Base abstract class for all operator handlers.</summary>
  abstract internal class OperatorHandler : BaseEvaluatorHandler {

    protected OperatorHandler(IToken @operator, IDictionary<string, object> data) {
      Assertion.Require(@operator, nameof(@operator));
      Assertion.Require(@operator.Type == TokenType.Operator, "operator type is not TokenType.Operator");
      Assertion.Require(data, nameof(data));

      this.Operator = @operator;

      LoadData(data);
    }


    protected IToken Operator {
      get;
    }


    abstract protected internal decimal Evaluate(IToken parameter);


    abstract protected internal decimal Evaluate(IToken parameter1, IToken parameter2);


  }  // class OperatorHandler

}  // namespace Empiria.Expressions.Execution
