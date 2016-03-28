/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Geographic Data Services            *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : Address                                        Pattern  : IExtensibleData class               *
*  Version   : 6.7                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Contains extensible data about geographical addresses.                                        *
*                                                                                                            *
********************************* Copyright (c) 2009-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

namespace Empiria.Geography {

  /// <summary>Contains extensible data about geographical addresses.</summary>
  public class Address : IExtensibleData {

    #region Constructors and parsers

    public Address() {
      this.Municipality = Geography.Municipality.Empty;
      this.Location = Geography.Location.Empty;
      this.Settlement = Geography.Settlement.Empty;
      this.PostalCode = String.Empty;

      this.Street = Roadway.Empty;
      this.StreetSegment = String.Empty;
      this.FromStreet = Roadway.Empty;
      this.ToStreet = Roadway.Empty;
      this.BackStreet = Roadway.Empty;

      this.ExternalNo = String.Empty;
      this.InternalNo = String.Empty;
      this.UbicationReference = String.Empty;
    }

    static public Address FromJson(string p) {
      return new Address();
    }

    static private readonly Address _empty = new Address() {
      IsEmptyInstance = true
    };

    static public Address Empty {
      get {
        return _empty;
      }
    }

    #endregion Constructors and parsers

    #region Properties

    public bool IsEmptyInstance {
      get;
      private set;
    }

    public Municipality Municipality {
      get;
      set;
    }

    public Location Location {
      get;
      set;
    }

    public Settlement Settlement {
      get;
      set;
    }

    public string PostalCode {
      get;
      set;
    }

    public GeographicRoad Street {
      get;
      set;
    }

    public string StreetSegment {
      get;
      set;
    }

    public GeographicRoad FromStreet {
      get;
      set;
    }

    public GeographicRoad ToStreet {
      get;
      set;
    }

    public GeographicRoad BackStreet {
      get;
      set;
    }

    public string ExternalNo {
      get;
      set;
    }

    public string InternalNo {
      get;
      set;
    }

    public string UbicationReference {
      get;
      set;
    }

    public string Keywords {
      get {
        return EmpiriaString.BuildKeywords(this.Street.Name, this.ExternalNo, this.InternalNo,
                                           this.Settlement.Keywords, this.Location.Name,
                                           this.PostalCode, this.StreetSegment, this.Municipality.Name);
      }
    }

    #endregion Properties

    #region Methods

    public string ToJson() {
      if (!this.IsEmptyInstance) {
        return Empiria.Json.JsonConverter.ToJson(this.GetObject());
      } else {
        return String.Empty;
      }
    }

    public override string ToString() {
      string temp = Street.IsEmptyInstance ? UbicationReference : Street.Name;
      if (temp.Length == 0) {
        temp += "Vialidad desconocida";
      }
      if (this.StreetSegment.Length != 0) {
        temp += " " + StreetSegment;
      }
      if (this.ExternalNo.Length != 0) {
        temp += "Núm: " + this.ExternalNo;
      } else if (!Street.IsEmptyInstance) {
        temp += " S/N ";
      }
      if (this.InternalNo.Length != 0) {
        temp += " Int: " + this.InternalNo;
      }
      if (temp.Length != 0) {
        temp += "\n";
      }
      if (!this.Settlement.IsEmptyInstance) {
        temp += Settlement.Name + "\n";
      }
      if (!this.Location.IsEmptyInstance) {
        temp += this.Location.Name;
      }
      if (this.PostalCode.Length != 0) {
        temp += ", C.P. " + this.PostalCode;
      }
      return temp;
    }

    public string ToSearchVector() {
      return String.Format("|{0}|{1}|{2}|{3}|{4}|{5}|{6}|", Municipality.Id, Location.Id,
                           Settlement.Id, Street.Id, FromStreet.Id, ToStreet.Id, BackStreet.Id);
    }

    private object GetObject() {
      return new {
        MunicipalityId = this.Municipality.Id,
      };
    }

    #endregion Methods

  }  // class Address

} // namespace Empiria.Geography
