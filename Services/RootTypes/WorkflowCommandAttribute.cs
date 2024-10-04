/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Services Framework                         Component : Infrastructure provider                 *
*  Assembly : Empiria.Services.dll                       Pattern   : Aspect Attribute type                   *
*  Type     : WorkflowCommandAttribute                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Marks a command method that can be handled by a workflow engine.                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

using Empiria.Aspects;

namespace Empiria.Services {

  /// <summary>Marks a command method that can be handled by a workflow engine.</summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  public class WorkflowCommandAttribute : AspectAttribute {

    public WorkflowCommandAttribute() {

    }

  }  // class WorkflowCommandAttribute

}  // namespace Empiria.Services
