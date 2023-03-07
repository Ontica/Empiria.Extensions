/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Service provider                        *
*  Type     : Preprocessor                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Performs text code preprocessing to prepare code before its compilation.                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Expressions {

  /// <summary>Performs text code preprocessing to prepare code before its compilation.</summary>
  internal class Preprocessor {

    private readonly LexicalGrammar _grammar;

    internal Preprocessor(LexicalGrammar grammar) {

      Assertion.Require(grammar, nameof(grammar));

      _grammar = grammar;
    }


    internal string Preprocess(string script) {
      if (String.IsNullOrWhiteSpace(script)) {
        return string.Empty;
      }

      var statements = new StatementBuilder(script).SeparateStatements();

      string preprocessedScript = string.Empty;

      foreach (var statement in statements) {
        preprocessedScript += PreprocessStatement(statement);
      }

      return preprocessedScript;
    }

    #region Helpers

    private string PreprocessStatement(string statement) {
      if (statement.StartsWith("@MACRO")) {
        return ReplaceMacro(statement);
      }
      return statement;
    }


    private string ReplaceMacro(string macroStatement) {
      string macroName = macroStatement.Replace("@MACRO", string.Empty)
                                       .Replace(";", string.Empty);

      macroName = EmpiriaString.TrimAll(macroName);

      IMacro macro = _grammar.Macros().GetMacro(macroName);

      return macro.Code;
    }

    #endregion Helpers

  }  // class Preprocessor

}  // namespace Empiria.Expressions
