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

      string[] reconstructableSymbols = GetReconstructableSymbols();
      string[] stroppableSymbols      = GetStroppableSymbols();

      var lineReconstructor = new LineReconstructor(reconstructableSymbols,
                                                    stroppableSymbols);

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

      if (IsKeyword(candidate)) {
        return candidate;
      }

      if (IsOperator(candidate)) {
        return candidate;
      }

      if (IsLiteral(candidate)) {
        return candidate;
      }

      if (IsFunction(candidate)) {
        return candidate;
      }

      if (IsVariable(candidate)) {
        return candidate;
      }

      return null;
    }


    private bool IsFunction(string candidate) {
      string[] functions = GetFunctions();

      foreach (var @function in functions) {
        if (candidate == function) {
          return true;
        }
      }

      return false;
    }


    private bool IsKeyword(string candidate) {
      string[] keywords = GetKeywords();

      foreach (var keyword in keywords) {
        if (candidate == keyword) {
          return true;
        }
      }

      return false;
    }


    private bool IsLiteral(string candidate) {
      if (EmpiriaString.IsQuantity(candidate)) {
        return true;
      }

      if (IsStringOrDateConstant(candidate)) {
        return true;
      }

      return false;
    }


    private bool IsStringOrDateConstant(string candidate) {
      string[] separators = CommonMethods.ConvertToArray(_lexicalGrammar.ConstantSeparators);

      foreach (var separator in separators) {
        if (candidate.StartsWith(separator) && candidate.EndsWith(separator)) {
          return true;
        }
      }

      return false;
    }


    private bool IsOperator(string candidate) {
      string[] allOperators = GetOperators();

      foreach (var @operator in allOperators) {
        if (candidate == @operator) {
          return true;
        }
      }

      return false;
    }


    private bool IsVariable(string candidate) {
      return char.IsLetter(candidate[0]);
    }


    private string[] GetFunctions() {
      string allFunctions = $"{_lexicalGrammar.FunctionIdentifiers}";

      return CommonMethods.ConvertToArray(allFunctions);
    }


    private string[] GetKeywords() {
      string allKeywords = $"{_lexicalGrammar.ReservedWords}";

      return CommonMethods.ConvertToArray(allKeywords);
    }


    private string[] GetOperators() {
      string allOperators = $"{_lexicalGrammar.ArithmeticalOperators} {_lexicalGrammar.LogicalOperators} " +
                            $"{_lexicalGrammar.RelationalOperators} {_lexicalGrammar.GroupingOperators}";

      return CommonMethods.ConvertToArray(allOperators);
    }


    private string[] GetReconstructableSymbols() {
      return GetOperators();
    }


    private string[] GetStroppableSymbols() {
      string allStroppableSymbols = $"{_lexicalGrammar.StroppableSymbols}";

      return CommonMethods.ConvertToArray(allStroppableSymbols);
    }


    #endregion Helpers

  }  // internal class Scanner

}  // namespace Empiria.Expressions
