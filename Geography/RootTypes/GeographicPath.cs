/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeographicPath                                 Pattern  : Empiria Object Type                 *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Represents a geographic path that serves as a base type for streets, roads, avenues, ...      *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System.Data;

namespace Empiria.Geography {

  /// <summary>Represents a geographic path that serves as a base type for streets, roads, 
  /// avenues, ...</summary>
  public class GeographicPath : GeographicItem {

    #region Fields

    private const string thisTypeName = "ObjectType.GeographicItem.GeographicPath";

    #endregion Fields

    #region Constructors and parsers

    private GeographicPath() : base(thisTypeName) {
      // For create instances use GeographicItemType.CreateInstance method instead
    }

    protected GeographicPath(string typeName) : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    static public new GeographicPath Parse(int id) {
      return BaseObject.Parse<GeographicPath>(thisTypeName, id);
    }

    static private readonly GeographicPath _empty = BaseObject.ParseEmpty<GeographicPath>(thisTypeName);
    static public GeographicPath Empty {
      get {
        return _empty.Clone<GeographicPath>();
      }
    }

    static private readonly GeographicPath _unknown = BaseObject.ParseUnknown<GeographicPath>(thisTypeName);
    static public GeographicPath Unknown {
      get {
        return _unknown.Clone<GeographicPath>();
      }
    }

    #endregion Constructors and parsers

  } // class GeographicPath

} // namespace Empiria.Geography
