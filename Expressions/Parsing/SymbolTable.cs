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
using System.Collections.Generic;

namespace Empiria.Expressions {

  /// <summary>Extracts the collection of symbols from a syntax tree.</summary>
  internal class SymbolTable {

    private readonly Dictionary<string, SymbolTableEntry> _symbolTable;

    public SymbolTable(SyntaxTree syntaxTree) {
      Assertion.Require(syntaxTree, nameof(syntaxTree));

    }

    public SymbolTable(FixedList<IToken> tokens) {

      _symbolTable = new Dictionary<string, SymbolTableEntry>(tokens.Count);

      BuildTable(tokens);
    }


    private void BuildTable(FixedList<IToken> tokens) {
      foreach (var token in tokens) {
        if (IsSymbol(token.Type)) {
          _symbolTable.Add(token.Lexeme, new SymbolTableEntry(token));
        }

      }
    }

    public decimal GetValue(IToken token) {
      return _symbolTable[token.Lexeme].Value;
    }

    public void SetValue(IToken token, decimal newValue) {
      _symbolTable[token.Lexeme].SetValue(newValue);
    }

    private bool IsSymbol(TokenType type) {
      return type == TokenType.Variable;
    }

  }  // class SymbolTable



  internal class SymbolTableEntry {

    public SymbolTableEntry(IToken token) {
      Token = token;

    }

    public IToken Token {
      get;
    }

    public decimal Value {
      get;
      private set;
    }


    internal void SetValue(decimal newValue) {
      this.Value = newValue;
    }

  }


}  // namespace Empiria.Expressions
