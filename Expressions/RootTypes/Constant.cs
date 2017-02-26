/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Expressions               *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : Constant                                         Pattern  : Standard Class                    *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Defines an string, numerical, boolean or datetime constant.                                   *
*                                                                                                            *
********************************* Copyright (c) 2008-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

namespace Empiria.Expressions {

  /// <summary>Defines an string, numerical, boolean or datetime constant.</summary>
  public class Constant : Operand {

    #region Fields

    static internal readonly string[] ReservedKeywords = new string[] {
                                                                  "true", "verdadero",
                                                                  "false", "falso",
                                                                  "hoy", "today",
                                                                  "PI"
                                                                  };

    #endregion Fields

    #region Constructors and parsers

    private Constant(object value)
      : base(value) {

    }

    static public Constant Parse(bool value) {
      return new Constant(value);
    }

    static public Constant Parse(string value) {
      return new Constant(Constant.ParseString(value));
    }

    static public Constant Parse(decimal value) {
      return new Constant(value);
    }

    static public Constant Parse(DateTime value) {
      return new Constant(value);
    }

    static public Constant Parse(object value) {
      if (value.GetType() == typeof(bool)) {
        return Parse((bool) value);
      } else if (value.GetType() == typeof(decimal)) {
        return Parse((decimal) value);
      } else if (value.GetType() == typeof(int)) {
        return Parse(Convert.ToDecimal(value));
      } else if (value.GetType() == typeof(DateTime)) {
        return Parse((DateTime) value);
      } else if (value.GetType() == typeof(string)) {
        return Parse((string) value);
      } else {
        return Parse(Convert.ToString(value));
      }
    }

    #endregion Constructors and parsers

    #region Private methods

    static private object ParseString(string textValue) {
      DateTime dateTimeResult = new DateTime();
      Boolean boolResult = new Boolean();
      char delimiter = char.MinValue;

      if (String.IsNullOrEmpty(textValue)) {
        return String.Empty;
      }
      if (textValue.Length > 1) {
        if (textValue.EndsWith("i", StringComparison.InvariantCultureIgnoreCase) &&
            EmpiriaString.IsQuantity(textValue.Substring(0, textValue.Length - 1))) {
          return Convert.ToInt32(textValue.Substring(0, textValue.Length - 1));
        } else if (textValue.EndsWith("f", StringComparison.InvariantCultureIgnoreCase) &&
                   EmpiriaString.IsQuantity(textValue.Substring(0, textValue.Length - 1))) {
          return Convert.ToSingle(textValue.Substring(0, textValue.Length - 1));
        } else if (textValue.EndsWith("m", StringComparison.InvariantCultureIgnoreCase) &&
                   EmpiriaString.IsQuantity(textValue.Substring(0, textValue.Length - 1))) {
          return Convert.ToDecimal(textValue.Substring(0, textValue.Length - 1));
        } else if (textValue.StartsWith("$") && EmpiriaString.IsQuantity(textValue.Substring(1))) {
          return Convert.ToDecimal(textValue.Substring(1, textValue.Length - 1));
        } else if (textValue.EndsWith("%") &&
                   EmpiriaString.IsQuantity(textValue.Substring(0, textValue.Length - 1))) {
          return Convert.ToDecimal(textValue.Substring(0, textValue.Length - 1)) / 100m;
        }
      }

      if (EmpiriaString.IsQuantity(textValue)) {
        return Convert.ToDecimal(textValue);
      }
      if (textValue.StartsWith("'") && textValue.EndsWith("'")) {
        delimiter = '\'';
        textValue = textValue.Trim('\'');
      } else if (textValue.StartsWith("#") && textValue.EndsWith("#")) {
        delimiter = '#';
        textValue = textValue.Trim('#');
      } else if (textValue.StartsWith("\"") && textValue.EndsWith("\"")) {
        delimiter = '\\';
        textValue = textValue.Trim('\"');
      }

      if (DateTime.TryParse(textValue, out dateTimeResult)) {
        return dateTimeResult;
      } else if (Boolean.TryParse(textValue, out boolResult)) {
        return boolResult;
      } else if (textValue.ToLowerInvariant() == "true" || textValue.ToLowerInvariant() == "verdadero") {
        return true;
      } else if (textValue.ToLowerInvariant() == "false" || textValue.ToLowerInvariant() == "falso") {
        return false;
      } else if (textValue.ToLowerInvariant() == "pi") {
        return Convert.ToDecimal(Math.PI);
      } else if (textValue.ToLowerInvariant() == "hoy" || textValue.ToLowerInvariant() == "today") {
        return DateTime.Today;
      } else if (delimiter == '#' && textValue.ToLowerInvariant() == "e") {
        return Convert.ToDecimal(Math.E);
      } else {
        return Convert.ToString(textValue);
      }
    }

    #endregion Private methods

  }  // class Literal

} // namespace Empiria.Expressions
