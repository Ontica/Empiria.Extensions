/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Service provider                        *
*  Type     : Literal                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : A token that repreents a numerical, logical, date, or string constant in an expression.        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Expressions {

  /// <summary>A token that repreents a numerical, logical, date, or string constant in an expression.</summary>
  internal class Literal : Token, IOperand {

    internal Literal(object value) : base(TokenType.Literal, value.ToString()) {

      this.Value = value;
    }


    public object Value {
      get;
    }

  }  // class Literal

}  // namespace Empiria.Expressions
