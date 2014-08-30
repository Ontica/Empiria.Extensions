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

    #endregion Fields

    #region Constructors and parsers

    protected Settlement() : base(thisTypeName) {
      // For create instances use GeographicItemType.CreateInstance method instead
    }

    protected Settlement(string typeName) : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    internal Settlement(Location location, SettlementType settlementType, string settlementName)
                                                          : base(thisTypeName, settlementName) {
      this.SettlementType = settlementType;
      this.Location = location;
      this.Municipality = location.Municipality;
    }

    internal Settlement(Municipality municipality, SettlementType settlementType, string settlementName)
                                                          : base(thisTypeName, settlementName) {
      this.SettlementType = settlementType;
      this.Municipality = municipality;
      this.Location = Location.Empty;
    }

    static public new Settlement Parse(int id) {
      return BaseObject.Parse<Settlement>(thisTypeName, id);
    }

    static private readonly Settlement _empty = BaseObject.ParseEmpty<Settlement>(thisTypeName);
    static public new Settlement Empty {
      get {
        return _empty.Clone<Settlement>();
      }
    }

    static private readonly Settlement _unknown = BaseObject.ParseUnknown<Settlement>(thisTypeName);
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

    [DataField("GeoItemExtData.SettlementType")]
    public SettlementType SettlementType {
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

    protected override void OnLoadObjectData(DataRow row) {
      base.OnLoadObjectData(row);

      var parent = GeographicRegion.Parse((int) row["GeoItemParentId"]);
      SetMunicipalityAndLocationWithParent(parent);
    }

    private void SetMunicipalityAndLocationWithParent(GeographicRegion parent) {
      if (parent.IsEmptyInstance && this.IsSpecialCase) {
        this.Municipality = Municipality.Empty;
        this.Location = Location.Empty;
      } else if (parent is Municipality) {
        this.Municipality = (Municipality) parent;
        this.Location = Location.Empty;
      } else {
        throw Assertion.AssertNoReachThisCode();
      }
    }

    #endregion Public methods

  } // class Settlement

} // namespace Empiria.Geography
