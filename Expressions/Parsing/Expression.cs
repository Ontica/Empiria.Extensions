/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Information Holder                      *
*  Type     : Expression                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides a service to evaluate an expression.                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Dynamic;

namespace Empiria.Expressions {

  /// <summary>Provides a service to evaluate an expression.</summary>
  public class Expression {

    internal Expression(FunctionObject function) {
      Assertion.Require(function, nameof(function));

      Function = function;
    }

    internal FunctionObject Function {
      get;
    }


    public T Evaluate<T>(DynamicObject data) {
      return (T) (object) 1;
    }

  }  // class Expression

}  // namespace Empiria.Expressions
