/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : State                                          Pattern  : Empiria Object Type                 *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Represents a state within a country.                                                          *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Empiria.Geography {

  /// <summary>Represents a state within a country.</summary>
  public class State : GeographicRegion {

    #region Fields

    private const string thisTypeName = "ObjectType.GeographicItem.GeographicRegion.State";
    private Lazy<List<Municipality>> municipalitiesList = null;

    #endregion Fields

    #region Constructors and parsers

    protected State(string typeName) : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    internal State(Country country, string stateName, string stateCode): base(thisTypeName, stateName) {
      this.Country = country;
      this.Code = stateCode;
    }

    protected override void OnInitialize() {
      base.OnInitialize();
      municipalitiesList = new Lazy<List<Municipality>>(() => GeographicData.GetChildGeoItems<Municipality>(this));
    }

    static public new State Parse(int id) {
      return BaseObject.Parse<State>(thisTypeName, id);
    }

    static private readonly State _empty = BaseObject.ParseEmpty<State>(thisTypeName);
    static public new State Empty {
      get {
        return _empty.Clone<State>();
      }
    }

    static private readonly State _unknown = BaseObject.ParseUnknown<State>(thisTypeName);
    static public new State Unknown {
      get {
        return _unknown.Clone<State>();
      }
    }

    #endregion Constructors and parsers

    #region Public properties

    [DataField("GeoItemExtData.Code")]
    public string Code {
      get;
      private set;
    }

    [DataField("GeoItemParentId")]
    public Country Country {
      get;
      private set;
    }

    [DataField("GeoItemExtData.PostalCodesRegEx")]
    public string PostalCodesPattern {
      get;
      private set;
    }

    public FixedList<Municipality> Municipalities {
      get {
        return municipalitiesList.Value.ToFixedList();
      }
    }

    protected internal override GeographicRegion Parent {
      get {
        return this.Country;
      }
    }

    #endregion Public properties

    #region Public methods

    public Municipality AddMunicipality(string municipalityName) {
      Assertion.AssertObject(municipalityName, "municipalityName");

      var municipality = new Municipality(this, municipalityName);

      municipalitiesList.Value.Add(municipality);

      return municipality;
    }

    internal void AssertPostalCodeIsValid(string value) {
      Assertion.Assert(value != null, "value can't be null");
      if (value.Length == 0 || this.PostalCodesPattern.Length == 0) {
        return;
      }
      if (!Regex.IsMatch(value, this.PostalCodesPattern)) {
        throw new GeographyException(GeographyException.Msg.InvalidPostalCode, value, this.Name);
      }
    }

    #endregion Public methods

  } // class State

} // namespace Empiria.Geography
