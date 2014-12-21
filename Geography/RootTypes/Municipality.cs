/* Empiria Extended Framework 2015 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : Municipality                                   Pattern  : Empiria Object Type                 *
*  Version   : 6.0        Date: 04/Jan/2015                   License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Represents a municipality within a country state.                                             *
*                                                                                                            *
********************************* Copyright (c) 2009-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;

namespace Empiria.Geography {

  /// <summary>Represents a municipality within a country state.</summary>
  public class Municipality : GeographicRegion {

    #region Fields

    private Lazy<List<Location>> locationsList = null;
    private Lazy<List<Settlement>> settlementsList = null;
    private Lazy<List<Highway>> highwaysList = null;
    private Lazy<List<Roadway>> roadwaysList = null;

    #endregion Fields

    #region Constructors and parsers

    private Municipality() {
      // Required by Empiria Framework.
    }

    internal Municipality(State state, string municipalityName) : base(municipalityName) {
      this.State = state;
    }

    protected override void OnInitialize() {
      base.OnInitialize();
      locationsList = new Lazy<List<Location>>(() => GeographicData.GetChildGeoItems<Location>(this));
      settlementsList = new Lazy<List<Settlement>>(() => GeographicData.GetChildGeoItems<Settlement>(this));
      highwaysList = new Lazy<List<Highway>>(() => GeographicData.GetChildGeoItems<Highway>(this));
      roadwaysList = new Lazy<List<Roadway>>(() => GeographicData.GetChildGeoItems<Roadway>(this));
    }

    static public new Municipality Parse(int id) {
      return BaseObject.ParseId<Municipality>(id);
    }

    static private readonly Municipality _empty = BaseObject.ParseEmpty<Municipality>();
    static public new Municipality Empty {
      get {
        return _empty.Clone<Municipality>();
      }
    }

    static private readonly Municipality _unknown = BaseObject.ParseUnknown<Municipality>();
    static public new Municipality Unknown {
      get {
        return _unknown.Clone<Municipality>();
      }
    }

    #endregion Constructors and parsers

    #region Public properties

    /// <summary>Gets the list of municipal and rural highways managed by this municipality.</summary>
    public FixedList<Highway> Highways {
      get {
        return highwaysList.Value.ToFixedList();
      }
    }

    /// <summary>Gets the list of locations within this municipality.</summary>
    public FixedList<Location> Locations {
      get {
        return locationsList.Value.ToFixedList();
      }
    }

    protected internal override GeographicRegion Parent {
      get {
        return this.State;
      }
    }

    /// <summary>Gets the list of roadways belonging to this municipality instance.</summary>
    public FixedList<Roadway> Roadways {
      get {
        return roadwaysList.Value.ToFixedList();
      }
    }

    /// <summary>Gets the list of settlements within this municipality instance.</summary>
    public FixedList<Settlement> Settlements {
      get {
        return settlementsList.Value.ToFixedList();
      }
    }

    [DataField("GeoItemParentId")]
    public State State {
      get;
      private set;
    }

    #endregion Public properties

    #region Public methods

    /// <summary>Adds a new rural highway to this municipality instance.</summary>
    public Highway AddHighway(RuralHighwayKind ruralHighwayKind,
                              HighwaySection fromOriginToDestination) {
      Assertion.AssertObject(ruralHighwayKind, "ruralHighwayKind");
      Assertion.AssertObject(fromOriginToDestination, "fromOriginToDestination");

      var highway = new Highway(this, ruralHighwayKind, fromOriginToDestination);
      highwaysList.Value.Add(highway);

      return highway;
    }

    /// <summary>Adds a new highway managed by this municipality instance.</summary> 
    public Highway AddHighway(MunicipalHighwayKind municipalHighwayKind,
                              HighwaySection fromOriginToDestination) {
      Assertion.AssertObject(municipalHighwayKind, "municipalHighwayKind");
      Assertion.AssertObject(fromOriginToDestination, "fromOriginToDestination");

      var highway = new Highway(this, municipalHighwayKind, fromOriginToDestination);
      highwaysList.Value.Add(highway);

      return highway;
    }

    /// <summary>Adds a new location within this municipality. Locations are typically small
    /// isolated towns or villages within large municipalities with rural areas.</summary>
    public Location AddLocation(string locationName) {
      Assertion.AssertObject(locationName, "locationName");

      var location = new Location(this, locationName);
      
      locationsList.Value.Add(location);

      return location;
    }

    /// <summary>Adds a new roadway that belongs to this municipality. Roadways attached directly to
    /// municipalities are common in urban or metropolitan municipalities.</summary>
    public Roadway AddRoadway(RoadwayKind roadwayKind, string name) {
      Assertion.AssertObject(roadwayKind, "roadwayKind");
      Assertion.AssertObject(name, "name");

      var roadway = new Roadway(this, roadwayKind, name);
      roadwaysList.Value.Add(roadway);

      return roadway;
    }

    /// <summary>Adds a new settlement within this municipality. Settlements attached to municipalities
    /// are common in densely populated municipalities without isolated locations.</summary>
    public Settlement AddSettlement(SettlementKind settlementKind, string settlementName) {
      Assertion.AssertObject(settlementKind, "settlementKind");
      Assertion.AssertObject(settlementName, "settlementName");

      var settlement = new Settlement(this, settlementKind, settlementName);

      settlementsList.Value.Add(settlement);

      return settlement;
    }

    /// <summary>Adds a new settlement with postal code within this municipality. Settlements attached to
    /// municipalities are common in densely populated municipalities without isolated locations.</summary>
    public Settlement AddSettlement(SettlementKind settlementKind, string settlementName, 
                                    string postalCode) {
      Assertion.AssertObject(postalCode, "postalCode");

      var settlement = this.AddSettlement(settlementKind, settlementName);
      settlement.PostalCode = postalCode;

      return settlement;
    }

    /// <summary>Removes a roadway from this municipality.</summary>
    public void RemoveRoadway(Roadway roadway) {
      Assertion.AssertObject(roadway, "roadway");

      roadway.Remove();
      roadwaysList.Value.Remove(roadway);
    }

    #endregion Public methods

  } // class Municipality

} // namespace Empiria.Geography
