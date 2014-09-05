/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : Highway                                        Pattern  : Empiria Object Type                 *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Represents a federal, state, municipal or rural highway.                                      *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;
using System.Data;

namespace Empiria.Geography {

  /// <summary>Represents a federal, state, municipal or rural highway.</summary>
  public class Highway : GeographicRoad {

    #region Fields

    private const string thisTypeName = "ObjectType.GeographicItem.GeographicRoad.Highway";
    private const string federalHighwayTypeName = "ObjectType.GeographicItem.GeographicRoad.Highway.FederalHighway";
    private const string stateHighwayTypeName = "ObjectType.GeographicItem.GeographicRoad.Highway.StateHighway";
    private const string municipalHighwayTypeName = "ObjectType.GeographicItem.GeographicRoad.Highway.MunicipalHighway";
    private const string ruralHigwayTypeName = "ObjectType.GeographicItem.GeographicRoad.Highway.RuralHighway";

    private List<HighwaySection> highwaySectionsList = new List<HighwaySection>();

    #endregion Fields

    #region Constructors and parsers

    protected Highway(string typeName) : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    /// <summary>Creates a federal highway that typically cross two or more states.</summary>
    internal Highway(Country country, FederalHighwayKind highwayKind, string highwayNumber, 
                     HighwaySection fromOriginToDestination)
      : base(federalHighwayTypeName, BuildName(highwayKind, highwayNumber, fromOriginToDestination)) {
      this.Region = country;
      this.HighwayKind = highwayKind;
    }

    /// <summary>Creates a state highway without an official designated number.</summary>
    internal Highway(State state, StateHighwayKind highwayKind, HighwaySection fromOriginToDestination)
      : base(stateHighwayTypeName, BuildName(highwayKind, String.Empty, fromOriginToDestination)) {
      this.Region = state;
      this.HighwayKind = highwayKind;
    }

    /// <summary>Creates a state highway with an official highway number.</summary>
    internal Highway(State state, StateHighwayKind stateHighwayKind, string highwayNumber,
                     HighwaySection fromOriginToDestination)
      : base(stateHighwayTypeName, BuildName(stateHighwayKind, highwayNumber, fromOriginToDestination)) {
      this.Region = state;
      this.HighwayKind = stateHighwayKind;
    }

    /// <summary>Creates a state rural highway.</summary>
    internal Highway(State state, RuralHighwayKind ruralHighwayKind,
                     HighwaySection fromOriginToDestination)
      : base(ruralHigwayTypeName, BuildName(ruralHighwayKind, String.Empty, 
                                            fromOriginToDestination)) {
      this.Region = state;
      this.HighwayKind = ruralHighwayKind;
    }

    /// <summary>Creates a municipal rural highway.</summary>
    internal Highway(Municipality municipality, RuralHighwayKind ruralHighwayKind,
                     HighwaySection fromOriginToDestination)
      : base(ruralHigwayTypeName, BuildName(ruralHighwayKind, String.Empty, 
                                            fromOriginToDestination)) {
      this.Region = municipality;
      this.HighwayKind = ruralHighwayKind;
    }

    /// <summary>Creates a municipal highway.</summary>
    internal Highway(Municipality municipality, MunicipalHighwayKind municipalHighwayKind,
                     HighwaySection fromOriginToDestination)
      : base(municipalHighwayTypeName, BuildName(municipalHighwayKind, String.Empty, 
                                                 fromOriginToDestination)) {
      this.Region = municipality;
      this.HighwayKind = municipalHighwayKind;
    }

    static public new Highway Parse(int id) {
      return BaseObject.ParseId<Highway>(id);
    }

    #endregion Constructors and parsers

    #region Public properties

    public IHighwayKind HighwayKind {
      get;
      private set;
    }

    protected internal override GeographicRegion Parent {
      get { return this.Region; }
    }

    [DataField("GeoItemParentId")]
    public GeographicRegion Region {
      get;
      private set;
    }

    public FixedList<HighwaySection> Sections {
      get {
        return highwaySectionsList.ToFixedList();
      }
    }

    #endregion Public properties

    #region Public methods

    /// <summary>Adds a highway stretch or section as a named segment of this highway.</summary>
    public HighwaySection AddSection(string originPlace, string destinationPlace) {
      Assertion.AssertObject(originPlace, "originPlace");
      Assertion.AssertObject(destinationPlace, "destinationPlace");

      var section = new HighwaySection(originPlace, destinationPlace);
      highwaySectionsList.Add(section);

      return section;
    }

    protected override void OnLoadObjectData(DataRow row) {
      base.OnLoadObjectData(row);
      var json = Empiria.Data.JsonObject.Parse(base.ExtendedDataString);
      this.HighwayKind = this.ParseHighwayKind(json.Get<string>("HighwayKind", "No determinado"));
      this.highwaySectionsList = json.GetList<HighwaySection>("Sections", false);
    }

    #endregion Public methods

    #region Private methods

    static private string BuildName(IHighwayKind highwayKind, string highwayNumber,
                                    HighwaySection fromOriginToDestination) {
      Assertion.AssertObject(highwayKind, "highwayKind");
      Assertion.Assert(highwayNumber != null, "highwayNumber can't be null.");
      Assertion.AssertObject(fromOriginToDestination, "fromOriginToDestination");
      Assertion.Assert(highwayNumber.Length != 0 || !fromOriginToDestination.IsEmptyValue,
                       "To build the highway name I need at least its number or its main section.");

      if (highwayNumber.Length != 0 && !fromOriginToDestination.IsEmptyValue) {
        return highwayKind + " " + highwayNumber + " " + fromOriginToDestination;
      } else if (highwayNumber.Length != 0 && fromOriginToDestination.IsEmptyValue) {
        return highwayKind + " " + highwayNumber;
      } else if (highwayNumber.Length == 0 && !fromOriginToDestination.IsEmptyValue) {
        return highwayKind + " " + fromOriginToDestination;
      } else {    //highwayNumber.Length == 0 && fromOriginToDestination.IsEmptyValue
        throw Assertion.AssertNoReachThisCode();
      }
    }

    /// <summary>Factory method for this instance HighwayKind.</summary>
    private IHighwayKind ParseHighwayKind(string highwayKindName) {
      Assertion.AssertObject(highwayKindName, "highwayName");

      switch (this.GeographicItemType.Name) {
        case federalHighwayTypeName:
          return FederalHighwayKind.Parse(highwayKindName);
        case stateHighwayTypeName:
          return StateHighwayKind.Parse(highwayKindName);
        case municipalHighwayTypeName:
          return MunicipalHighwayKind.Parse(highwayKindName);
        case ruralHigwayTypeName:
          return RuralHighwayKind.Parse(highwayKindName);
        default:
          throw Assertion.AssertNoReachThisCode();
      }
    }

    #endregion Private methods

  } // class Highway

} // namespace Empiria.Geography
