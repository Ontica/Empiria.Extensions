/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeographicItemValidator                        Pattern  : Validation Services Static Class    *
*  Version   : 5.5        Date: 25/Jun/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Static class that provides geographic items validation methods.                               *
*                                                                                                            *
********************************* Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

namespace Empiria.Geography {

  /// <summary>Static class that provides geographic items validation methods.</summary>
  static public class GeographicItemValidator {

    #region Public methods

    static public GeographicRegionItem SearchSettlement(GeographicItemType settlementType,
                                                        GeographicRegionItem municipality,
                                                        string settlementName) {
      FixedList<GeographicRegionItem> settlements =
                                          municipality.GetRegions("Municipality_Settlements", settlementType);

      foreach (GeographicRegionItem item in settlements) {
        if (EmpiriaString.Similar(item.Name, settlementName)) {
          return item;
        }
      }
      return GeographicRegionItem.Empty;
    }

    static public FixedList<GeographicRegionItem> SearchSettlements(GeographicRegionItem municipality,
                                                                     string settlementName, decimal precision) {
      Predicate<GeographicRegionItem> predicate =
                        ((x) => EmpiriaString.JaroWinklerProximityFactor(x.Name, settlementName) >= precision);

      return municipality.GetRegions("Municipality_Settlements", predicate);
    }

    #endregion Public methods

  } // class GeographicItemValidator

} // namespace Empiria.Geography
