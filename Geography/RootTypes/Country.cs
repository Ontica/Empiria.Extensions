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
using System.Data;

namespace Empiria.Geography {

  /// <summary>Represents a country.</summary>
  public class Country : GeographicRegionItem {

    #region Fields

    private const string thisTypeName = "ObjectType.GeographicItem.GeographicRegionItem.Country";

    #endregion Fields

    #region Constructors and parsers

    protected Country() : base(thisTypeName) {
      // For create instances use GeographicItemType.CreateInstance method instead
    }

    protected Country(string typeName) : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    static public new Country Parse(int id) {
      return BaseObject.Parse<Country>(thisTypeName, id);
    }

    static internal new Country Parse(DataRow row) {
      return BaseObject.Parse<Country>(thisTypeName, row);
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

    static public new FixedList<Country> GetList(string filter) {
      return GeographicData.GetRegions<Country>(filter);
    }

    #endregion Constructors and parsers

    #region Public methods

    public void AddState(State state) {
      var role = base.ObjectTypeInfo.Associations["Country_States"];
      base.Link(role, state);
    }

    public FixedList<State> GetStates() {
      return base.GetLinks<State>("Country_States");
    }

    #endregion Public methods

  } // class Country

} // namespace Empiria.Geography
