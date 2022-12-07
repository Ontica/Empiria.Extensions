/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parsing                     *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Information Holder                      *
*  Type     : LexicalGrammar                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Contains the elements and rules of a lexical grammar.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Expressions {

  /// <summary>Contains the elements and rules of a lexical grammar.</summary>
  public class LexicalGrammar {

    #region Constructors and parsers

    private LexicalGrammar() {
      this.ArithmeticalOperators = @"+ - * / \ %";
      this.LogicalOperators = @"&& || !";
      this.RelationalOperators = @"== != <> <= >= < >";
      this.GroupingOperators = @"( [ { } ] ) , ;";
      this.ReservedWords = @"true false if then else";
      this.FunctionIdentifiers = @"SI SUM ABS ROUND VALORIZAR DEUDORAS_MENOS_ACREEDORAS";
      this.StroppableSymbols = @"== != <> <= >=";
      this.ConstantSeparators = @"' """;
    }

    static internal readonly LexicalGrammar Default = new LexicalGrammar();

    #endregion Constructors and parsers

    #region Properties

    public string ArithmeticalOperators {
      get;
    }


    public string LogicalOperators {
      get;
    }


    public string RelationalOperators {
      get;
    }


    public string GroupingOperators {
      get;
    }


    public string ReservedWords {
      get;
    }


    public string FunctionIdentifiers {
      get;
    }


    public string StroppableSymbols {
      get;
    }


    public string ConstantSeparators {
      get;
    }

    #endregion Properties

    #region Methods


    public bool IsFunction(string candidate) {
      string[] functions = GetFunctions();

      foreach (var @function in functions) {
        if (candidate == function) {
          return true;
        }
      }

      return false;
    }


    public bool IsKeyword(string candidate) {
      string[] keywords = GetKeywords();

      foreach (var keyword in keywords) {
        if (candidate == keyword) {
          return true;
        }
      }

      return false;
    }


    public bool IsLiteral(string candidate) {
      if (EmpiriaString.IsQuantity(candidate)) {
        return true;
      }

      if (IsStringOrDateConstant(candidate)) {
        return true;
      }

      return false;
    }


    public bool IsOperator(string candidate) {
      string[] allOperators = GetOperators();

      foreach (var @operator in allOperators) {
        if (candidate == @operator) {
          return true;
        }
      }

      return false;
    }


    private bool IsStringOrDateConstant(string candidate) {
      string[] separators = CommonMethods.ConvertToArray(ConstantSeparators);

      foreach (var separator in separators) {
        if (candidate.StartsWith(separator) && candidate.EndsWith(separator)) {
          return true;
        }
      }

      return false;
    }


    public bool IsVariable(string candidate) {
      return char.IsLetter(candidate[0]);
    }

    #endregion Methods

    #region Temporal

    internal int ArityOf(IToken token) {
      if (token.Type != TokenType.Function) {
        return -1;
      }
      if (token.Lexeme == "ABS" || token.Lexeme == "VALORIZAR") {
        return 1;
      }
      if (token.Lexeme == "SI" || token.Lexeme == "DEUDORAS_MENOS_ACREEDORAS") {
        return 3;
      }
      if (token.Lexeme == "ROUND") {
        return 2;
      }
      return -1;
    }

    internal bool IsListItemDelimiter(IToken token) {
      return token.Type == TokenType.Operator && (token.Lexeme == "," || token.Lexeme == ";");
    }

    internal bool IsRightParenthesis(IToken token) {
      return token.Type == TokenType.Operator && token.Lexeme == ")";
    }


    internal bool IsLeftParenthesis(IToken token) {
      return token.Type == TokenType.Operator && token.Lexeme == "(";
    }


    internal bool IsOperand(IToken token) {
      return token.Type == TokenType.Variable || token.Type == TokenType.Literal;
    }


    internal bool IsOperator(IToken token) {
      return (token.Type == TokenType.Operator || token.Type == TokenType.Function);
    }


    internal int Precedence(IToken token) {
      var precedenceTable = new string[] { " ( ",          // Evaluates later
                                           " , ; ",
                                           " OR ",         // Evaluates last
                                           " AND ",
                                           " = <> != ",
                                           " > < ",
                                           " + - ",
                                          @" * / ÷ \ ",
                                           " ) ",          // Evaluates inmediatly
                                         };

      for (int i = 0; i < precedenceTable.Length; i++) {
        if (precedenceTable[i].Contains(token.Lexeme)) {
          return i;
        }
      }
      return 9999;
    }

    #endregion Temporal

    #region Helpers

    private string[] GetFunctions() {
      string allFunctions = $"{FunctionIdentifiers}";

      return CommonMethods.ConvertToArray(allFunctions);
    }


    private string[] GetKeywords() {
      string allKeywords = $"{ReservedWords}";

      return CommonMethods.ConvertToArray(allKeywords);
    }

    private string[] GetOperators() {
      string allOperators = $"{ArithmeticalOperators} {LogicalOperators} " +
                            $"{RelationalOperators} {GroupingOperators}";

      return CommonMethods.ConvertToArray(allOperators);
    }


    internal string[] GetReconstructableSymbols() {
      return GetOperators();
    }


    internal string[] GetStroppableSymbols() {
      string allStroppableSymbols = $"{StroppableSymbols}";

      return CommonMethods.ConvertToArray(allStroppableSymbols);
    }



    #endregion Helpers

  }  // class LexicalGrammar

}  // namespace Empiria.Expressions
