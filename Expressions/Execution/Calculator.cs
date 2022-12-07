/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Service provider                        *
*  Type     : Calculator                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Performs basic arithmetical operations.                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Expressions {

  /// <summary>Performs basic arithmetical operations.</summary>
  static internal class Calculator {

    static internal decimal Calculate(IToken operation,
                                      FixedList<IToken> parameters,
                                      IDictionary<string, object> inputData) {

      switch (operation.Lexeme) {

        case "+":
        case "SUM":
          return Sum(parameters, inputData);

        case "-":
          return Substraction(parameters, inputData);

        case "*":
          return Multiplication(parameters, inputData);

        case "/":
          return Division(parameters, inputData);

        case "ROUND":
          return Round(parameters, inputData);

        case "VALORIZAR":
          return Valorize(parameters, inputData);

        case "DEUDORAS_MENOS_ACREEDORAS":
          return DeudorasMenosAcreedoras(parameters, inputData);

        default:
          throw new NotImplementedException($"Lexeme function handler not found {operation.Lexeme}.");
      }

    }


    static private decimal Division(FixedList<IToken> parameters,
                                    IDictionary<string, object> inputData) {
      return GetDecimal(parameters[0], inputData) / GetDecimal(parameters[1], inputData);
    }


    static private decimal Multiplication(FixedList<IToken> parameters,
                                          IDictionary<string, object> inputData) {
      return GetDecimal(parameters[0], inputData) * GetDecimal(parameters[1], inputData);
    }


    static private decimal Round(FixedList<IToken> parameters,
                                 IDictionary<string, object> inputData) {
      return Math.Round(GetDecimal(parameters[0], inputData),
                        Convert.ToInt32(GetDecimal(parameters[1], inputData)));
    }


    static private decimal Substraction(FixedList<IToken> parameters,
                                        IDictionary<string, object> inputData) {

      if (parameters.Count == 1) {
        return -1 * GetDecimal(parameters[0], inputData);
      } else {
        return GetDecimal(parameters[0], inputData) - GetDecimal(parameters[1], inputData);
      }
    }


    static private decimal Sum(FixedList<IToken> parameters,
                               IDictionary<string, object> inputData) {
      decimal sum = 0;

      foreach (IToken parameter in parameters) {
        sum += GetDecimal(parameter, inputData);
      }

      return sum;
    }


    static private decimal Valorize(FixedList<IToken> parameters,
                                    IDictionary<string, object> inputData) {
      return 20 * GetDecimal(parameters[0], inputData);
    }


    static private decimal DeudorasMenosAcreedoras(FixedList<IToken> parameters,
                                                   IDictionary<string, object> inputData) {
      string conceptCode = GetString(parameters[0], inputData);

      decimal deudoras = GetDecimal(parameters[1], inputData);
      decimal acreedoras = GetDecimal(parameters[2], inputData);

      if (conceptCode.StartsWith("2") || conceptCode.StartsWith("4") || conceptCode.StartsWith("5")) {
        return acreedoras - deudoras;

      //} else if (conceptCode.StartsWith("3")) {
      //  return deudoras + acreedoras;

      } else {
        return deudoras - acreedoras;
      }

    }


    static private decimal GetDecimal(IToken parameter,
                                      IDictionary<string, object> inputData) {

      if (parameter.Type == TokenType.Literal) {
        return Convert.ToDecimal(parameter.Lexeme);

      } else if (inputData.ContainsKey(parameter.Lexeme)) {

        return Convert.ToDecimal(inputData[parameter.Lexeme]);

      } else {
        return 0;

      }
    }


    static private string GetString(IToken parameter,
                                    IDictionary<string, object> inputData) {

      if (parameter.Type == TokenType.Literal) {
        return parameter.Lexeme;

      } else if (inputData.ContainsKey(parameter.Lexeme)) {

        return Convert.ToString(inputData[parameter.Lexeme]);

      } else {
        return String.Empty;

      }
    }

  }  // class Calculator

}  // namespace Empiria.Expressions
