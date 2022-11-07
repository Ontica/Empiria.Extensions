/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Service provider                        *
*  Type     : SymbolTable                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Extracts the collection of symbols from a syntax tree.                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Expressions {

  /// <summary>Extracts the collection of symbols from a syntax tree.</summary>
  internal class SymbolTable {

    private readonly SyntaxTree _syntaxTree;

    public SymbolTable(SyntaxTree syntaxTree) {
      Assertion.Require(syntaxTree, nameof(syntaxTree));

      _syntaxTree = syntaxTree;
    }

  }  // class SymbolTable

}  // namespace Empiria.Expressions
