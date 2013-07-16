/* Empiria® Extended Framework 2013 **************************************************************************
*                                                                                                            *
*  Solution  : Empiria® Extended Framework                    System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeographicItemValidator                        Pattern  : Validation Services Static Class    *
*  Date      : 25/Jun/2013                                    Version  : 5.1     License: CC BY-NC-SA 3.0    *
*                                                                                                            *
*  Summary   : Static class that provides geographic items validation methods.                               *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2013. **/
using System;

namespace Empiria.Geography {

  /// <summary>Static class that provides geographic items validation methods.</summary>
  static public class GeographicItemValidator {

    #region Public methods

    static public GeographicRegionItem SearchSettlement(GeographicItemType settlementType,
                                                        GeographicRegionItem municipality,
                                                        string settlementName) {
      ObjectList<GeographicRegionItem> settlements =
                                          municipality.GetRegions("Municipality_Settlements", settlementType);

      foreach (GeographicRegionItem item in settlements) {
        if (EmpiriaString.Similar(item.Name, settlementName)) {
          return item;
        }
      }
      return GeographicRegionItem.Empty;
    }

    static public ObjectList<GeographicRegionItem> SearchSettlements(GeographicRegionItem municipality,
                                                                     string settlementName, decimal precision) {
      Predicate<GeographicRegionItem> predicate =
                        ((x) => EmpiriaString.JaroWinklerProximityFactor(x.Name, settlementName) >= precision);

      return municipality.GetRegions("Municipality_Settlements", predicate);
    }

    #endregion Public methods

  } // class GeographicItemValidator

} // namespace Empiria.Geography