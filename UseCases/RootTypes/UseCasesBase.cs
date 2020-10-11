/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Use Cases Framework                        Component : Infrastructure provider                 *
*  Assembly : Empiria.UseCases.dll                       Pattern   : Abstract type                           *
*  Type     : UseCasesBase                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Abstract type that provides an infrastructure to build flexible and configurable use cases,    *
*             running on a secured and transactionable infrastructure.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Aspects;

namespace Empiria.UseCases {

  /// <summary>Abstract type that provides an infrastructure to build flexible and configurable use cases,
  /// running on a secured and transactionable infrastructure.</summary>
  abstract public class UseCasesBase : MarshalByRefObject, IDisposable {

    #region Constructors and parsers

    static public T CreateInstance<T>() where T: UseCasesBase, new() {
      var usecase = new T();

      /// Returns the use case instance decorated with the configurated aspects.</summary>
      return Aspect.Decorate(usecase);
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


    ~UseCasesBase() {
      Dispose(false);
    }

    #endregion IDisposable interface

  }   // class UseCasesBase

}  // namespace Empiria.OnePoint.EFiling.UseCases
