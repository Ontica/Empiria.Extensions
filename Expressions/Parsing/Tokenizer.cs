/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parsing                     *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Service provider                        *
*  Type     : Tokenizer                                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Converts a plain text expression into a list of lexical tokens (aka. scanner or lexer).        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Expressions {

  /// <summary>Converts a plain text expression into a list of
  /// lexical tokens (aka. scanner or lexer).</summary>
  internal class Tokenizer {

    private readonly LexicalGrammar _lexicalGrammar;

    #region Public

    internal Tokenizer() : this(LexicalGrammar.Default) {
      // no-op
    }

    internal Tokenizer(LexicalGrammar lexicalGrammar) {
      Assertion.Require(lexicalGrammar, nameof(lexicalGrammar));

      _lexicalGrammar = lexicalGrammar;
    }


    internal FixedList<IToken> Tokenize(string expression) {
      Assertion.Require(expression, nameof(expression));

      var scanner = new Scanner(_lexicalGrammar);

      FixedList<string> lexemes = scanner.Scan(expression);

      FixedList<IToken> tokens = Evaluate(lexemes);

      return tokens;
    }

    #endregion Public

    #region Helpers

    private FixedList<IToken> Evaluate(FixedList<string> lexemes) {
      var tokens = new List<IToken>(lexemes.Count);

      foreach (var lexeme in lexemes) {
        IToken token = TryToTokenize(lexeme);

        Assertion.Require(token, $"Unrecognized token was found: {lexeme}.");

        tokens.Add(token);
      }

      return tokens.ToFixedList();
    }


    private IToken TryToTokenize(string lexeme) {
      lexeme = EmpiriaString.TrimAll(lexeme);

      Assertion.Require(lexeme, nameof(lexeme));

      if (_lexicalGrammar.IsConstantKeyword(lexeme)) {
        return new Token(TokenType.Literal, lexeme);
      }

      if (_lexicalGrammar.IsKeyword(lexeme)) {
        return new Token(TokenType.Keyword, lexeme);
      }

      if (_lexicalGrammar.IsOperator(lexeme)) {
        return new Token(TokenType.Operator, lexeme);
      }

      if (_lexicalGrammar.IsLiteral(lexeme)) {
        return new Token(TokenType.Literal, lexeme);
      }

      if (_lexicalGrammar.IsFunction(lexeme)) {
        return new Token(TokenType.Function, lexeme);
      }

      if (_lexicalGrammar.IsVariable(lexeme)) {
        return new Token(TokenType.Variable, lexeme);
      }

      return null;
    }

    #endregion Helpers

  }  // class Tokenizer

}  // namespace Empiria.Expressions
