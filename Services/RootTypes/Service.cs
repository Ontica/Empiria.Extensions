/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Service Framework                          Component : Infrastructure provider                 *
*  Assembly : Empiria.Services.dll                       Pattern   : Abstract type                           *
*  Type     : Service                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Abstract type that provides an infrastructure to build flexible and configurable services,     *
*             running on a secured and transactionable infrastructure.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Aspects;
using Empiria.Reflection;

namespace Empiria.Services {

  /// <summary>Abstract type that provides an infrastructure to build flexible and configurable services,
  /// running on a secured and transactionable infrastructure.</summary>
  abstract public class Service : MarshalByRefObject, IDisposable {

    #region Constructors and parsers

    protected Service() {
      // no-op
    }

    /// <summary>Returns a service instance decorated with its configurated aspects.</summary>
    static protected T CreateInstance<T>() where T: Service {
      var service = ObjectFactory.CreateObject<T>();

      return Aspect.Decorate(service);
    }

    #endregion Constructors and parsers

    #region Methods

    protected void EnsureUserHasDataAccessTo<T>(T entity, string failMsg = "") where T : IIdentifiable {
      if (ExecutionServer.CurrentPrincipal.HasDataAccessTo(entity)) {
        return;
      }

      var msg = String.IsNullOrWhiteSpace(failMsg) ?
                        "Invalid user permissions for protected data object." : failMsg;

      throw new InvalidOperationException(msg);
    }


    protected FixedList<T> RestrictUserDataAccessTo<T>(FixedList<T> entityList) where T : IIdentifiable {
      return entityList.FindAll(entity => ExecutionServer.CurrentPrincipal
                                                         .HasDataAccessTo(entity));
    }

    #endregion Methods

    #region IDisposable interface

    public void Dispose() {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {
      // no-op
    }


    ~Service() {
      Dispose(false);
    }

    #endregion IDisposable interface

  }   // class Service

}  // namespace Empiria.Services
