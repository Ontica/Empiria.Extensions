/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                       System   : Expressions Runtime Library       *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : ExpressionList                                   Pattern  : Collection Class                  *
*  Version   : 5.5        Date: 25/Jun/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Defines an ordered list of expression type objects.                                           *
*                                                                                                            *
********************************* Copyright (c) 2009-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/

using Empiria.Collections;

namespace Empiria.Expressions {

  /// <summary>Defines an ordered list of expression type objects.</summary>
  public class ExpressionList : EmpiriaCollection<string, Expression> {

    #region Fields

    #endregion Fields

    #region Constructors and parsers

    public ExpressionList() {

    }

    #endregion Constructors and parsers

    #region Public properties

    #endregion Public properties

    #region Internal methods

    internal new void Add(string returnVariableName, Expression expression) {
      base.Add(returnVariableName, expression);
    }

    internal new void Clear() {
      base.Clear();
    }

    #endregion Public methods

  }  // class ExpressionList

} // namespace Empiria.Expressions