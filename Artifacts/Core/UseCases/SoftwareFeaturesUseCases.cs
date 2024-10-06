/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management          Component : Use cases Layer                         *
*  Assembly : Empiria.Artifacts.dll                      Pattern   : Use case interactor class               *
*  Type     : SoftwareFeaturesUseCases                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases for read and write software features.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Services;

using Empiria.Artifacts.Adapters;

namespace Empiria.Artifacts.UseCases {

  /// <summary>Use cases for read and write software features.</summary>
  public class SoftwareFeaturesUseCases : UseCase {

    #region Constructors and parsers

    protected SoftwareFeaturesUseCases() {
      // no-op
    }


    static public SoftwareFeaturesUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<SoftwareFeaturesUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public void DeleteFeature(string softwareProductUID, string featureUID) {
      Assertion.Require(softwareProductUID, nameof(softwareProductUID));
      Assertion.Require(featureUID, nameof(featureUID));

      var product = SoftwareProduct.Parse(softwareProductUID);

      BaseFeature feature = product.GetFeature(featureUID);

      product.RemoveFeature(feature);
    }


    public FixedList<FeatureDto> GetFeatures(string softwareProductUID) {
      Assertion.Require(softwareProductUID, nameof(softwareProductUID));

      var product = SoftwareProduct.Parse(softwareProductUID);

      FeatureList features = product.GetFeatures();

      return FeaturesMapping.Map(features);
    }


    public FixedList<FeatureDto> InsertFeature(InsertFeatureCommand command) {
      Assertion.Require(command, nameof(command));

      var product = SoftwareProduct.Parse(command.SoftwareProductUID);

      FeatureList features = product.GetFeatures();

      BaseFeature newFeature;

      if (command.IsGroupingFeature) {
        newFeature = new GroupingFeature(product, command.Name);
      } else {
        newFeature = new Feature(product, command.Name);
      }

      if (command.Index == -1) {
        features.Add(newFeature);
      } else {
        features.Insert(command.Index, newFeature);
      }

      return FeaturesMapping.Map(features);
    }

    #endregion Use cases

  } // class SoftwareFeaturesUseCases

} // namespace Empiria.Artifacts.UseCases
