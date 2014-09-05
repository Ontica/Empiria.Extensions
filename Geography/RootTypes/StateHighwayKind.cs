/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : StateHighwayType                               Pattern  : Value object                        *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : String value that describes a kind of state highway.                                          *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Linq;

using Empiria.Ontology;

namespace Empiria.Geography {

  /// <summary>String value that describes a kind of state highway.</summary>
  public class StateHighwayType : ValueObject<string>, IHighwayType {

    #region Fields

    private const string thisTypeName = "ValueType.ListItem.StateHighwayType";

    static private FixedList<StateHighwayType> valuesList =
       StateHighwayType.ValueTypeInfo.GetValuesList<StateHighwayType, string>((x) => new StateHighwayType(x));

    #endregion Fields

    #region Constructors and parsers

    private StateHighwayType(string value) : base(value) {

    }

    static public StateHighwayType Parse(string value) {
      Assertion.AssertObject(value, "value");

      if (value == StateHighwayType.Empty.Value) {
        return StateHighwayType.Empty;
      }
      if (value == StateHighwayType.Unknown.Value) {
        return StateHighwayType.Unknown;
      }
      return valuesList.First((x) => x.Value == value);
    }

    static public StateHighwayType Empty {
      get {
        StateHighwayType empty = new StateHighwayType("No determinado");
        empty.MarkAsEmpty();

        return empty;
      }
    }

    static public StateHighwayType Unknown {
      get {
        StateHighwayType unknown = new StateHighwayType("No proporcionado");
        unknown.MarkAsUnknown();

        return unknown;
      }
    }

    static public ValueTypeInfo ValueTypeInfo {
      get {
        return ValueTypeInfo.Parse(thisTypeName);
      }
    }

    static public FixedList<StateHighwayType> GetList() {
      return valuesList;
    }

    #endregion Constructors and parsers

  } // class StateHighwayType

} // namespace Empiria.Geography
