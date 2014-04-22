/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeographicItem                                 Pattern  : Empiria Object Type                 *
*  Version   : 5.5        Date: 25/Jun/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Represents an abstract place: region, city, country, world zone, zip code region, street, ... *
*                                                                                                            *
********************************* Copyright (c) 2009-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Data;

using Empiria.Contacts;

namespace Empiria.Geography {

  public enum GeographicItemStatus {
    Pending = 'P',
    Active = 'A',
    Suspended = 'S',
    Obsolete = 'O',
    Delete = 'X',
  }

  public abstract class GeographicItem : BaseObject {

    #region Fields

    private const string thisTypeName = "ObjectType.GeographicItem";

    private string name = String.Empty;
    private string code = String.Empty;
    private string fullName = String.Empty;
    private string keywords = String.Empty;
    private Contact postedBy = Person.Empty;
    private int replacedById = 0;
    private DateTime postingDate = DateTime.Now;
    private GeographicItemStatus status = GeographicItemStatus.Pending;
    private DateTime startDate = DateTime.Today;
    private DateTime endDate = ExecutionServer.DateMaxValue;

    private GeographicItemType geographicItemType = null;

    #endregion Fields

    #region Constructors and parsers

    protected GeographicItem(string typeName)
      : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    static public GeographicItem Parse(int id) {
      return BaseObject.Parse<GeographicItem>(thisTypeName, id);
    }

    #endregion Constructors and parsers

    #region Public properties

    public string Code {
      get { return code; }
      set { code = value; }
    }

    public DateTime EndDate {
      get { return endDate; }
    }

    public string Keywords {
      get { return keywords; }
      protected set { keywords = value; }
    }

    public GeographicItemType GeographicItemType {
      get {
        if (geographicItemType == null) {
          geographicItemType = GeographicItemType.Parse(base.ObjectTypeInfo);
        }
        return geographicItemType;
      }
      internal set {
        geographicItemType = value;
      }
    }

    public string Name {
      get { return name; }
      set { name = value; }
    }

    public string FullName {
      get { return fullName; }
      set { fullName = value; }
    }

    public Contact PostedBy {
      get { return postedBy; }
    }

    public DateTime PostingDate {
      get { return postingDate; }
    }

    protected internal int ReplacedById {
      get { return replacedById; }
    }

    public DateTime StartDate {
      get { return startDate; }
    }

    public GeographicItemStatus Status {
      get { return status; }
    }

    #endregion Public properties

    #region Public methods

    protected override void ImplementsLoadObjectData(DataRow row) {
      this.name = (string) row["GeoItemName"];
      this.code = (string) row["GeoItemCode"];
      this.fullName = (string) row["GeoItemNotes"];
      this.keywords = (string) row["GeoItemKeywords"];
      this.postedBy = Contact.Parse((int) row["PostedById"]);
      this.replacedById = (int) row["ReplacedById"];
      this.postingDate = (DateTime) row["PostingDate"];
      this.status = (GeographicItemStatus) Convert.ToChar(row["GeoItemStatus"]);
      this.startDate = (DateTime) row["StartDate"];
      this.endDate = (DateTime) row["EndDate"];
    }

    #endregion Public methods

  } // class GeographicItem

} // namespace Empiria.Geography