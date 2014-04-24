/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : Address                                        Pattern  : IExtensibleData class               *
*  Version   : 5.5        Date: 25/Jun/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Contains extensible data about geographical addresses.                                        *
*                                                                                                            *
********************************* Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using Empiria.DataTypes;

namespace Empiria.Geography {

  /// <summary>Contains extensible data about geographical addresses.</summary>
  public class Address : IExtensibleData {

    #region Constructors and parsers

    public Address() {
      this.Municipality = GeographicRegionItem.Empty;
      this.Locality = GeographicRegionItem.Empty;
      this.Settlement = GeographicRegionItem.Empty;
      this.PostalCode = GeographicRegionItem.Empty;

      this.Street = GeographicPathItem.Empty;
      this.StreetSegment = String.Empty;
      this.FromStreet = GeographicPathItem.Empty;
      this.ToStreet = GeographicPathItem.Empty;
      this.BackStreet = GeographicPathItem.Empty;

      this.ExternalNo = String.Empty;
      this.InternalNo = String.Empty;
      this.UbicationReference = String.Empty;
    }

    public static Address FromJson(string p) {
      return new Address();
    }

    static public Address Empty {
      get {
        return new Address();
      }
    }

    #endregion Constructors and parsers

    #region Properties

    public GeographicRegionItem Municipality {
      get;
      set;
    }

    public GeographicRegionItem Locality {
      get;
      set;
    }

    public GeographicRegionItem Settlement {
      get;
      set;
    }

    public GeographicRegionItem PostalCode {
      get;
      set;
    }

    public GeographicPathItem Street {
      get;
      set;
    }

    public string StreetSegment {
      get;
      set;
    }

    public GeographicPathItem FromStreet {
      get;
      set;
    }

    public GeographicPathItem ToStreet {
      get;
      set;
    }

    public GeographicPathItem BackStreet {
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

    public bool IsEmptyInstance {
      get {
        if (String.IsNullOrWhiteSpace(this.ExternalNo)) {
          return true;
        }
        return false;
      }
    }

    public string Keywords {
      get {
        return EmpiriaString.BuildKeywords(this.Street.Name, this.ExternalNo, this.InternalNo, 
                                           this.Settlement.Keywords, this.Locality.Name,
                                           this.PostalCode.Name, this.StreetSegment, this.Municipality.Name);
      }
    }

    #endregion Properties

    #region Methods

    public string ToJson() {
      if (!this.IsEmptyInstance) {
        return Empiria.Data.JsonConverter.ToJson(this.GetObject());
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
      if (!this.Locality.IsEmptyInstance) {
        temp += this.Locality.Name + ", "  ;//+ this.Locality.State.Code;
      }
      if (!this.PostalCode.IsEmptyInstance) {
        temp += this.PostalCode.Code;
      }
      return temp;
    }

    public string ToSearchVector() {
      return String.Format("|{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|", Municipality.Id, Locality.Id,
                           Settlement.Id, PostalCode.Id, Street.Id, FromStreet.Id, 
                           ToStreet.Id, BackStreet.Id);
    }

    private object GetObject() {
      //List<KeyValuePair> list = new List<KeyValuePair>();

      //Empiria.KeyValuePair
      return new {
        MunicipalityId = this.Municipality.Id,
      };
    }

    #endregion Methods

  }  // class Address

} // namespace Empiria.Geography