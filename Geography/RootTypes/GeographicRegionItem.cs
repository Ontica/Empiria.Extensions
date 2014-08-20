/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeographicRegionItem                           Pattern  : Empiria Object Type                 *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Represents a geographic area or region: city, country, world zone, zip code region, ...       *
*                                                                                                            *
********************************* Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Data;

using Empiria.Contacts;
using Empiria.DataTypes;
using Empiria.Ontology;

namespace Empiria.Geography {

  /// <summary>Represents a geographic area or region: city, country, world zone, zip code region, ...</summary>
  public class GeographicRegionItem : GeographicItem {

    #region Fields

    private const string thisTypeName = "ObjectType.GeographicItem.GeographicRegionItem";

    #endregion Fields

    #region Constructors and parsers

    protected GeographicRegionItem() : base(thisTypeName) {
      // For create instances use GeographicItemType.CreateInstance method instead
    }

    protected GeographicRegionItem(string typeName) : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    static public new GeographicRegionItem Parse(int id) {
      return BaseObject.Parse<GeographicRegionItem>(thisTypeName, id);
    }

    static internal GeographicRegionItem Parse(DataRow row) {
      return BaseObject.Parse<GeographicRegionItem>(thisTypeName, row);
    }

    static public GeographicRegionItem Empty {
      get { return BaseObject.ParseEmpty<GeographicRegionItem>(thisTypeName); }
    }

    static public GeographicRegionItem Unknown {
      get { return BaseObject.ParseUnknown<GeographicRegionItem>(thisTypeName); }
    }

    static public FixedList<GeographicRegionItem> GetList(string filter) {
      return GeographicData.GetRegions(filter);
    }

    #endregion Constructors and parsers

    #region Public properties

    public virtual string CompoundName {
      get { return base.Name + " (" + base.GeographicItemType.DisplayName + ")"; }
    }

    #endregion Public properties

    #region Public methods

    public void AddMember(string roleName, GeographicItem member) {
      TypeAssociationInfo role = base.ObjectTypeInfo.Associations[roleName];
      base.Link(role, member);
    }

    public FixedList<T> GetContacts<T>(string roleName) where T : Contact {
      return base.GetLinks<T>(roleName);
    }

    public FixedList<GeographicPathItem> GetPaths(string pathRoleName) {
      var list = base.GetLinks<GeographicPathItem>(pathRoleName);

      list.Sort((x, y) => x.Name.CompareTo(y.Name));

      return list;
    }

    public FixedList<GeographicPathItem> GetPaths(string pathRoleName,
                                                   GeographicItemType pathItemType) {
      var list = base.GetLinks<GeographicPathItem>(pathRoleName, (x) => (pathItemType.IsTypeOf(x)));

      list.Sort((x, y) => x.Name.CompareTo(y.Name));

      return list;
    }

    public FixedList<GeographicRegionItem> GetRegions(string regionRoleName) {
      var list = base.GetLinks<GeographicRegionItem>(regionRoleName);

      list.Sort((x, y) => x.Name.CompareTo(y.Name));

      return list;
    }

    public FixedList<GeographicRegionItem> GetRegions(string regionRoleName,
                                                       GeographicItemType geoItemType) {
      var list = base.GetLinks<GeographicRegionItem>(regionRoleName, (x) => (geoItemType.IsTypeOf(x)));

      list.Sort((x, y) => x.Name.CompareTo(y.Name));

      return list;
    }

    public FixedList<GeographicRegionItem> GetRegions(string regionRoleName,
                                                       Predicate<GeographicRegionItem> predicate) {
      var list = base.GetLinks<GeographicRegionItem>(regionRoleName, predicate);

      list.Sort((x, y) => x.Name.CompareTo(y.Name));

      return list;
    }

    #endregion Public methods

  } // class GeographicRegionItem

} // namespace Empiria.Geography
