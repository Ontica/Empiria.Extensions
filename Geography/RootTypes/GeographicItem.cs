﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Geographic Data Services            *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeographicItem                                 Pattern  : Empiria Object Type                 *
*  Version   : 6.8                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Represents an abstract place: region, city, country, world zone, zip code region, street, ... *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;
using Empiria.Ontology;
using Empiria.StateEnums;

namespace Empiria.Geography {

  public abstract class GeographicItem : BaseObject {

    #region Constructors and parsers

    protected GeographicItem() {
      // Required by Empiria Framework.
    }

    protected GeographicItem(ObjectTypeInfo powertype) : base(powertype) {
      // Used by derived partitioned types to create new instances of GeographicItem.
    }

    protected GeographicItem(string geoItemName) {
      this.Name = geoItemName;
    }

    protected GeographicItem(ObjectTypeInfo powertype, string geoItemName) : base(powertype) {
      this.Name = geoItemName;
    }

    static public GeographicItem Parse(int id) {
      return BaseObject.ParseId<GeographicItem>(id);
    }

    #endregion Constructors and parsers

    #region Public properties

    [DataField("GeoItemName")]
    public string Name {
      get;
      private set;
    }

    public virtual string FullName {
      get {
        return this.Name;
      }
    }

    [DataField("GeoItemExtData")]
    internal protected JsonObject ExtensionData {
      get;
      set;
    }

    internal protected virtual string Keywords {
      get {
        return EmpiriaString.BuildKeywords(this.FullName, this.GetEmpiriaType().DisplayName);
      }
    }

    internal protected virtual GeographicRegion Parent {
      get {
        return GeographicRegion.Empty;
      }
    }

    [DataField("GeoItemStatus", Default = EntityStatus.Pending)]
    internal protected EntityStatus Status {
      get;
      private set;
    }

    [DataField("StartDate")]
    internal protected DateTime StartDate {
      get;
      private set;
    }

    [DataField("EndDate", Default = "ExecutionServer.DateMaxValue")]
    internal protected DateTime EndDate {
      get;
      private set;
    }

    #endregion Public properties

    #region Public methods

    protected internal void Remove() {
      this.Status = EntityStatus.Deleted;
    }

    protected override void OnSave() {
      GeographicData.WriteGeographicItem(this);
    }

    #endregion Public methods

  } // class GeographicItem

} // namespace Empiria.Geography
