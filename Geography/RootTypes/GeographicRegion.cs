﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Geographic Data Services            *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeographicRegion                               Pattern  : Empiria Object Type                 *
*  Version   : 6.8                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Represents a geographic area or region: city, country, world zone, postal code region, etc.   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Geography {

  /// <summary>Represents a geographic area or region: city, country, world zone,
  /// postal code region, etc.</summary>
  public class GeographicRegion : GeographicItem {

    #region Constructors and parsers

    protected GeographicRegion() {
      // Required by Empiria Framework.
    }

    protected GeographicRegion(string regionName) : base(regionName) {
      // Used by derived types to create new instances of GeographicRegion.
    }

    static public new GeographicRegion Parse(int id) {
      return BaseObject.ParseId<GeographicRegion>(id);
    }

    static private readonly GeographicRegion _empty = BaseObject.ParseEmpty<GeographicRegion>();
    static public GeographicRegion Empty {
      get {
        return _empty.Clone<GeographicRegion>();
      }
    }

    static private readonly GeographicRegion _unknown = BaseObject.ParseUnknown<GeographicRegion>();
    static public GeographicRegion Unknown {
      get {
        return _unknown.Clone<GeographicRegion>();
      }
    }

    #endregion Constructors and parsers

    #region Public properties

    public new bool IsSpecialCase {
      get {
        return base.IsSpecialCase;
      }
    }

    public virtual string CompoundName {
      get {
        return base.Name + " (" + base.GetEmpiriaType().DisplayName + ")";
      }
    }

    #endregion Public properties

  } // class GeographicRegion

} // namespace Empiria.Geography
