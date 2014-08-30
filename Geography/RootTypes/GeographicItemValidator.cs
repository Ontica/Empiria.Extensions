/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeographicItemValidator                        Pattern  : Validation Services Static Class    *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Static class that provides geographic items validation methods.                               *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

namespace Empiria.Geography {

  /// <summary>Static class that provides geographic items validation methods.</summary>
  static public class GeographicItemValidator {

    #region Public methods

    static public Settlement SearchSettlement(SettlementType settlementType,
                                              Municipality municipality,
                                              string settlementName) {
      FixedList<Settlement> settlements = municipality.GetSettlements(settlementType);

      foreach (Settlement item in settlements) {
        if (EmpiriaString.Similar(item.Name, settlementName)) {
          return item;
        }
      }
      return Settlement.Empty;
    }

    static public FixedList<Settlement> SearchSettlements(Municipality municipality,
                                                          string settlementName, decimal precision) {
      Predicate<Settlement> predicate =
                  ((x) => EmpiriaString.JaroWinklerProximityFactor(x.Name, settlementName) >= precision);

      return municipality.Settlements.FindAll(predicate).ToFixedList();
    }

    #endregion Public methods

  } // class GeographicItemValidator

} // namespace Empiria.Geography
