/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : MunicipalHighwayType                           Pattern  : Value object                        *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : String value type that describes a kind of municipal highway.                                 *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Linq;

using Empiria.Ontology;

namespace Empiria.Geography {

  /// <summary>String value type that describes a kind of municipal highway.</summary>
  public class MunicipalHighwayType : ValueObject<string>, IHighwayType {

    #region Fields

    private const string thisTypeName = "ValueType.ListItem.MunicipalHighwayType";

    static private FixedList<MunicipalHighwayType> valuesList =
      MunicipalHighwayType.ValueTypeInfo.GetValuesList<MunicipalHighwayType, string>((x) => new MunicipalHighwayType(x));

    #endregion Fields

    #region Constructors and parsers

    private MunicipalHighwayType(string value)
      : base(value) {

    }

    static public MunicipalHighwayType Parse(string value) {
      Assertion.AssertObject(value, "value");

      if (value == MunicipalHighwayType.Empty.Value) {
        return MunicipalHighwayType.Empty;
      }
      if (value == MunicipalHighwayType.Unknown.Value) {
        return MunicipalHighwayType.Unknown;
      }
      return valuesList.First((x) => x.Value == value);
    }

    static public MunicipalHighwayType Empty {
      get {
        var empty = new MunicipalHighwayType("No determinado");
        empty.MarkAsEmpty();

        return empty;
      }
    }

    static public MunicipalHighwayType Unknown {
      get {
        var unknown = new MunicipalHighwayType("No proporcionado");
        unknown.MarkAsUnknown();

        return unknown;
      }
    }

    static public ValueTypeInfo ValueTypeInfo {
      get {
        return ValueTypeInfo.Parse(thisTypeName);
      }
    }

    static public FixedList<MunicipalHighwayType> GetList() {
      return valuesList;
    }

    #endregion Constructors and parsers

  } // class MunicipalHighwayType

} // namespace Empiria.Geography
