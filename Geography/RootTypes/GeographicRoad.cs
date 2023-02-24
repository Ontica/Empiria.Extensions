/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Geographic Data Services            *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeographicRoad                                 Pattern  : Empiria Object Type                 *
*  Version   : 6.8                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Represents a geographic road that could be a roadway, highway or a rural road.                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Ontology;

namespace Empiria.Geography {

  /// <summary>Represents a geographic road that could be a roadway, highway or a rural road.</summary>
  public abstract class GeographicRoad : GeographicItem {

    #region Constructors and parsers

    protected GeographicRoad() {
      // Required by Empiria Framework.
    }

    protected GeographicRoad(string roadName) : base(roadName) {
      // Used by derived types to create new instances of GeographicRoad.
    }

    protected GeographicRoad(ObjectTypeInfo powertype) : base(powertype) {
      // Required by Empiria Framework for all partitioned types.
    }

    protected GeographicRoad(ObjectTypeInfo powertype, string roadName) : base(powertype, roadName) {
      // Used by derived partitioned types to create new instances of GeographicRoad.
    }

    static public new GeographicRoad Parse(int id) {
      return BaseObject.ParseId<GeographicRoad>(id);
    }

    #endregion Constructors and parsers

  } // class GeographicRoad

} // namespace Empiria.Geography
