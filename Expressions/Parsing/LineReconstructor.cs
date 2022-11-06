/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parsing                     *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Service provider                        *
*  Type     : LineReconstructor                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Reconstructs an expression into a candidate lexemes stream to be used by the scanner.          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Expressions {

  /// <summary>Reconstructs an expression into a candidate lexemes stream to be used by the scanner.</summary>
  internal class LineReconstructor {

    private readonly LexicalGrammar _lexicalGrammar;

    const char stroppingChar = '§';

    #region Public

    public LineReconstructor(LexicalGrammar lexicalGrammar) {
      Assertion.Require(lexicalGrammar, nameof(lexicalGrammar));

      _lexicalGrammar = lexicalGrammar;
    }

    internal string[] LexemeCandidates(string expression) {
      var reconstructedExpression = EmpiriaString.TrimControl(expression);

      reconstructedExpression = Stropping(reconstructedExpression);

      reconstructedExpression = SeparateWords(reconstructedExpression);

      reconstructedExpression = Unstropping(reconstructedExpression);

      string[] lexemeCandidates = CommonMethods.ConvertToArray(reconstructedExpression);

      return lexemeCandidates;
    }

    #endregion Public

    #region Line reconstruction helpers

    private bool IsStropped(string part) {
      return part.StartsWith($"{stroppingChar}") && part.EndsWith($"{stroppingChar}");
    }


    private string ReconstructSymbols(string word) {
      var symbols = _lexicalGrammar.GetReconstructableSymbols();

      foreach (var symbol in symbols) {
        word = word.Replace(symbol, $" {symbol} ");
      }

      return EmpiriaString.TrimAll(word);
    }


    private string SeparateWords(string expression) {
      var words = CommonMethods.ConvertToArray(expression);

      string temp = string.Empty;

      foreach (var word in words) {
        if (IsStropped(word)) {
          temp += $" {word} ";
        } else {
          temp += $" {ReconstructSymbols(word)} ";
        }
      }

      return EmpiriaString.TrimAll(temp);
    }


    private string Stropping(string expression) {
      string temp = expression;

      var symbols = _lexicalGrammar.GetStroppableSymbols();

      foreach (var symbol in symbols) {
        var stropped = $" {stroppingChar}{symbol}{stroppingChar} ";

        temp = temp.Replace(symbol, stropped);
      }

      return temp;
    }


    private string Unstropping(string expression) {
      string temp = expression.Replace(stroppingChar, ' ');

      return EmpiriaString.TrimAll(temp);
    }

    #endregion Line reconstruction helpers

  }  // class LineReconstructor

}  // namespace Empiria.Expressions
