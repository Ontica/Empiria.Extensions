﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Geographic Data Services            *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : MunicipalHighwayKind                           Pattern  : Value object                        *
*  Version   : 6.8                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : String value type that describes a kind of municipal highway.                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;

using Empiria.Ontology;

namespace Empiria.Geography {

  /// <summary>String value type that describes a kind of municipal highway.</summary>
  public class MunicipalHighwayKind : ValueObject<string>, IHighwayKind {

    #region Fields

    private const string thisTypeName = "ValueType.ListItem.MunicipalHighwayKind";

    static private FixedList<MunicipalHighwayKind> valuesList =
      MunicipalHighwayKind.ValueTypeInfo.GetValuesList<MunicipalHighwayKind, string>((x) => new MunicipalHighwayKind(x));

    #endregion Fields

    #region Constructors and parsers

    private MunicipalHighwayKind(string value)
      : base(value) {

    }

    static public MunicipalHighwayKind Parse(string value) {
      Assertion.Require(value, "value");

      if (value == MunicipalHighwayKind.Empty.Value) {
        return MunicipalHighwayKind.Empty;
      }
      if (value == MunicipalHighwayKind.Unknown.Value) {
        return MunicipalHighwayKind.Unknown;
      }
      return valuesList.First((x) => x.Value == value);
    }

    static public MunicipalHighwayKind Empty {
      get {
        var empty = new MunicipalHighwayKind("No determinado");
        empty.MarkAsEmpty();

        return empty;
      }
    }

    static public MunicipalHighwayKind Unknown {
      get {
        var unknown = new MunicipalHighwayKind("No proporcionado");
        unknown.MarkAsUnknown();

        return unknown;
      }
    }

    static public ValueTypeInfo ValueTypeInfo {
      get {
        return ValueTypeInfo.Parse(thisTypeName);
      }
    }

    static public FixedList<MunicipalHighwayKind> GetList() {
      return valuesList;
    }

    #endregion Constructors and parsers

  } // class MunicipalHighwayKind

} // namespace Empiria.Geography
