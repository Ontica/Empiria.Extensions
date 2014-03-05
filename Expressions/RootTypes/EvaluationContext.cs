/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                       System   : Expressions Runtime Library       *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : EvaluationContext                                Pattern  : Unit of Work                      *
*  Version   : 5.5        Date: 28/Mar/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Evaluation context for a list of arithmetical and logical expressions.                        *
*                                                                                                            *
********************************* Copyright (c) 1999-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;

namespace Empiria.Expressions {

  /// <summary>Evaluation context for a list of arithmetical and logical expressions.</summary>
  public class EvaluationContext : IDisposable {

    #region Delegates

    private delegate object OnEndEvaluationDelegate(Expression expression);

    #endregion Delegates

    #region Fields

    private OnEndEvaluationDelegate onEndEvaluationDelegate = null;
    private IAsyncResult asyncResult = null;

    private FunctionLibrary loadedFunctions = null;
    private Type returnType = typeof(decimal);

    private bool isCompiled = false;
    private bool trace = false;
    private string traceString = String.Empty;

    private bool disposed = false;

    #endregion Fields

    #region Constructors and parsers

    public EvaluationContext() {
      this.loadedFunctions = new FunctionLibrary();
      isCompiled = false;
    }

    ~EvaluationContext() {
      Dispose(false);
    }

    #endregion Constructors and parsers

    #region Public methods

    public string LastExpressionTrace {
      get { return this.traceString; }
      set { this.traceString = value; }
    }

    public bool TraceEnabled {
      get { return this.trace; }
      set { this.trace = value; }
    }

    public void AddFunction(Function function) {
      loadedFunctions.AddFunction(function);

      this.isCompiled = false;
    }

    public void AddLibrary(RulesLibrary library) {
      for (int i = 0; i < library.Count; i++) {
        Function function = Function.Parse(library.GetRule(i));
        loadedFunctions.AddFunction(function);
      }
      this.isCompiled = false;
    }

    public IAsyncResult BeginEvaluate(Expression expression, AsyncCallback callback, object state) {
      if (!isCompiled) {
        throw new ExpressionsException(ExpressionsException.Msg.UncompiledExpression);
      }
      onEndEvaluationDelegate = new OnEndEvaluationDelegate(this.Evaluate);
      asyncResult = onEndEvaluationDelegate.BeginInvoke(expression, callback, state);

      return asyncResult;
    }

    public void Clear() {
      loadedFunctions.Clear();
      isCompiled = false;
    }

    public void Close() {
      isCompiled = false;
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    public void Compile() {
      IEnumerator<Function> functionsEnumerator = loadedFunctions.GetEnumerator();
      lock (functionsEnumerator) {
        if (isCompiled) {
          return;
        }
        while (functionsEnumerator.MoveNext()) {
          functionsEnumerator.Current.Compile();
        }
      }
      isCompiled = true;
    }

    public void Dispose() {
      Close();
    }

    public object EndEvaluate(IAsyncResult asyncResult) {
      return onEndEvaluationDelegate.EndInvoke(asyncResult);
    }

    public object Evaluate(Expression expression) {
      if (!expression.IsCompiled) {
        expression.Compile();
      }
      Stack<Operand> operandsStack = new Stack<Operand>();

      IExpressionToken[] postfixTokens = expression.ToPostfixTokensArray();
      for (int i = 0; i < postfixTokens.Length; i++) {
        if ((postfixTokens[i] is Operand)) {
          Operand operand = (Operand) postfixTokens[i];
          if (operand.IsNull && operand is Variable) {
            throw new ExpressionsException(ExpressionsException.Msg.UseOfUnassignedVariable,
                                          ((Variable) operand).Symbol, expression.ToString());
          } else if (operand.IsNull) {
            throw new ExpressionsException(ExpressionsException.Msg.UseOfUnassignedVariable,
                                           "?", expression.ToString());
          }
          operandsStack.Push(operand);
        } else if (postfixTokens[i] is Operator) {
          Operator currentOperator = (Operator) postfixTokens[i];
          if (operandsStack.Count < currentOperator.Arity) {
            throw new ExpressionsException(ExpressionsException.Msg.NotWellFormedExpression, expression.ToString());
          }
          Operand[] operandsArray = new Operand[currentOperator.Arity];
          for (int j = 0; j < operandsArray.Length; j++) {
            operandsArray[j] = operandsStack.Pop();
          }
          if (postfixTokens[i] is Functor) {
            operandsStack.Push(loadedFunctions.Call(this, (Functor) postfixTokens[i], operandsArray));
          } else {    // Is base operator
            operandsStack.Push(currentOperator.Apply(operandsArray));
          }
        }
      }
      if (operandsStack.Count != 1) {
        throw new ExpressionsException(ExpressionsException.Msg.NotWellFormedExpression, expression.ToString());
      }
      object result = operandsStack.Pop().GetObject();
      if (expression.EnforcedReturnType != null) {
        return this.CastAndRound(result, expression.EnforcedReturnType, expression.NumericalPrecision);
      } else {
        return result;
      }
    }

    public object Evaluate(Expression expression, Type enforcedReturnType, int numericalPrecision) {
      object result = this.Evaluate(expression);

      return this.CastAndRound(result, enforcedReturnType, numericalPrecision);
    }

    public FunctionLibrary GetFunctions() {
      return this.loadedFunctions;
    }

    #endregion Public methods

    #region Private methods

    private object CastAndRound(object result, Type enforcedReturnType, int numericalPrecision) {
      Assertion.AssertObject(enforcedReturnType, "enforcedReturnType");
      Assertion.Ensure(numericalPrecision >= 0, "NumericalPrecision can be grater or equal than zero.");

      result = Convert.ChangeType(result, enforcedReturnType);
      if (numericalPrecision != -1 && enforcedReturnType == typeof(decimal)) {
        result = Math.Round((decimal) result, numericalPrecision);
      } else if (numericalPrecision != -1 && enforcedReturnType == typeof(float)) {
        result = Math.Round((float) result, numericalPrecision);
      }
      return result;
    }

    private void Dispose(bool disposing) {
      if (!disposed) {
        disposed = true;
        try {
          while ((asyncResult != null) && (!asyncResult.IsCompleted)) {
            // wait until transaction ends
          }
          if (disposing) {
            this.Clear();
          }
        } finally {

        } //try
      } // if
    }

    #endregion Private methods

  } //class EvaluationContext

} //namespace Empiria.Expressions