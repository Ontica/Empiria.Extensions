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
using System.Data;

namespace Empiria.Geography {

  /// <summary>Represents a municipality within a country state.</summary>
  public class Location : GeographicRegionItem {

    #region Fields

    private const string thisTypeName = "ObjectType.GeographicItem.GeographicRegionItem.Location";

    #endregion Fields

    #region Constructors and parsers

    protected Location() : base(thisTypeName) {
      // For create instances use GeographicItemType.CreateInstance method instead
    }

    protected Location(string typeName) : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    static public new Location Parse(int id) {
      return BaseObject.Parse<Location>(thisTypeName, id);
    }

    static internal new Location Parse(DataRow row) {
      return BaseObject.Parse<Location>(thisTypeName, row);
    }

    static public new Location Empty {
      get {
        return BaseObject.ParseEmpty<Location>(thisTypeName);
      }
    }

    static public new Location Unknown {
      get {
        return BaseObject.ParseUnknown<Location>(thisTypeName);
      }
    }

    static public new FixedList<Location> GetList(string filter) {
      return GeographicData.GetRegions<Location>(filter);
    }

    #endregion Constructors and parsers

    #region Public properties

    public override string CompoundName {
      get { return base.Name + " (" + base.GeographicItemType.DisplayName + ")"; }
    }

    public Municipality Municipality {
      get;
      private set;
    }

    #endregion Public properties

    #region Public methods

    public void AddSettlement(Settlement settlement) {
      var role = base.ObjectTypeInfo.Associations["Location_Settlements"];
      base.Link(role, settlement);
    }

    public FixedList<Settlement> GetSettlements() {
      return base.GetLinks<Settlement>("Location_Settlements");
    }

    #endregion Public methods

  } // class Location

} // namespace Empiria.Geography
