/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Service provider                        *
*  Type     : Script                                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides a service for scripts execution.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Expressions {

  /// <summary>Provides a service for scripts execution.</summary>
  public class Script : IStatement {

    private readonly FixedList<IStatement> _executables;

    public Script(string script) : this(LexicalGrammar.Default, script) {
      // no-op
    }


    public Script(LexicalGrammar grammar, string script) {
      Assertion.Require(grammar, nameof(grammar));
      Assertion.Require(script, nameof(script));

      FixedList<Statement> statements = GetStatements(grammar, script);

      _executables = Compile(statements);
    }


    public void Execute(IDictionary<string, object> data) {
      foreach (var executable in _executables) {
        executable.Execute(data);
      }
    }


    #region Helpers

    private FixedList<IStatement> Compile(FixedList<Statement> statements) {
      var executables = new List<IStatement>(statements.Count);

      foreach (var statement in statements) {

        IStatement executable = statement.Compile();

        executables.Add(executable);
      }

      return executables.ToFixedList();
    }


    private FixedList<Statement> GetStatements(LexicalGrammar grammar, string script) {
      var builder = new StatementBuilder(grammar, script);

      return builder.Build();
    }

    #endregion Helpers

  }  // class Script

}  // namespace Empiria.Expressions
