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

namespace Empiria.Expressions {

  /// <summary>Converts a stream of tokens into a syntax tree.</summary>
  internal class SyntaxTreeParser {

    private readonly FixedList<IToken> _tokens;


    internal SyntaxTreeParser(FixedList<IToken> tokens) : this(LexicalGrammar.Default, tokens) {
      // no-op
    }


    internal SyntaxTreeParser(LexicalGrammar grammar, FixedList<IToken> tokens) {
      Assertion.Require(grammar, nameof(grammar));
      Assertion.Require(tokens, nameof(tokens));

      _tokens = tokens;
    }


    internal SyntaxTree SyntaxTree() {
      return new SyntaxTree();
    }


  }  // class SyntaxTreeParser

}  // namespace Empiria.Expressions
