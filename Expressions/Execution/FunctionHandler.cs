/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Abstract type                           *
*  Type     : FunctionHandler                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Abstract base class for Empiria Expressions functions that can be evaluated.                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Expressions.Execution {

  /// <summary>Abstract base class for Empiria Expressions functions that can be evaluated.</summary>
  abstract public class FunctionHandler : BaseEvaluatorHandler {

    public IList<IToken> Parameters {
      get;
      private set;
    }

    protected internal abstract decimal Evaluate();


    internal void SetParameters(IList<IToken> parameters) {
      Assertion.Require(parameters, nameof(parameters));

      this.Parameters = parameters;
    }

  }  // class FunctionHandler

}  // namespace Empiria.Expressions.Execution
