/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Expressions Parsing                     *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Methods library                         *
*  Type     : CommonMethods                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Static library with common methods.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Expressions {

  /// <summary>Static library with common methods.</summary>
  static internal class CommonMethods {

    static internal string[] ConvertToArray(string expression) {
      var temp = EmpiriaString.TrimAll(expression);

      return temp.Split(' ');
    }

  }  // class CommonMethods

}  // namespace Empiria.Expressions
