/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria.DynamicData / ExternalData         Component : Use cases Layer                         *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Use case interactor class               *
*  Type     : ExternalVariablesUseCases                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to create and update external variables.                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Services;

using Empiria.DynamicData.ExternalData.Adapters;

namespace Empiria.DynamicData.ExternalData.UseCases {

  /// <summary>Use cases used to create and update external variables.</summary>
  public class ExternalVariablesUseCases : UseCase {

    #region Constructors and parsers

    protected ExternalVariablesUseCases() {
      // no-op
    }

    static public ExternalVariablesUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<ExternalVariablesUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public ExternalVariableDto AddVariable(string variablesSetUID, ExternalVariableFields fields) {
      Assertion.Require(variablesSetUID, nameof(variablesSetUID));
      Assertion.Require(fields, nameof(fields));

      ExternalVariablesSet set = ExternalVariablesSet.Parse(variablesSetUID);

      ExternalVariable variable = set.AddVariable(fields);

      variable.Save();

      return ExternalVariableMapper.Map(variable);
    }


    public FixedList<ExternalVariableDto> GetVariables(string variablesSetUID, DateTime date) {
      Assertion.Require(variablesSetUID, nameof(variablesSetUID));

      ExternalVariablesSet set = ExternalVariablesSet.Parse(variablesSetUID);

      return ExternalVariableMapper.Map(set.GetVariables(date));
    }


    public FixedList<ExternalVariablesSetDto> GetVariablesSets() {
      FixedList<ExternalVariablesSet> sets = ExternalVariablesSet.GetList();

      sets = base.RestrictUserDataAccessTo(sets);

      return ExternalVariableMapper.Map(sets);
    }


    public void RemoveVariable(string variablesSetUID, string variableUID) {
      Assertion.Require(variablesSetUID, nameof(variablesSetUID));
      Assertion.Require(variableUID, nameof(variableUID));

      ExternalVariablesSet set = ExternalVariablesSet.Parse(variablesSetUID);

      ExternalVariable variable = set.GetVariable(variableUID);

      set.DeleteVariable(variable);

      variable.Save();
    }


    public ExternalVariableDto UpdateVariable(string variablesSetUID,
                                              string variableUID,
                                              ExternalVariableFields fields) {
      Assertion.Require(variablesSetUID, nameof(variablesSetUID));
      Assertion.Require(variableUID, nameof(variableUID));
      Assertion.Require(fields, nameof(fields));

      ExternalVariablesSet set = ExternalVariablesSet.Parse(variablesSetUID);

      ExternalVariable variable = set.GetVariable(variableUID);

      set.UpdateVariable(variable, fields);

      variable.Save();

      return ExternalVariableMapper.Map(variable);
    }

    #endregion Use cases

  }  // class ExternalVariablesUseCases

}  // namespace Empiria.DynamicData.ExternalData.UseCases
