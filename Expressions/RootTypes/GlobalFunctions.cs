/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                       System   : Expressions Runtime Library       *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : GlobalFunctions                                  Pattern  : Standard Class                    *
*  Version   : 5.5        Date: 25/Jun/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Contains general methods on arithmetical and logical functions.                               *
*                                                                                                            *
********************************* Copyright (c) 2009-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

namespace Empiria.Expressions {

  /// <summary>Contains general methods on arithmetical, date, string, and logical functions.</summary>
  static public class GlobalFunctions {

    #region Fields

    #endregion Fields

    #region Static public methods

    static public bool Contains(string functionName) {
      return (functionName == "SI" || functionName == "IF" || functionName == "MAX" || functionName == "MIN" ||
              functionName == "ABS" || functionName == "SQRT" || functionName == "RAIZ");
    }

    static public object Execute(string functionName, object[] parameters) {
      switch (functionName) {
        case "SI":
        case "IF":
          return IF((bool) parameters[0], parameters[1], parameters[2]);
        case "MAX":
          return MAX((decimal) parameters[0], (decimal) parameters[1]);
        case "MIN":
          return MIN((decimal) parameters[0], (decimal) parameters[1]);
        case "ABS":
          return ABS((decimal) parameters[0]);
        case "SQRT":
        case "RAIZ":
          return SQRT((decimal) parameters[0]);
        default:
          throw new ExpressionsException(ExpressionsException.Msg.UndefinedGlobalFunction, functionName);
      }
    }

    static private decimal ABS(decimal value) {
      return Math.Abs(value);
    }

    static private object IF(bool predicate, object trueReturnValue, object falseReturnValue) {
      if (predicate) {
        return trueReturnValue;
      } else {
        return falseReturnValue;
      }
    }

    static private decimal MAX(decimal value1, decimal value2) {
      if (value1 < value2) {
        return value2;
      } else {
        return value1;
      }
    }

    static private decimal MIN(decimal value1, decimal value2) {
      if (value1 < value2) {
        return value1;
      } else {
        return value2;
      }
    }

    static private decimal SQRT(decimal value1) {
      return Convert.ToDecimal(Math.Sqrt((double) value1));
    }

    static private Constant CountDaysBetween(Operand[] operandsArray) {
      DateTime fromDate = operandsArray[0].GetDateTime();
      DateTime toDate = operandsArray[1].GetDateTime();

      TimeSpan timeSpan = toDate.Subtract(fromDate);

      return Constant.Parse(timeSpan.Days);
    }

    static private Constant CountYearsBetween(Operand[] operandsArray) {
      DateTime fromDate = operandsArray[0].GetDateTime();
      DateTime toDate = operandsArray[1].GetDateTime();

      int years = (toDate.Year - fromDate.Year);
      if ((toDate.DayOfYear < fromDate.DayOfYear) && (years > 0)) {
        years = years - 1;
      }
      return Constant.Parse(years);
    }

    static private Constant Mean(Operand[] operandsArray) {
      return Constant.Parse(Sum(operandsArray).GetDecimal() / (decimal) operandsArray.Length);
    }

    static private Constant Sum(Operand[] operandsArray) {
      decimal result = 0m;
      for (int i = 0; i < operandsArray.Length; i++) {
        result += operandsArray[i].GetDecimal();
      }
      return Constant.Parse(result);
    }

    #endregion Static public methods

  }  // class GlobalFunctions

} // namespace Empiria.Expressions