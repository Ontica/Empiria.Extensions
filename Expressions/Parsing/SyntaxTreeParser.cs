/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Service provider                        *
*  Type     : SyntaxTreeParser                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Converts a stream of tokens into a syntax tree.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Expressions {

  /// <summary>Converts a stream of tokens into a syntax tree.</summary>
  internal class SyntaxTreeParser {

    private readonly LexicalGrammar _grammar;
    private readonly FixedList<IToken> _tokens;

    internal SyntaxTreeParser(FixedList<IToken> tokens) : this(LexicalGrammar.Default, tokens) {
      // no-op
    }


    internal SyntaxTreeParser(LexicalGrammar grammar, FixedList<IToken> tokens) {
      Assertion.Require(grammar, nameof(grammar));
      Assertion.Require(tokens, nameof(tokens));

      _grammar = grammar;
      _tokens = tokens;
    }


    internal SyntaxTree SyntaxTree() {

      var tree = new SyntaxTree();

      // SyntaxTreeNode root = tree.Root;

      return tree;
    }


    internal FixedList<IToken> PostfixList() {
      var operatorStack = new Stack<IToken>();

      var postfixList = new List<IToken>(_tokens.Count);

      foreach (IToken token in _tokens) {
        if (_grammar.IsOperand(token)) {
          postfixList.Add(token);
          continue;
        }

        if (_grammar.IsLeftParenthesis(token)) {
          operatorStack.Push(token);
          continue;
        }

        if (token.Type == TokenType.Function) {
          operatorStack.Push(token);
          continue;
        }

        if (_grammar.IsRightParenthesis(token)) {

          // Process all operators until founds the last pushed left parenthesis
          while (true) {
            IToken lastOperator = operatorStack.Pop();

            if (!_grammar.IsLeftParenthesis(lastOperator)) {
              postfixList.Add(lastOperator);
            } else {
              break;
            }
          }

          continue;
        }

        // Process all stack operators with highest precedence

        while (operatorStack.Count != 0) {
          if (_grammar.Precedence(token) <= _grammar.Precedence(operatorStack.Peek())) {
            postfixList.Add(operatorStack.Pop());
          } else {
            break;
          }
        } // while

        // Discard lists separators from the postfix array
        if (!_grammar.IsListItemDelimiter(token)) {
          operatorStack.Push(token);
        }

      }  // for

      while (operatorStack.Count != 0) {
        postfixList.Add(operatorStack.Pop());
      }

      return postfixList.ToFixedList();
    }


    #region Helpers

    private SyntaxTreeNode CreateNode(IToken token) {
      return new SyntaxTreeNode(token);
    }

    #endregion Helpers

  }  // class SyntaxTreeParser

}  // namespace Empiria.Expressions
