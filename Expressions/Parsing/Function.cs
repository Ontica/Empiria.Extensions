/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Service provider                        *
*  Type     : Function                                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : ERxpression token that represents a function that can be evaluated using a calle.              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Expressions.Execution;

namespace Empiria.Expressions {

  /// <summary>Expression token that represents a function that can be evaluated using a calle.</summary>
  public class Function : Token {

    public Function(string lexeme, int arity,
                    Func<FunctionHandler> calle) : base(TokenType.Function, lexeme) {

      Assertion.Require(arity >= 0, "Function arity must be non negative");
      Assertion.Require(calle, nameof(calle));

      Arity = arity;
      Calle = calle;
    }


    public int Arity {
      get;
    }


    public Func<FunctionHandler> Calle {
      get;
    }

  }  // class Function

}  // namespace Empiria.Expressions
