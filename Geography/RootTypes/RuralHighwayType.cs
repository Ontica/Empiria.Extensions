/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : RuralHighwayType                               Pattern  : Value object                        *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : String value type that describes a kind of rural highway.                                     *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Linq;

using Empiria.Ontology;

namespace Empiria.Geography {

  /// <summary>String value type that describes a kind of rural highway.</summary>
  public class RuralHighwayType : ValueObject<string>, IHighwayType {

    #region Fields

    private const string thisTypeName = "ValueType.ListItem.RuralHighwayType";

    static private FixedList<RuralHighwayType> valuesList =
      RuralHighwayType.ValueTypeInfo.GetValuesList<RuralHighwayType, string>((x) => new RuralHighwayType(x));

    #endregion Fields

    #region Constructors and parsers

    private RuralHighwayType(string value) : base(value) {

    }

    static public RuralHighwayType Parse(string value) {
      Assertion.AssertObject(value, "value");

      if (value == RuralHighwayType.Empty.Value) {
        return RuralHighwayType.Empty;
      }
      if (value == RuralHighwayType.Unknown.Value) {
        return RuralHighwayType.Unknown;
      }
      return valuesList.First((x) => x.Value == value);
    }

    static public RuralHighwayType Empty {
      get {
        RuralHighwayType empty = new RuralHighwayType("No determinado");
        empty.MarkAsEmpty();

        return empty;
      }
    }

    static public RuralHighwayType Unknown {
      get {
        RuralHighwayType unknown = new RuralHighwayType("No proporcionado");
        unknown.MarkAsUnknown();

        return unknown;
      }
    }

    static public ValueTypeInfo ValueTypeInfo {
      get {
        return ValueTypeInfo.Parse(thisTypeName);
      }
    }

    static public FixedList<RuralHighwayType> GetList() {
      return valuesList;
    }

    #endregion Constructors and parsers

  } // class RuralHighwayType

} // namespace Empiria.Geography
