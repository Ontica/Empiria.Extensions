/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : Road                                           Pattern  : Empiria Object Type                 *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Represents a road.                                                                            *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Data;

namespace Empiria.Geography {

  /// <summary>Represents a country.</summary>
  public class Road : GeographicPathItem {

    #region Fields

    private const string thisTypeName = "ObjectType.GeographicItem.GeographicPathItem.Road";

    #endregion Fields

    #region Constructors and parsers

    protected Road() : base(thisTypeName) {
      // For create instances use GeographicItemType.CreateInstance method instead
    }

    protected Road(string typeName) : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    static public new Road Parse(int id) {
      return BaseObject.Parse<Road>(thisTypeName, id);
    }

    static internal Road Parse(DataRow row) {
      return BaseObject.Parse<Road>(thisTypeName, row);
    }

    static public new Road Empty {
      get {
        return BaseObject.ParseEmpty<Road>(thisTypeName);
      }
    }

    static public new Road Unknown {
      get {
        return BaseObject.ParseUnknown<Road>(thisTypeName);
      }
    }

    static public FixedList<Road> GetList(State state, string filter) {
      return GeographicData.GetRoads<Road>(filter);
    }
    
    static public FixedList<Road> GetList(Municipality municipality, string filter) {
      return GeographicData.GetRoads<Road>(filter);
    }

    static public FixedList<Road> GetList(Location location, string filter) {
      return GeographicData.GetRoads<Road>(filter);
    }

    static public FixedList<Road> GetList(Settlement settlement, string filter) {
      return GeographicData.GetRoads<Road>(filter);
    }

    #endregion Constructors and parsers

  } // class Road

} // namespace Empiria.Geography
