/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Service provider                        *
*  Type     : ExpressionEvaluator                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Evaluates an expression.                                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using Empiria.Expressions.Libraries;

namespace Empiria.Expressions {


  internal interface IExecutable {

    object Execute();

    T Execute<T>();

    object Execute(IDictionary<string, object> data);

    T Execute<T>(IDictionary<string, object> data);

  }


  /// <summary>Converts a stream of tokens into an evaluatable Expression object.</summary>
  internal class ExpressionEvaluator : IExecutable {

    private readonly LexicalGrammar _grammar;
    private readonly FixedList<IToken> _postfixTokens;

    public ExpressionEvaluator(LexicalGrammar grammar,
                               FixedList<IToken> postfixTokens) {
      Assertion.Require(grammar, nameof(grammar));
      Assertion.Require(postfixTokens, nameof(postfixTokens));

      _grammar = grammar;
      _postfixTokens = postfixTokens;
    }


    public object Execute() {
      return Evaluate(new Dictionary<string, object>());
    }


    public T Execute<T>() {
      return (T) this.Execute();
    }


    public object Execute(IDictionary<string, object> data) {
      return this.Evaluate(data);
    }


    public T Execute<T>(IDictionary<string, object> data) {
      return (T) Execute(data);
    }


    public decimal Evaluate(IDictionary<string, object> data) {

      var operandsStack = new Stack<IToken>();

      decimal returnValue = 0;

      foreach (var token in _postfixTokens) {
        if (_grammar.IsOperand(token)) {
          operandsStack.Push(token);

          continue;
        }

        if (!_grammar.IsOperator(token)) {
          continue;
        }

        if (token.Type == TokenType.Operator) {

          returnValue = EvaluateOperator(token, operandsStack, data);

        } else if (token.Type == TokenType.Function) {

          returnValue = EvaluateFunction(token, operandsStack, data, returnValue);

        }

        operandsStack.Push((new Token(TokenType.Literal, returnValue.ToString())));

       } // foreach

      return returnValue;

    }

    #region Helpers

    private decimal EvaluateFunction(IToken token,
                                     Stack<IToken> operandsStack,
                                     IDictionary<string, object> data,
                                     decimal returnValue) {
      Assertion.Require(token.Type == TokenType.Function, "token.Type is not a function.");

      var parameters = new List<IToken>();

      if (operandsStack.Count == 0) {

        parameters.Add(new Token(TokenType.Literal, returnValue.ToString()));


      } else if (operandsStack.Count > 0) {

        for (int i = 0; i < _grammar.ArityOf(token); i++) {
          parameters.Insert(0, operandsStack.Pop());
        }

      }

      LibrariesRegistry libraries = _grammar.Libraries();

      FunctionHandler functionHandler = libraries.GetFunctionHandler(token, parameters, data);

      return functionHandler.Evaluate();
    }


    private decimal EvaluateOperator(IToken token,
                                     Stack<IToken> operandsStack,
                                     IDictionary<string, object> data) {
      Assertion.Require(token.Type == TokenType.Operator, "token.Type is not an operator.");

      if (operandsStack.Count >= 2) {

        IToken parameter2 = operandsStack.Pop();
        IToken parameter1 = operandsStack.Pop();

        var handler = new OperatorHandler(token, data);

        return handler.Evaluate(parameter1, parameter2);      // Binary operators

      } else if (operandsStack.Count == 1) {

        IToken parameter1 = operandsStack.Pop();

        var op = new OperatorHandler(token, data);

        return op.Evaluate(parameter1);                       // Unary operator

      } else {
        throw Assertion.EnsureNoReachThisCode("Operands stack must be not empty.");
      }
    }

    #endregion Helpers

  }  // class ExpressionEvaluator

}  // namespace Empiria.Expressions
