/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management              Component : Domain Layer                        *
*  Assembly : Empiria.Artifacts.dll                          Pattern   : Information Holder                  *
*  Type     : SoftwareProduct                                License   : Please read LICENSE.txt file        *
*                                                                                                            *
*  Summary  : Holds information about a software system.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Artifacts {

  /// <summary>Holds information about a software product.</summary>
  public class SoftwareProduct : BaseObject, INamedEntity {

    static internal SoftwareProduct Parse(int id) {
      return BaseObject.ParseId<SoftwareProduct>(id);
    }

    static internal SoftwareProduct Parse(string uid) {
      return BaseObject.ParseKey<SoftwareProduct>(uid);
    }

    static internal SoftwareProduct Empty => BaseObject.ParseEmpty<SoftwareProduct>();

    static internal FixedList<SoftwareProduct> GetList() {
      return BaseObject.GetList<SoftwareProduct>()
                       .ToFixedList();
    }

    #region Properties


    [DataField("ObjectName")]
    public string Name {
      get;
      private set;
    }

    #endregion Properties

    #region Methods

    internal ArtifactList GetArtifacts() {
      return ArtifactList.Parse(this);
    }


    internal FeatureList GetFeatures() {
      return FeatureList.Parse(this);
    }


    internal BaseFeature GetFeature(string featureUID) {
      FeatureList features = this.GetFeatures();

      BaseFeature feature = features.Items.Find(x => x.UID == featureUID);

      Assertion.Require(feature, $"There is not defined a feature with uid = {featureUID}");

      return feature;
    }


    internal void RemoveFeature(BaseFeature feature) {
      FeatureList features = this.GetFeatures();

      features.Remove(feature);
    }

    #endregion Methods

  }  // class SoftwareSystem

}  // namespace Empiria.Artifacts
