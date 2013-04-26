/* Empiria® Extended Framework 2013 **************************************************************************
*                                                                                                            *
*  Solution  : Empiria® Extended Framework                      System   : Geographic Information Services   *
*  Namespace : Empiria.Geography                                Assembly : Empiria.Geography.dll             *
*  Type      : GeographicData                                   Pattern  : Data Services Static Class        *
*  Date      : 25/Jun/2013                                      Version  : 5.1     License: CC BY-NC-SA 3.0  *
*                                                                                                            *
*  Summary   : Provides data read and write methods for the Empiria Geographic Information Services.         *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1994-2013. **/
using System;
using System.Data;

using Empiria.Data;
using Empiria.DataTypes;

namespace Empiria.Geography {

  static internal class GeographicData {

    #region Internal methods

    static internal DataTable GetRegions(string filterExpression) {
      return GeneralDataOperations.GetEntities("EOSGeoItems", filterExpression, "GeoItemNotes, GeoItemName");
    }

    static internal int WriteGeographicRegionItem(GeographicRegionItem o) {
      DataOperation dataOperation = DataOperation.Parse("writeEOSGeoItem", o.Id, o.ObjectTypeInfo.Id, o.Name,
                                                        o.Code, o.FullName, o.Keywords, o.WebPage, o.PhonePrefix,
                                                        o.Population, o.AreaSqKm, o.GDPPerCapita.Amount, o.GDPPerCapita.Currency.Id,
                                                        o.PostedBy.Id, o.ReplacedById, o.PostingDate,
                                                        (char) o.Status, o.StartDate, o.EndDate);
      return DataWriter.Execute(dataOperation);
    }

    static internal int WriteGeographicPathItem(GeographicPathItem o) {
      DataOperation dataOperation = DataOperation.Parse("writeEOSGeoItem", o.Id, o.ObjectTypeInfo.Id, o.Name,
                                                        o.Code, o.FullName, o.Keywords, String.Empty, String.Empty,
                                                        0m, 0m, 0m, Currency.Default.Id,
                                                        o.PostedBy.Id, o.ReplacedById, o.PostingDate,
                                                        (char) o.Status, o.StartDate, o.EndDate);
      return DataWriter.Execute(dataOperation);
    }

    #endregion Internal methods

  } // class GeographicData

} // Empiria.Geography.Data