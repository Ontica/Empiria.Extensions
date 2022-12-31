/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parsing                     *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Information holder                      *
*  Type     : Token                                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds data about a token that classifies a lexeme.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Expressions {

  /// <summary>Classifies a lexeme.</summary>
  public enum TokenType {

    Keyword,

    Operator,

    Literal,

    Function,

    Variable,

  }  // enum TokenType



  public interface IToken {

    TokenType Type { get; }

    string Lexeme { get; }

  }


  /// <summary>Holds data about a token that classifies a lexeme.</summary>
  public class Token : IToken {

    public Token(TokenType type, string lexeme) {
      Assertion.Require(type, nameof(type));
      Assertion.Require(lexeme, nameof(lexeme));

      Type = type;
      Lexeme = lexeme;
    }


    public TokenType Type {
      get;
    }


    public string Lexeme {
      get;
    }

  } // class Token

}  // namespace Empiria.Expressions
