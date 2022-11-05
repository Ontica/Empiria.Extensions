/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Service provider                        *
*  Type     : Operator                                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents an operator in an expression.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Expressions {

  /// <summary>Represents an operator in an expression.</summary>
  internal class Operator : IExpressionToken {

    private const string arithmeticalOperators = @" + - * / ÷ \ ^ Ω ";
    private const string logicalOperators = @" ¬ ∧ ∨ ≡ ≢ ⇒ ";   // ∀∃
    private const string relationalOperators = @" > < ≥ ≤ = ≠ ∈ ";    //∩∪⊂ ⊄ ⊆ ⊊ ∈ ∉ ∅
    private const string groupingOperators = @" ( [ { ) ] } , ; ";

    static internal Operator TryToTokenize(string lexeme) {
      return null;
    }

  }  // class Operator

}  // namespace Empiria.Expressions
