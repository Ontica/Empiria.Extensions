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
    internal Highway(Country country, FederalHighwayType highwayType, string highwayNumber, 
                     HighwaySection fromOriginToDestination)
      : base(federalHighwayTypeName, BuildName(highwayType, highwayNumber, fromOriginToDestination)) {
      this.Region = country;
      this.HighwayType = highwayType;
    }

    /// <summary>Creates a state highway without an official designated number.</summary>
    internal Highway(State state, StateHighwayType highwayType, HighwaySection fromOriginToDestination)
      : base(stateHighwayTypeName, BuildName(highwayType, String.Empty, fromOriginToDestination)) {
      this.Region = state;
      this.HighwayType = highwayType;
    }

    /// <summary>Creates a state highway with an official highway number.</summary>
    internal Highway(State state, StateHighwayType highwayType, string highwayNumber,
                     HighwaySection fromOriginToDestination)
      : base(stateHighwayTypeName, BuildName(highwayType, highwayNumber, fromOriginToDestination)) {
      this.Region = state;
      this.HighwayType = highwayType;
    }

    /// <summary>Creates a state rural highway.</summary>
    internal Highway(State state, RuralHighwayType ruralHighwayType,
                     HighwaySection fromOriginToDestination)
      : base(ruralHigwayTypeName, BuildName(ruralHighwayType, String.Empty, 
                                            fromOriginToDestination)) {
      this.Region = state;
      this.HighwayType = ruralHighwayType;
    }

    /// <summary>Creates a municipal rural highway.</summary>
    internal Highway(Municipality municipality, RuralHighwayType ruralHighwayType,
                     HighwaySection fromOriginToDestination)
      : base(ruralHigwayTypeName, BuildName(ruralHighwayType, String.Empty, 
                                            fromOriginToDestination)) {
      this.Region = municipality;
      this.HighwayType = ruralHighwayType;
    }

    /// <summary>Creates a municipal highway.</summary>
    internal Highway(Municipality municipality, MunicipalHighwayType municipalHighwayType,
                     HighwaySection fromOriginToDestination)
      : base(municipalHighwayTypeName, BuildName(municipalHighwayType, String.Empty, 
                                                 fromOriginToDestination)) {
      this.Region = municipality;
      this.HighwayType = municipalHighwayType;
    }

    static public new Highway Parse(int id) {
      return BaseObject.Parse<Highway>(thisTypeName, id);
    }

    //static private readonly Highway _empty = BaseObject.ParseEmpty<Highway>(thisTypeName);
    //static public new Highway Empty {
    //  get {
    //    return _empty.Clone<Highway>();
    //  }
    //}

    //static private readonly Highway _unknown = BaseObject.ParseUnknown<Highway>(thisTypeName);

    //static public new Highway Unknown {
    //  get {
    //    return _unknown.Clone<Highway>();
    //  }
    //}

    #endregion Constructors and parsers

    #region Public properties

    public IHighwayType HighwayType {
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
      this.HighwayType = this.ParseHighwayType(json.Get<string>("HighwayType", "No determinado"));
      this.highwaySectionsList = json.GetList<HighwaySection>("Sections");
    }

    #endregion Public methods

    #region Private methods

    static private string BuildName(IHighwayType highwayType, string highwayNumber,
                                    HighwaySection fromOriginToDestination) {
      Assertion.AssertObject(highwayType, "highwayType");
      Assertion.Assert(highwayNumber != null, "highwayNumber can't be null.");
      Assertion.AssertObject(fromOriginToDestination, "fromOriginToDestination");
      Assertion.Assert(highwayNumber.Length != 0 || !fromOriginToDestination.IsEmptyValue,
                       "To build the highway name I need at least its number or its main section.");

      if (highwayNumber.Length != 0 && !fromOriginToDestination.IsEmptyValue) {
        return highwayType + " " + highwayNumber + " " + fromOriginToDestination;
      } else if (highwayNumber.Length != 0 && fromOriginToDestination.IsEmptyValue) {
        return highwayType + " " + highwayNumber;
      } else if (highwayNumber.Length == 0 && !fromOriginToDestination.IsEmptyValue) {
        return highwayType + " " + fromOriginToDestination;
      } else {    //highwayNumber.Length == 0 && fromOriginToDestination.IsEmptyValue
        throw Assertion.AssertNoReachThisCode();
      }
    }

    /// <summary>Factory method for the HighwayType.</summary>
    private IHighwayType ParseHighwayType(string highwayName) {
      Assertion.AssertObject(highwayName, "highwayName");

      switch (this.GeographicItemType.Name) {
        case federalHighwayTypeName:
          return FederalHighwayType.Parse(highwayName);
        case stateHighwayTypeName:
          return StateHighwayType.Parse(highwayName);
        case municipalHighwayTypeName:
          return MunicipalHighwayType.Parse(highwayName);
        case ruralHigwayTypeName:
          return RuralHighwayType.Parse(highwayName);
        default:
          throw Assertion.AssertNoReachThisCode();
      }
    }

    #endregion Private methods

  } // class Highway

} // namespace Empiria.Geography
