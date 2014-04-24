/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeographicData                                 Pattern  : Data Services Static Class          *
*  Version   : 5.5        Date: 25/Jun/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Provides data read and write methods for the Empiria Geographic Information Services.         *
*                                                                                                            *
********************************* Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Data;

using Empiria.Data;
using Empiria.DataTypes;

namespace Empiria.Geography {

  static internal class GeographicData {

    #region Internal methods

    static internal FixedList<GeographicRegionItem> GetRegions(string filterExpression, 
                                                                string sortExpression = "GeoItemNotes, GeoItemName") {
      DataTable table = GeneralDataOperations.GetEntities("EOSGeoItems", filterExpression, sortExpression);

      return new FixedList<GeographicRegionItem>((x) => GeographicRegionItem.Parse(x), table);
    }

    static internal int WriteGeographicRegionItem(GeographicRegionItem o) {
      var dataOperation = DataOperation.Parse("writeEOSGeoItem", o.Id, o.ObjectTypeInfo.Id, o.Name,
                                               o.Code, o.FullName, o.Keywords, o.WebPage, o.PhonePrefix,
                                               o.Population, o.AreaSqKm, o.GDPPerCapita.Amount, o.GDPPerCapita.Currency.Id,
                                               o.PostedBy.Id, o.ReplacedById, o.PostingDate,
                                               (char) o.Status, o.StartDate, o.EndDate);
      return DataWriter.Execute(dataOperation);
    }

    static internal int WriteGeographicPathItem(GeographicPathItem o) {
      var dataOperation = DataOperation.Parse("writeEOSGeoItem", o.Id, o.ObjectTypeInfo.Id, o.Name,
                                               o.Code, o.FullName, o.Keywords, String.Empty, String.Empty,
                                               0m, 0m, 0m, Currency.Default.Id,
                                               o.PostedBy.Id, o.ReplacedById, o.PostingDate,
                                               (char) o.Status, o.StartDate, o.EndDate);
      return DataWriter.Execute(dataOperation);
    }

    #endregion Internal methods

  } // class GeographicData

} // Empiria.Geography.Data