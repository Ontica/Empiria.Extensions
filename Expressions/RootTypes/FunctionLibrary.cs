/* Empiria® Extended Framework 2013 **************************************************************************
*                                                                                                            *
*  Solution  : Empiria® Extended Framework                      System   : Expressions Runtime Library       *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : FunctionLibrary                                  Pattern  : Standard Class                    *
*  Date      : 25/Jun/2013                                      Version  : 5.1     License: CC BY-NC-SA 3.0  *
*                                                                                                            *
*  Summary   : Defines a list of functions used in an evaluation context.                                    *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2013. **/

using Empiria.Collections;

namespace Empiria.Expressions {

  /// <summary>Defines a list of functions used in an evaluation context.</summary>
  public class FunctionLibrary : EmpiriaHashList<Function> {

    #region Constructors and parsers

    public FunctionLibrary()
      : base(true) {

    }

    #endregion Constructors and parsers

    #region Public methods

    public bool ContainsFunction(string functionName) {
      return base.ContainsKey(functionName);
    }

    public Function GetFunction(string itemName) {
      return base[itemName];
    }

    public Function[] GetFunctionsArray() {
      Function[] array = new Function[base.Count];

      base.Values.CopyTo(array, 0);

      return array;
    }

    public string[] GetNamesArray() {
      string[] array = new string[base.Count];

      base.Keys.CopyTo(array, 0);

      return array;
    }

    #endregion Public methods

    #region Internal methods

    internal void AddFunction(Function function) {
      base.Add(function.Name, function);
    }

    internal Operand Call(EvaluationContext context, Functor functor, Operand[] arguments) {
      // If is  a global function call it
      if (GlobalFunctions.Contains(functor.FunctionName)) {
        return Constant.Parse(GlobalFunctions.Execute(functor.FunctionName, Operand.ToObjectsArray(arguments)));
      }
      // If is a library function continue, else throw error
      if (!this.ContainsFunction(functor.FunctionName)) {
        throw new ExpressionsException(ExpressionsException.Msg.UnloadedFunction, functor.FunctionName);
      }

      // If is a library function parse, compile and execute the function
      Function function = this.GetFunction(functor.FunctionName);

      if (!function.IsCompiled) {
        function.Compile();
      }
      object[] objectArguments = Operand.ToObjectsArray(arguments);

      return Constant.Parse(function.Execute(context, objectArguments));
    }

    internal new void Clear() {
      base.Clear();
    }

    #endregion Internal methods

  }  // class FunctionLibrary

} // namespace Empiria.Expressions