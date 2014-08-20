/* Empiria Extended Framework 2014 ***************************************************************************
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

namespace Empiria.Geography {

  public abstract class GeographicItem : BaseObject {

    #region Fields

    private const string thisTypeName = "ObjectType.GeographicItem";

    #endregion Fields

    #region Constructors and parsers

    protected GeographicItem(string typeName) : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    static public GeographicItem Parse(int id) {
      return BaseObject.Parse<GeographicItem>(thisTypeName, id);
    }

    #endregion Constructors and parsers

    #region Public properties

    [DataField("GeographicItemTypeId")]
    public GeographicItemType GeographicItemType {
      get;
      internal set;
    }

    [DataField("GeoItemKindId")]
    public GeoItemKind GeoItemKind {
      get;
      protected set;
    }

    [DataField("GeoItemCode")]
    internal protected string Code {
      get;
      set;
    }

    [DataField("GeoItemName")]
    public string Name {
      get;
      protected set;
    }

    public virtual string FullName {
      get {
        return this.Name;
      }
    }

    [DataField("GeoItemNotes")]
    public string Notes {
      get;
      set;
    }

    public virtual string Keywords {
      get {
        return EmpiriaString.BuildKeywords(this.FullName, this.Code, 
                                           this.GeographicItemType.DisplayName);
      }
    }

    [DataField("ReplacedById")]
    protected internal int ReplacedById {
      get;
      private set;
    }

    [DataField("PostedById", Default = "Contacts.Person.Empty")]
    public Contact PostedBy {
      get;
      private set;
    }

    [DataField("PostingTime", Default = "DateTime.Now")]
    public DateTime PostingTime {
      get;
      private set;
    }

    [DataField("GeoItemStatus", Default = GeneralObjectStatus.Pending)]
    public GeneralObjectStatus Status {
      get;
      private set;
    }

    [DataField("StartDate", Default = "DateTime.Today")]
    public DateTime StartDate {
      get;
      private set;
    }

    [DataField("EndDate", Default = "ExecutionServer.DateMaxValue")]
    public DateTime EndDate {
      get;
      private set;
    }

    #endregion Public properties

    protected override void OnSave() {
      GeographicData.WriteGeographicItem(this);
    }

  } // class GeographicItem

} // namespace Empiria.Geography
