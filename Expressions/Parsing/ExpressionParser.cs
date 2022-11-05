/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Service provider                        *
*  Type     : ExpressionParser                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Converts a plain text expression into an Expression object.                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.Collections.Generic;

namespace Empiria.Expressions {

  internal interface IExpressionToken {

  }

  /// <summary>Converts a plain text expression into an Expression object.</summary>
  public class ExpressionParser {

    private readonly FixedList<IExpressionToken> _tokenizedExpression;

    public ExpressionParser(string textExpression) {
      Assertion.Require(textExpression, nameof(textExpression));

      _tokenizedExpression = TokenizePlainTextExpression(textExpression);
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


    static private FixedList<IExpressionToken> TokenizePlainTextExpression(string expression) {
      string[] lexemeList = EmpiriaString.TrimAll(expression).Split(' ');

      List<IExpressionToken> tokens = new List<IExpressionToken>(lexemeList.Length);

      foreach (string lexeme in lexemeList) {
        IExpressionToken token = TryToTokenize(lexeme);

        Assertion.Require(token, $"Expression has an unrecognized part: '{lexeme}'");

        tokens.Add(token);
      }

      return tokens.ToFixedList();
    }


    private static IExpressionToken TryToTokenize(string lexeme) {
      lexeme = EmpiriaString.TrimAll(lexeme);

      Assertion.Require(lexeme, nameof(lexeme));

      IExpressionToken token = Keyword.TryToTokenize(lexeme);

      if (token != null) {
        return token;
      }

      token = Operator.TryToTokenize(lexeme);

      if (token != null) {
        return token;
      }

      token = Literal.TryToTokenize(lexeme);

      if (token != null) {
        return token;
      }

      token = Function.TryToTokenize(lexeme);

      if (token != null) {
        return token;
      }

      return Variable.TryToTokenize(lexeme);

    }

  }  // class ExpressionParser

}  // namespace Empiria.Expressions
