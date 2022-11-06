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


    internal FixedList<IExpressionToken> Tokenize(string expression) {
      Assertion.Require(expression, nameof(expression));

      var scanner = new Scanner(_lexicalGrammar);

      FixedList<string> lexemes = scanner.Scan(expression);

      FixedList<IExpressionToken> tokens = Evaluate(lexemes);

      return tokens;
    }

    #endregion Public

    #region Helpers

    private FixedList<IExpressionToken> Evaluate(FixedList<string> lexemes) {
      var tokens = new List<IExpressionToken>(lexemes.Count);

      foreach (var lexeme in lexemes) {
        IExpressionToken token = TryToTokenize(lexeme);

        Assertion.Require(token, $"Unrecognized token was found: {lexeme}.");

        tokens.Add(token);
      }

      return tokens.ToFixedList();
    }


    private IExpressionToken TryToTokenize(string lexeme) {
      lexeme = EmpiriaString.TrimAll(lexeme);

      Assertion.Require(lexeme, nameof(lexeme));

      IExpressionToken token = Keyword.TryToTokenize(lexeme);

      if (token != null) {
        return token;
      }

      token = Operator.TryToTokenize(lexeme);

      if (token != null) {
        return token;
      }

      token = Literal.TryToTokenize(lexeme);

      if (token != null) {
        return token;
      }

      token = Function.TryToTokenize(lexeme);

      if (token != null) {
        return token;
      }

      return Variable.TryToTokenize(lexeme);

    }

    #endregion Helpers

  }  // class Tokenizer

}  // namespace Empiria.Expressions
