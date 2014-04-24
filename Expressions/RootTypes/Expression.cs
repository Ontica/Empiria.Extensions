/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                       System   : Expressions Runtime Library       *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : Expression                                       Pattern  : Standard Class                    *
*  Version   : 5.5        Date: 25/Jun/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Defines an arithmetical, logical, string or datetime expression.                              *
*                                                                                                            *
********************************* Copyright (c) 2008-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections;
using System.Collections.Generic;

namespace Empiria.Expressions {

  /// <summary>Defines an arithmetical, logical, string or datetime expression.</summary>
  public class Expression : Operand {

    #region Fields

    private readonly string textForm;
    private string parsedTextForm = String.Empty;
    private Type enforcedReturnType = null;
    private int numericalPrecision = -1;
    private Type returnType = typeof(decimal);

    private State<Variable> variablesState = null;
    private Dictionary<string, Functor> functorsTable = null;

    private IExpressionToken[] infixTokensArray = null;
    private IExpressionToken[] postfixTokensArray = null;

    private bool isCompiled = false;

    #endregion Fields

    #region Constructors and parsers

    private Expression(string textForm)
      : base(textForm) {
      this.textForm = textForm;
      isCompiled = false;
    }

    static public Expression Parse(string textForm) {
      return new Expression(textForm);
    }

    #endregion Constructors and parsers

    #region Public properties

    public Type EnforcedReturnType {
      get { return enforcedReturnType; }
      set { enforcedReturnType = value; }
    }

    public int NumericalPrecision {
      get { return numericalPrecision; }
      set { numericalPrecision = value; }
    }

    public Type ReturnType {
      get { return this.returnType; }
      internal set { this.returnType = value; }
    }

    #endregion Public properties

    #region Public methods

    internal void Compile() {
      try {
        Dictionary<string, string> literals = new Dictionary<string, string>();
        this.parsedTextForm = this.textForm;
        this.parsedTextForm = EmpiriaString.TrimAll(EmpiriaString.TrimControl(this.parsedTextForm));
        ExtractLiterals(literals);
        this.parsedTextForm = EmpiriaString.TrimAll(this.parsedTextForm);
        this.parsedTextForm = Operator.ParseOperators(this.parsedTextForm);
        SetVariableReferences();
        SetFunctorReferences();
        this.infixTokensArray = this.ConvertToTokensArray(literals);
        this.postfixTokensArray = this.ConvertToPostfixTokensArray();
        PlaceLiterals(literals);
        isCompiled = true;
      } catch (Exception e) {
        throw new ExpressionsException(ExpressionsException.Msg.CompileError, e, this.textForm);
      }
    }

    public IExpressionToken[] ToInfixTokensArray() {
      if (this.isCompiled) {
        return this.infixTokensArray;
      } else {
        throw new ExpressionsException(ExpressionsException.Msg.UncompiledExpression, this.textForm);
      }
    }

    public override string ToString() {
      return this.textForm;
    }

    public IExpressionToken[] ToPostfixTokensArray() {
      if (this.isCompiled) {
        return this.postfixTokensArray;
      } else {
        throw new ExpressionsException(ExpressionsException.Msg.UncompiledExpression, this.textForm);
      }
    }

    #endregion Public methods

    #region Internal properties

    internal Dictionary<string, Functor> Functors {
      get {
        if (isCompiled) {
          return this.functorsTable;
        } else {
          throw new ExpressionsException(ExpressionsException.Msg.UncompiledExpression, this.textForm);
        }
      }
    }

    internal string GetParsedText() {
      if (isCompiled) {
        return this.parsedTextForm;
      } else {
        throw new ExpressionsException(ExpressionsException.Msg.UncompiledExpression, this.textForm);
      }
    }

    internal bool IsCompiled {
      get { return this.isCompiled; }
    }

    internal State<Variable> Variables {
      get {
        if (isCompiled) {
          return this.variablesState;
        } else {
          throw new ExpressionsException(ExpressionsException.Msg.UncompiledExpression, this.textForm);
        }
      }
    }

    #endregion Internal properties

    #region Private methods

    private IExpressionToken[] ConvertToPostfixTokensArray() {
      Stack<Operator> operatorsStack = new Stack<Operator>();

      List<IExpressionToken> postfixTokensList = new List<IExpressionToken>();

      for (int i = 0; i < infixTokensArray.Length; i++) {
        if (infixTokensArray[i] is Operand) {
          postfixTokensList.Add(infixTokensArray[i]);
        } else if (infixTokensArray[i] is Operator) {
          Operator currentOperator = (Operator) infixTokensArray[i];
          if (currentOperator.IsLeftParenthesis) {
            operatorsStack.Push(currentOperator);
          } else if (currentOperator is Functor) {
            operatorsStack.Push(currentOperator);
          } else if (currentOperator.IsRightParenthesis) {
            // Process all operators until founds the last pushed left parenthesis
            while (true) {
              Operator lastOperator = operatorsStack.Pop();
              if (!lastOperator.IsLeftParenthesis) {
                postfixTokensList.Add(lastOperator);
              } else {
                break;
              }
            }
          } else {
            // Process all stack operators with highest precedence
            while (operatorsStack.Count != 0) {
              if (currentOperator.Precedence <= operatorsStack.Peek().Precedence) {
                Operator lastOperator = operatorsStack.Pop();
                postfixTokensList.Add(lastOperator);
              } else {
                break;
              }
            } // while
            // Discard lists separtors operators from the postfix array
            if (!Operator.IsListItemDelimiter(currentOperator.Symbol)) {
              operatorsStack.Push(currentOperator);
            }
          } // if 
        } // if IsOperand
      } // for
      while (operatorsStack.Count != 0) {
        Operator lastOperator = operatorsStack.Pop();
        postfixTokensList.Add(lastOperator);
      }

      IExpressionToken[] postfixTokensArray = new IExpressionToken[postfixTokensList.Count];
      postfixTokensList.CopyTo(postfixTokensArray);

      return postfixTokensArray;
    }

    private IExpressionToken[] ConvertToTokensArray(Dictionary<string, string> literalsTable) {
      IExpressionToken[] infixTokens = null;
      string[] stringTokensArray = null;

      this.parsedTextForm = EmpiriaString.TrimAll(this.parsedTextForm);
      stringTokensArray = this.parsedTextForm.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
      infixTokens = new IExpressionToken[stringTokensArray.Length];

      for (int i = 0; i < stringTokensArray.Length; i++) {
        if (literalsTable.ContainsKey(stringTokensArray[i])) {
          infixTokens[i] = Constant.Parse(literalsTable[stringTokensArray[i]]);
        } else if (this.variablesState.ContainsKey(stringTokensArray[i])) {
          infixTokens[i] = this.variablesState.GetItem(stringTokensArray[i]);
        } else if (this.functorsTable.ContainsKey(stringTokensArray[i])) {
          infixTokens[i] = this.functorsTable[stringTokensArray[i]];
        } else if (Operator.IsOperator(stringTokensArray[i][0])) {
          infixTokens[i] = Operator.Parse(stringTokensArray[i][0]);
        } else {
          infixTokens[i] = Constant.Parse(stringTokensArray[i]);
        }
      }
      return infixTokens;
    }

    private void ExtractLiterals(Dictionary<string, string> literals) {
      RemoveLiteralsForDelimiter(literals, '"');
      RemoveLiteralsForDelimiter(literals, '\'');
      RemoveLiteralsForDelimiter(literals, '#');

      this.parsedTextForm = " " + this.parsedTextForm + " ";
      for (int i = 0; i < Constant.ReservedKeywords.Length; i++) {
        if (this.parsedTextForm.Contains(" " + Constant.ReservedKeywords[i] + " ")) {
          int literalIndex = literals.Count;
          string literalTag = "§" + literalIndex.ToString("0000");
          literals.Add(literalTag, Constant.ReservedKeywords[i]);
          this.parsedTextForm = this.parsedTextForm.Replace(Constant.ReservedKeywords[i], literalTag);
        }
      }
      this.parsedTextForm = EmpiriaString.TrimAll(this.parsedTextForm);
    }

    private void PlaceLiterals(Dictionary<string, string> literals) {
      IEnumerator enumerator = literals.GetEnumerator();

      while (enumerator.MoveNext()) {
        KeyValuePair<string, string> item = (KeyValuePair<string, string>) enumerator.Current;
        this.parsedTextForm = this.parsedTextForm.Replace(item.Key, item.Value);
      }
    }

    private void RemoveLiteralsForDelimiter(Dictionary<string, string> literals, char delimiter) {
      while (true) {
        int startIndex = this.parsedTextForm.IndexOf(delimiter);
        if (startIndex == -1) {
          return;
        }
        int endIndex = this.parsedTextForm.IndexOf(delimiter, startIndex + 1);
        if (endIndex == -1) {
          throw new ExpressionsException(ExpressionsException.Msg.EndOfLiteralCharNotFound,
                                         delimiter.ToString(), this.ToString());
        }
        int literalIndex = literals.Count;
        string literalValue = this.parsedTextForm.Substring(startIndex, endIndex - startIndex + 1);
        string literalTag = "§" + literalIndex.ToString("0000");
        literals.Add(literalTag, literalValue);
        this.parsedTextForm = this.parsedTextForm.Replace(literalValue, literalTag);
      } // while
    }

    private void SetFunctorReferences() {
      const string symbolStarters = " _ A B C D E F G H I J K L M N Ñ O P Q R S T U V W X Y Z Á É Í Ó Ú ";
      string temp = EmpiriaString.TrimAll(this.parsedTextForm);
      string[] tokens = temp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

      this.functorsTable = new Dictionary<string, Functor>();
      for (int i = 0; i < tokens.Length - 1; i++) {
        // if not starts with a symbolStarter, continue
        if (!symbolStarters.Contains(tokens[i][0].ToString())) {
          continue;
        }
        // Not a function because the next item isn't a left parenthesis, continue
        if (!Operator.IsLeftParenthesisSymbol(tokens[i + 1][0])) {
          continue;
        }
        //// Otherwise is a function, then get functor arity
        int functionArity = 0;
        int leftParenthesisCounter = 0;
        for (int j = i + 1; j < tokens.Length; j++) {
          if (Operator.IsLeftParenthesisSymbol(tokens[j][0])) {
            leftParenthesisCounter++;
          } else if (Operator.IsRightParenthesisSymbol(tokens[j][0])) {
            leftParenthesisCounter--;
          } else if (Operator.IsListItemDelimiter(tokens[j][0]) && (leftParenthesisCounter == 1)) {
            functionArity++;
          } else if (functionArity == 0) {  // Has somewhere into the arguments, then can't be nullary
            functionArity = 1;
          }
          if (leftParenthesisCounter == 0) {
            break;
          }
        } // for

        // Load functor into functors table and do functionName substitution
        string functionSymbolTag = "Ω" + this.functorsTable.Count.ToString("0000");
        string functionName = tokens[i];
        this.functorsTable.Add(functionSymbolTag, Functor.Parse(functionName, functionArity));
        tokens[i] = functionSymbolTag;
      } // for i
      this.parsedTextForm = String.Join(" ", tokens);
    }

    private void SetVariableReferences() {
      const string symbolStarters = " _ A B C D E F G H I J K L M N Ñ O P Q R S T U V W X Y Z Á É Í Ó Ú ";
      string temp = EmpiriaString.TrimAll(this.parsedTextForm);
      string[] tokens = temp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

      this.variablesState = new State<Variable>();
      for (int i = 0; i < tokens.Length; i++) {
        // if not starts with a symbolStarter, continue
        if (!symbolStarters.Contains(tokens[i][0].ToString())) {
          continue;
        }
        // If is function because the next item is a left parenthesis, continue
        if (i < (tokens.Length - 1)) {
          if (Operator.IsLeftParenthesisSymbol(tokens[i + 1][0])) {
            continue;
          }
        }
        // Otherwise 
        string variableSymbolTag = "ɸ" + tokens[i];
        string variableName = tokens[i];
        this.variablesState.Add(variableSymbolTag, Variable.Parse(variableName));
        for (int j = i; j < tokens.Length; j++) {
          if (tokens[j] == variableName) {
            tokens[j] = variableSymbolTag;
          }
        } // for j
      } // for i
      this.parsedTextForm = String.Join(" ", tokens);
    }

    #endregion Private methods

  }  // class Expression

} // namespace Empiria.Expressions