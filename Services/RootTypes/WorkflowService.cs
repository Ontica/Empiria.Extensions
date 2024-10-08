/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Services Framework                         Component : Infrastructure provider                 *
*  Assembly : Empiria.Services.dll                       Pattern   : Abstract type                           *
*  Type     : Service                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Abstract type that provides an infrastructure to build workflow-enabled services,              *
*             running on a secured and transactionable infrastructure.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Reflection;

using Empiria.Services.Aspects;
using Empiria.Services.Providers;

namespace Empiria.Services {

  /// <summary>Abstract type that provides an infrastructure to build workflow-enabled services,
  /// running on a secured and transactionable infrastructure.</summary>
  abstract public class WorkflowService : Service {

    #region Constructors and parsers

    protected WorkflowService() {
      // no-op
    }

    /// <summary>Returns a workflow-enabled service instance.</summary>
    public static new T CreateInstance<T>() where T: WorkflowService {
      var service = ObjectFactory.CreateObject<T>();

      service = WorkflowAspect.Decorate(service);
      service = LogAspect.Decorate(service);

      return service;
    }

    #endregion Constructors and parsers

    #region Methods

    protected void SendWorkflowEvent(string eventName, WorkItemDto workItem) {
      Assertion.Require(eventName, nameof(eventName));
      Assertion.Require(workItem, nameof(workItem));

      IWorkItemProvider provider = WorkflowProviders.WorkItemProvider();

      IWorkItemEvent workItemEvent = new WorkItemEvent(eventName, workItem);

      provider.SendEvent(workItemEvent);
    }

    #endregion Methods

  }   // class WorkflowService

}  // namespace Empiria.Services
