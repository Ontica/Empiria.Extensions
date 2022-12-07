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

namespace Empiria.Expressions {

  internal interface IExecutable {

    object Execute();

    T Execute<T>();

    object Execute(IDictionary<string, object> data);

    T Execute<T>(IDictionary<string, object> data);

  }


  /// <summary>Converts a stream of tokens into an evaluatable Expression object.</summary>
  internal class ExpressionEvaluator : IExecutable {

    // private readonly SyntaxTree _syntaxTree;
    private readonly SymbolTable _symbolTable;

    private FixedList<IToken> _postfixTokens;

    public ExpressionEvaluator(FixedList<IToken> postfixTokens, SymbolTable symbolTable) {
      _postfixTokens = postfixTokens;
      _symbolTable = symbolTable;
    }

    //internal ExpressionEvaluator(SyntaxTree syntaxTree, SymbolTable symbolTable) {
    //  Assertion.Require(syntaxTree, nameof(syntaxTree));
    //  Assertion.Require(symbolTable, nameof(symbolTable));

    //  _syntaxTree = syntaxTree;
    //  _symbolTable = symbolTable;
    //}


    public object Execute() {
      return Evaluate(null);
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

      var _grammar = LexicalGrammar.Default;

      decimal returnValue = 0;

      foreach (var token in _postfixTokens) {
        if (_grammar.IsOperand(token)) {
          operandsStack.Push(token);

          continue;
        }

        if (!_grammar.IsOperator(token)) {
          continue;
        }

        var parameters = new List<IToken>();


        if (token.Type == TokenType.Function && operandsStack.Count == 0) {

          parameters.Add(new Token(TokenType.Literal, returnValue.ToString()));


        } else if (token.Type == TokenType.Function && operandsStack.Count > 0) {

          for (int i = 0; i < _grammar.ArityOf(token); i++) {
            parameters.Insert(0, operandsStack.Pop());
          }

        } else if (token.Type == TokenType.Operator && operandsStack.Count >= 2) {

          parameters.Insert(0, operandsStack.Pop());
          parameters.Insert(0, operandsStack.Pop());

        } else if (token.Type == TokenType.Operator && operandsStack.Count == 1) {

          parameters.Insert(0, operandsStack.Pop());

        }

        returnValue = Calculator.Calculate(token,
                                           parameters.ToFixedList(),
                                           data);

        operandsStack.Push((new Token(TokenType.Literal, returnValue.ToString())));


      } // foreach

      return returnValue;

    }

  }  // class ExpressionEvaluator

}  // namespace Empiria.Expressions
