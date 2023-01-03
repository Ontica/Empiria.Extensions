/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Information Holder                      *
*  Type     : Expression                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides a service to evaluate an expression.                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using Empiria.Expressions.Execution;

namespace Empiria.Expressions {

  /// <summary>Provides a service to evaluate an expression.</summary>
  public class Expression {

    private readonly IExecutable _executable;

    public Expression(string expression) : this(LexicalGrammar.Default, expression) {
      // no-op
    }


    public Expression(LexicalGrammar grammar, string expression) {
      Assertion.Require(grammar, nameof(grammar));
      Assertion.Require(expression, nameof(expression));

      _executable = Compile(grammar, expression);
    }


    public T Evaluate<T>() {
      return _executable.Execute<T>();
    }

    public T Evaluate<T>(IDictionary<string, object> data) {
      return _executable.Execute<T>(data);
    }


    private IExecutable Compile(LexicalGrammar grammar, string expression) {
      var tokenizer = new Tokenizer(grammar);

      FixedList<IToken> tokens = tokenizer.Tokenize(expression);

      var parser = new SyntaxTreeParser(grammar, tokens);

      FixedList<IToken> postfixTokens = parser.PostfixList();

      return new ExpressionEvaluator(grammar, postfixTokens);
    }


  }  // class Expression

}  // namespace Empiria.Expressions
