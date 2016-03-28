/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Geographic Data Services            *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : RuralHighwayKind                               Pattern  : Value object                        *
*  Version   : 6.7                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : String value type that describes a kind of rural highway.                                     *
*                                                                                                            *
********************************* Copyright (c) 2009-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Linq;

using Empiria.Ontology;

namespace Empiria.Geography {

  /// <summary>String value type that describes a kind of rural highway.</summary>
  public class RuralHighwayKind : ValueObject<string>, IHighwayKind {

    #region Fields

    private const string thisTypeName = "ValueType.ListItem.RuralHighwayKind";

    static private FixedList<RuralHighwayKind> valuesList =
      RuralHighwayKind.ValueTypeInfo.GetValuesList<RuralHighwayKind, string>((x) => new RuralHighwayKind(x));

    #endregion Fields

    #region Constructors and parsers

    private RuralHighwayKind(string value) : base(value) {

    }

    static public RuralHighwayKind Parse(string value) {
      Assertion.AssertObject(value, "value");

      if (value == RuralHighwayKind.Empty.Value) {
        return RuralHighwayKind.Empty;
      }
      if (value == RuralHighwayKind.Unknown.Value) {
        return RuralHighwayKind.Unknown;
      }
      return valuesList.First((x) => x.Value == value);
    }

    static public RuralHighwayKind Empty {
      get {
        RuralHighwayKind empty = new RuralHighwayKind("No determinado");
        empty.MarkAsEmpty();

        return empty;
      }
    }

    static public RuralHighwayKind Unknown {
      get {
        RuralHighwayKind unknown = new RuralHighwayKind("No proporcionado");
        unknown.MarkAsUnknown();

        return unknown;
      }
    }

    static public ValueTypeInfo ValueTypeInfo {
      get {
        return ValueTypeInfo.Parse(thisTypeName);
      }
    }

    static public FixedList<RuralHighwayKind> GetList() {
      return valuesList;
    }

    #endregion Constructors and parsers

  } // class RuralHighwayKind

} // namespace Empiria.Geography
