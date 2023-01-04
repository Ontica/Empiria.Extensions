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

namespace Empiria.Expressions.Execution {


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


    public object Evaluate(IDictionary<string, object> data) {

      var operandsStack = new Stack<IToken>();

      object returnValue = 0m;

      foreach (var token in _postfixTokens) {

        if (_grammar.IsOperand(token)) {
          operandsStack.Push(token);

          continue;
        }

        if (!_grammar.IsOperatorOrFunction(token)) {
          continue;
        }

        if (token.Type == TokenType.Operator) {

          returnValue = EvaluateOperator(token, operandsStack, data);

        } else if (token.Type == TokenType.Function) {

          returnValue = EvaluateFunction(token, operandsStack, data);

        }

        operandsStack.Push((new Token(TokenType.Literal, returnValue.ToString())));

      } // foreach

      return returnValue;

    }

    #region Helpers

    private object EvaluateFunction(IToken token,
                                    Stack<IToken> operandsStack,
                                    IDictionary<string, object> data) {
      Assertion.Require(token.Type == TokenType.Function, "token.Type is not a function.");

      var parameters = new List<IToken>();

      int functionArity = _grammar.ArityOf(token);

      Assertion.Require(operandsStack.Count >= functionArity,
                        $"Function '{token.Lexeme}' needs {functionArity} parameters, " +
                        $"but operands stack has fewer: {operandsStack.Count}.");

      for (int i = 0; i < functionArity; i++) {
        parameters.Insert(0, operandsStack.Pop());
      }

      LibrariesRegistry libraries = _grammar.Libraries();

      FunctionHandler functionHandler = libraries.GetFunctionHandler(token, parameters, data);

      return functionHandler.Evaluate();
    }


    private object EvaluateOperator(IToken token,
                                    Stack<IToken> operandsStack,
                                    IDictionary<string, object> data) {
      Assertion.Require(token.Type == TokenType.Operator, "token.Type is not an operator.");

      OperatorHandler opHandler = GetOperatorHandler(token, data);

      if (operandsStack.Count >= 2) {

        IToken parameter2 = operandsStack.Pop();
        IToken parameter1 = operandsStack.Pop();

        return opHandler.Evaluate(parameter1, parameter2);      // Binary operators

      } else if (operandsStack.Count == 1) {

        IToken parameter1 = operandsStack.Pop();

        return opHandler.Evaluate(parameter1);                  // Unary operator

      } else {
        throw Assertion.EnsureNoReachThisCode("Operands stack must be not empty.");
      }
    }


    private OperatorHandler GetOperatorHandler(IToken token,
                                               IDictionary<string, object> data) {
      if (_grammar.IsArithmeticalOperator(token)) {
        return new ArithmeticalOperatorHandler(token, data);
      }

      if (_grammar.IsRelationalOperator(token)) {
        return new RelationalOperatorHandler(token, data);
      }

      if (_grammar.IsLogicalOperator(token)) {
        return new LogicalOperatorHandler(token, data);
      }


      throw Assertion.EnsureNoReachThisCode($"Unhandled operator type '{token.Type}'.");
    }

    #endregion Helpers

  }  // class ExpressionEvaluator

}  // namespace Empiria.Expressions.Execution
