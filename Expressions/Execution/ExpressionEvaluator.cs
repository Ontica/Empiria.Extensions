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
using System.Dynamic;

namespace Empiria.Expressions {

  internal interface IExecutable {

    object Execute(DynamicObject data);

    T Execute<T>(DynamicObject data);

  }


  /// <summary>Converts a stream of tokens into an evaluatable Expression object.</summary>
  internal class ExpressionEvaluator : IExecutable {

    private readonly SyntaxTree _syntaxTree;
    private readonly SymbolTable _symbolTable;

    internal ExpressionEvaluator(SyntaxTree syntaxTree, SymbolTable symbolTable) {
      Assertion.Require(syntaxTree, nameof(syntaxTree));
      Assertion.Require(symbolTable, nameof(symbolTable));

      _syntaxTree = syntaxTree;
      _symbolTable = symbolTable;
    }


    public object Execute(DynamicObject data) {
      return 1m;
    }

    public T Execute<T>(DynamicObject data) {
      return (T) Execute(data);
    }

  }  // class ExpressionEvaluator

}  // namespace Empiria.Expressions
