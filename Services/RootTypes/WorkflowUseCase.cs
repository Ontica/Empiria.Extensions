/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Services Framework                         Component : Infrastructure provider                 *
*  Assembly : Empiria.Services.dll                       Pattern   : Abstract type                           *
*  Type     : WorkflowUseCase                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Abstract type that provides an infrastructure to build flexible and configurable               *
*             workflow-enabled use cases, running on a secured and transactionable infrastructure.           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Services {

  /// <summary>Abstract type that provides an infrastructure to build flexible and configurable
  /// workflow-enabled use cases, running on a secured and transactionable infrastructure.</summary>
  abstract public class WorkflowUseCase : WorkflowService {

    #region Constructors and parsers

    protected WorkflowUseCase() {
      // no-op
    }

    #endregion Constructors and parsers

  }   // class WorkflowUseCase

}  // namespace Empiria.Services
