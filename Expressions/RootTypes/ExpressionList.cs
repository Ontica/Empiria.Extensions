﻿/* Empiria® Extended Framework 2013 **************************************************************************
*                                                                                                            *
*  Solution  : Empiria® Extended Framework                      System   : Expressions Runtime Library       *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : ExpressionList                                   Pattern  : Collection Class                  *
*  Date      : 25/Jun/2013                                      Version  : 5.1     License: CC BY-NC-SA 3.0  *
*                                                                                                            *
*  Summary   : Defines an ordered list of expression type objects.                                           *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1994-2013. **/

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