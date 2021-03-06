﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Expressions               *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : Operand                                          Pattern  : Standard Class                    *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Defines an arithmetic, logical, string, char or datetime operand.                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Expressions {

  /// <summary>Defines an arithmetic, logical or string operand.</summary>
  public abstract class Operand : IExpressionToken {

    #region Fields

    private object value = null;

    #endregion Fields

    #region Constructors and parsers

    protected Operand(object value) {
      this.value = value;
    }

    static public object[] ToObjectsArray(Operand[] operandsArray) {
      object[] objectsArray = new object[operandsArray.Length];

      for (int i = 0; i < objectsArray.Length; i++) {
        objectsArray[i] = operandsArray[objectsArray.Length - i - 1].GetObject();
      }

      return objectsArray;
    }

    #endregion Constructors and parsers

    #region Public properties

    public bool IsBoolean {
      get { return (this.GetType() == typeof(bool)); }
    }

    public bool IsNull {
      get { return (value == null); }
    }

    public bool IsNumeric {
      get {
        Type type = this.GetType();
        if (type == typeof(decimal) || type == typeof(int) || type == typeof(float)) {
          return true;
        } else {
          return false;
        }
      }
    }

    public bool IsString {
      get { return (this.GetType() == typeof(string)); }
    }

    public bool IsDateTime {
      get { return (this.GetType() == typeof(DateTime)); }
    }

    #endregion Public properties

    #region Public methods

    public bool GetBool() {
      return Convert.ToBoolean(this.value);
    }

    public DateTime GetDateTime() {

      return Convert.ToDateTime(this.value);
    }

    public decimal GetDecimal() {
      return Convert.ToDecimal(this.value);
    }

    public float GetFloat() {
      return Convert.ToSingle(this.value);
    }

    public int GetInteger() {
      return Convert.ToInt32(this.value);
    }

    public object GetObject() {
      return this.value;
    }

    public string GetString() {
      return Convert.ToString(this.value);
    }

    public new Type GetType() {
      return this.value.GetType();
    }

    #endregion Public methods

    #region Internal methods

    internal void SetValue(object newValue) {
      this.value = newValue;
    }

    #endregion Internal methods

  }  // class Operand

} // namespace Empiria.Expressions
