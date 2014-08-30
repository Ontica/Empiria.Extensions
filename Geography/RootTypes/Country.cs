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

    private const string thisTypeName = "ObjectType.GeographicItem.GeographicRegion.Country";

    private Lazy<List<State>> countryStatesList = null;

    #endregion Fields

    #region Constructors and parsers

    protected Country(string typeName) : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    public Country(string countryName, string countryCode) : base(thisTypeName, countryName) {
      Assertion.AssertObject(countryName, "countryName");
      Assertion.Assert(countryCode != null, "countryCode");

      this.Code = countryCode;
    }

    protected override void OnInitialize() {
      base.OnInitialize();
      countryStatesList = new Lazy<List<State>>(() => GeographicData.GetChildGeoItems<State>(this));
    }

    static public new Country Parse(int id) {
      return BaseObject.Parse<Country>(thisTypeName, id);
    }

    static public new Country Empty {
      get {
        return BaseObject.ParseEmpty<Country>(thisTypeName);
      }
    }

    static public new Country Unknown {
      get {
        return BaseObject.ParseUnknown<Country>(thisTypeName);
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

    public FixedList<State> States {
      get {
        return countryStatesList.Value.ToFixedList();
      }
    }

    #endregion Public properties

    #region Public methods

    public State AddState(string stateName, string stateCode) {
      Assertion.AssertObject(stateName, "stateName");
      Assertion.Assert(stateCode != null, "stateCode can't be null.");

      var state = new State(this, stateName, stateCode);
      
      countryStatesList.Value.Add(state);      
      
      return state;
    }

    #endregion Public methods

  } // class Country

} // namespace Empiria.Geography
