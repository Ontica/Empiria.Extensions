/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : FederalHighwayType                             Pattern  : Value object                        *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : String value type that describes a kind of federal highway.                                   *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Linq;

using Empiria.Ontology;

namespace Empiria.Geography {

  /// <summary>String value type that describes a kind of federal highway.</summary>
  public class FederalHighwayType : ValueObject<string>, IHighwayType {

    #region Fields

    private const string thisTypeName = "ValueType.ListItem.FederalHighwayType";

    static private FixedList<FederalHighwayType> valuesList =
    FederalHighwayType.ValueTypeInfo.GetValuesList<FederalHighwayType, string>((x) => new FederalHighwayType(x));

    #endregion Fields

    #region Constructors and parsers

    private FederalHighwayType(string value) : base(value) {

    }

    static public FederalHighwayType Parse(string value) {
      Assertion.AssertObject(value, "value");

      if (value == FederalHighwayType.Empty.Value) {
        return FederalHighwayType.Empty;
      }
      if (value == FederalHighwayType.Unknown.Value) {
        return FederalHighwayType.Unknown;
      }
      return valuesList.First((x) => x.Value == value);
    }

    static public FederalHighwayType Empty {
      get {
        FederalHighwayType empty = new FederalHighwayType("No determinado");
        empty.MarkAsEmpty();

        return empty;
      }
    }

    static public FederalHighwayType Unknown {
      get {
        FederalHighwayType unknown = new FederalHighwayType("No proporcionado");
        unknown.MarkAsUnknown();

        return unknown;
      }
    }

    static public ValueTypeInfo ValueTypeInfo {
      get {
        return ValueTypeInfo.Parse(thisTypeName);
      }
    }

    static public FixedList<FederalHighwayType> GetList() {
      return valuesList;
    }

    #endregion Constructors and parsers

  } // class FederalHighwayType

} // namespace Empiria.Geography
