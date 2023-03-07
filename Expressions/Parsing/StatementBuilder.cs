/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Service provider                        *
*  Type     : StatementBuilder                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Converts a text script in a list of statements.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Empiria.Expressions {

  /// <summary>Converts a text script in a list of statements.</summary>
  internal class StatementBuilder {

    private readonly LexicalGrammar _grammar;
    private readonly string _script;

    internal StatementBuilder(string script) : this(LexicalGrammar.Default, script) {
      // no-op
    }


    internal StatementBuilder(LexicalGrammar grammar, string script) {
      Assertion.Require(grammar, nameof(grammar));
      Assertion.Require(script, nameof(script));

      _grammar = grammar;
      _script = script;
    }


    internal FixedList<Statement> Build() {
      FixedList<string> textStatements = SeparateStatements();

      var statements = new List<Statement>(textStatements.Count);

      foreach (var textStatement in textStatements) {

        var statement = new Statement(_grammar, textStatement);

        statements.Add(statement);
      }

      return statements.ToFixedList();
    }


    internal FixedList<string> SeparateStatements() {
      var statements = _script.Split(';');

      return statements.Select(statement => EmpiriaString.Clean(statement))
                       .ToFixedList()
                       .FindAll(x => x.Length != 0)
                       .Select(x => x + ';')
                       .ToFixedList();
    }

  }  // class StatementBuilder

}  // namespace Empiria.Expressions
