/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Client                             Component : Services Layer                          *
*  Assembly : Empiria.WebApi.Client.dll                  Pattern   : Information Holder                      *
*  Type     : ResponseModel<T>                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Payload model for Empiria web service responses.                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.WebApi.Client {

  /// <summary>Payload model for Empiria web service responses.</summary>
  /// <typeparam name="T">The type of the returned data contained in the 'data' response's field.</typeparam>
  public class ResponseModel<T> {

    public string Status {
      get; set;
    }


    public string DataType {
      get; set;
    }


    public string PayloadType {
      get; set;
    }


    public string Version {
      get; set;
    }


    public int DataItems {
      get; set;
    }


    public Guid RequestId {
      get; set;
    }

    public T Data {
      get; set;
    }

  }  // class ResponseModel

}  // namespace Empiria.WebApi.Client
