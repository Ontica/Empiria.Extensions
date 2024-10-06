/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management              Component : Domain Layer                        *
*  Assembly : Empiria.Artifacts.dll                          Pattern   : Information Holder                  *
*  Type     : Feature                                        License   : Please read LICENSE.txt file        *
*                                                                                                            *
*  Summary  : Holds information about a software system feature.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Artifacts {

  /// <summary>Holds information about a software system metrable feature.</summary>
  public class Feature : BaseFeature {

    private Feature() {
      // Used by Empiria Framework
    }

    public Feature(SoftwareProduct product, string name) : base(product, name) {
      // no-op
    }

    [DataObject]
    private CosmicSize Size {
      get; set;
    }

    internal override CosmicSize GetSize() {
      return this.Size;
    }

  }  // class Feature

}  // namespace Empiria.SoftwareConfiguration
