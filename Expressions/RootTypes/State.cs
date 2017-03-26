/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Expressions               *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : State<T>                                         Pattern  : Standard Class                    *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Defines a list of variables with associated values.                                           *
*                                                                                                            *
********************************* Copyright (c) 2008-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;

using Empiria.Collections;

namespace Empiria.Expressions {

  /// <summary>Defines a list of variables names with associated values.</summary>
  public class State<T> where T : Operand {

    #region Fields

    private EmpiriaDictionary<string, T> collection = new EmpiriaDictionary<string, T>();

    #endregion Fields

    #region Constructors and parsers

    public State() {

    }

    static public State<T> Parse(string stateTextForm) {
      return new State<T>();
    }

    #endregion Constructors and parsers

    #region Public methods

    public T GetItem(string itemName) {
      return collection[itemName];
    }

    public object GetValue(string itemName) {
      return collection[itemName].GetObject();
    }

    #endregion Public methods

    #region Internal methods

    internal void Add(string itemName, T item) {
      collection.Insert(itemName, item);
    }

    internal void Clear() {
      collection.Clear();
    }

    internal bool ContainsKey(string itemName) {
      return collection.ContainsKey(itemName);
    }

    public IEnumerator<T> GetEnumerator() {
      return collection.Values.GetEnumerator();
    }

    internal string[] GetKeysArray() {
      string[] array = new string[collection.Count];

      collection.Keys.CopyTo(array, 0);

      return array;
    }

    internal T[] GetValuesArray() {
      T[] array = new T[collection.Count];

      collection.Values.CopyTo(array, 0);

      return array;
    }

    internal void SetValue(string itemName, object value) {
      collection[itemName].SetValue(value);
    }

    #endregion Internal methods

  }  // class State

} // namespace Empiria.Expressions
