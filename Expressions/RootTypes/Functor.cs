/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Expressions               *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : Functor                                          Pattern  : Standard Class                    *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Defines a numeric, string, boolean or datetime functor, a function pointer to a library       *
*              defined function.                                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Expressions {

  /// <summary>Defines a numeric, string, boolean or datetime function.</summary>
  public class Functor : Operator {

    #region Fields

    private string functionName = String.Empty;

    #endregion Fields

    #region Constructors and parsers

    protected Functor(string functionName, int functionArity)
      : base('Ω', functionArity) {
      this.functionName = functionName;
    }

    static public Functor Parse(string functionName, int functionArity) {
      Assertion.Require(functionName, "functionName");
      Assertion.Require(functionArity >= 0, "Function arity less than zero");

      return new Functor(functionName, functionArity);
    }

    #endregion Constructors and parsers

    #region Public properties

    public string FunctionName {
      get { return this.functionName; }
    }

    #endregion Public properties

    #region Public methods

    public override string ToString() {
      return this.functionName.ToString();
    }

    #endregion Public methods

  }  // class Functor

} // namespace Empiria.Expressions
