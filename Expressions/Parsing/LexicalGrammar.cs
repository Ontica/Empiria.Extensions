/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parsing                     *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Information Holder                      *
*  Type     : LexicalGrammar                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Contains the elements of a lexical grammar.                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Expressions {

  /// <summary>Contains the elements of a lexical grammar.</summary>
  public class LexicalGrammar {

    #region Constructors and parsers

    private LexicalGrammar() {
      this.ArithmeticalOperators = @"+ - * / \ %";
      this.LogicalOperators = @"&& || !";
      this.RelationalOperators = @"== != <> <= >= < >";
      this.GroupingOperators = @"( [ { } ] ) , ;";
      this.ReservedWords = @"true false if then else";
      this.FunctionIdentifiers = @"SI SUM SUMAR ABS VALORIZAR";
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

  }  // class LexicalGrammar

}  // namespace Empiria.Expressions
