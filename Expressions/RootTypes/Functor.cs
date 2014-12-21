/* Empiria Extended Framework 2015 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                       System   : Expressions Runtime Library       *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : Functor                                          Pattern  : Standard Class                    *
*  Version   : 6.0        Date: 04/Jan/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Defines a numeric, string, boolean or datetime functor, a function pointer to a library       *
*              defined function.                                                                             *
*                                                                                                            *
********************************* Copyright (c) 2008-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
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
      Assertion.AssertObject(functionName, "functionName");
      Assertion.Assert(functionArity >= 0, "Function arity less than zero");

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
