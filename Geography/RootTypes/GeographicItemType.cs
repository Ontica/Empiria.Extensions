/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeographicItemType                             Pattern  : Power type                          *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : PowerType used to define geographic items like country, state, municiaplity, location,        *
*              highway, roadway, and so on.                                                                  *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

using Empiria.Ontology;

namespace Empiria.Geography {

  /// <summary>PowerType used to define geographic items like country, state, municiaplity, location,
  /// highway, roadway, and so on.</summary>
  public sealed class GeographicItemType : PowerType<GeographicItem> {

    #region Fields

    private const string thisTypeName = "PowerType.GeographicItemType";

    #endregion Fields

    #region Constructors and parsers

    private GeographicItemType(int typeId) : base(thisTypeName, typeId) {
      // Empiria Power type pattern classes always has this constructor. Please don't delete.
    }

    static public new GeographicItemType Parse(int typeId) {
      return PowerType<GeographicItem>.Parse<GeographicItemType>(typeId);
    }

    static internal GeographicItemType Parse(ObjectTypeInfo typeInfo) {
      return PowerType<GeographicItem>.Parse<GeographicItemType>(typeInfo);
    }

    static public GeographicItemType Empty {
      get {
        return
          GeographicItemType.Parse(ObjectTypeInfo.Parse("ObjectType.GeographicItem.Empty"));
      }
    }

    #endregion Constructors and parsers

    #region Public methods

    public new GeographicItem CreateInstance() {
      GeographicItem instance = base.CreateInstance();

      instance.GeographicItemType = this;

      return instance;
    }

    public bool IsTypeOf(GeographicItem geoItem) {
      return (geoItem.ObjectTypeInfo.Equals(this.PartitionedType) ||
              geoItem.ObjectTypeInfo.IsSubclassOf(base.PartitionedType));
    }

    #endregion Public methods

  } // class GeographicItemType

} // namespace Empiria.Geography
