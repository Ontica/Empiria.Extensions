/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Services Framework                         Component : External Services Providers             *
*  Assembly : Empiria.Services.dll                       Pattern   : Providers Factory                       *
*  Type     : WorkflowProviders                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Factory that provides object instances used to access workflow services.                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Reflection;

namespace Empiria.Services.Providers {

  /// <summary>Factory that provides object instances used to access workflow services.</summary>
  static internal class WorkflowProviders {

    static internal IWorkItemProvider WorkItemProvider() {
      Type type = ObjectFactory.GetType("Empiria.Workflow",
                                        "Empiria.Workflow.Services.WorkflowStepsProvider");

      return (IWorkItemProvider) ObjectFactory.CreateObject(type);
    }

  }  // class WorkflowProviders

}  // namespace Empiria.Services.Providers
