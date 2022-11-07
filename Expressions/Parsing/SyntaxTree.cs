/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Expressions                        Component : Domain Layer                            *
*  Assembly : Empiria.Extensions.dll                     Pattern   : Service provider                        *
*  Type     : SyntaxTree                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : An abstract syntax tree data structure (AST).                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Expressions {

  /// <summary>An abstract syntax tree data structure (AST).</summary>
  internal class SyntaxTree {

    public int Count {
      get;
      private set;
    }

  }  // class SyntaxTree

}  // namespace Empiria.Expressions
