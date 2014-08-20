/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeographicData                                 Pattern  : Data Services Static Class          *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Provides data read and write methods for the Empiria Geographic Information Services.         *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Data;

using Empiria.Data;
using Empiria.DataTypes;

namespace Empiria.Geography {

  static internal class GeographicData {

    #region Internal methods

    static internal FixedList<T> GetRegions<T>(string filter) where T : GeographicRegionItem {
      throw new NotImplementedException();
    }

    static internal FixedList<Road> GetRoads<T>(string filter) where T : GeographicPathItem {
      throw new NotImplementedException();
    }

    static internal FixedList<GeographicRegionItem> GetRegions(string filterExpression, 
                                                               string sortExpression = "GeoItemNotes, GeoItemName") {
      DataTable table = GeneralDataOperations.GetEntities("EOSGeoItems", filterExpression, sortExpression);

      return new FixedList<GeographicRegionItem>((x) => GeographicRegionItem.Parse(x), table);
    }

    static internal int WriteGeographicItem(GeographicItem o) {
      var dataOperation = DataOperation.Parse("writeEOSGeoItem", o.Id, o.GeographicItemType.Id, 
                                              o.GeoItemKind.Id, o.Name, o.Code, o.FullName,
                                              o.Notes, o.Keywords, o.ReplacedById, o.PostedBy.Id,
                                              o.PostingTime, (char) o.Status, o.StartDate, o.EndDate);
      return DataWriter.Execute(dataOperation);
    }

    #endregion Internal methods

  } // class GeographicData

} // Empiria.Geography.Data
