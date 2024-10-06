/* Empiria OnePoint Artifacts ********************************************************************************
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

  static internal class FeaturesMapping {

    static internal FixedList<FeatureDto> Map(FeatureList features) {
      return features.Items.Select(feature => Map(feature))
                           .ToFixedList();
    }

    static private FeatureDto Map(BaseFeature feature) {
      return new FeatureDto {
        UID = feature.UID,
        Name = feature.Name,
        IsGroupingFeature = feature is GroupingFeature,
        Size = Map(feature.GetSize())
      };
    }

    static private FeatureSizeDto Map(CosmicSize size) {
      return new FeatureSizeDto {
        Entries = size.Entries,
        Reads   = size.Reads,
        Writes  = size.Writes,
        Exits   = size.Exits,
        Total   = size.Total
      };
    }

  }  // class FeaturesMapping

}  // namespace Empiria.Artifacts.Adapters
