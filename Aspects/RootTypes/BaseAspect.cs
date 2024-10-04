/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Aspects                            Component : Infrastructure provider                 *
*  Assembly : Empiria.Aspects.dll                        Pattern   : Aspect type                             *
*  Type     : BaseAspect                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Abstract aspect that serves as a base class for all Empiria aspect types.                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Reflection;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;

namespace Empiria.Aspects {

  /// <summary>Abstract aspect that serves as a base class for all Empiria aspect types.</summary>
  abstract public class BaseAspect : RealProxy {

    #region Constructors and parsers

    protected BaseAspect(object instance) : base(instance.GetType()) {
      Assertion.Require(instance, nameof(instance));

      CurrentInstance = instance;
    }

    #endregion Constructors and parsers

    #region Methods

    protected object CurrentInstance {
      get;
    }

    #endregion Methods

    #region Methods

    protected abstract IMessage DecorateImplementation(IMethodCallMessage methodCall);


    public override IMessage Invoke(IMessage msg) {
      var methodCall = msg as IMethodCallMessage;

      if (methodCall == null) {
        return null;
      }

      if (methodCall.MethodBase.IsDefined(typeof(AspectAttribute), false)) {
        return DecorateImplementation(methodCall);
      }

      return NoDecoratedMethodCall(methodCall);
    }


    protected IMessage NoDecoratedMethodCall(IMethodCallMessage methodCall) {
      try {
        var result = methodCall.MethodBase.Invoke(CurrentInstance, methodCall.InArgs);

        return new ReturnMessage(result, null, 0, methodCall.LogicalCallContext, methodCall);

      } catch (TargetInvocationException invocationException) {
        var e = invocationException.InnerException;

        EmpiriaLog.Error(e);

        return new ReturnMessage(e, methodCall);

      } catch (Exception e) {

        EmpiriaLog.Error(e);

        return new ReturnMessage(e, methodCall);
      }
    }

    #endregion Methods

  }  // BaseAspect

}  // namespace Empiria.Aspects
