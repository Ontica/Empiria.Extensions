/* Empiria Service-Oriented Architecture Framework ***********************************************************
*                                                                                                            *
*  Solution  : Empiria SOA Framework                            System   : Empiria Web Api Framework         *
*  Namespace : Empiria.WebApi.Models                            Assembly : Empiria.WebApi.dll                *
*  Type      : IBaseResponseModel                               Pattern  : Interface                         *
*  Version   : 1.0        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Base interface for web api response types.                                                    *
*                                                                                                            *
********************************* Copyright (c) 2014-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

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

    Guid RequestId {
      get;
    }

  }  // interface IBaseResponseModel

} // namespace Empiria.WebApi.Models
