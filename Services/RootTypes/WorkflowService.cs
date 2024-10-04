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

using Empiria.Aspects;
using Empiria.Reflection;

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

      service.WorkItem = null; // ToDo: Improper null assignment

      return service;
    }

    /// <summary>Returns a workflow-enabled service instance with a base workflow item.</summary>
    public static T CreateInstance<T>(IIdentifiable workItem) where T : WorkflowService {
      var service = CreateInstance<T>();

      service.WorkItem = workItem;

      return service;
    }

    #endregion Constructors and parsers

    #region Properties

    public IIdentifiable WorkItem {
      get;
      private set;
    }

    #endregion Properties

  }   // class WorkflowService

}  // namespace Empiria.Services
