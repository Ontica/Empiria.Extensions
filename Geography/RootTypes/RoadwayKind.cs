/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Geographic Data Services            *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : RoadwayKind                                    Pattern  : Value object                        *
*  Version   : 6.5                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : String classificator for roadway item types. A roadway is defined by the GeographicItemType   *
*              powertype but roadway instances can also be qualified within it using a string classificator  *
*              like 'Street' or 'Avenue'.                                                                    *
*                                                                                                            *
********************************* Copyright (c) 2009-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Linq;

using Empiria.Ontology;

namespace Empiria.Geography {

  /// <summary>String classificator for roadway item types. A roadway is defined by the GeographicItemType
  /// powertype but roadway instances can also be qualified within it using a string classificator
  /// like 'Street' or 'Avenue'.
  /// </summary>
  public class RoadwayKind : ValueObject<string> {

    #region Fields

    private const string thisTypeName = "ValueType.ListItem.RoadwayKind";

    static private FixedList<RoadwayKind> valuesList =
           RoadwayKind.ValueTypeInfo.GetValuesList<RoadwayKind, string>((x) => new RoadwayKind(x));

    #endregion Fields

    #region Constructors and parsers

    private RoadwayKind(string value) : base(value) {

    }

    static public RoadwayKind Parse(string value) {
      Assertion.AssertObject(value, "value");

      if (value == RoadwayKind.Empty.Value) {
        return RoadwayKind.Empty;
      }
      if (value == RoadwayKind.Unknown.Value) {
        return RoadwayKind.Unknown;
      }
      return valuesList.First((x) => x.Value == value);
    }

    static public RoadwayKind Empty {
      get {
        RoadwayKind empty = new RoadwayKind("No determinado");
        empty.MarkAsEmpty();

        return empty;
      }
    }

    static public RoadwayKind Unknown {
      get {
        RoadwayKind unknown = new RoadwayKind("No proporcionado");
        unknown.MarkAsUnknown();

        return unknown;
      }
    }

    static public ValueTypeInfo ValueTypeInfo {
      get {
        return ValueTypeInfo.Parse(thisTypeName);
      }
    }

    static public FixedList<RoadwayKind> GetList() {
      return valuesList;
    }

    #endregion Constructors and parsers

  } // class RoadwayKind

} // namespace Empiria.Geography
