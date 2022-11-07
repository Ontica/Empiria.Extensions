/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parsing                     *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Information holder                      *
*  Type     : ExpressionToken                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds data about a token that classifies a lexeme.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Expressions {

  /// <summary>Classifies a lexeme.</summary>
  internal enum ExpressionTokenType {

    Keyword,

    Operator,

    Literal,

    Function,

    Variable,

  }

  /// <summary>Holds data about a token that classifies a lexeme.</summary>
  internal class ExpressionToken : IExpressionToken {

    public ExpressionToken(ExpressionTokenType type, string lexeme) {
      Assertion.Require(type, nameof(type));
      Assertion.Require(lexeme, nameof(lexeme));

      Type = type;
      Lexeme = lexeme;
    }


    public ExpressionTokenType Type {
      get;
    }


    public string Lexeme {
      get;
    }

  } // class ExpressionToken

}  // namespace Empiria.Expressions
