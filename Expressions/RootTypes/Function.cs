/* Empiria Extended Framework 2015 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                       System   : Expressions Runtime Library       *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : Function                                         Pattern  : Unit of Work                      *
*  Version   : 6.0        Date: 04/Jan/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Represents a prime or compound function.                                                      *
*                                                                                                            *
********************************* Copyright (c) 2008-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;

using Empiria.Ontology;

namespace Empiria.Expressions {

  /// <summary>Represents a prime or compound function.</summary>
  public sealed class Function {

    #region Fields

    private StatementList<Assignment> statements = null;
    private string[] argumentNames = null;
    private string name = String.Empty;
    private int arity = 0;

    private bool isCompiled = false;
    private Type returnType = typeof(decimal);
    private int precision = -1;
    private int typeMethodId = 0;

    private Dictionary<string, object> variables = null;

    #endregion Fields

    #region Constructors and parsers

    public Function(string name)
      : this(name, typeof(decimal), 2, new string[] { }) {
      // no-op
    }

    public Function(string name, Type returnType, int precision) :
      this(name, returnType, precision, new string[] { }) {
      // no-op
    }

    public Function(string name, string[] argumentNames)
      : this(name, typeof(decimal), 2, argumentNames) {
      // no-op
    }

    public Function(string name, Type returnType, int precision, string[] argumentNames) {
      Assertion.AssertObject(name, "name");
      Assertion.AssertObject(returnType, "returnType");
      Assertion.Assert(precision >= 0, "Numerical precision is less than zero");
      Assertion.AssertObject(argumentNames, "argumentNames");

      this.name = name;
      this.returnType = returnType;
      this.precision = precision;
      this.arity = argumentNames.Length;
      this.argumentNames = argumentNames;
      this.statements = new StatementList<Assignment>();
      this.variables = new Dictionary<string, object>();
      this.isCompiled = false;
    }

    static internal Function Parse(BusinessRule rule) {
      Function function = new Function(rule.Name, rule.ReturnValueUnderlyingSystemType, rule.Precision, rule.ArgumentsNames);
      if (rule.BusinessRuleType == BusinessRuleType.Textual) {
        BusinessRuleItem[] list = rule.Statements;
        for (int i = 0; i < list.Length; i++) {
          function.AddAssignment(list[i].VariableSymbol, list[i].Statement);
        }
      } else {
        function.typeMethodId = rule.TypeMethodId;
      }
      return function;
    }

    #endregion Constructors and parsers

    #region Public properties

    public int Arity {
      get { return this.arity; }
    }

    public string[] ArgumentNames {
      get { return argumentNames; }
    }

    public bool IsCompiled {
      get { return this.isCompiled; }
    }

    public string Name {
      get { return this.name; }
    }

    public int Precision {
      get { return this.precision; }
    }

    public Type ReturnType {
      get { return this.returnType; }
    }

    public StatementList<Assignment> Statements {
      get { return statements; }
    }

    #endregion Public properties

    #region Public methods

    public void AddAssignment(Assignment assignment) {
      statements.Add(assignment);

      if (!variables.ContainsKey(assignment.Variable.Symbol)) {
        variables.Add(assignment.Variable.Symbol, assignment.Variable.GetObject());
      }
      this.isCompiled = false;
    }

    public void AddAssignment(string variableSymbol, string expressionTextForm) {
      Expression expression = Expression.Parse(expressionTextForm);

      Assignment assignment = Assignment.Parse(variableSymbol, expression);
      this.AddAssignment(assignment);
    }

    public void Clear() {
      statements.Clear();
      variables.Clear();
      isCompiled = false;
    }

    public void Compile() {
      if (isCompiled) {
        return;
      }
      lock (statements) {
        for (int i = 0; i < statements.Count; i++) {
          ((Assignment) statements[i]).Expression.Compile();
        }
        isCompiled = true;
      }
    }

    internal object Execute(EvaluationContext context, object[] parameters) {
      if (this.arity != parameters.Length) {
        throw new ExpressionsException(ExpressionsException.Msg.InvalidFunctionArguments, this.name,
                                       this.arity, parameters.Length);
      }
      if (!isCompiled) {
        throw new ExpressionsException(ExpressionsException.Msg.UncompiledExpression, this.Name);
      }
      if (this.typeMethodId == 0) {
        return ExecuteTextual(context, parameters);
      } else {
        return ExecuteMethod(context, parameters);
      }
    }

    private object ExecuteMethod(EvaluationContext context, object[] parameters) {
      TypeMethodInfo method = TypeMethodInfo.Parse(typeMethodId);
      if (method.IsStatic) {
        return method.Invoke(parameters);
      } else {
        return method.Invoke(context, parameters);
      }
    }

    private object ExecuteTextual(EvaluationContext context, object[] parameters) {
      if (context.TraceEnabled) {
        context.LastExpressionTrace = String.Empty;
      }
      for (int i = 0; i < this.argumentNames.Length; i++) {
        if (!variables.ContainsKey(argumentNames[i])) {
          variables.Add(argumentNames[i], parameters[i]);
        } else {
          variables[argumentNames[i]] = parameters[i];
        }
      }
      object lastResult = decimal.Zero;
      string trace = String.Empty;
      if (context.TraceEnabled) {
        trace = this.Name + "()" + Environment.NewLine;
      }
      for (int i = 0; i < statements.Count; i++) {
        ApplyCurrentState(statements[i].Expression);
        lastResult = context.Evaluate(statements[i].Expression);
        variables[statements[i].Variable.Symbol] = lastResult;
        statements[i].Variable.SetValue(lastResult);
        if (context.TraceEnabled) {
          trace += i.ToString("00") + ")   " + statements[i].Variable.Symbol + " := " + lastResult + Environment.NewLine;
        }
      } // for
      lastResult = CastAndRound(lastResult);
      if (context.TraceEnabled) {
        context.LastExpressionTrace = trace;
      }
      return lastResult;
    }

    #endregion Public methods

    #region Private methods

    private void ApplyCurrentState(Expression expression) {
      IEnumerator<Variable> expressionVariables = expression.Variables.GetEnumerator();
      lock (expressionVariables) {
        while (expressionVariables.MoveNext()) {
          Variable expressionVariable = (Variable) expressionVariables.Current;
          if (this.variables.ContainsKey(expressionVariable.Symbol)) {
            expressionVariable.SetValue(this.variables[expressionVariable.Symbol]);
          }
        } // while
      }
    }

    private object CastAndRound(object result) {
      result = Convert.ChangeType(result, returnType);
      if (this.precision != -1 && this.returnType == typeof(decimal)) {
        result = Math.Round((decimal) result, this.precision);
      } else if (this.precision != -1 && this.returnType == typeof(float)) {
        result = Math.Round((float) result, this.precision);
      }
      return result;
    }

    #endregion Private methods

  } //class Function

} //namespace Empiria.Expressions
