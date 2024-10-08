/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Services Framework                         Component : Aspects                                 *
*  Assembly : Empiria.Services.dll                       Pattern   : Aspect Attribute type                   *
*  Type     : WorkflowEventAttribute                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Marks a method that sends an event that can be handled by a workflow engine.                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

namespace Empiria.Services.Aspects {

  /// <summary>Marks a method that sends an event that can be handled by a workflow engine.</summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
  public class WorkflowEventAttribute : AspectAttribute {

    public WorkflowEventAttribute(string eventName) {
      Assertion.Require(eventName, nameof(eventName));

      EventName = eventName;
    }

    #region Properties

    public string EventName {
      get;
    }

    #endregion Properties

  }  // class WorkflowEventAttribute

}  // namespace Empiria.Services.Aspects
