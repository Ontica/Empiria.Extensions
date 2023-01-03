/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Abstract type                           *
*  Type     : BaseEvaluatorHandler                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Abstract base class for functions and operator evaluation handlers.                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Expressions.Execution {

  /// <summary>Abstract base class for function and operator evaluation handlers.</summary>
  abstract public class BaseEvaluatorHandler {

    public IDictionary<string, object> Data {
      get;
      private set;
    } = new Dictionary<string, object>();



    internal void LoadData(IDictionary<string, object> data) {
      Assertion.Require(data, nameof(data));

      Data = data;
    }


    protected bool GetBoolean(IToken parameter) {

      if (parameter.Type == TokenType.Literal) {
        return Convert.ToBoolean(parameter.Lexeme);

      } else if (Data.ContainsKey(parameter.Lexeme)) {

        return Convert.ToBoolean(Data[parameter.Lexeme]);

      } else {
        return false;

      }
    }


    protected decimal GetDecimal(IToken parameter) {

      if (parameter.Type == TokenType.Literal) {
        return Convert.ToDecimal(parameter.Lexeme);

      } else if (Data.ContainsKey(parameter.Lexeme)) {

        return Convert.ToDecimal(Data[parameter.Lexeme]);

      } else {
        return 0;

      }
    }


    protected string GetString(IToken parameter) {

      if (parameter.Type == TokenType.Literal) {
        return parameter.Lexeme;

      } else if (Data.ContainsKey(parameter.Lexeme)) {

        return Convert.ToString(Data[parameter.Lexeme]);

      } else {
        return string.Empty;

      }
    }


  }  // class BaseEvaluatorHandler

}  // namespace Empiria.Expressions.Execution
