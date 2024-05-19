/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria.DynamicData / Datasets             Component : Interface adapters                      *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Data Transfer Object                    *
*  Type     : DatasetKindDto                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO that describes tha rules of a dataset.                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Storage;

namespace Empiria.DynamicData.Datasets.Adapters {

  /// <summary>Output DTO that describes tha rules of a dataset.</summary>
  public class DatasetKindDto {

    public string Name {
      get; internal set;
    }

    public string Type {
      get; internal set;
    }

    public FileType FileType {
      get; internal set;
    }

    public bool Optional {
      get; internal set;
    }

    public int Count {
      get; internal set;
    }

    public string TemplateUrl {
      get; internal set;
    }

  }  // class DatasetKindDto

}  // namespace Empiria.DynamicData.Datasets.Adapters
