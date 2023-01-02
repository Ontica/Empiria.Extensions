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

namespace Empiria.Expressions
{

    /// <summary>Contains the elements and rules of a lexical grammar.</summary>
    public class LexicalGrammar {

    private readonly LibrariesRegistry _librariesRegistry = new LibrariesRegistry();

    #region Constructors and parsers

    private LexicalGrammar() {
      this.ArithmeticalOperators = @"+ - * / \ %";
      this.LogicalOperators = @"&& || !";
      this.RelationalOperators = @"== != <> <= >= < >";
      this.GroupingOperators = @"( [ { } ] ) , ;";
      this.ReservedWords = @"true false if then else";
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


    public string ConstantSeparators {
      get;
    }

    #endregion Properties

    #region Methods

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
      return GetOperators();
    }


    internal string[] GetStroppableSymbols() {
      string allStroppableSymbols = $"{StroppableSymbols}";

      return CommonMethods.ConvertToArray(allStroppableSymbols);
    }


    #endregion Helpers

  }  // class LexicalGrammar

}  // namespace Empiria.Expressions
