/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria.DynamicData / ExternalData         Component : Domain Layer                            *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Information Holder                      *
*  Type     : ExternalValuesDataSet                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds the external values in a given date for a external variables set.                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using Empiria.Collections;

using Empiria.DynamicData.ExternalData.Data;

namespace Empiria.DynamicData.ExternalData {

  /// <summary>Holds the external values in a given date for a external variables set.</summary>
  public class ExternalValuesDataSet {

    #region Constructors and parsers

    public ExternalValuesDataSet(ExternalVariablesSet variablesSet, DateTime date) {
      Assertion.Require(variablesSet, nameof(variablesSet));

      VariablesSet = variablesSet;
      Date = date;
    }

    #endregion Constructors and parsers

    #region Properties

    public DateTime Date {
      get;
    }


    public ExternalVariablesSet VariablesSet {
      get;
    }

    #endregion Properties

    #region Methods

    internal FixedList<ExternalValueDatasetEntry> GetAllValues() {
      var list = new List<ExternalValueDatasetEntry>(VariablesSet.ExternalVariables.Count);

      FixedList<ExternalValue> valuesList = ExternalValuesData.GetValues(this.VariablesSet, this.Date);

      foreach (var variable in VariablesSet.GetVariables(this.Date)) {

        var externalValue = valuesList.Find(x => x.ExternalVariable.Equals(variable));

        ExternalValueDatasetEntry entry;

        if (externalValue != null) {
          entry = new ExternalValueDatasetEntry(variable,
                                                externalValue.GetDynamicFields());
        } else {
          entry = new ExternalValueDatasetEntry(variable);
        }

        list.Add(entry);
      }

      return list.ToFixedList();
    }


    internal FixedList<ExternalValueDatasetEntry> GetLoadedValues() {
      FixedList<ExternalValue> loadedValues = ExternalValuesData.GetValues(this.VariablesSet, this.Date);

      var list = new List<ExternalValueDatasetEntry>(loadedValues.Count);

      foreach (var externalValue in loadedValues) {

        var entry = new ExternalValueDatasetEntry(externalValue.ExternalVariable,
                                                  externalValue.GetDynamicFields());

        list.Add(entry);
      }

      return list.ToFixedList();
    }


    public EmpiriaHashTable<ExternalValue> GetLoadedValuesAsHashTable() {
      FixedList<ExternalValue> loadedValues = ExternalValuesData.GetValues(this.VariablesSet, this.Date);

      var hashTable = new EmpiriaHashTable<ExternalValue>(loadedValues.Count);

      foreach (var entry in loadedValues) {
        hashTable.Insert(entry.ExternalVariable.Code, entry);
      }

      return hashTable;
    }

    #endregion Methods

  } // class ExternalValuesDataSet

}  // namespace Empiria.DynamicData.ExternalData
