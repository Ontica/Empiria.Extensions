/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Geographic Data Services            *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : HighwayType                                    Pattern  : Power type                          *
*  Version   : 6.5                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Power type used to define highways types like federal, state or rural highways types.         *
*                                                                                                            *
********************************* Copyright (c) 2009-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

using Empiria.Ontology;

namespace Empiria.Geography {

  /// <summary>Power type used to define highways types like federal, state or rural highways types.</summary>
  [Powertype(typeof(Highway))]
  public sealed class HighwayType : Powertype {

    #region Constructors and parsers

    private HighwayType() {
      // Empiria powertype types always have this constructor.
    }

    static public new HighwayType Parse(int typeId) {
      return ObjectTypeInfo.Parse<HighwayType>(typeId);
    }

    static public new HighwayType Parse(string typeName) {
      return ObjectTypeInfo.Parse<HighwayType>(typeName);
    }

    static public HighwayType FederalHighwayType {
      get {
        return HighwayType.Parse("ObjectType.GeographicItem.GeographicRoad.Highway.FederalHighway");
      }
    }

    static public HighwayType MunicipalHighwayType {
      get {
        return HighwayType.Parse("ObjectType.GeographicItem.GeographicRoad.Highway.MunicipalHighway");
      }
    }

    static public HighwayType RuralHighwayType {
      get {
        return HighwayType.Parse("ObjectType.GeographicItem.GeographicRoad.Highway.RuralHighway");
      }
    }

    static public HighwayType StateHighwayType {
      get {
        return HighwayType.Parse("ObjectType.GeographicItem.GeographicRoad.Highway.StateHighway");
      }
    }

    #endregion Constructors and parsers

    #region Public methods

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
