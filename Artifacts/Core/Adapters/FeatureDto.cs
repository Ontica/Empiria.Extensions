/* Empiria Artifacts *****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management          Component : Use cases Layer                         *
*  Assembly : Empiria.Artifacts.dll                      Pattern   : Use case interactor class               *
*  Type     : SoftwareFeaturesUseCases                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases for read and write software features.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Artifacts.Adapters {

  public class FeatureDto {

    public string UID {
      get; internal set;
    }

    public string Name {
      get; internal set;
    }

    public FeatureSizeDto Size {
      get; internal set;
    }

    public bool IsGroupingFeature {
      get; internal set;
    }


  }  // class FeatureDto


  public class FeatureSizeDto {

    public int Entries {
      get; internal set;
    }

    public int Reads {
      get; internal set;
    }

    public int Writes {
      get; internal set;
    }

    public int Exits {
      get; internal set;
    }

    public int Total {
      get; internal set;
    }

  }  // class FeatureSizeDto

}  // namespace Empiria.Artifacts.Adapters
