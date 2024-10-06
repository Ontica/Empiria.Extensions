/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management          Component : Test cases                              *
*  Assembly : Empiria.Artifacts.Tests.dll                Pattern   : Use cases tests                         *
*  Type     : SoftwareFeaturesUseCasesTests              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use case tests for software features.                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Xunit;

using Empiria.Artifacts.Adapters;

using Empiria.Artifacts.UseCases;

namespace Empiria.Tests.Artifacts {

  /// <summary>TUse case tests for software features.</summary>
  public class SoftwareFeaturesUseCasesTests {

    #region Facts

    [Fact]
    public void Should_Read_Software_Features() {

      using (var usecase = SoftwareFeaturesUseCases.UseCaseInteractor()) {
        FixedList<FeatureDto> features = usecase.GetFeatures(TestingConstants.SOFTWARE_PRODUCT_UID);

        Assert.NotEmpty(features);

      }
    }


    [Fact]
    public void Should_Append_A_Grouping_Feature() {

      const string TEST_GROUP_NAME = "Test grouping feature";

      using (var usecases = SoftwareFeaturesUseCases.UseCaseInteractor()) {
        FixedList<FeatureDto> features =
                        usecases.GetFeatures(TestingConstants.SOFTWARE_PRODUCT_UID);
        var command = new InsertFeatureCommand {
          Name = TEST_GROUP_NAME,
          IsGroupingFeature = true,
          SoftwareProductUID = TestingConstants.SOFTWARE_PRODUCT_UID
        };

        int sut = features.Count;

        features = usecases.InsertFeature(command);

        Assert.Equal(sut + 1, features.Count);

        Assert.Equal(TEST_GROUP_NAME, features[sut].Name);
        Assert.True(features[sut].IsGroupingFeature);
      }
    }


    [Fact]
    public void Should_Delete_A_Feature() {
      using (var usecases = SoftwareFeaturesUseCases.UseCaseInteractor()) {
        FixedList<FeatureDto> features =
                        usecases.GetFeatures(TestingConstants.SOFTWARE_PRODUCT_UID);

        FeatureDto toDelete = features.FindLast(x => !x.IsGroupingFeature);

        int sut = features.Count;

        usecases.DeleteFeature(TestingConstants.SOFTWARE_PRODUCT_UID, toDelete.UID);

        features = usecases.GetFeatures(TestingConstants.SOFTWARE_PRODUCT_UID);

        Assert.Equal(sut - 1, features.Count);
      }
    }


    [Fact]
    public void Should_Insert_A_Grouping_Feature() {

      const string TEST_GROUP_NAME = "Test grouping feature";
      const int INDEX = 25;

      using (var usecases = SoftwareFeaturesUseCases.UseCaseInteractor()) {
        FixedList<FeatureDto> features =
                        usecases.GetFeatures(TestingConstants.SOFTWARE_PRODUCT_UID);
        var command = new InsertFeatureCommand {
          Name = TEST_GROUP_NAME,
          IsGroupingFeature = true,
          SoftwareProductUID = TestingConstants.SOFTWARE_PRODUCT_UID,
          Index = INDEX,
        };

        int sut = features.Count;

        features = usecases.InsertFeature(command);

        Assert.Equal(sut + 1, features.Count);

        Assert.Equal(TEST_GROUP_NAME, features[INDEX].Name);

        Assert.True(features[INDEX].IsGroupingFeature);
      }
    }

    #endregion Facts

  }  // class SoftwareFeaturesUseCases

}  // namespace Empiria.Tests.Artifacts
