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

    private readonly string[] _reconstructableSymbols;
    private readonly string[] _stroppableSymbols;

    const char stroppingChar = '§';

    #region Public

    internal LineReconstructor(string[] reconstructableSymbols, string[] stroppableSymbols) {
      Assertion.Require(reconstructableSymbols, nameof(reconstructableSymbols));
      Assertion.Require(stroppableSymbols, nameof(stroppableSymbols));

      _reconstructableSymbols = reconstructableSymbols;
      _stroppableSymbols = stroppableSymbols;
    }


    internal string[] LexemeCandidates(string expression) {
      var reconstructedExpression = EmpiriaString.TrimControl(expression);

      reconstructedExpression = Stropping(reconstructedExpression);

      reconstructedExpression = Separate(reconstructedExpression);

      reconstructedExpression = Unstropping(reconstructedExpression);

      string[] lexemeCandidates = CommonMethods.ConvertToArray(reconstructedExpression);

      return lexemeCandidates;
    }

    #endregion Public

    #region Line reconstruction helpers

    private string Separate(string expression) {
      var candidates = CommonMethods.ConvertToArray(expression);

      string temp = string.Empty;

      foreach (var candidate in candidates) {
        if (IsStropped(candidate)) {
          temp += $" {candidate} ";
        } else {
          temp += $" {SeparateOperatorsAndFunctions(candidate)} ";
        }
      }

      return EmpiriaString.TrimAll(temp);
    }

    private string SeparateOperatorsAndFunctions(string expression) {
      foreach (var op in _reconstructableSymbols) {
        expression = expression.Replace(op, $" {op} ");
      }

      return EmpiriaString.TrimAll(expression);
    }


    private bool IsStropped(string part) {
      return part.StartsWith($"{stroppingChar}") && part.EndsWith($"{stroppingChar}");
    }


    private string Stropping(string expression) {
      string temp = expression;

      foreach (var symbol in _stroppableSymbols) {
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
