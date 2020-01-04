/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Expressions               *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : ExpressionsException                             Pattern  : Exception Class                   *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : The exception that is thrown when a problem occurs in Empiria Expressions Runtime Library.    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Reflection;

namespace Empiria.Expressions {

  /// <summary>The exception that is thrown when a problem occurs in Empiria
  /// Expressions Runtime Library.</summary>
  [Serializable]
  public sealed class ExpressionsException : EmpiriaException {

    public enum Msg {
      CantCompareOperands,
      CompileError,
      EndOfLiteralCharNotFound,
      InvalidFunctionArguments,
      InvalidOperandDataType,
      InvalidOperator,
      InvalidOperatorArgumentsNumber,
      NotWellFormedAssignment,
      NotWellFormedExpression,
      UncompiledExpression,
      UndefinedGlobalFunction,
      UndefinedOperatorArity,
      UndefinedOperatorMethod,
      UndefinedOperatorPrecedence,
      UnloadedFunction,
      UnrecognizedOperand,
      UseOfUnassignedVariable,
      ZeroDivision,
    }

    static private string resourceBaseName = "Empiria.Expressions.RootTypes.ExpressionsExceptionMsg";

    #region Constructors and parsers

    /// <summary>Initializes a new instance of ExpressionsException class with a specified error
    /// message.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="args">An optional array of items to format into the exception message.</param>
    public ExpressionsException(Msg message, params object[] args)
      : base(message.ToString(), GetMessage(message, args)) {
      //base.Publish();
    }

    /// <summary>Initializes a new instance of ExpressionsException class with a specified error
    ///  message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="innerException">This is the inner exception.</param>
    /// <param name="args">An optional array of items to format into the exception message.</param>
    public ExpressionsException(Msg message, Exception innerException, params object[] args)
      : base(message.ToString(), GetMessage(message, args), innerException) {
      //base.Publish();
    }

    #endregion Constructors and parsers

    #region Private methods

    static private string GetMessage(Msg message, params object[] args) {
      return GetResourceMessage(message.ToString(), resourceBaseName, Assembly.GetExecutingAssembly(), args);
    }

    #endregion Private methods

  } // class ExpressionsException

} // namespace Empiria.Expressions
