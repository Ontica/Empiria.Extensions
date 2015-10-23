/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Geographic Data Services            *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : SettlementKind                                 Pattern  : Value object                        *
*  Version   : 6.5                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Serves as a kind classificator for settlement item types. A settlement is defined by the      *
*              GeographicItemType powertype but settlement instances can also be qualified within it using   *
*              a SettlementKind like 'borough' or 'township', or 'colonia' or 'barrio' in Spanish.           *
*                                                                                                            *
********************************* Copyright (c) 2009-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Linq;

using Empiria.Ontology;

namespace Empiria.Geography {

   /// <summary>Serves as a kind classificator for settlement item types. A settlement is defined by the
  /// GeographicItemType powertype but settlement instances can also be qualified within it using
  /// a SettlementKind like 'borough' or 'township', or 'colonia' or 'barrio' in Spanish.</summary>
  public class SettlementKind : ValueObject<string> {

    #region Fields

    private const string thisTypeName = "ValueType.ListItem.SettlementKind";

    static private FixedList<SettlementKind> valuesList =
           SettlementKind.ValueTypeInfo.GetValuesList<SettlementKind, string>((x) => new SettlementKind(x));
    #endregion Fields

    #region Constructors and parsers

    private SettlementKind(string value) : base(value) {

    }

    static public SettlementKind Parse(string value) {
      Assertion.AssertObject(value, "value");
      if (value == SettlementKind.Empty.Value) {
        return SettlementKind.Empty;
      }
      if (value == SettlementKind.Unknown.Value) {
        return SettlementKind.Unknown;
      }
      return valuesList.First((x) => x.Value == value);
    }

    static public SettlementKind Empty {
      get {
        SettlementKind empty = new SettlementKind("No determinado");
        empty.MarkAsEmpty();

        return empty;
      }
    }

    static public SettlementKind Unknown {
      get {
        SettlementKind unknown = new SettlementKind("No proporcionado");
        unknown.MarkAsUnknown();

        return unknown;
      }
    }

    static public ValueTypeInfo ValueTypeInfo {
      get {
        return ValueTypeInfo.Parse(thisTypeName);
      }
    }

    static public FixedList<SettlementKind> GetList() {
      return valuesList;
    }

    #endregion Constructors and parsers

  } // class SettlementKind

} // namespace Empiria.Geography
