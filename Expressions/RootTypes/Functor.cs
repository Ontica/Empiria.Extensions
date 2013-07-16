/* Empiria® Extended Framework 2013 **************************************************************************
*                                                                                                            *
*  Solution  : Empiria® Extended Framework                      System   : Expressions Runtime Library       *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : Functor                                          Pattern  : Standard Class                    *
*  Date      : 25/Jun/2013                                      Version  : 5.1     License: CC BY-NC-SA 3.0  *
*                                                                                                            *
*  Summary   : Defines a numeric, string, boolean or datetime functor, a function pointer to a library       *
*              defined function.                                                                             *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2013. **/
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
      Assertion.EnsureObject(functionName, "functionName");
      Assertion.Ensure(functionArity >= 0, "Function arity less than zero");

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