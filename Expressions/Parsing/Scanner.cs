/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parsing                     *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Service provider                        *
*  Type     : Scanner                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Scans an expression generating a stream of lexemes to be used by the tokenizer.                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Expressions {

  /// <summary>Scans an expression generating a stream of lexemes to be used by the tokenizer.</summary>
  internal class Scanner {

    private readonly LexicalGrammar _lexicalGrammar;

    #region Public

    internal Scanner() : this(LexicalGrammar.Default) {
      // no-op
    }


    internal Scanner(LexicalGrammar lexicalGrammar) {
      Assertion.Require(lexicalGrammar, nameof(lexicalGrammar));

      _lexicalGrammar = lexicalGrammar;
    }


    internal FixedList<string> Scan(string expression) {
      Assertion.Require(expression, nameof(expression));

      var lineReconstructor = new LineReconstructor(_lexicalGrammar);

      string[] lexemeCandidates = lineReconstructor.LexemeCandidates(expression);

      Assertion.Require(lexemeCandidates.Length >= 2,
                        $"Unrecognized expression: {expression}");

      var lexemes = new List<string>(lexemeCandidates.Length);

      foreach (var candidate in lexemeCandidates) {
        var lexeme = TryToScanLexemeCandidate(candidate);

        Assertion.Require(lexeme,
                          $"Expression has an unrecognized part: '{candidate}'");

        lexemes.Add(lexeme);
      }

      return lexemes.ToFixedList();
    }

    #endregion Public

    #region Helpers

    private string TryToScanLexemeCandidate(string candidate) {
      candidate = EmpiriaString.TrimAll(candidate);

      Assertion.Require(candidate, nameof(candidate));

      if (_lexicalGrammar.IsKeyword(candidate)) {
        return candidate;
      }

      if (_lexicalGrammar.IsOperator(candidate)) {
        return candidate;
      }

      if (_lexicalGrammar.IsLiteral(candidate)) {
        return candidate;
      }

      if (_lexicalGrammar.IsFunction(candidate)) {
        return candidate;
      }

      if (_lexicalGrammar.IsVariable(candidate)) {
        return candidate;
      }

      return null;
    }

    #endregion Helpers

  }  // internal class Scanner

}  // namespace Empiria.Expressions
