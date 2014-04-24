/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                       System   : Expressions Runtime Library       *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : State                                            Pattern  : Standard Class                    *
*  Version   : 5.5        Date: 25/Jun/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Defines a list of variables with associated values.                                           *
*                                                                                                            *
********************************* Copyright (c) 2008-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using Empiria.Collections;

namespace Empiria.Expressions {

  /// <summary>Defines a list of variables names with associated values.</summary>
  public class State<T> : EmpiriaHashList<T> where T : Operand {

    #region Fields

    #endregion Fields

    #region Constructors and parsers

    public State()
      : base(true) {

    }

    static public State<T> Parse(string stateTextForm) {
      return new State<T>();
    }

    #endregion Constructors and parsers

    #region Public methods

    public T GetItem(string itemName) {
      return base[itemName];
    }

    public object GetValue(string itemName) {
      return base[itemName].GetObject();
    }

    #endregion Public methods

    #region Internal methods

    internal new void Add(string itemName, T item) {
      base.Add(itemName, item);
    }

    internal new void Clear() {
      base.Clear();
    }

    internal new bool ContainsKey(string itemName) {
      return base.ContainsKey(itemName);
    }

    internal string[] GetKeysArray() {
      string[] array = new string[base.Count];

      base.Keys.CopyTo(array, 0);

      return array;
    }

    internal T[] GetValuesArray() {
      T[] array = new T[base.Count];

      base.Values.CopyTo(array, 0);

      return array;
    }

    internal void SetValue(string itemName, object value) {
      base[itemName].SetValue(value);
    }

    #endregion Internal methods

  }  // class State

} // namespace Empiria.Expressions