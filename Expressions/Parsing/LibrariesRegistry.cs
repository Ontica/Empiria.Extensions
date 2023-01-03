/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parsing                     *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Information Holder                      *
*  Type     : LibrariesRegistry                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Mantains a registry of loaded function libraries to be parsed and invoked.                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using Empiria.Expressions.Execution;

namespace Empiria.Expressions {

  /// <summary>Mantains a registry of loaded function libraries that can be parsed and invoked.</summary>
  internal class LibrariesRegistry {

    private readonly List<BaseFunctionsLibrary> _libraries = new List<BaseFunctionsLibrary>();

    internal LibrariesRegistry() {
      // no-op
    }


    internal void Add(BaseFunctionsLibrary library) {
      Assertion.Require(library, nameof(library));

      _libraries.Add(library);
    }


    internal Function GetFunction(IToken token) {
      foreach (var library in _libraries) {
        if (library.HasFunction(token)) {
          return library.GetFunction(token);
        }
      }

      throw Assertion.EnsureNoReachThisCode($"Unregistered library function '{token.Lexeme}'.");
    }


    internal FunctionHandler GetFunctionHandler(IToken token,
                                                List<IToken> parameters,
                                                IDictionary<string, object> data) {
      Func<FunctionHandler> creator = GetFunction(token).Calle;

      FunctionHandler functionHandler = creator.Invoke();

      functionHandler.SetParameters(parameters);
      functionHandler.LoadData(data);

      return functionHandler;
    }


    internal bool HasRegisteredFunction(string candidate) {
      IList<Function> allFunctions = GetAllFunctions();

      foreach (var @function in allFunctions) {
        if (candidate == function.Lexeme) {
          return true;
        }
      }

      return false;
    }

    #region Helpers

    private IList<Function> GetAllFunctions() {
      var allFunctions = new List<Function>(64);

      foreach (var library in _libraries) {
        allFunctions.AddRange(library.Functions);
      }
      return allFunctions;
    }

    #endregion Helpers

  }  // class LibrariesRegistry

}  // namespace Empiria.Expressions
