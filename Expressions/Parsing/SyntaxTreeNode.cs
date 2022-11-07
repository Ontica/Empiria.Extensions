/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Information holder                      *
*  Type     : SyntaxTreeNode                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : A node with a lexical token in a SyntaxTree.                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Expressions {

  /// <summary>A node with a lexical token in a SyntaxTree.</summary>
  internal class SyntaxTreeNode {

    private readonly List<SyntaxTreeNode> _children = new List<SyntaxTreeNode>();


    internal SyntaxTreeNode(IToken token) {
      Assertion.Require(token, nameof(token));

      Token = token;
    }


    #region Properties

    public IToken Token {
      get;
    }


    internal FixedList<SyntaxTreeNode> Children {
      get {
        return _children.ToFixedList();
      }
    }

    #endregion Properties

    #region Methods

    internal void AddChild(SyntaxTreeNode child) {
      Assertion.Require(child, nameof(child));

      _children.Add(child);
    }

    #endregion Methods

  }  // class SyntaxTreeNode

}  // namespace Empiria.Expressions
