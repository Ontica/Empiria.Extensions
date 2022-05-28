/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Geographic Data Services            *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : StateHighwayKind                               Pattern  : Value object                        *
*  Version   : 6.8                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : String value that describes a kind of state highway.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;

using Empiria.Ontology;

namespace Empiria.Geography {

  /// <summary>String value that describes a kind of state highway.</summary>
  public class StateHighwayKind : ValueObject<string>, IHighwayKind {

    #region Fields

    private const string thisTypeName = "ValueType.ListItem.StateHighwayKind";

    static private FixedList<StateHighwayKind> valuesList =
       StateHighwayKind.ValueTypeInfo.GetValuesList<StateHighwayKind, string>((x) => new StateHighwayKind(x));

    #endregion Fields

    #region Constructors and parsers

    private StateHighwayKind(string value) : base(value) {

    }

    static public StateHighwayKind Parse(string value) {
      Assertion.Require(value, "value");

      if (value == StateHighwayKind.Empty.Value) {
        return StateHighwayKind.Empty;
      }
      if (value == StateHighwayKind.Unknown.Value) {
        return StateHighwayKind.Unknown;
      }
      return valuesList.First((x) => x.Value == value);
    }

    static public StateHighwayKind Empty {
      get {
        StateHighwayKind empty = new StateHighwayKind("No determinado");
        empty.MarkAsEmpty();

        return empty;
      }
    }

    static public StateHighwayKind Unknown {
      get {
        StateHighwayKind unknown = new StateHighwayKind("No proporcionado");
        unknown.MarkAsUnknown();

        return unknown;
      }
    }

    static public ValueTypeInfo ValueTypeInfo {
      get {
        return ValueTypeInfo.Parse(thisTypeName);
      }
    }

    static public FixedList<StateHighwayKind> GetList() {
      return valuesList;
    }

    #endregion Constructors and parsers

  } // class StateHighwayKind

} // namespace Empiria.Geography
