/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Service Framework                          Component : Infrastructure provider                 *
*  Assembly : Empiria.Services.dll                       Pattern   : Abstract type                           *
*  Type     : UseCase                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Abstract type that provides an infrastructure to build flexible and configurable use cases,    *
*             running on a secured and transactionable infrastructure.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Services {

  /// <summary>Abstract type that provides an infrastructure to build flexible and configurable use cases,
  /// running on a secured and transactionable infrastructure.</summary>
  abstract public class UseCase : Service {

    #region Constructors and parsers

    protected UseCase() {
      // no-op
    }

    #endregion Constructors and parsers

  }   // class UseCase

}  // namespace Empiria.Services
