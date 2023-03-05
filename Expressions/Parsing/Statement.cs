/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Information holder                      *
*  Type     : Statement                                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents an executable statement. Statements can be compound or simple.                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using System.Collections.Generic;

using Empiria.Expressions.Execution;

namespace Empiria.Expressions {


  internal interface IStatement {

    void Execute(IDictionary<string, object> data);

  }


  /// <summary>Represents an executable statement. Statements can be compound or simple.</summary>
  internal class Statement : IStatement {

    private readonly IStatement _executable;

    private readonly LexicalGrammar _grammar;
    private readonly string _statement;

    public Statement(string statement) : this(LexicalGrammar.Default, statement) {
      // no-op
    }


    public Statement(LexicalGrammar grammar, string statement) {
      Assertion.Require(grammar, nameof(grammar));
      Assertion.Require(statement, nameof(statement));

      _grammar = grammar;
      _statement = statement;
      _executable = Compile();
    }


    public void Execute(IDictionary<string, object> data) {
      _executable.Execute(data);
    }


    public IStatement Compile() {
      var tokenizer = new Tokenizer(_grammar);

      FixedList<IToken> tokens = tokenizer.Tokenize(_statement);

      return GetStatementHandler(tokens);
    }


    public override string ToString() {
      return _statement;
    }


    #region Helpers

    private IStatement GetStatementHandler(FixedList<IToken> tokens) {
      if (tokens.Count >= 3 && tokens[1].Lexeme == ":=") {
        return GetAssignmentHandler(tokens);
      }

      throw Assertion.EnsureNoReachThisCode($"Statement handler was not implemented: '{_statement}'");
    }


    private IStatement GetAssignmentHandler(FixedList<IToken> tokens) {
      var leftToken = tokens[0];

      var rightExpressionTokens = tokens.Sublist(2);

      var parser = new SyntaxTreeParser(_grammar, rightExpressionTokens);

      FixedList<IToken> postfixTokens = parser.PostfixList();

      var rightExpression = new ExpressionEvaluator(_grammar, postfixTokens);

      return new AssignmentHandler(leftToken, rightExpression);
    }

    #endregion Helpers

  }  // class Statement

}  // namespace Empiria.Expressions
