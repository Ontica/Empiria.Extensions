/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeoItemKind                                    Pattern  : Power type                          *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Serves as a naming space for geographical item types. For example, a Location geographical    *
*              item is defined by a Location powertype instance, but it can also be qualified within it      *
*              using a GeoItemKind 'borough' or 'township', or 'colonia' or 'barrio' in Spanish.             *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

namespace Empiria.Geography {

  /// <summary>Serves as a naming space for geographical item types. For example, a Location geographical
  /// item is defined by a Location powertype instance, but it can also be qualified within it by the 
  /// GeoItemKind 'borough' or 'township', or 'colonia' or 'barrio' in Spanish.</summary>
  public class GeoItemKind : GeneralObject {

    #region Fields

    private const string thisTypeName = "ObjectType.GeneralObject.GeoItemKind";

    #endregion Fields

    #region Constructors and parsers

    protected GeoItemKind(string typeName) : base(typeName) {
      // Empiria Object Type pattern classes always has this constructor. Don't delete
    }

    static public GeoItemKind Parse(int id) {
      return BaseObject.Parse<GeoItemKind>(thisTypeName, id);
    }

    static public GeoItemKind Empty {
      get {
        return BaseObject.ParseEmpty<GeoItemKind>(thisTypeName);
      }
    }

    static public GeoItemKind Unknown {
      get {
        return BaseObject.ParseUnknown<GeoItemKind>(thisTypeName);
      }
    }

    #endregion Constructors and parsers

  } // class GeoItemKind

} // namespace Empiria.Geography
