/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Services Framework                         Component : Aspects                                 *
*  Assembly : Empiria.Services.dll                       Pattern   : Aspect type                             *
*  Type     : LogAspect                                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : This aspect serves to log method calls using EmpiriaLog.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

using System.Runtime.Remoting.Messaging;

namespace Empiria.Services.Aspects {

  /// <summary>This aspect serves to log method calls using EmpiriaLog.</summary>
  public class LogAspect : BaseAspect {

    #region Constructors and parsers

    private LogAspect(object instance) : base(instance) {
      // Required by Empiria Framework.
    }


    /// <summary>Returns a T instance decorated with the LogAspect.</summary>
    static public T Decorate<T>(T instance) where T : MarshalByRefObject {
      return (T) new LogAspect(instance).GetTransparentProxy();
    }

    #endregion Constructors and parsers

    #region Methods

    protected override IMessage DecorateImplementation(IMethodCallMessage methodCall) {
      EmpiriaLog.Info($"LogAspect BEFORE execution {methodCall.MethodBase.Name} " +
                      $"invoked with {methodCall.ArgCount} args.");

      var result = base.Execute(methodCall);

      if (result != null) {
        EmpiriaLog.Info($"LogAspect Aspect AFTER code executed {methodCall.MethodName}. Returns {result.GetType().Name}");
      } else {
        EmpiriaLog.Info($"LogAspect Aspect AFTER code executed {methodCall.MethodName}. Void returned");
      }

      // Return message supposes that methodCall hasn't out arguments
      return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);
    }

    #endregion Methods

  }  // LogAspect

}  // namespace Empiria.Services.Aspects
