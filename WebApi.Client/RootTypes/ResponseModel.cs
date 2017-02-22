/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Empiria Web API Services            *
*  Namespace : Empiria.WebApi.Client                          Assembly : Empiria.WebApi.Client.dll           *
*  Type      : ResponseModel<T>                               Pattern  : Information holder                  *
*  Version   : 1.1                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Contains the payload model of Empiria web service reponses.                                   *
*                                                                                                            *
********************************* Copyright (c) 2004-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

namespace Empiria.WebApi.Client {

  /// <summary>Contains the payload model of Empiria web service reponses.</summary>
  /// <typeparam name="T">The type of the returned data contained in the 'data' response's field.</typeparam>
  public class ResponseModel<T> {

    public string Status {
      get;
      set;
    }


    public string DataType {
      get;
      set;
    }


    public string PayloadType {
      get;
      set;
    }


    public string Version {
      get;
      set;
    }


    public int DataItems {
      get;
      set;
    }


    public Guid RequestId {
      get;
      set;
    }

    public T Data {
      get;
      set;
    }

  }  // class ResponseModel<T>

}  // namespace Empiria.WebApi.Client
