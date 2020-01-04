/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Expressions               *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : FunctionLibrary                                  Pattern  : Standard Class                    *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Defines a list of functions used in an evaluation context.                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using Empiria.Collections;

namespace Empiria.Expressions {

  /// <summary>Defines a list of functions used in an evaluation context.</summary>
  public class FunctionLibrary {

    #region Fields

    private EmpiriaDictionary<string, Function> collection = new EmpiriaDictionary<string, Function>();

    #endregion Fields

    #region Constructors and parsers

    public FunctionLibrary() {

    }

    #endregion Constructors and parsers

    #region Public methods

    public bool ContainsFunction(string functionName) {
      return collection.ContainsKey(functionName);
    }

    public IEnumerator<Function> GetEnumerator() {
      return collection.Values.GetEnumerator();
    }

    public Function GetFunction(string itemName) {
      Assertion.AssertObject(itemName, "itemName");

      return collection[itemName];
    }

    public Function[] GetFunctionsArray() {
      Function[] array = new Function[collection.Count];

      collection.Values.CopyTo(array, 0);

      return array;
    }

    public string[] GetNamesArray() {
      string[] array = new string[collection.Count];

      collection.Keys.CopyTo(array, 0);

      return array;
    }

    #endregion Public methods

    #region Internal methods

    internal void AddFunction(Function function) {
      collection.Insert(function.Name, function);
    }

    internal Operand Call(EvaluationContext context, Functor functor, Operand[] arguments) {
      // If it is a global function call it
      if (GlobalFunctions.Contains(functor.FunctionName)) {
        return Constant.Parse(GlobalFunctions.Execute(functor.FunctionName, Operand.ToObjectsArray(arguments)));
      }
      // If it is a library function continue, else throw an error
      if (!this.ContainsFunction(functor.FunctionName)) {
        throw new ExpressionsException(ExpressionsException.Msg.UnloadedFunction, functor.FunctionName);
      }

      // If it is a library function parse, compile and execute the function
      Function function = this.GetFunction(functor.FunctionName);

      if (!function.IsCompiled) {
        function.Compile();
      }
      object[] objectArguments = Operand.ToObjectsArray(arguments);

      return Constant.Parse(function.Execute(context, objectArguments));
    }

    internal void Clear() {
      collection.Clear();
    }

    #endregion Internal methods

  }  // class FunctionLibrary

} // namespace Empiria.Expressions
