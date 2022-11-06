/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Service provider                        *
*  Type     : ExpressionParser                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Converts a plain text expression into an Expression object.                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;


namespace Empiria.Expressions {

  internal interface IExpressionToken {

  }

  /// <summary>Converts a plain text expression into an Expression object.</summary>
  public class ExpressionParser {

    private readonly FixedList<IExpressionToken> _tokens;


    public ExpressionParser(string textExpression) {
      Assertion.Require(textExpression, nameof(textExpression));

      var tokenizer = new Tokenizer();

      _tokens = tokenizer.Tokenize(textExpression);
    }


    public Expression Compile() {
      FunctionObject function = ParseFunctionObject();

      return new Expression(function);
    }


    private FunctionObject ParseFunctionObject() {
      FunctionToken token = ParseFunctionToken();

      FixedList<FunctionParameter> parameters = ParseFunctionParameters();

      return new FunctionObject(token, parameters);
    }


    private FunctionToken ParseFunctionToken() {
      return FunctionToken.SUM;
    }


    private FixedList<FunctionParameter> ParseFunctionParameters() {
      return new FixedList<FunctionParameter>();
    }

  }  // class ExpressionParser

}  // namespace Empiria.Expressions
