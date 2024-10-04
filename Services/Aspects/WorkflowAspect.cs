/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Services Framework                         Component : Aspects                                 *
*  Assembly : Empiria.Services.dll                       Pattern   : Aspect type                             *
*  Type     : WorkflowAspect                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : This aspect controls workflow-enabled methods and types.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

using System.Runtime.Remoting.Messaging;

namespace Empiria.Services.Aspects {

  /// <summary>This aspect controls workflow-enabled methods and types.</summary>
  public class WorkflowAspect : BaseAspect {

    #region Constructors and parsers

    private WorkflowAspect(object instance) : base(instance) {
      // Required by Empiria Framework.
    }

    /// <summary>Returns a T instance decorated with this aspect.</summary>
    static public T Decorate<T>(T instance) where T : MarshalByRefObject {
      return (T) new WorkflowAspect(instance).GetTransparentProxy();
    }

    #endregion Constructors and parsers

    #region Methods

    protected override IMessage DecorateImplementation(IMethodCallMessage methodCall) {
      if (!base.HasAspectAttribute<WorkflowCommandAttribute>(methodCall)) {
        return base.NoDecoratedMethodCall(methodCall);
      }

      var workflowCommand = GetAspectAttribute<WorkflowCommandAttribute>(methodCall);

      if (workflowCommand != null) {
        EmpiriaLog.Info($"Workflow BEFORE execution {methodCall.MethodBase.Name} " +
                        $"invoked with {methodCall.ArgCount} args.");
      }

      object result = base.Execute(methodCall);

      if (result != null) {
        EmpiriaLog.Info($"Workflow AFTER code executed {methodCall.MethodName}. " +
                        $"Returns {result.GetType().Name}");
      } else {
        EmpiriaLog.Info($"Workflow AFTER code executed {methodCall.MethodName}. Void returned");
      }

      // Return message supposes that methodCall hasn't out arguments
      return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
    }

    #endregion Methods

  }  // WorkflowAspect

}  // namespace Empiria.Services.Aspects
