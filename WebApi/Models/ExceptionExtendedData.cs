/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi.Models                            Assembly : Empiria.WebApi.dll                *
*  Type      : ExceptionData                                    Pattern  : Information Holder                *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Contains the data for an exception response.                                                  *
*                                                                                                            *
********************************* Copyright (c) 2014-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Net;
using System.Runtime.Serialization;

namespace Empiria.WebApi.Models {

  internal class ExceptionExtendedData : ExceptionData {

    public ExceptionExtendedData(Exception exception) : base (exception) {
      this.Exception = exception;
    }

    [DataMember(Name = "exception", Order = 100)]
    public Exception Exception {
      get;
      internal set;
    }

  }  // ExceptionExtendedData

} // namespace Empiria.WebApi.Models
