/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : State                                          Pattern  : Empiria Object Type                 *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Represents a state within a country.                                                          *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Data;

namespace Empiria.Geography {

  /// <summary>Represents a state within a country.</summary>
  public class State : GeographicRegionItem {

    #region Fields

    private const string thisTypeName = "ObjectType.GeographicItem.GeographicRegionItem.State";

    #endregion Fields

    #region Constructors and parsers

    protected State() : base(thisTypeName) {
      // For create instances use GeographicItemType.CreateInstance method instead
    }

    protected State(string typeName) : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    static public new State Parse(int id) {
      return BaseObject.Parse<State>(thisTypeName, id);
    }

    static internal new State Parse(DataRow row) {
      return BaseObject.Parse<State>(thisTypeName, row);
    }

    static public new State Empty {
      get {
        return BaseObject.ParseEmpty<State>(thisTypeName);
      }
    }

    static public new State Unknown {
      get {
        return BaseObject.ParseUnknown<State>(thisTypeName);
      }
    }

    static public new FixedList<State> GetList(string filter) {
      return GeographicData.GetRegions<State>(filter);
    }

    #endregion Constructors and parsers

    #region Public properties

    public override string CompoundName {
      get { return base.Name + " (" + base.GeographicItemType.DisplayName + ")"; }
    }

    public Country Country {
      get;
      private set;
    }

    #endregion Public properties

    #region Public methods

    public void AddMunicipality(Municipality municipality) {
      var role = base.ObjectTypeInfo.Associations["State_Muncipalities"];
      base.Link(role, municipality);
    }

    public FixedList<Municipality> GetMunicipalities() {
      return base.GetLinks<Municipality>("State_Muncipalities");
    }

    #endregion Public methods

  } // class State

} // namespace Empiria.Geography
