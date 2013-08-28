/* Empiria® Extended Framework 2013 **************************************************************************
*                                                                                                            *
*  Solution  : Empiria® Extended Framework                      System   : Expressions Runtime Library       *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : Assignment                                       Pattern  : Standard Class                    *
*  Date      : 23/Oct/2013                                      Version  : 5.2     License: CC BY-NC-SA 3.0  *
*                                                                                                            *
*  Summary   : Defines an assignment statement.                                                              *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2013. **/
using System;

namespace Empiria.Expressions {

  /// <summary>Defines an assignment statement.</summary>
  public class Assignment : IStatement {

    #region Fields

    private Variable variable = null;
    private Expression expression = null;

    #endregion Fields

    #region Constructors and parsers

    private Assignment(string variableSymbol, Expression expression) {
      this.variable = Variable.Parse(variableSymbol);
      this.expression = expression;
    }

    static public Assignment Parse(string variableSymbol, Expression expression) {
      Assertion.EnsureObject(variableSymbol, "variableSymbol");
      Assertion.EnsureObject(expression, "expression");

      return new Assignment(variableSymbol, expression);
    }

    #endregion Constructors and parsers

    #region Public properties

    public Expression Expression {
      get { return this.expression; }
    }

    public Variable Variable {
      get { return this.variable; }
    }

    #endregion Public properties

    #region Public methods

    public void Execute() {

    }

    #endregion Public methods


  }  // class Variable

} // namespace Empiria.Expressions