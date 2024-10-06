/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management              Component : Domain Layer                        *
*  Assembly : Empiria.Artifacts.dll                          Pattern   : Information Holder                  *
*  Type     : ArtifactList                                   License   : Please read LICENSE.txt file        *
*                                                                                                            *
*  Summary  : Holds information about a list of software artifacts.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using Empiria.Artifacts.Data;

namespace Empiria.Artifacts {

  /// <summary>Holds information about a list of software artifacts.</summary>
  public class ArtifactList {

    private readonly SoftwareProduct _system;
    private Lazy<List<Artifact>> _items;

    private ArtifactList(SoftwareProduct system) {
      _system = system;

      RefreshArtifacts();
    }

    static internal ArtifactList Parse(SoftwareProduct system) {
      Assertion.Require(system, nameof(system));

      return new ArtifactList(system);
    }

    public FixedList<Artifact> Items {
      get {
        return _items.Value.ToFixedList();
      }
    }

    private void RefreshArtifacts() {
      _items = new Lazy<List<Artifact>>(() => ArtifactsData.GetArtifacts(_system));
    }

  }  // class ArtifactList

}  // namespace Empiria.Artifacts
