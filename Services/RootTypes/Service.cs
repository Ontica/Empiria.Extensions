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

namespace Empiria.Services {

  /// <summary>Abstract type that provides an infrastructure to build flexible and configurable services,
  /// running on a secured and transactionable infrastructure.</summary>
  abstract public class Service : MarshalByRefObject, IDisposable {

    #region Constructors and parsers

    static public T CreateInstance<T>() where T: Service, new() {
      var service = new T();

      /// Returns the service instance decorated with the configurated aspects.</summary>
      return Aspect.Decorate(service);
    }

    #endregion Constructors and parsers


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
