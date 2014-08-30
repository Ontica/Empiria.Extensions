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
using System.Collections.Generic;
using System.Data;

using Empiria.Data;
using Empiria.DataTypes;
using Empiria.Ontology;

namespace Empiria.Geography {

  static internal class GeographicData {

    #region Internal methods

    internal static List<T> GetChildGeoItems<T>(GeographicRegion region) where T : GeographicItem {
      if (region.IsNew || region.IsEmptyInstance) {
        return new List<T>();
      }
      string sql = String.Format("SELECT * FROM EOSGeoItems WHERE GeoItemParentId = {0} AND " + 
                                 "GeoItemTypeId = {1} AND GeoItemStatus = 'A'", region.Id, 307);
      return DataReader.GetList<T>(DataOperation.Parse(sql), (x) => GeographicItem.Parse<T>(x));
    }

    internal static FixedList<T> GetGeographicItems<T>() where T : GeographicItem {
      return GeographicData.GetGeographicItems<T>(String.Empty);
    }

    static internal FixedList<T> GetGeographicItems<T>(string filter) where T : GeographicItem {
      return GeographicData.GetGeographicItems<T>(filter, "GeoItemStatus = 'A'");
    }

    static internal FixedList<T> GetGeographicItems<T>(string filter, string sort) where T : GeographicItem {
      var objectTypeInfo = ObjectTypeInfo.Parse<T>();
      var dataOperation = objectTypeInfo.GetListDataOperation(filter, sort);

      return DataReader.GetFixedList<T>(dataOperation, (x) => GeographicItem.Parse<T>(x));
    }

    static internal int WriteGeographicItem(GeographicItem o) {
      var operation = DataOperation.Parse("writeEOSGeoItem", o.Id, o.GeographicItemType.Id, 
                                          o.Name, o.FullName, o.ExtendedDataString, 
                                          o.Keywords, o.Parent.Id, o.PostedBy.Id, o.PostingTime, 
                                          (char) o.Status, o.StartDate, o.EndDate);
      return DataWriter.Execute(operation);
    }


    //static internal FixedList<GeographicRegion> GetRegions(string filter) {
    //  var operation = DataOperation.Parse("SELECT * FROM EOSGeoItems WHERE GeoItemStatus = 'A'");

    //  return DataReader.GetFixedList<GeographicRegion>(operation, (x) => GeographicRegion.Parse(x),
    //                                                       filter, "GeoItemName, GeoItemFullName");
    //}

    //static internal FixedList<Road> GetRoads<T>(string filter) where T : GeographicPath {
    //  throw new NotImplementedException();
    //}

    #endregion Internal methods

  } // class GeographicData

} // Empiria.Geography
