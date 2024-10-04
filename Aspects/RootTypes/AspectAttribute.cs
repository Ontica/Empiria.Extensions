/* Empiria Core **********************************************************************************************
*                                                                                                            *
*  Module   : Empiria Aspects                            Component : Infrastructure provider                 *
*  Assembly : Empiria.Aspects.dll                        Pattern   : Attribute class                         *
*  Type     : AspectAttribute                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Abstract attribute used to mark a method to be be handled by a decoration aspect.              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

namespace Empiria.Aspects {

  /// <summary>Abstract attribute used to mark a method to be be handled by a decoration aspect.</summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  abstract public class AspectAttribute : Attribute {

    protected AspectAttribute() {

    }

  }  // class AspectAttribute

}  // namespace Empiria.Aspects
