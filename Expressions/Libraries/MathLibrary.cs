/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Libraries                               *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Empiria Expressions Functions Library   *
*  Type     : MathLibrary                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Default arithmetic functions library.                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Expressions.Libraries {

  /// <summary>Default arithmetic functions library.</summary>
  internal class MathLibrary : BaseFunctionsLibrary {

    private MathLibrary() {
      LoadFunctions();
    }


    static public MathLibrary Instance {
      get {
        return new MathLibrary();
      }
    }


    private void LoadFunctions() {
      var functions = new[] {
        new Function(lexeme: "ABS", arity: 1, calle: () => new AbsoluteValueFunction()),
        new Function(lexeme: "SUM", arity: 2, calle: () => new SumFunction()),
        new Function(lexeme: "ROUND", arity: 2, calle: () => new RoundFunction()),
      };

      base.AddRange(functions);
    }


    /// <summary>Returns the absolute value of a decimal number.</summary>
    sealed private class AbsoluteValueFunction : FunctionHandler {

      protected internal override decimal Evaluate() {

        decimal quantity = GetDecimal(Parameters[0]);

        return Math.Abs(quantity);
      }

    }  // AbsoluteValueFunction



    /// <summary>Returns the sum of two decimal numbers.</summary>
    sealed private class SumFunction : FunctionHandler {

      protected internal override decimal Evaluate() {

        decimal sum = 0;

        foreach (IToken parameter in base.Parameters) {
          sum += GetDecimal(parameter);
        }

        return sum;
      }

    }  // SumFunction


    /// <summary>Rounds a decimal number to a specified fractional digits.</summary>
    sealed private class RoundFunction : FunctionHandler {

      protected internal override decimal Evaluate() {

        decimal quantity = GetDecimal(base.Parameters[0]);

        int decimalDigits = Convert.ToInt32(GetDecimal(base.Parameters[1]));

        return Math.Round(quantity, decimalDigits);
      }

    }  // RoundFunction

  }  // class MathLibrary

}  // namespace Empiria.Expressions.Libraries
