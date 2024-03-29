﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Geographic Data Services            *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeographicData                                 Pattern  : Data Services Static Class          *
*  Version   : 6.8                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : Provides data read and write methods for the Empiria Geographic Information Services.         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using Empiria.Data;
using Empiria.Ontology;

namespace Empiria.Geography {

  static internal class GeographicData {

    #region Internal methods

    static internal List<T> GetChildGeoItems<T>(GeographicRegion region) where T : GeographicItem {
      if (region.IsNew || region.IsSpecialCase) {
        return new List<T>();
      }
      var geoItemChildsType = ObjectTypeInfo.Parse<T>();
      string typesString = geoItemChildsType.GetSubclassesFilter();
      string sql = String.Format("SELECT * FROM GeoItems WHERE GeoItemParentId = {0} AND " +
                                 "GeoItemTypeId IN ({1}) AND GeoItemStatus = 'A' ORDER BY GeoItemFullName",
                                 region.Id, typesString);
      return DataReader.GetList<T>(DataOperation.Parse(sql));
    }


    static internal void WriteGeographicItem(GeographicItem o) {
      var operation = DataOperation.Parse("writeGeoItem", o.Id, o.GetEmpiriaType().Id,
                                          o.Name, o.FullName, o.ExtensionData.ToString(), o.Keywords,
                                          o.Parent.Id, (char) o.Status, o.StartDate, o.EndDate);

      DataWriter.Execute(operation);
    }

    #endregion Internal methods

  } // class GeographicData

} // Empiria.Geography
