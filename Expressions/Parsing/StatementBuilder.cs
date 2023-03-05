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

    private readonly string _script;

    internal StatementBuilder(string script) {
      Assertion.Require(script, nameof(script));

      _script = script;
    }


    internal FixedList<Statement> Build() {
      FixedList<string> textStatements = SeparateStatements(_script);

      var statements = new List<Statement>(textStatements.Count);

      foreach (var textStatement in textStatements) {

        var statement = new Statement(textStatement);

        statements.Add(statement);
      }

      return statements.ToFixedList();
    }

    #region Helpers

    private FixedList<string> SeparateStatements(string script) {
      var statements = script.Split(';');

      return statements.Select(statement => EmpiriaString.Clean(statement))
                       .ToFixedList()
                       .FindAll(x => x.Length != 0)
                       .Select(x => x + ';')
                       .ToFixedList();
    }

    #endregion Helpers

  }  // class StatementBuilder

}  // namespace Empiria.Expressions
