/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Service provider                        *
*  Type     : Variable                                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a variable in an expression.                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Expressions {

  /// <summary>Represents a variable in an expression.</summary>
  internal class Variable : Token, IOperand {

    public Variable(string lexeme) : base(TokenType.Variable, lexeme) {
      // no-op
    }

  }  // class Variable

}  // namespace Empiria.Expressions
