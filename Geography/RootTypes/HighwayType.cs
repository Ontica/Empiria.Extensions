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
  public sealed class HighwayType : Powertype<Highway> {

    #region Fields

    private const string federalHighwayTypeName = "ObjectType.GeographicItem.GeographicRoad.Highway.FederalHighway";
    private const string stateHighwayTypeName = "ObjectType.GeographicItem.GeographicRoad.Highway.StateHighway";
    private const string municipalHighwayTypeName = "ObjectType.GeographicItem.GeographicRoad.Highway.MunicipalHighway";
    private const string ruralHigwayTypeName = "ObjectType.GeographicItem.GeographicRoad.Highway.RuralHighway";

    private const string thisTypeName = "PowerType.GeographicItemType";

    #endregion Fields

    #region Constructors and parsers

    private HighwayType(int typeId) : base(thisTypeName, typeId) {
      // Empiria Powertype classes always has this constructor.
    }

    static public new HighwayType Parse(int typeId) {
      return Powertype<Highway>.Parse<HighwayType>(typeId);
    }

    static internal HighwayType Parse(ObjectTypeInfo typeInfo) {
      return Powertype<Highway>.Parse<HighwayType>(typeInfo);
    }

    static public HighwayType Empty {
      get {
        return
          HighwayType.Parse(ObjectTypeInfo.Parse("ObjectType.GeographicItem.Empty"));
      }
    }

    #endregion Constructors and parsers

    #region Public methods

    static internal HighwayType FederalHighwayType {
      get {
        return HighwayType.Parse(ObjectTypeInfo.Parse(federalHighwayTypeName));
      }
    }

    static internal HighwayType MunicipalHighwayType {
      get {
        return HighwayType.Parse(ObjectTypeInfo.Parse(municipalHighwayTypeName));
      }
    }

    static internal HighwayType RuralHighwayType {
      get {
        return HighwayType.Parse(ObjectTypeInfo.Parse(ruralHigwayTypeName));
      }
    }

    static internal HighwayType StateHighwayType {
      get {
        return HighwayType.Parse(ObjectTypeInfo.Parse(stateHighwayTypeName));
      }
    }

    /// <summary>Factory method for this type HighwayKind.</summary>
    internal IHighwayKind ParseHighwayKind(string highwayKindName) {
      Assertion.AssertObject(highwayKindName, "highwayName");

      if (this.Equals(HighwayType.FederalHighwayType)) {
        return FederalHighwayKind.Parse(highwayKindName);
      } else if (this.Equals(HighwayType.StateHighwayType)) {
        return StateHighwayKind.Parse(highwayKindName);
      } else if (this.Equals(HighwayType.MunicipalHighwayType)) {
        return MunicipalHighwayKind.Parse(highwayKindName);
      } else if (this.Equals(HighwayType.RuralHighwayType)) {
        return RuralHighwayKind.Parse(highwayKindName);
      } else {
        throw Assertion.AssertNoReachThisCode();
      }
    }

    #endregion Public methods

  } // class HighwayType

} // namespace Empiria.Geography
