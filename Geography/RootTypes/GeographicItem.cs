﻿/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeographicItem                                 Pattern  : Empiria Object Type                 *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Represents an abstract place: region, city, country, world zone, zip code region, street, ... *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Data;

using Empiria.Contacts;
using Empiria.Ontology;

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

    static protected FixedList<T> GetList<T>() where T : GeographicItem {
      return GeographicData.GetGeographicItems<T>();
    }

    static protected FixedList<T> GetList<T>(string filter, string sort) where T : GeographicItem {
      return GeographicData.GetGeographicItems<T>(filter, sort);
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
    internal protected string ExtendedDataString {
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

    [DataField("PostingTime", Default = "DateTime.Now")]
    internal protected DateTime PostingTime {
      get;
      private set;
    }

    [DataField("PostedById")]
    internal protected Contact PostedBy {
      get;
      set;
    }

    [DataField("GeoItemStatus", Default = GeneralObjectStatus.Pending)]
    internal protected GeneralObjectStatus Status {
      get;
      private set;
    }

    [DataField("StartDate", Default = "DateTime.Today")]
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
      this.Status = GeneralObjectStatus.Deleted;
      //this.MarkAsDeleted();
    }

    protected override void OnSave() {
      GeographicData.WriteGeographicItem(this);
    }

    #endregion Public methods

  } // class GeographicItem

} // namespace Empiria.Geography
