/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Service provider                        *
*  Type     : SyntaxTree                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : An abstract syntax tree data structure (AST).                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Expressions {

  /// <summary>An abstract syntax tree data structure (AST).</summary>
  internal class SyntaxTree {

    private readonly SyntaxTreeNode _root;

    internal SyntaxTree() {
      IToken rootToken = new Token(TokenType.Function, "START");

      _root = new SyntaxTreeNode(rootToken);
    }

    public SyntaxTreeNode Root {
      get {
        return _root;
      }
    }

  }  // class SyntaxTree

}  // namespace Empiria.Expressions

