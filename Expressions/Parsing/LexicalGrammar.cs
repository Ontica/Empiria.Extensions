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

using Empiria.Expressions.Libraries;

namespace Empiria.Expressions {

  /// <summary>Contains the elements and rules of a lexical grammar.</summary>
  public class LexicalGrammar {

    private readonly LibrariesRegistry _librariesRegistry = new LibrariesRegistry();

    #region Constructors and parsers

    private LexicalGrammar() {
      this.ArithmeticalOperators = @"+ - * / \ %";
      this.LogicalOperators = @"&& AND || OR ! NOT";
      this.RelationalOperators = @"== != <> <= >= < >";
      this.GroupingOperators = @"( [ { } ] ) , ;";
      this.ConstantKeywords = @"true false";
      this.ReservedWords = @"if then else";
      this.StroppableSymbols = @"== != <> <= >=";
      this.ConstantSeparators = @"' """;
    }

    static internal LexicalGrammar Default {
      get {
        var defaultGrammar = new LexicalGrammar();

        defaultGrammar.LoadLibrary(MathLibrary.Instance);
        defaultGrammar.LoadLibrary(LogicalLibrary.Instance);

        return defaultGrammar;
      }
    }


    static public LexicalGrammar CreateFromDefault() {
      return Default;
    }


    #endregion Constructors and parsers

    #region Properties

    public string ArithmeticalOperators {
      get;
    }


    public string ConstantKeywords {
      get;
    }


    public string ConstantSeparators {
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


    public string StroppableSymbols {
      get;
    }

    #endregion Properties

    #region Methods

    internal bool IsConstantKeyword(string candidate) {
      string[] constantKeywords = CommonMethods.ConvertToArray(ConstantKeywords);

      foreach (var keyword in constantKeywords) {
        if (candidate == keyword) {
          return true;
        }
      }

      return false;
    }


    public bool IsFunction(string candidate) {
      return _librariesRegistry.HasRegisteredFunction(candidate);
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

      if (candidate == "true" || candidate == "false") {
        return true;
      }

      if (IsStringOrDateConstant(candidate)) {
        return true;
      }

      return false;
    }


    public object LiteralValue(string candidate) {
      if (EmpiriaString.IsQuantity(candidate)) {
        return Convert.ToDecimal(candidate);
      }

      if (candidate == "true" || candidate == "false") {
        return Convert.ToBoolean(candidate);
      }

      var temp = candidate.Substring(1);

      temp = temp.Substring(0, temp.Length - 1);


      if (EmpiriaString.IsDate(temp)) {
        return Convert.ToDateTime(temp);

      } else {
        return temp;

      }
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


    internal LibrariesRegistry Libraries() {
      return _librariesRegistry;
    }


    public void LoadLibrary(BaseFunctionsLibrary library) {
      Assertion.Require(library, nameof(library));

      _librariesRegistry.Add(library);
      // no-op
    }

    #endregion Methods

    #region Temporal

    internal int ArityOf(IToken token) {
      if (token.Type != TokenType.Function) {
        return -1;
      }

      var function = _librariesRegistry.GetFunction(token);

      return function.Arity;
    }


    internal bool IsArithmeticalOperator(IToken token) {
      return IsTokenIn(token, ArithmeticalOperators);
    }


    internal bool IsLeftParenthesis(IToken token) {
      return token.Type == TokenType.Operator && token.Lexeme == "(";
    }


    internal bool IsListItemDelimiter(IToken token) {
      return token.Type == TokenType.Operator && (token.Lexeme == "," || token.Lexeme == ";");
    }


    internal bool IsLogicalOperator(IToken token) {
      return IsTokenIn(token, LogicalOperators);
    }


    internal bool IsOperand(IToken token) {
      return token.Type == TokenType.Variable || token.Type == TokenType.Literal;
    }


    internal bool IsOperatorOrFunction(IToken token) {
      return (token.Type == TokenType.Operator || token.Type == TokenType.Function);
    }


    internal bool IsRelationalOperator(IToken token) {
      return IsTokenIn(token, RelationalOperators);
    }


    internal bool IsRightParenthesis(IToken token) {
      return token.Type == TokenType.Operator && token.Lexeme == ")";
    }


    internal int Precedence(IToken token) {
      var precedenceTable = new string[] { " ( ",          // Evaluates later
                                           " , ; ",
                                           " OR || ",       // Evaluates last
                                           " AND && ",
                                           " NOT ! ",
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

      return 99;
    }

    #endregion Temporal

    #region Helpers

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
      string allOperators = $"{ArithmeticalOperators} {LogicalOperators} " +
                            $"{RelationalOperators} {GroupingOperators}";

      allOperators = allOperators.Replace("AND", string.Empty);
      allOperators = allOperators.Replace("OR", string.Empty);
      allOperators = allOperators.Replace("NOT", string.Empty);

      return CommonMethods.ConvertToArray(allOperators);
    }


    internal string[] GetStroppableSymbols() {
      string allStroppableSymbols = $"{StroppableSymbols}";

      return CommonMethods.ConvertToArray(allStroppableSymbols);
    }


    private bool IsTokenIn(IToken token, string lexemesString) {
      string[] lexemesArray = CommonMethods.ConvertToArray(lexemesString);

      foreach (var lexeme in lexemesArray) {
        if (token.Lexeme == lexeme) {
          return true;
        }
      }

      return false;
    }

    #endregion Helpers

  }  // class LexicalGrammar

}  // namespace Empiria.Expressions
