/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria.DynamicData / Datasets             Component : Interface adapters                      *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Output DTO                              *
*  Type     : DatasetOutputDto                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return data sets.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Storage;

namespace Empiria.DynamicData.Datasets.Adapters {

  /// <summary>Output DTO used to return data sets.</summary>
  public class DatasetOutputDto {

    internal DatasetOutputDto() {
      // no-op
    }

    public string UID {
      get; internal set;
    }


    public string Name {
      get; internal set;
    }


    public string DatasetFamily {
      get; internal set;
    }

    public string DatasetKind {
      get; internal set;
    }

    public DateTime ElaborationDate {
      get; internal set;
    }


    public string ElaboratedBy {
      get; internal set;
    }


    public FileType FileType {
      get; internal set;
    }


    public string FileName {
      get; internal set;
    }


    public long FileSize {
      get; internal set;
    }


    public string Url {
      get; internal set;
    }

  }  // class DatasetOutputDto

}  // namespace Empiria.DynamicData.Datasets.Adapters
