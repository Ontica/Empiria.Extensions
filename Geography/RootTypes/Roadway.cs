/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : Roadway                                     Pattern  : Empiria Object Type                    *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Represents a roadway (street, avenue, etc). A roadway belongs to a settlement or location.    *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;
using System.Data;

namespace Empiria.Geography {

  /// <summary>Represents a roadway (street, avenue, etc).</summary>
  public class Roadway : GeographicRoad {

    #region Fields

    private const string thisTypeName = "ObjectType.GeographicItem.GeographicRoad.Roadway";

    #endregion Fields

    #region Constructors and parsers

    protected Roadway(string typeName) : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    internal Roadway(Location location, RoadwayKind roadwayKind, string roadwayName)
                    : base(thisTypeName, roadwayName) {
      this.RoadwayKind = roadwayKind;
      this.Settlement = Settlement.Empty;
      this.Location = location;
      this.Municipality = this.Location.Municipality;
    }

    internal Roadway(Settlement settlement, RoadwayKind roadwayKind, string roadwayName)
                     : base(thisTypeName, roadwayName) {
      this.RoadwayKind = roadwayKind;
      this.Settlement = settlement;
      this.Location = settlement.Location;
      this.Municipality = this.Location.Municipality;
    }

    internal Roadway(Municipality municipality, RoadwayKind roadwayKind, string roadwayName)
                     : base(thisTypeName, roadwayName) {
      this.RoadwayKind = roadwayKind;
      this.Settlement = Settlement.Empty;
      this.Location = Location.Empty;
      this.Municipality = municipality;
    }

    static public new Roadway Parse(int id) {
      return BaseObject.ParseId<Roadway>(id);
    }

    static private readonly Roadway _empty = BaseObject.ParseEmpty<Roadway>();
    static public Roadway Empty {
      get {
        return _empty.Clone<Roadway>();
      }
    }

    static private readonly Roadway _unknown = BaseObject.ParseUnknown<Roadway>();
    static public Roadway Unknown {
      get {
        return _unknown.Clone<Roadway>();
      }
    }

    #endregion Constructors and parsers

    #region Public properties

    public Location Location {
      get;
      private set;
    }

    public Municipality Municipality {
      get;
      private set;
    }

    protected internal override GeographicRegion Parent {
      get {
        if (!this.Settlement.IsEmptyInstance) {
          return this.Settlement;
        } else if (!this.Location.IsEmptyInstance) {
          return this.Location;
        } else if (!this.Municipality.IsEmptyInstance) {
          return this.Municipality;
        } else {
          throw Assertion.AssertNoReachThisCode();
        }
      }
    }

    [DataField("GeoItemExtData.RoadwayKind")]
    public RoadwayKind RoadwayKind {
      get;
      private set;
    }

    public Settlement Settlement {
      get;
      private set;
    }

    #endregion Public properties

    #region Public methods

    protected override void OnLoadObjectData(DataRow row) {
      base.OnLoadObjectData(row);

      var parent = GeographicRegion.Parse((int) row["GeoItemParentId"]);
      SetSettlementOrLocationWithParent(parent);
    }

    private void SetSettlementOrLocationWithParent(GeographicRegion parent) {
      if (parent.IsEmptyInstance && this.IsSpecialCase) {
        this.Settlement = Settlement.Empty;
        this.Location = Location.Empty;
        this.Municipality = Municipality.Empty;
      } else if (parent is Settlement) {
        this.Settlement = (Settlement) parent;
        this.Location = this.Settlement.Location;
        this.Municipality = this.Location.Municipality;
      } else if (parent is Location) {
        this.Settlement = Settlement.Empty;
        this.Location = (Location) parent;
        this.Municipality = this.Location.Municipality;
      } else if (parent is Municipality) {
        this.Settlement = Settlement.Empty;
        this.Location = Location.Empty;
        this.Municipality = (Municipality) parent;
      } else {
        throw Assertion.AssertNoReachThisCode();
      }
    }

    #endregion Public methods

  } // class Roadway

} // namespace Empiria.Geography
