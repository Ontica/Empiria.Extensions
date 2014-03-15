/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeographicItemType                             Pattern  : Power type                          *
*  Version   : 5.5        Date: 28/Mar/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : PowerType that defines a geographic item type like CountryType, BorroughType or StreetType.   *
*                                                                                                            *
********************************* Copyright (c) 2009-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

using Empiria.Ontology;

namespace Empiria.Geography {

  /// <summary>PowerType that defines a geographic item type like CountryType, BorroughType 
  /// or StreetType.</summary>
  public sealed class GeographicItemType : PowerType<GeographicItem> {

    #region Fields

    private const string thisTypeName = "PowerType.GeographicItemType";

    #endregion Fields

    #region Constructors and parsers

    private GeographicItemType(int typeId) : base(thisTypeName, typeId) {
      // Empiria Power type pattern classes always has this constructor. Don't delete
    }

    static public new GeographicItemType Parse(int typeId) {
      return PowerType<GeographicItem>.Parse<GeographicItemType>(typeId);
    }

    static internal GeographicItemType Parse(ObjectTypeInfo typeInfo) {
      return PowerType<GeographicItem>.Parse<GeographicItemType>(typeInfo);
    }

    public ObjectList<GeographicRegionItem> GetList() {
      return GeographicData.GetRegions("GeoItemTypeId = " + this.Id.ToString());
    }

    #endregion Constructors and parsers

    #region Public methods

    public bool IsTypeOf(GeographicItem geoItem) {
      return (geoItem.ObjectTypeInfo.Equals(this.PartitionedType) ||
              geoItem.ObjectTypeInfo.IsSubclassOf(base.PartitionedType));
    }

    #endregion Public methods

  } // class GeographicItemType

} // namespace Empiria.Geography