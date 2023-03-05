/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Information holder                      *
*  Type     : Statement                                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents an executable statement. Statements can be compound or simple.                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Expressions {

  /// <summary>Represents an executable statement. Statements can be compound or simple.</summary>
  internal class Statement {

    private readonly string _textStatement;

    public Statement(string textStatement) {
      Assertion.Require(textStatement, nameof(textStatement));

      _textStatement = textStatement;
    }


    public override string ToString() {
      return _textStatement;
    }

  }  // class Statement

}  // namespace Empiria.Expressions
