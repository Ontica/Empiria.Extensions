/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Payload Models                        *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Information Holder                    *
*  Type     : ExceptionExtendedData                        License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Contains the extended data for an exception response.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Runtime.Serialization;

namespace Empiria.WebApi.Internals {

  internal class ExceptionExtendedData : ExceptionData {

    internal ExceptionExtendedData(Exception exception) : base(exception) {
      Exception = exception;
    }


    [DataMember(Name = "exception", Order = 100)]
    public Exception Exception {
      get; private set;
    }

  }  // ExceptionExtendedData

} //namespace Empiria.WebApi.Internals
