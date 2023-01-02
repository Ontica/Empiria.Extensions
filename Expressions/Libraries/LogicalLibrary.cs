/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Libraries                               *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Empiria Expressions Functions Library   *
*  Type     : LogicalLibrary                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Default logical functions library.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Expressions.Libraries {

  /// <summary>Default logical functions library.</summary>
  internal class LogicalLibrary : BaseFunctionsLibrary {

    private LogicalLibrary() {
      LoadFunctions();
    }


    static public LogicalLibrary Instance {
      get {
        return new LogicalLibrary();
      }
    }


    private void LoadFunctions() {
      var functions = new[] {
        new Function("SI", 3, () => new IfFunction())
      };

      base.AddRange(functions);
    }

    sealed private class IfFunction : FunctionHandler {

      protected internal override decimal Evaluate() {
        bool condition = base.GetBoolean(Parameters[0]);

        if (condition) {
          return GetDecimal(Parameters[1]);
        } else {
          return GetDecimal(Parameters[2]);
        }
      }

    }  // class IfFunction

  }  // class LogicalLibrary

}  // namespace Empiria.Expressions.Libraries
