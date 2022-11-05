/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Information Holder                      *
*  Type     : FunctionDefinition                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Defines the type of executable function.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

namespace Empiria.Expressions {


  internal enum FunctionToken {

    SUM,

  }


  /// <summary>Defines the type of executable function.</summary>
  internal class FunctionDefinition {

    #region Constructors and parsers

    internal FunctionDefinition(FunctionToken token) {
      Token = token;
    }


    static internal FunctionDefinition Parse(FunctionToken token) {
      return new FunctionDefinition(token);
    }


    #endregion Constructors and parsers

    #region Properties

    internal FunctionToken Token {
      get; private set;
    }

    #endregion Properties

  }  // class FunctionDefinition

}  // namespace Empiria.Expressions
