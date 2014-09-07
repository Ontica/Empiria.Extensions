/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : Country                                        Pattern  : Empiria Object Type                 *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Represents a country.                                                                         *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;

namespace Empiria.Geography {

  /// <summary>Represents a country.</summary>
  public class Country : GeographicRegion {

    #region Fields

    private Lazy<List<State>> statesList = null;
    private Lazy<List<Highway>> highwaysList = null;

    #endregion Fields

    #region Constructors and parsers

    private Country() {
      // Required by Empiria Framework.
    }

    public Country(string countryName, string countryCode) : base(countryName) {
      Assertion.AssertObject(countryName, "countryName");
      Assertion.Assert(countryCode != null, "countryCode");

      this.Code = countryCode;
    }

    protected override void OnInitialize() {
      base.OnInitialize();
      statesList = new Lazy<List<State>>(() => GeographicData.GetChildGeoItems<State>(this));
      highwaysList = new Lazy<List<Highway>>(() => GeographicData.GetChildGeoItems<Highway>(this));
    }

    static public new Country Parse(int id) {
      return BaseObject.ParseId<Country>(id);
    }

    static public new Country Empty {
      get {
        return BaseObject.ParseEmpty<Country>();
      }
    }

    static public new Country Unknown {
      get {
        return BaseObject.ParseUnknown<Country>();
      }
    }

    static public FixedList<Country> GetList() {
      return GeographicItem.GetList<Country>();
    }

    #endregion Constructors and parsers

    #region Public properties

    [DataField("GeoItemExtData.Code")]
    public string Code {
      get;
      private set;
    }

    public FixedList<Highway> Highways {
      get {
        return highwaysList.Value.ToFixedList();
      }
    }

    public FixedList<State> States {
      get {
        return statesList.Value.ToFixedList();
      }
    }

    #endregion Public properties

    #region Public methods

    public Highway AddHighway(FederalHighwayKind highwayKind, string number,
                              HighwaySection fromOriginToDestination) {
      Assertion.AssertObject(highwayKind, "highwayKind");
      Assertion.AssertObject(number, "number");
      Assertion.AssertObject(fromOriginToDestination, "fromOriginToDestination");

      var highway = new Highway(this, highwayKind, number, fromOriginToDestination);
      highwaysList.Value.Add(highway);

      return highway;
    }

    public State AddState(string stateName, string stateCode) {
      Assertion.AssertObject(stateName, "stateName");
      Assertion.Assert(stateCode != null, "stateCode can't be null.");

      var state = new State(this, stateName, stateCode);
      statesList.Value.Add(state);

      return state;
    }

    public void RemoveHighway(Highway highway) {
      Assertion.AssertObject(highway, "highway");

      highway.Remove();
      highwaysList.Value.Remove(highway);
    }

    #endregion Public methods

  } // class Country

} // namespace Empiria.Geography
