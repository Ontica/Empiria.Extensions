﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Geographic Data Services            *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : Country                                        Pattern  : Empiria Object Type                 *
*  Version   : 6.8                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Represents a country.                                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using Empiria.StateEnums;

namespace Empiria.Geography {

  /// <summary>Represents a country.</summary>
  public class Country : GeographicRegion {

    #region Fields

    private Lazy<List<State>> statesList = new Lazy<List<State>>();
    private Lazy<List<Highway>> highwaysList = new Lazy<List<Highway>>();

    #endregion Fields

    #region Constructors and parsers

    private Country() {
      // Required by Empiria Framework.
    }

    public Country(string countryName, string countryCode) : base(countryName) {
      Assertion.Require(countryName, "countryName");
      Assertion.Require(countryCode != null, "countryCode");

      this.Code = countryCode;
    }

    protected override void OnLoad() {
      statesList = new Lazy<List<State>>(() => GeographicData.GetChildGeoItems<State>(this));
      highwaysList = new Lazy<List<Highway>>(() => GeographicData.GetChildGeoItems<Highway>(this));
    }

    static public new Country Parse(int id) {
      return BaseObject.ParseId<Country>(id);
    }

    static private readonly Country _empty = BaseObject.ParseEmpty<Country>();
    static public new Country Empty {
      get {
        return _empty.Clone<Country>();
      }
    }

    static public new Country Unknown {
      get {
        return BaseObject.ParseUnknown<Country>();
      }
    }

    static public FixedList<Country> GetList() {
      return GeographicItem.GetList<Country>().ToFixedList();
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
        Predicate<Highway> match = (x) => x.Status != EntityStatus.Deleted;
        return highwaysList.Value.FindAll(match).ToFixedList();
      }
    }

    public FixedList<State> States {
      get {
        Predicate<State> match = (x) => x.Status != EntityStatus.Deleted;
        return statesList.Value.FindAll(match).ToFixedList();
      }
    }

    #endregion Public properties

    #region Public methods

    public Highway AddHighway(FederalHighwayKind highwayKind, string number,
                              HighwaySection fromOriginToDestination) {
      Assertion.Require(highwayKind, "highwayKind");
      Assertion.Require(number, "number");
      Assertion.Require(fromOriginToDestination, "fromOriginToDestination");

      var highway = new Highway(this, highwayKind, number, fromOriginToDestination);
      highwaysList.Value.Add(highway);

      return highway;
    }

    public State AddState(string stateName, string stateCode) {
      Assertion.Require(stateName, "stateName");
      Assertion.Require(stateCode != null, "stateCode can't be null.");

      var state = new State(this, stateName, stateCode);
      statesList.Value.Add(state);

      return state;
    }

    public void RemoveHighway(Highway highway) {
      Assertion.Require(highway, "highway");

      highway.Remove();
      highwaysList.Value.Remove(highway);
    }

    protected override void OnSave() {
      using (var context = StorageContext.Open()) {
        base.OnSave();
        foreach (State state in statesList.Value) {
          state.Save();
        }
        foreach (Highway highway in highwaysList.Value) {
          highway.Save();
        }
        context.Update();
      }
    }

    #endregion Public methods

  } // class Country

} // namespace Empiria.Geography
