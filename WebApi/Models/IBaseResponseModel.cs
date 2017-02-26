/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi.Models                            Assembly : Empiria.WebApi.dll                *
*  Type      : IBaseResponseModel                               Pattern  : Interface                         *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Base interface for web api response types.                                                    *
*                                                                                                            *
********************************* Copyright (c) 2014-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Net.Http;

namespace Empiria.WebApi.Models {

  /// <summary>Base interface for web api response types.</summary>
  public interface IBaseResponseModel {

    string Status {
      get;
    }

    string Version {
      get;
    }

    string TypeName {
      get;
    }

    int DataItemsCount {
      get;
    }

    HttpRequestMessage Request {
      get;
    }

    Guid RequestId {
      get;
    }

  }  // interface IBaseResponseModel

} // namespace Empiria.WebApi.Models
