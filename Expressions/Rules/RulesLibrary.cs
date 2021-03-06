﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Expressions               *
*  Namespace : Empiria.Expressions.Rules                        Assembly : Empiria.Expressions.dll           *
*  Type      : RulesLibrary                                     Pattern  : Ontology Object Type              *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Represents a collection of business rules belonging to a named library.                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.Data;

namespace Empiria.Expressions.Rules {

  /// <summary>Represents a rules library type.</summary>
  public class RulesLibrary : GeneralObject {

    #region Fields

    private BusinessRule[] rules = null;

    #endregion Fields

    #region Constructors and parsers

    private RulesLibrary() {
      // Required by Empiria Framework.
    }

    static public RulesLibrary Parse(int id) {
      return BaseObject.ParseId<RulesLibrary>(id);
    }

    static public RulesLibrary Parse(string rulesLibraryName) {
      return BaseObject.ParseKey<RulesLibrary>(rulesLibraryName);
    }

    #endregion Constructors and parsers

    public int Count {
      get { return Rules.Length; }
    }

    public BusinessRule[] Rules {
      get {
        if (rules == null) {
          rules = LoadRules();
        }
        return rules;

      }
    }

    public BusinessRule GetRule(int index) {
      return Rules[index];
    }

    public BusinessRule GetRule(string ruleName) {
      for (int i = 0; i < rules.Length; i++) {
        if (Rules[i].Name.ToLowerInvariant() == ruleName.ToLowerInvariant()) {
          return Rules[i];
        }
      }
      return Rules[0];
    }

    private BusinessRule[] LoadRules() {
      DataTable rulesTable = RulesData.GetRulesLibrary(this.Id);
      BusinessRule[] rulesArray = new BusinessRule[rulesTable.Rows.Count];

      for (int i = 0; i < rulesTable.Rows.Count; i++) {
        rulesArray[i] = BusinessRule.ParseWithDataRow(rulesTable.Rows[i]);
      }
      return rulesArray;
    }

  } // class RulesLibrary

} // namespace Empiria.Expressions.Rules
