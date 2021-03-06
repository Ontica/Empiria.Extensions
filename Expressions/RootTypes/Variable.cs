﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Expressions               *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : Variable                                         Pattern  : Standard Class                    *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Defines a numeric, string, boolean or datetime variable.                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Expressions {

  /// <summary>Defines an string, boolean or datetime constant or literal.</summary>
  public class Variable : Operand {

    #region Fields

    private string symbol = String.Empty;

    #endregion Fields

    #region Constructors and parsers

    private Variable(string symbol, object value)
      : base(value) {
      this.symbol = symbol;
    }

    static public Variable Parse(string symbol) {
      Assertion.AssertObject(symbol, "symbol");

      return new Variable(symbol, null);
    }

    static public Variable Parse(string symbol, object value) {
      Assertion.AssertObject(symbol, "symbol");
      Assertion.AssertObject(value, "value");

      return new Variable(symbol, value);
    }

    #endregion Constructors and parsers

    #region Public properties

    public string Symbol {
      get { return this.symbol; }
    }

    #endregion Public properties

  }  // class Variable

} // namespace Empiria.Expressions
