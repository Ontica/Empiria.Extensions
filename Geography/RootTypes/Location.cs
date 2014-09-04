/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : Location                                       Pattern  : Empiria Object Type                 *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Represents a location within a municipality.                                                  *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;

namespace Empiria.Geography {

  /// <summary>Represents a location within a municipality.</summary>
  public class Location : GeographicRegion {

    #region Fields

    private const string thisTypeName = "ObjectType.GeographicItem.GeographicRegion.Location";

    private Lazy<List<Settlement>> settlementsList = null;
    private Lazy<List<Roadway>> roadwaysList = null;

    #endregion Fields

    #region Constructors and parsers

    protected Location(string typeName) : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise.
    }

    internal Location(Municipality municipality, string locationName) : 
                                                 base(thisTypeName, locationName) {
      this.Municipality = municipality;
    }

    protected override void OnInitialize() {
      base.OnInitialize();
      settlementsList = new Lazy<List<Settlement>>(() => GeographicData.GetChildGeoItems<Settlement>(this));
      roadwaysList = new Lazy<List<Roadway>>(() => GeographicData.GetChildGeoItems<Roadway>(this));
    }

    static public new Location Parse(int id) {
      return BaseObject.Parse<Location>(thisTypeName, id);
    }

    static private readonly Location _empty = BaseObject.ParseEmpty<Location>(thisTypeName);
    static public new Location Empty {
      get {
        return _empty.Clone<Location>();
      }
    }

    static private readonly Location _unknown = BaseObject.ParseUnknown<Location>(thisTypeName);
    static public new Location Unknown {
      get {
        return _unknown.Clone<Location>();
      }
    }

    #endregion Constructors and parsers

    #region Public properties

    [DataField("GeoItemParentId")]
    public Municipality Municipality {
      get;
      private set;
    }

    protected internal override GeographicRegion Parent {
      get {
        return this.Municipality;
      }
    }

    public FixedList<Settlement> Settlements {
      get {
        return settlementsList.Value.ToFixedList();
      }
    }

    public State State {
      get {
        return this.Municipality.State;
      }
    }

    #endregion Public properties

    #region Public methods

    public Roadway AddRoadway(RoadwayType roadwayType, string name) {
      Assertion.AssertObject(roadwayType, "roadwayType");
      Assertion.AssertObject(name, "name");

      var roadway = new Roadway(this, roadwayType, name);
      roadwaysList.Value.Add(roadway);

      return roadway;
    }

    public Settlement AddSettlement(SettlementType settlementType, string settlementName) {
      Assertion.AssertObject(settlementType, "settlementType");
      Assertion.AssertObject(settlementName, "settlementName");

      var settlement = new Settlement(this, settlementType, settlementName);

      settlementsList.Value.Add(settlement);

      return settlement;
    }

    public void RemoveRoadway(Roadway roadway) {
      Assertion.AssertObject(roadway, "roadway");

      roadway.Remove();
      roadwaysList.Value.Remove(roadway);
    }

    #endregion Public methods

  } // class Location

} // namespace Empiria.Geography
