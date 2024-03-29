﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Geographic Data Services            *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : Highway                                        Pattern  : Partitioned type                    *
*  Version   : 6.8                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Partitioned type that represents a federal, state, municipal or rural highway.                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Data;

using Empiria.Ontology;

namespace Empiria.Geography {

  /// <summary>Partitioned type that represents a federal, state, municipal or rural highway.</summary>
  [PartitionedType(typeof(HighwayType))]
  public class Highway : GeographicRoad {

    #region Fields

    private List<HighwaySection> highwaySectionsList = new List<HighwaySection>();

    #endregion Fields

    #region Constructors and parsers

    private Highway(HighwayType powertype) : base(powertype) {
      // Required by Empiria Framework for all partitioned types.
    }

    /// <summary>Creates a federal highway that typically cross two or more states.</summary>
    internal Highway(Country country, FederalHighwayKind highwayKind, string highwayNumber,
                     HighwaySection fromOriginToDestination)
                    : base(HighwayType.FederalHighwayType, BuildName(highwayKind, highwayNumber,
                                                                     fromOriginToDestination)) {
      this.Region = country;
      this.HighwayKind = highwayKind;
    }

    /// <summary>Creates a state highway without an official designated number.</summary>
    internal Highway(State state, StateHighwayKind highwayKind, HighwaySection fromOriginToDestination)
                    : base(HighwayType.StateHighwayType, BuildName(highwayKind, String.Empty,
                                                                   fromOriginToDestination)) {
      this.Region = state;
      this.HighwayKind = highwayKind;
    }

    /// <summary>Creates a state highway with an official highway number.</summary>
    internal Highway(State state, StateHighwayKind stateHighwayKind, string highwayNumber,
                     HighwaySection fromOriginToDestination)
                     : base(HighwayType.StateHighwayType, BuildName(stateHighwayKind, highwayNumber,
                                                                    fromOriginToDestination)) {
      this.Region = state;
      this.HighwayKind = stateHighwayKind;
    }

    /// <summary>Creates a state rural highway.</summary>
    internal Highway(State state, RuralHighwayKind ruralHighwayKind,
                     HighwaySection fromOriginToDestination)
                     : base(HighwayType.RuralHighwayType, BuildName(ruralHighwayKind, String.Empty,
                                                                    fromOriginToDestination)) {
      this.Region = state;
      this.HighwayKind = ruralHighwayKind;
    }

    /// <summary>Creates a municipal rural highway.</summary>
    internal Highway(Municipality municipality, RuralHighwayKind ruralHighwayKind,
                     HighwaySection fromOriginToDestination)
                     : base(HighwayType.RuralHighwayType, BuildName(ruralHighwayKind, String.Empty,
                                                                    fromOriginToDestination)) {
      this.Region = municipality;
      this.HighwayKind = ruralHighwayKind;
    }

    /// <summary>Creates a municipal highway.</summary>
    internal Highway(Municipality municipality, MunicipalHighwayKind municipalHighwayKind,
                     HighwaySection fromOriginToDestination)
                     : base(HighwayType.MunicipalHighwayType, BuildName(municipalHighwayKind, String.Empty,
                                                                        fromOriginToDestination)) {
      this.Region = municipality;
      this.HighwayKind = municipalHighwayKind;
    }

    static public new Highway Parse(int id) {
      return BaseObject.ParseId<Highway>(id);
    }

    #endregion Constructors and parsers

    #region Public properties

    public IHighwayKind HighwayKind {
      get;
      private set;
    }

    public HighwayType HighwayType {
      get {
        return (HighwayType) base.GetEmpiriaType();
      }
    }

    protected internal override GeographicRegion Parent {
      get { return this.Region; }
    }

    [DataField("GeoItemParentId")]
    public GeographicRegion Region {
      get;
      private set;
    }

    public FixedList<HighwaySection> Sections {
      get {
        return highwaySectionsList.ToFixedList();
      }
    }

    #endregion Public properties

    #region Public methods

    /// <summary>Adds a highway stretch or section as a named segment of this highway.</summary>
    public HighwaySection AddSection(string originPlace, string destinationPlace) {
      Assertion.Require(originPlace, "originPlace");
      Assertion.Require(destinationPlace, "destinationPlace");

      var section = new HighwaySection(originPlace, destinationPlace);
      highwaySectionsList.Add(section);

      return section;
    }

    protected override void OnLoadObjectData(DataRow row) {
      base.OnLoadObjectData(row);
      var json = base.ExtensionData;
      this.HighwayKind = this.HighwayType.ParseHighwayKind(json.Get<string>("HighwayKind", "No determinado"));
      this.highwaySectionsList = json.GetList<HighwaySection>("Sections", false);
    }

    #endregion Public methods

    #region Private methods

    static private string BuildName(IHighwayKind highwayKind, string highwayNumber,
                                    HighwaySection fromOriginToDestination) {
      Assertion.Require(highwayKind, "highwayKind");
      Assertion.Require(highwayNumber != null, "highwayNumber can't be null.");
      Assertion.Require(fromOriginToDestination, "fromOriginToDestination");
      Assertion.Require(highwayNumber.Length != 0 || !fromOriginToDestination.IsEmptyValue,
                       "To build the highway name I need at least its number or its main section.");

      if (highwayNumber.Length != 0 && !fromOriginToDestination.IsEmptyValue) {
        return highwayKind + " " + highwayNumber + " " + fromOriginToDestination;
      } else if (highwayNumber.Length != 0 && fromOriginToDestination.IsEmptyValue) {
        return highwayKind + " " + highwayNumber;
      } else if (highwayNumber.Length == 0 && !fromOriginToDestination.IsEmptyValue) {
        return highwayKind + " " + fromOriginToDestination;
      } else {    //highwayNumber.Length == 0 && fromOriginToDestination.IsEmptyValue
        throw Assertion.EnsureNoReachThisCode();
      }
    }

    #endregion Private methods

  } // class Highway

} // namespace Empiria.Geography
