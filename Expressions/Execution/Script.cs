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

using Empiria.Expressions.Execution;

namespace Empiria.Expressions {

  /// <summary>Provides a service for scripts execution.</summary>
  public class Script {

    private readonly IExecutable _executable;

    public Script(string script) : this(LexicalGrammar.Default, script) {
      // no-op
    }


    public Script(LexicalGrammar grammar, string script) {
      Assertion.Require(grammar, nameof(grammar));
      Assertion.Require(script, nameof(script));

      _executable = Compile(grammar, script);
    }


    public void Execute() {
      _executable.Execute();
    }


    public void Execute(IDictionary<string, object> data) {
      _executable.Execute(data);
    }


    private IExecutable Compile(LexicalGrammar grammar, string script) {
      var tokenizer = new Tokenizer(grammar);

      FixedList<IToken> tokens = tokenizer.Tokenize(script);

      var parser = new SyntaxTreeParser(grammar, tokens);

      FixedList<IToken> postfixTokens = parser.PostfixList();

      return new ExpressionEvaluator(grammar, postfixTokens);
    }

  }  // class Script

}  // namespace Empiria.Expressions
