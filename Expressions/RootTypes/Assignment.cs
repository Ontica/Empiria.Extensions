/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Expressions               *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : Assignment                                       Pattern  : Standard Class                    *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Defines an assignment statement.                                                              *
*                                                                                                            *
********************************* Copyright (c) 2008-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
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
      Assertion.AssertObject(variableSymbol, "variableSymbol");
      Assertion.AssertObject(expression, "expression");

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
