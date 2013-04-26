/* Empiria® Extended Framework 2013 **************************************************************************
*                                                                                                            *
*  Solution  : Empiria® Extended Framework                      System   : Expressions Runtime Library       *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : Variable                                         Pattern  : Standard Class                    *
*  Date      : 25/Jun/2013                                      Version  : 5.1     License: CC BY-NC-SA 3.0  *
*                                                                                                            *
*  Summary   : Defines a numeric, string, boolean or datetime variable.                                      *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1994-2013. **/
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
      Assertion.EnsureObject(symbol, "symbol");

      return new Variable(symbol, null);
    }

    static public Variable Parse(string symbol, object value) {
      Assertion.EnsureObject(symbol, "symbol");
      Assertion.EnsureObject(value, "value");

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