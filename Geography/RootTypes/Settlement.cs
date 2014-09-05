/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : Settlement                                     Pattern  : Empiria Object Type                 *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Represents a settlement within a location or municipality.                                    *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;
using System.Data;

namespace Empiria.Geography {

  /// <summary>Represents a settlement within a location or municipality.</summary>
  public class Settlement : GeographicRegion {

    #region Fields

    private const string thisTypeName = "ObjectType.GeographicItem.GeographicRegion.Settlement";
    private Lazy<List<Roadway>> roadwaysList = null;

    #endregion Fields

    #region Constructors and parsers

    protected Settlement(string typeName) : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    internal Settlement(Location location, SettlementKind SettlementKind, string settlementName)
                        : base(thisTypeName, settlementName) {
      this.SettlementKind = SettlementKind;
      this.Location = location;
      this.Municipality = location.Municipality;
    }

    internal Settlement(Municipality municipality, SettlementKind SettlementKind, string settlementName)
                        : base(thisTypeName, settlementName) {
      this.SettlementKind = SettlementKind;
      this.Municipality = municipality;
      this.Location = Location.Empty;
    }

    protected override void OnInitialize() {
      base.OnInitialize();
      roadwaysList = new Lazy<List<Roadway>>(() => GeographicData.GetChildGeoItems<Roadway>(this));
    }

    static public new Settlement Parse(int id) {
      return BaseObject.ParseId<Settlement>(id);
    }

    static private readonly Settlement _empty = BaseObject.ParseEmpty<Settlement>();
    static public new Settlement Empty {
      get {
        return _empty.Clone<Settlement>();
      }
    }

    static private readonly Settlement _unknown = BaseObject.ParseUnknown<Settlement>();
    static public new Settlement Unknown {
      get {
        return _unknown.Clone<Settlement>();
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
        if (!this.Location.IsEmptyInstance) {
          return this.Location;
        } else {
          return this.Municipality;
        }
      }
    }

    [DataField("GeoItemExtData.PostalCode")]
    private string _postalCode = String.Empty;
    public string PostalCode {
      get {
        return _postalCode;
      }
      internal set {
        this.State.AssertPostalCodeIsValid(value);
        _postalCode = value;
      }
    }

    public FixedList<Roadway> Roadways {
      get {
        return roadwaysList.Value.ToFixedList();
      }
    }

    [DataField("GeoItemExtData.SettlementKind")]
    public SettlementKind SettlementKind {
      get;
      private set;
    }

    public State State {
      get {
        return this.Municipality.State;
      }
    }

    #endregion Public properties

    #region Public methods

    public Roadway AddRoadway(RoadwayKind roadwayKind, string name) {
      Assertion.AssertObject(roadwayKind, "roadwayKind");
      Assertion.AssertObject(name, "name");

      var roadway = new Roadway(this, roadwayKind, name);
      roadwaysList.Value.Add(roadway);

      return roadway;
    }

    protected override void OnLoadObjectData(DataRow row) {
      base.OnLoadObjectData(row);

      var parent = GeographicRegion.Parse((int) row["GeoItemParentId"]);
      SetMunicipalityAndLocationWithParent(parent);
    }

    public void RemoveRoadway(Roadway roadway) {
      Assertion.AssertObject(roadway, "roadway");

      roadway.Remove();
      roadwaysList.Value.Remove(roadway);
    }

    #endregion Public methods

    #region Private methods

    private void SetMunicipalityAndLocationWithParent(GeographicRegion parent) {
      if (parent.IsEmptyInstance && this.IsSpecialCase) {
        this.Location = Location.Empty;
        this.Municipality = Municipality.Empty;
      } else if (parent is Location) {
        this.Location = (Location) parent;
        this.Municipality = this.Location.Municipality;
      } else if (parent is Municipality) {
        this.Location = Location.Empty;
        this.Municipality = (Municipality) parent;
      } else {
        throw Assertion.AssertNoReachThisCode();
      }
    }

    #endregion Private methods

  } // class Settlement

} // namespace Empiria.Geography
