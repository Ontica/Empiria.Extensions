/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Services Framework                         Component : External Services Providers             *
*  Assembly : Empiria.Services.dll                       Pattern   : Dependency inversion interface          *
*  Type     : IWorkItemProvider                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Interface used to integrate Empiria.Services with workflow systems using work items.           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Services.Providers {

  /// <summary>Interface used to integrate Empiria.Services with workflow systems using work items.</summary>
  public interface IWorkItemProvider {

    void SendEvent(IWorkItemEvent workItemEvent);

  }  // interface IWorkItemProvider

}  // namespace Empiria.Services.Providers
