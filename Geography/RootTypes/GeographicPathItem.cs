/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeographicPathItem                             Pattern  : Empiria Object Type                 *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Represents a geographic path that serves as a base type for streets, roads, avenues, ...      *
*                                                                                                            *
********************************* Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System.Data;

namespace Empiria.Geography {

  /// <summary>Represents a geographic path that serves as a base type for streets, roads, 
  /// avenues, ...</summary>
  public class GeographicPathItem : GeographicItem {

    #region Fields

    private const string thisTypeName = "ObjectType.GeographicItem.GeographicPathItem";

    #endregion Fields

    #region Constructors and parsers

    private GeographicPathItem()
      : base(thisTypeName) {
      // For create instances use GeographicItemType.CreateInstance method instead
    }

    protected GeographicPathItem(string typeName)
      : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    static public new GeographicPathItem Parse(int id) {
      return BaseObject.Parse<GeographicPathItem>(thisTypeName, id);
    }

    static public GeographicPathItem Empty {
      get { return BaseObject.ParseEmpty<GeographicPathItem>(thisTypeName); }
    }

    static public GeographicPathItem Unknown {
      get { return BaseObject.ParseUnknown<GeographicPathItem>(thisTypeName); }
    }

    #endregion Constructors and parsers

    #region Public methods

    protected override void OnSave() {
      base.Keywords = EmpiriaString.BuildKeywords(this.Name, this.ObjectTypeInfo.DisplayName);
      GeographicData.WriteGeographicPathItem(this);
    }

    #endregion Public methods

  } // class GeographicPathItem

} // namespace Empiria.Geography
