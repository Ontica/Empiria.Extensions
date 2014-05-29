/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                       System   : Expressions Runtime Library       *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : Operator                                         Pattern  : Standard Class                    *
*  Version   : 5.5        Date: 25/Jun/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Defines an arithmetic, logical, string, char or datetime operator.                            *
*                                                                                                            *
********************************* Copyright (c) 2008-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

namespace Empiria.Expressions {

  public enum OperatorType {
    Arithmetical = 'A',
    Logical = 'L',
    Relational = 'R',
    Grouping = 'G',
  }

  /// <summary>Defines an arithmetic, logical, string or datetime operator.</summary>
  public class Operator : IExpressionToken {

    #region Fields

    private const string arithmeticalOperators = @" + - * / ÷ \ ^ Ω ";
    private const string logicalOperators = @" ¬ ∧ ∨ ≡ ≢ ⇒ ";   // ∀∃ 
    private const string relationalOperators = @" > < ≥ ≤ = ≠ ∈ ";    //∩∪⊂ ⊄ ⊆ ⊊ ∈ ∉ ∅
    private const string groupingOperators = @" ( [ { ) ] } , ; ";

    private OperatorType operatorType = OperatorType.Arithmetical;
    private char operatorChar = Char.MinValue;
    private uint operatorArity = 2;
    private int precedence = 99;

    #endregion Fields

    #region Constructors and parsers

    private Operator(char operatorChar) {
      this.operatorChar = operatorChar;

      if (this.operatorChar != '¬') {
        this.operatorArity = 2;
      } else {
        this.operatorArity = 1;
      }
      SetOperatorType();
      SetOperatorPrecedence();
    }

    protected Operator(char operatorChar, int operatorArity) {
      this.operatorChar = operatorChar;
      this.operatorArity = (uint) operatorArity;
      SetOperatorType();
      SetOperatorPrecedence();
    }

    static public Operator Parse(char operatorChar) {
      if (Operator.IsOperator(operatorChar)) {
        return new Operator(operatorChar);
      } else {
        throw new ExpressionsException(ExpressionsException.Msg.InvalidOperator, operatorChar.ToString());
      }
    }

    static public bool IsOperator(char tryOperatorChar) {
      if (arithmeticalOperators.Contains(tryOperatorChar.ToString())) {
        return true;
      } else if (logicalOperators.Contains(tryOperatorChar.ToString())) {
        return true;
      } else if (relationalOperators.Contains(tryOperatorChar.ToString())) {
        return true;
      } else if (groupingOperators.Contains(tryOperatorChar.ToString())) {
        return true;
      } else {
        return false;
      }
    }

    #endregion Constructors and parsers

    #region Public properties

    public int Arity {
      get { return (int) this.operatorArity; }
    }

    public bool IsLeftParenthesis {
      get { return this.operatorChar == '('; }
    }

    public bool IsRightParenthesis {
      get { return this.operatorChar == ')'; }
    }

    public OperatorType OperatorType {
      get { return this.operatorType; }
    }

    public int Precedence {
      get { return this.precedence; }
    }

    public char Symbol {
      get { return this.operatorChar; }
    }

    #endregion Public properties

    #region Public methods

    public override string ToString() {
      return this.operatorChar.ToString();
    }

    #endregion Public methods

    #region Internal methods

    internal virtual Operand Apply(Operand[] operandsArray) {
      if (operandsArray.Length != this.Arity) {
        throw new ExpressionsException(ExpressionsException.Msg.InvalidOperatorArgumentsNumber, this.Symbol,
                                       this.Arity, operandsArray.Length);
      }

      if (this.Arity == 2) {
        Operand operandA = operandsArray[1];
        Operand operandB = operandsArray[0];
        switch (this.operatorType) {
          case OperatorType.Arithmetical:
            return ApplyArithmetical(operandA, operandB);
          case OperatorType.Logical:
            return ApplyLogical(operandA, operandB);
          case OperatorType.Relational:
            return ApplyRelational(operandA, operandB);
          default:
            throw new ExpressionsException(ExpressionsException.Msg.UnrecognizedOperand, this.Symbol.ToString());
        }
      } else if (this.Arity == 1) {
        return this.ApplyUnary(operandsArray[0]);
      } else {
        throw new ExpressionsException(ExpressionsException.Msg.UndefinedOperatorArity, this.Symbol, this.Arity);
      }
    }

    static internal bool IsLeftParenthesisSymbol(char symbol) {
      return (symbol == '(');
    }

    static internal bool IsListItemDelimiter(char symbol) {
      return (symbol == ',' || symbol == ';');
    }

    static internal bool IsRightParenthesisSymbol(char symbol) {
      return (symbol == ')');
    }

    static internal string ParseOperators(string expression) {

      // Replace all begin parenthetical characters with ' ( '
      expression = expression.Replace("(", " ( ");
      expression = expression.Replace("[", " ( ");
      expression = expression.Replace("{", " ( ");

      // Replace all end parenthetical characters with ' ) '
      expression = expression.Replace(")", " ) ");
      expression = expression.Replace("]", " ) ");
      expression = expression.Replace("}", " ) ");

      // Replace all list separators with ' , '
      expression = expression.Replace(",", " , ");
      expression = expression.Replace(";", " , ");

      // Replace all grammar operators with their correspondent internal symbols
      expression = expression.Replace("&&", "∧");      // Logical AND or conjuntion
      expression = expression.Replace("||", "∨");      // Logical OR  or disjunction
      expression = expression.Replace("<=", "≤");       // Less or equal than
      expression = expression.Replace("=<", "≤");       // Less or equal than
      expression = expression.Replace(">=", "≥");       // Higher or equal than
      expression = expression.Replace("=>", "≥");       // Higher or equal than
      expression = expression.Replace("==", "=");       // Equal
      expression = expression.Replace("<>", "≠");       // Not equal
      expression = expression.Replace("!=", "≠");       // Not equal
      expression = expression.Replace("->", "⇒");      // Implication

      expression = expression.Replace("&", "∧");       // Logical AND or conjuntion
      expression = expression.Replace("|", "∨");       // Logical OR or disjunction
      expression = expression.Replace("!", "¬");        // Logical NOT

      // Replace all grammar textual operators with their correspondent internal symbols

      expression = expression.Replace(" MOD ", " ÷ ");   // Modulus (x Mod y. e.g, 13 Mod 3 = 1)
      expression = expression.Replace(" AND ", " ∧ ");
      expression = expression.Replace(" OR ", " ∨ ");
      expression = expression.Replace(" XOR ", " ≢ ");  // Logical XOR or not equivalent
      expression = expression.Replace(" NOT ", " ¬ ");
      expression = expression.StartsWith("NOT ") ? "¬" + expression.Remove(0, 3) : expression;
      expression = expression.Replace(" IN ", " ∈ ");

      // Suppress all innecessary unary plus
      expression = expression.StartsWith("+") ? expression.Remove(0, 1) : expression;
      expression = expression.Replace("( +", "( ");
      expression = expression.Replace(", +", ", ");
      // Replace all unary minus with their correspondent arithmetical value -x == (0 - x)
      expression = expression.StartsWith("-") ? "0 - " + expression.Remove(0, 1) : expression;
      expression = expression.Replace("( -", "( 0 - ");
      expression = expression.Replace(", -", ", 0 - ");

      // Adds to all parenthetical products the multiplication symbol '*'
      expression = expression.Replace(") (", ") * (");

      // Sets all operators symbols between spaces
      const string standardOperators = @" ¬ ^ * / \ ÷ + - > < ≥ ≤ = ≠ , ; ";
      for (int i = 0; i < standardOperators.Length; i++) {
        if (standardOperators[i] != ' ') {
          expression = expression.Replace(standardOperators[i].ToString(), " " + standardOperators[i] + " ");
        }
      }
      const string additionalOperators = @" ∈ ∧ ∨ ≡ ≢ ⇒ ";   // Operators with symbol in no standard Unicode char
      for (int i = 0; i < additionalOperators.Length; i++) {
        if (additionalOperators[i] != ' ') {
          expression = expression.Replace(additionalOperators[i].ToString(), " " + additionalOperators[i] + " ");
        }
      }
      expression = EmpiriaString.TrimAll(expression);
      for (int i = 0; i < standardOperators.Length; i++) {
        // Suppress all innecesary unary plus symbols
        expression = expression.Replace(standardOperators[i].ToString() + " +", standardOperators[i].ToString());
        // Suppress all unary minus with their correspondent arithmetical value -x == (0 - x)
        expression = expression.Replace(standardOperators[i].ToString() + " -", standardOperators[i].ToString() + " 0 - ");
        //expression = expression.Replace(standardOperators[i].ToString() + " -", standardOperators[i].ToString() + " (0 - 1) * ");
      }

      return EmpiriaString.TrimAll(expression);
    }

    #endregion Internal methods

    #region Private methods

    private Operand ApplyArithmetical(Operand operandA, Operand operandB) {
      if (this.Symbol == '+') {
        if (operandA.GetType() == typeof(string) || operandB.GetType() == typeof(string)) {
          return Constant.Parse((string) (operandA.GetString() + operandB.GetString()));
        }
      }
      if (!operandA.IsNumeric) {
        throw new ExpressionsException(ExpressionsException.Msg.InvalidOperandDataType,
                                       this.Symbol.ToString(), operandA.ToString(), operandA.GetType());
      }
      if (!operandB.IsNumeric) {
        throw new ExpressionsException(ExpressionsException.Msg.InvalidOperandDataType,
                                       this.Symbol.ToString(), operandB.ToString(), operandB.GetType());
      }
      switch (this.Symbol) {
        case '+':
          return Constant.Parse(operandA.GetDecimal() + operandB.GetDecimal());
        case '-':   // Substraction
          return Constant.Parse(operandA.GetDecimal() - operandB.GetDecimal());
        case '*':   // Multiplication
          return Constant.Parse(operandA.GetDecimal() * operandB.GetDecimal());
        case '/':   // Division
          if (operandB.GetDecimal() == 0) {
            throw new ExpressionsException(ExpressionsException.Msg.ZeroDivision);
          }
          return Constant.Parse(operandA.GetDecimal() / operandB.GetDecimal());
        case '\\':  // Integral Division
          if (operandB.GetDecimal() == 0) {
            throw new ExpressionsException(ExpressionsException.Msg.ZeroDivision);
          }
          return Constant.Parse((int) (operandA.GetDecimal() / operandB.GetDecimal()));
        case '÷':   // Module (division reminder)
          if (operandB.GetDecimal() == 0) {
            throw new ExpressionsException(ExpressionsException.Msg.ZeroDivision);
          }
          return Constant.Parse((int) (operandA.GetDecimal() % operandB.GetDecimal()));
        case '^':   // POWER(x,y) = x*x*...*x, y times
          return Constant.Parse(Convert.ToDecimal(Math.Pow(Convert.ToDouble(operandA.GetDecimal()),
                                                          Convert.ToDouble(operandB.GetDecimal()))));
        default:
          throw new ExpressionsException(ExpressionsException.Msg.UndefinedOperatorMethod, this.Symbol.ToString());
      }
    }

    private Operand ApplyLogical(Operand operandA, Operand operandB) {
      if (operandA.GetType() != typeof(bool)) {
        throw new ExpressionsException(ExpressionsException.Msg.InvalidOperandDataType,
                                       this.Symbol.ToString(), operandA.ToString(), operandA.GetType());
      } else if (operandB.GetType() != typeof(bool)) {
        throw new ExpressionsException(ExpressionsException.Msg.InvalidOperandDataType,
                                       this.Symbol.ToString(), operandB.ToString(), operandB.GetType());
      }
      switch (this.Symbol) {
        case '∧':    // AND
          return Constant.Parse(operandA.GetBool() && operandB.GetBool());
        case '∨':    // OR
          return Constant.Parse(operandA.GetBool() || operandB.GetBool());
        case '≢':     // XOR = Exclusive OR
          return Constant.Parse(operandA.GetBool() != operandB.GetBool());
        case '≡':     // Logical equality
          return Constant.Parse(operandA.GetBool() == operandB.GetBool());
        case '⇒':    // Implication
          return Constant.Parse((operandA.GetBool() == true) && (operandB.GetBool() == false) ? false : true);
        default:
          throw new ExpressionsException(ExpressionsException.Msg.UndefinedOperatorMethod, this.Symbol.ToString());
      }
    }

    private Operand ApplyRelationalLessThan(Operand operandA, Operand operandB) {
      if (operandA.IsNumeric && operandB.IsNumeric) {
        return Constant.Parse(operandA.GetDecimal() < operandB.GetDecimal());
      } else if (operandA.IsString && operandB.IsString) {
        return Constant.Parse(operandA.GetString().CompareTo(operandB.ToString()) < 0);
      } else if (operandA.IsDateTime && operandB.IsDateTime) {
        return Constant.Parse(operandA.GetDateTime() < operandB.GetDateTime());
      } else {
        throw new ExpressionsException(ExpressionsException.Msg.CantCompareOperands,
                                       "<", operandA.ToString(), operandA.GetType(),
                                       operandB.ToString(), operandB.GetType());
      }
    }

    private Operand ApplyRelationalLessOrEqualThan(Operand operandA, Operand operandB) {
      if (operandA.IsNumeric && operandB.IsNumeric) {
        return Constant.Parse(operandA.GetDecimal() <= operandB.GetDecimal());
      } else if (operandA.IsString && operandB.IsString) {
        return Constant.Parse(operandA.GetString().CompareTo(operandB.ToString()) <= 0);
      } else if (operandA.IsDateTime && operandB.IsDateTime) {
        return Constant.Parse(operandA.GetDateTime() <= operandB.GetDateTime());
      } else if (operandA.IsBoolean && operandB.IsBoolean) {
        return Constant.Parse(operandA.GetBool() == operandB.GetBool());
      } else {
        throw new ExpressionsException(ExpressionsException.Msg.CantCompareOperands,
                                       "≤", operandA.ToString(), operandA.GetType(),
                                       operandB.ToString(), operandB.GetType());
      }
    }

    private Operand ApplyRelationalGreaterThan(Operand operandA, Operand operandB) {
      if (operandA.IsNumeric && operandB.IsNumeric) {
        return Constant.Parse(operandA.GetDecimal() > operandB.GetDecimal());
      } else if (operandA.IsString && operandB.IsString) {
        return Constant.Parse(operandA.GetString().CompareTo(operandB.ToString()) > 0);
      } else if (operandA.IsDateTime && operandB.IsDateTime) {
        return Constant.Parse(operandA.GetDateTime() > operandB.GetDateTime());
      } else {
        throw new ExpressionsException(ExpressionsException.Msg.CantCompareOperands,
                                       ">", operandA.ToString(), operandA.GetType(),
                                       operandB.ToString(), operandB.GetType());
      }
    }

    private Operand ApplyRelationalGreaterOrEqualThan(Operand operandA, Operand operandB) {
      if (operandA.IsNumeric && operandB.IsNumeric) {
        return Constant.Parse(operandA.GetDecimal() >= operandB.GetDecimal());
      } else if (operandA.IsString && operandB.IsString) {
        return Constant.Parse(operandA.GetString().CompareTo(operandB.ToString()) >= 0);
      } else if (operandA.IsDateTime && operandB.IsDateTime) {
        return Constant.Parse(operandA.GetDateTime() >= operandB.GetDateTime());
      } else if (operandA.IsBoolean && operandB.IsBoolean) {
        return Constant.Parse(operandA.GetBool() == operandB.GetBool());
      } else {
        throw new ExpressionsException(ExpressionsException.Msg.CantCompareOperands,
                                       "≥", operandA.ToString(), operandA.GetType(),
                                       operandB.ToString(), operandB.GetType());
      }
    }

    private Operand ApplyRelationalEqual(Operand operandA, Operand operandB) {
      if (operandA.IsNumeric && operandB.IsNumeric) {
        return Constant.Parse(operandA.GetDecimal() == operandB.GetDecimal());
      } else if (operandA.IsString && operandB.IsString) {
        return Constant.Parse(operandA.GetString() == operandB.GetString());
      } else if (operandA.IsDateTime && operandB.IsDateTime) {
        return Constant.Parse(operandA.GetDateTime() == operandB.GetDateTime());
      } else if (operandA.IsBoolean && operandB.IsBoolean) {
        return Constant.Parse(operandA.GetBool() == operandB.GetBool());
      } else {
        throw new ExpressionsException(ExpressionsException.Msg.CantCompareOperands,
                                       "=", operandA.ToString(), operandA.GetType(),
                                       operandB.ToString(), operandB.GetType());
      }
    }


    private Operand ApplyRelationalDistinct(Operand operandA, Operand operandB) {
      if (operandA.IsNumeric && operandB.IsNumeric) {
        return Constant.Parse(operandA.GetDecimal() != operandB.GetDecimal());
      } else if (operandA.IsString && operandB.IsString) {
        return Constant.Parse(operandA.GetString() != operandB.GetString());
      } else if (operandA.IsDateTime && operandB.IsDateTime) {
        return Constant.Parse(operandA.GetDateTime() != operandB.GetDateTime());
      } else if (operandA.IsBoolean && operandB.IsBoolean) {
        return Constant.Parse(operandA.GetBool() != operandB.GetBool());
      } else {
        throw new ExpressionsException(ExpressionsException.Msg.CantCompareOperands,
                                       "≠", operandA.ToString(), operandA.GetType(),
                                       operandB.ToString(), operandB.GetType());
      }
    }

    private Operand ApplyRelational(Operand operandA, Operand operandB) {
      switch (this.Symbol) {
        case '<':
          return ApplyRelationalLessThan(operandA, operandB);
        case '≤':
          return ApplyRelationalLessOrEqualThan(operandA, operandB);
        case '>':
          return ApplyRelationalGreaterThan(operandA, operandB);
        case '≥':
          return ApplyRelationalGreaterOrEqualThan(operandA, operandB);
        case '=':
          return ApplyRelationalEqual(operandA, operandB);
        case '≠':
          return ApplyRelationalDistinct(operandA, operandB);
        default:
          throw new ExpressionsException(ExpressionsException.Msg.UndefinedOperatorMethod, this.Symbol.ToString());
      }
    }

    private Operand ApplyUnary(Operand operand) {
      switch (this.Symbol) {
        case '¬':
          return Constant.Parse(!operand.GetBool());
        default:
          throw new ExpressionsException(ExpressionsException.Msg.UndefinedOperatorMethod, this.Symbol.ToString());
      }
    }

    private void SetOperatorPrecedence() {
      // if i < j, operators in index j are evaluated first than each on index i in precedenceTable.
      string[] precedenceTable = new string[] { " ( ",          // Evaluates later
                                                ", ; ",
                                                " ≡ ≢ ",
                                                " ⇒ ",
                                                " ∨ ",         // Evaluates last
                                                " ∧ ",
                                                " = ≠ ",
                                                " > < ≥ ≤ ∈ ",
                                                " + - ",
                                                @" * / ÷ \ ",
                                                " ^ ",          // Evaluates first
                                                " ¬ ",
                                                " ) ",          // Evaluates inmediatly
                                                " Ω ",
                                                };

      for (int i = 0; i < precedenceTable.Length; i++) {
        if (precedenceTable[i].Contains(this.operatorChar.ToString())) {
          this.precedence = i;
          return;
        }
      } // for
      throw new ExpressionsException(ExpressionsException.Msg.UndefinedOperatorPrecedence, this.operatorChar.ToString());
    }

    private void SetOperatorType() {
      if (arithmeticalOperators.Contains(this.operatorChar.ToString())) {
        this.operatorType = OperatorType.Arithmetical;
      } else if (logicalOperators.Contains(this.operatorChar.ToString())) {
        this.operatorType = OperatorType.Logical;
      } else if (relationalOperators.Contains(this.operatorChar.ToString())) {
        this.operatorType = OperatorType.Relational;
      } else if (groupingOperators.Contains(this.operatorChar.ToString())) {
        this.operatorType = OperatorType.Grouping;
      } else {
        throw new ExpressionsException(ExpressionsException.Msg.InvalidOperator, this.operatorChar.ToString());
      }
    }

    #endregion Private methods

  }  // class Operator

} // namespace Empiria.Expressions
