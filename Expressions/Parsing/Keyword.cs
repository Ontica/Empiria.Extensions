/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Service provider                        *
*  Type     : Keyword                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : A domain specific language keyword in an expression.                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Expressions {

  /// <summary>A domain specific language keyword in an expression.</summary>
  internal class Keyword : IExpressionToken {

    static internal Keyword TryToTokenize(string lexeme) {
      return null;
    }

  }  // class Keyword

}  // namespace Empiria.Expressions
