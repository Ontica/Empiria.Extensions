/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : Municipality                                   Pattern  : Empiria Object Type                 *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Represents a municipality within a country state.                                             *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Data;

namespace Empiria.Geography {

  /// <summary>Represents a municipality within a country state.</summary>
  public class Municipality : GeographicRegionItem {

    #region Fields

    private const string thisTypeName = "ObjectType.GeographicItem.GeographicRegionItem.Municipality";

    #endregion Fields

    #region Constructors and parsers

    protected Municipality() : base(thisTypeName) {
      // For create instances use GeographicItemType.CreateInstance method instead
    }

    protected Municipality(string typeName) : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    static public new Municipality Parse(int id) {
      return BaseObject.Parse<Municipality>(thisTypeName, id);
    }

    static internal new Municipality Parse(DataRow row) {
      return BaseObject.Parse<Municipality>(thisTypeName, row);
    }

    static public new Municipality Empty {
      get {
        return BaseObject.ParseEmpty<Municipality>(thisTypeName);
      }
    }

    static public new Municipality Unknown {
      get {
        return BaseObject.ParseUnknown<Municipality>(thisTypeName);
      }
    }

    static public new FixedList<Municipality> GetList(string filter) {
      return GeographicData.GetRegions<Municipality>(filter);
    }

    #endregion Constructors and parsers

    #region Public properties

    public override string CompoundName {
      get { return base.Name + " (" + base.GeographicItemType.DisplayName + ")"; }
    }

    public State State {
      get;
      private set;
    }

    #endregion Public properties

    #region Public methods

    public void AddLocation(Location location) {
      var role = base.ObjectTypeInfo.Associations["Municipality_Locations"];
      base.Link(role, location);
    }

    public FixedList<Location> GetLocations() {
      return base.GetLinks<Location>("Municipality_Locations");
    }

    #endregion Public methods

  } // class Municipality

} // namespace Empiria.Geography
