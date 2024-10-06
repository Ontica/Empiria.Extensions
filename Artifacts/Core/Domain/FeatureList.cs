/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management              Component : Domain Layer                        *
*  Assembly : Empiria.Artifacts.dll                          Pattern   : Information Holder                  *
*  Type     : FeatureList                                    License   : Please read LICENSE.txt file        *
*                                                                                                            *
*  Summary  : A list of software features                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using Empiria.Artifacts.Data;

namespace Empiria.Artifacts {

  public class FeatureList {

    private readonly SoftwareProduct _system;
    private Lazy<List<BaseFeature>> _items;

    private FeatureList(SoftwareProduct system) {
      _system = system;
      RefreshItems();
    }

    static internal FeatureList Parse(SoftwareProduct system) {
      Assertion.Require(system, nameof(system));

      return new FeatureList(system);
    }

    static internal FixedList<BaseFeature> Children(GroupingFeature group) {
      return FeaturesData.GetChildren(group);
    }

    #region Properties

    public FixedList<BaseFeature> Items {
      get {
        return _items.Value.ToFixedList();
      }
    }

    #endregion Properties

    #region Methods

    public void Add(BaseFeature feature) {
      Assertion.Require(feature, nameof(feature));

      _items.Value.Add(feature);
    }


    public void Insert(int index, BaseFeature feature) {
      Assertion.Require(feature, nameof(feature));

      _items.Value.Insert(index, feature);
    }


    public void Remove(BaseFeature feature) {
      Assertion.Require(feature, nameof(feature));

      _items.Value.Remove(feature);
    }

    #endregion Methods

    #region Helpers

    private void RefreshItems() {
      _items = new Lazy<List<BaseFeature>>(() => FeaturesData.GetFeatures(_system));
    }

    #endregion Helpers

  }  // class FeatureList

}  // namespace Empiria.Artifacts
