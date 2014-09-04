/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : RoadwayType                                    Pattern  : Value object                        *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : String classificator for roadway item types. A roadway is defined by the GeographicItemType   *
*              powertype but roadway instances can also be qualified within it using a string classificator  *
*              like 'Street' or 'Avenue'.                                                                    *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Linq;

using Empiria.Ontology;

namespace Empiria.Geography {

  /// <summary>String classificator for roadway item types. A roadway is defined by the GeographicItemType
  /// powertype but roadway instances can also be qualified within it using a string classificator
  /// like 'Street' or 'Avenue'.
  /// </summary>
  public class RoadwayType : ValueObject<string> {

    #region Fields

    private const string thisTypeName = "ValueType.ListItem.RoadwayType";

    static private FixedList<RoadwayType> valuesList =
           RoadwayType.ValueTypeInfo.GetValuesList<RoadwayType, string>((x) => new RoadwayType(x));

    #endregion Fields

    #region Constructors and parsers

    private RoadwayType(string value) : base(value) {

    }

    static public RoadwayType Parse(string value) {
      Assertion.AssertObject(value, "value");

      if (value == RoadwayType.Empty.Value) {
        return RoadwayType.Empty;
      }
      if (value == RoadwayType.Unknown.Value) {
        return RoadwayType.Unknown;
      }
      return valuesList.First((x) => x.Value == value);
    }

    static public RoadwayType Empty {
      get {
        RoadwayType empty = new RoadwayType("No determinado");
        empty.MarkAsEmpty();

        return empty;
      }
    }

    static public RoadwayType Unknown {
      get {
        RoadwayType unknown = new RoadwayType("No proporcionado");
        unknown.MarkAsUnknown();

        return unknown;
      }
    }

    static public ValueTypeInfo ValueTypeInfo {
      get {
        return ValueTypeInfo.Parse(thisTypeName);
      }
    }

    static public FixedList<RoadwayType> GetList() {
      return valuesList;
    }

    #endregion Constructors and parsers
    
  } // class RoadwayType

} // namespace Empiria.Geography
