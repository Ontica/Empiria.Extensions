/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Expressions               *
*  Namespace : Empiria.Expressions.Rules                        Assembly : Empiria.Expressions.dll           *
*  Type      : RulesData                                        Pattern  : Data Services Static Class        *
*  Version   : 6.7                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Provides data read methods for Empiria Foundation Ontology objects.                           *
*                                                                                                            *
********************************* Copyright (c) 2002-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System.Data;

using Empiria.Data;

namespace Empiria.Expressions.Rules {

  static internal class RulesData {

    #region Internal methods
    static internal DataRow GetRule(int ruleId) {
      string sql = "SELECT * FROM vwRules WHERE (RuleId = " + ruleId.ToString() + ")";

      return DataReader.GetDataRow(DataOperation.Parse(sql));
    }

    static internal DataTable GetRulesLibrary(int rulesLibraryId) {
      string sql = "SELECT * FROM vwRules WHERE (RulesLibraryId = " + rulesLibraryId.ToString() + ")";

      return DataReader.GetDataTable(DataOperation.Parse(sql));
    }

    static internal DataTable GetRuleItems(int ruleId, string ruleItemType) {
      string sql = "SELECT * FROM RuleStructure " +
                   "WHERE (RuleId = " + ruleId.ToString() + " AND RuleItemType = '" + ruleItemType + "') " +
                   "ORDER BY RuleItemIndex";

      return DataReader.GetDataTable(DataOperation.Parse(sql));
    }

    #endregion Internal methods

  } // class RulesData

} // namespace Empiria.Expressions.Rules
