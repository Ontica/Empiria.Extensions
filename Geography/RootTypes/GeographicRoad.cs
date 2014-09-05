/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeographicRoad                                 Pattern  : Empiria Object Type                 *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Represents a geographic road that could be a roadway, highway or a rural road.                *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Data;

using Empiria.Contacts;
using Empiria.DataTypes;
using Empiria.Ontology;

namespace Empiria.Geography {

  /// <summary>Represents a geographic road that could be a roadway, highway or a rural road.</summary>
  public abstract class GeographicRoad : GeographicItem {

    #region Fields

    private const string thisTypeName = "ObjectType.GeographicItem.GeographicRoad";

    #endregion Fields

    #region Constructors and parsers

    protected GeographicRoad(string typeName) : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise.
    }

    protected GeographicRoad(string typeName, string roadName) : base(typeName, roadName) {

    }

    static public new GeographicRoad Parse(int id) {
      return BaseObject.ParseId<GeographicRoad>(id);
    }

    //static private readonly GeographicRoad _empty = BaseObject.ParseEmpty<GeographicRoad>(thisTypeName);
    //static public GeographicRoad Empty {
    //  get {
    //    return _empty.Clone<GeographicRoad>();
    //  }
    //}

    //static private readonly GeographicRoad _unknown = BaseObject.ParseUnknown<GeographicRoad>(thisTypeName);
    //static public GeographicRoad Unknown {
    //  get {
    //    return _unknown.Clone<GeographicRoad>();
    //  }
    //}

    #endregion Constructors and parsers

  } // class GeographicRoad

} // namespace Empiria.Geography
