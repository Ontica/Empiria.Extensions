/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Libraries                               *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Abstract class                          *
*  Type     : BaseFunctionsLibrary                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Abstract class used to implement Empiria Expressions functions libraries.                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Expressions {

  /// <summary>Abstract class used to implement Empiria Expressions functions libraries.</summary>
  public abstract class BaseFunctionsLibrary {

    private readonly List<Function> _functions = new List<Function>();


    public IList<Function> Functions {
      get {
        return _functions;
      }
    }

    protected void Add(Function function) {
      Assertion.Require(function, nameof(function));

      _functions.Add(function);
    }


    protected void AddRange(IEnumerable<Function> functions) {
      Assertion.Require(functions, nameof(functions));

      _functions.AddRange(functions);
    }


    internal Function GetFunction(IToken token) {
      var function = _functions.Find(x => x.Lexeme == token.Lexeme);

      Assertion.Require(function, "function");

      return function;
    }


    internal bool HasFunction(IToken token) {
      return _functions.Exists(x => x.Lexeme == token.Lexeme);
    }


  }  // abstract class FunctionsLibrary

}  // namespace Empiria.Expressions
