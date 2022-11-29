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
      string concept = GetString(parameters[0], inputData);

      if (concept.StartsWith("1") || concept.StartsWith("3") || concept.StartsWith("6") || concept.StartsWith("7")) {
        return GetDecimal(parameters[1], inputData) - GetDecimal(parameters[2], inputData);

      } else if (concept.StartsWith("2") || concept.StartsWith("4") || concept.StartsWith("5")) {
        return GetDecimal(parameters[2], inputData) - GetDecimal(parameters[1], inputData);

      } else {
        throw Assertion.EnsureNoReachThisCode($"Unhandled DeudorasMenosAcreedoras() rule for concept with code '{concept}'.");

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
