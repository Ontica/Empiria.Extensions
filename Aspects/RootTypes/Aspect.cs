/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Aspects                            Component : Infrastructure provider                 *
*  Assembly : Empiria.Aspects.dll                        Pattern   : Aspect type                             *
*  Type     : Aspect                                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : This is a proof of concept aspect. ToDo: Change this type for an abstract one.                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using System.Reflection;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;

namespace Empiria.Aspects {

  /// <summary>This is a proof of concept aspect. ToDo: Change this type for an abstract one.</summary>
  public class Aspect : RealProxy {

    private readonly object _instance;

    #region Constructors and parsers

    private Aspect(object _instance) : base(_instance.GetType()) {
      this._instance = _instance;
    }


    /// <summary>Returns a T instance decorated with this aspect.</summary>
    static public T Decorate<T>(T _instance) where T : MarshalByRefObject {
      return (T) new Aspect(_instance).GetTransparentProxy();
    }


    #endregion Constructors and parsers

    #region Methods

    public override IMessage Invoke(IMessage msg) {
      var methodCall = msg as IMethodCallMessage;

      if (methodCall == null) {
        return null;
      }

      return Decorate(methodCall);
    }


    IMessage Decorate(IMethodCallMessage methodCall) {
      try {

        // EmpiriaLog.Info($"Aspect.Decorate() code executed BEFORE {methodCall.MethodName}.");

        var result = methodCall.MethodBase.Invoke(_instance, methodCall.InArgs);

        // EmpiriaLog.Info($"Aspect.Decorate() code executed AFTER {methodCall.MethodName}.");

        // Return message supposes that methodCall hasn't out arguments
        return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);

      } catch (TargetInvocationException invocationException) {
        var e = invocationException.InnerException;

        EmpiriaLog.Error(new ServiceException("Aspect.Execution", "Aspect execution failed.", e));

        return new ReturnMessage(e, methodCall);
      }
    }

    #endregion Methods

  }  // Aspect

}  // namespace Empiria.Aspects
