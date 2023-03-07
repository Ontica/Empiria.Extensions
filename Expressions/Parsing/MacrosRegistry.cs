/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parsing                     *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Information Holder                      *
*  Type     : MacrosRegistry                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Mantains a registry of user code macros.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Expressions {

  /// <summary>Mantains a registry of user code macros.</summary>
  internal class MacrosRegistry {

    private readonly List<IMacro> _macros = new List<IMacro>();


    internal MacrosRegistry() {
      // no-op
    }


    internal void Add(IEnumerable<IMacro> macros) {
      Assertion.Require(macros, nameof(macros));

      _macros.AddRange(macros);
    }


    internal IMacro GetMacro(string name) {
      var macro = _macros.Find(x => x.Name == name);

      Assertion.Require(macro, $"A programming macro with name '{name}' was not defined.");

      return macro;
    }

  }  // class MacrosRegistry

}  // namespace Empiria.Expressions
