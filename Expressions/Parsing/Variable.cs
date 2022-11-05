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
  internal class Variable : IExpressionToken {

    static internal Variable TryToTokenize(string lexeme) {
      return null;
    }

  }  // class Variable

}  // namespace Empiria.Expressions
