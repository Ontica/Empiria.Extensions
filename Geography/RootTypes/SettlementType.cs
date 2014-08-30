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
using System.Collections.Generic;
using System.Linq;

using Empiria.Ontology;

namespace Empiria.Geography {

  /// <summary>Serves as a naming space for settlement item types. A settlement is defined by the
  /// GeographicItemType powertype but settlement instances can also be qualified within it using
  /// a SettlementType like 'borough' or 'township', or 'colonia' or 'barrio' in Spanish.</summary>
  public class SettlementType {

    #region Fields

    private const string thisTypeName = "ValueType.ListItem.SettlementType";

    static private List<SettlementType> settlementTypeList = SettlementType.GetSettlementTypeList();

    private string value;

    #endregion Fields

    #region Constructors and parsers

    private SettlementType(string value) {
      this.value = value;
    }

    static public SettlementType Parse(string value) {
      return settlementTypeList.First((x) => x.Value == value);
    }

    static public SettlementType Empty {
      get {
        return new SettlementType("No determinado");
      }
    }

    static public SettlementType Unknown {
      get {
        return new SettlementType("Desconocido");
      }
    }

    static public ValueTypeInfo ValueTypeInfo {
      get {
        return ValueTypeInfo.Parse(thisTypeName);
      }
    }

    static public FixedList<SettlementType> GetList() {
      return settlementTypeList.ToFixedList();
    }

    #endregion Constructors and parsers

    #region Public properties and operators overloading

    static public bool operator ==(SettlementType settlementType1, SettlementType settlementType2) {
      return (settlementType1.value == settlementType2.value);
    }

    static public bool operator !=(SettlementType settlementType1, SettlementType settlementType2) {
      return !(settlementType1 == settlementType2);
    }

    public bool IsEmptyInstance {
      get {
        return (this == SettlementType.Empty);
      }
    }

    public bool IsUnknownInstance {
      get {
        return (this == SettlementType.Unknown);
      }
    }

    public string Value {
      get {
        return (this.value);
      }
    }

    #endregion Public properties and operators overloading

    public override bool Equals(object obj) {
      if ((obj == null) || !(obj is SettlementType)) {
        return false;
      }
      return this.value == ((SettlementType) obj).value;
    }

    public bool Equals(SettlementType obj) {
      return this.value == obj.value;
    }

    public override int GetHashCode() {
      return this.value.GetHashCode();
    }

    public override string ToString() {
      return this.value;
    }

    #region Private methods

    static private List<SettlementType> GetSettlementTypeList() {
      var json = Empiria.Data.JsonObject.Parse(SettlementType.ValueTypeInfo.ExtensionData);
      List<string> list = json.GetList<string>("Values");

      return list.ConvertAll<SettlementType>((x) => new SettlementType(x));
    }

    #endregion Private methods

  } // class SettlementType

} // namespace Empiria.Geography
