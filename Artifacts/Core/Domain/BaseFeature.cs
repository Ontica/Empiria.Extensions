/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management              Component : Domain Layer                        *
*  Assembly : Empiria.Artifacts.dll                          Pattern   : Information Holder                  *
*  Type     : Feature                                        License   : Please read LICENSE.txt file        *
*                                                                                                            *
*  Summary  : Abstract class that describes a software feature.                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Artifacts {

  /// <summary>Abstract class that describes a software feature.</summary>
  abstract public class BaseFeature : BaseObject, INamedEntity {

    protected BaseFeature() {
      // Used by Empiria Framework
    }

    protected BaseFeature(SoftwareProduct product, string name) {
      Assertion.Require(product, nameof(product));
      Assertion.Require(name, nameof(name));

      this.Product = product;
      this.Name = name;
    }


    [DataField("FeatureName")]
    public string Name {
      get; private set;
    }


    [DataField("ProductId")]
    internal SoftwareProduct Product {
      get; private set;
    }

    internal abstract CosmicSize GetSize();

  }  // class BaseFeature

}  // namespace Empiria.Artifacts
