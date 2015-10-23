/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Expressions               *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : ExpressionList                                   Pattern  : Collection Class                  *
*  Version   : 6.5                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Defines an ordered list of expression type objects.                                           *
*                                                                                                            *
********************************* Copyright (c) 2008-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

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
