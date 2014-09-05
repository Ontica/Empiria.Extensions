/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : SettlementType                                 Pattern  : Value object                        *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Serves as a naming space for settlement item types. A settlement is defined by the            *
*              GeographicItemType powertype but settlement instances can also be qualified within it using   *
*              a SettlementType like 'borough' or 'township', or 'colonia' or 'barrio' in Spanish.           *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Linq;

using Empiria.Ontology;

namespace Empiria.Geography {

   /// <summary>Serves as a naming space for settlement item types. A settlement is defined by the
  /// GeographicItemType powertype but settlement instances can also be qualified within it using
  /// a SettlementType like 'borough' or 'township', or 'colonia' or 'barrio' in Spanish.</summary>
  public class SettlementType : ValueObject<string> {

    #region Fields

    private const string thisTypeName = "ValueType.ListItem.SettlementType";

    static private FixedList<SettlementType> valuesList =
           SettlementType.ValueTypeInfo.GetValuesList<SettlementType, string>((x) => new SettlementType(x));
    #endregion Fields

    #region Constructors and parsers

    private SettlementType(string value) : base(value) {

    }

    static public SettlementType Parse(string value) {
      Assertion.AssertObject(value, "value");
      if (value == SettlementType.Empty.Value) {
        return SettlementType.Empty;
      }
      if (value == SettlementType.Unknown.Value) {
        return SettlementType.Unknown;
      }
      return valuesList.First((x) => x.Value == value);
    }

    static public SettlementType Empty {
      get {
        SettlementType empty = new SettlementType("No determinado");
        empty.MarkAsEmpty();

        return empty;
      }
    }

    static public SettlementType Unknown {
      get {
        SettlementType unknown = new SettlementType("No proporcionado");
        unknown.MarkAsUnknown();

        return unknown;
      }
    }

    static public ValueTypeInfo ValueTypeInfo {
      get {
        return ValueTypeInfo.Parse(thisTypeName);
      }
    }

    static public FixedList<SettlementType> GetList() {
      return valuesList;
    }

    #endregion Constructors and parsers

  } // class SettlementType

} // namespace Empiria.Geography
