/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeographicRegion                               Pattern  : Empiria Object Type                 *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Represents a geographic area or region: city, country, world zone, zip code region, etc.      *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Data;

using Empiria.Contacts;
using Empiria.DataTypes;
using Empiria.Ontology;

namespace Empiria.Geography {

  /// <summary>Represents a geographic area or region: city, country, world zone,
  /// zip code region, etc.</summary>
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

    static private readonly GeographicRegion _unknown = 
                                                    BaseObject.ParseUnknown<GeographicRegion>();
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

    #endregion Public properties

  } // class GeographicRegion

} // namespace Empiria.Geography
