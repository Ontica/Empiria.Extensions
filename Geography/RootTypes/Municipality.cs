/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : Municipality                                   Pattern  : Empiria Object Type                 *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Represents a municipality within a country state.                                             *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;

namespace Empiria.Geography {

  /// <summary>Represents a municipality within a country state.</summary>
  public class Municipality : GeographicRegion {

    #region Fields

    private const string thisTypeName = "ObjectType.GeographicItem.GeographicRegion.Municipality";

    private Lazy<List<Location>> locationsList = null;
    private Lazy<List<Settlement>> settlementsList = null;

    #endregion Fields

    #region Constructors and parsers

    protected Municipality(string typeName) : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    internal Municipality(State state, string municipalityName) : base(thisTypeName, municipalityName) {
      this.State = state;
    }

    protected override void OnInitialize() {
      base.OnInitialize();
      locationsList = new Lazy<List<Location>>(() => GeographicData.GetChildGeoItems<Location>(this));
      settlementsList = new Lazy<List<Settlement>>(() => GeographicData.GetChildGeoItems<Settlement>(this));
    }

    static public new Municipality Parse(int id) {
      return BaseObject.Parse<Municipality>(thisTypeName, id);
    }

    static private readonly Municipality _empty = BaseObject.ParseEmpty<Municipality>(thisTypeName);
    static public new Municipality Empty {
      get {
        return _empty.Clone<Municipality>();
      }
    }

    static private readonly Municipality _unknown = BaseObject.ParseUnknown<Municipality>(thisTypeName);
    static public new Municipality Unknown {
      get {
        return _unknown.Clone<Municipality>();
      }
    }

    #endregion Constructors and parsers

    #region Public properties
    
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

    public Location AddLocation(string locationName) {
      Assertion.AssertObject(locationName, "locationName");

      var location = new Location(this, locationName);
      
      locationsList.Value.Add(location);

      return location;
    }

    public Settlement AddSettlement(SettlementType settlementType, string settlementName) {
      Assertion.AssertObject(settlementType, "settlementType");
      Assertion.AssertObject(settlementName, "settlementName");

      var settlement = new Settlement(this, settlementType, settlementName);

      settlementsList.Value.Add(settlement);

      return settlement;
    }

    public Settlement AddSettlement(SettlementType settlementType, string settlementName, 
                                    string postalCode) {
      Assertion.AssertObject(postalCode, "postalCode");

      Settlement settlement = this.AddSettlement(settlementType, settlementName);
      settlement.PostalCode = postalCode;

      return settlement;
    }

    public void AddRoad(Road road) {
      this.AssociateWith(road, "Roads");
    }

    public FixedList<Road> GetRoads() {
      return this.GetAssociations<Road>("Roads");
    }

    public FixedList<Settlement> GetSettlements(SettlementType settlementType) {
      throw new NotImplementedException();
    }

    #endregion Public methods

  } // class Municipality

} // namespace Empiria.Geography
