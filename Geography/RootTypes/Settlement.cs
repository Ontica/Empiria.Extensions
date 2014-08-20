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
using System.Data;

namespace Empiria.Geography {

  /// <summary>Represents a settlement within a location or municipality.</summary>
  public class Settlement : GeographicRegionItem {

    #region Fields

    private const string thisTypeName = "ObjectType.GeographicItem.GeographicRegionItem.Settlement";

    #endregion Fields

    #region Constructors and parsers

    protected Settlement() : base(thisTypeName) {
      // For create instances use GeographicItemType.CreateInstance method instead
    }

    protected Settlement(string typeName) : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    static public new Settlement Parse(int id) {
      return BaseObject.Parse<Settlement>(thisTypeName, id);
    }

    static internal new Settlement Parse(DataRow row) {
      return BaseObject.Parse<Settlement>(thisTypeName, row);
    }

    static public new Settlement Empty {
      get {
        return BaseObject.ParseEmpty<Settlement>(thisTypeName);
      }
    }

    static public new Settlement Unknown {
      get {
        return BaseObject.ParseUnknown<Settlement>(thisTypeName);
      }
    }

    static public new FixedList<Settlement> GetList(string filter) {
      return GeographicData.GetRegions<Settlement>(filter);
    }

    #endregion Constructors and parsers

    #region Public properties

    public override string CompoundName {
      get { return base.Name + " (" + base.GeographicItemType.DisplayName + ")"; }
    }

    public Location Location {
      get;
      private set;
    }

    #endregion Public properties

    #region Public methods

    public void AddSettlement(Location location) {
      var role = base.ObjectTypeInfo.Associations["Settlement_Locations"];
      base.Link(role, location);
    }

    public FixedList<Location> GetSettlements() {
      return base.GetLinks<Location>("Settlement_Locations");
    }

    #endregion Public methods

  } // class Settlement

} // namespace Empiria.Geography
