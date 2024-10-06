/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management              Component : Domain Layer                        *
*  Assembly : Empiria.Artifacts.dll                          Pattern   : Information Holder                  *
*  Type     : GroupingFeature                                License   : Please read LICENSE.txt file        *
*                                                                                                            *
*  Summary  : Holds information about a software system feature.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Artifacts {

  /// <summary>Holds information about a software system metrable feature.</summary>
  public class GroupingFeature : BaseFeature {

    private GroupingFeature() {
      // Used by Empiria Framework
    }

    public GroupingFeature(SoftwareProduct product, string name) : base(product, name) {
      // no-op
    }

    internal FixedList<BaseFeature> Children {
      get {
        return FeatureList.Children(this);
      }
    }

    internal override CosmicSize GetSize() {
      var total = new CosmicSize();

      foreach (var child in Children) {
        if (child is GroupingFeature) {
          total = total.Sum(child.GetSize());
        }
      }
      return total;
    }

  }  // class Feature

}  // namespace Empiria.Artifacts
