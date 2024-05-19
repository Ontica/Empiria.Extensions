/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria.DynamicData / ExternalData         Component : Domain Layer                            *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Service provider                        *
*  Type     : ExternalValuesWriter                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Service that writes all external values for a given dataset.                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.DynamicData.Datasets;

using Empiria.DynamicData.ExternalData.Adapters;

namespace Empiria.DynamicData.ExternalData {

  /// <summary>Service that writes all external values for a given dataset.</summary>
  internal class ExternalValuesWriter {

    private readonly Dataset _dataset;
    private readonly FixedList<ExternalValueFields> _externalValues;

    public ExternalValuesWriter(Dataset dataset, FixedList<ExternalValueFields> externalValues) {
      Assertion.Require(dataset, nameof(dataset));
      Assertion.Require(externalValues, nameof(externalValues));

      _dataset = dataset;
      _externalValues = externalValues;
    }

    internal void Write() {
      foreach (var data in _externalValues) {

        var externalValue = new ExternalValue(data);

        externalValue.Save();
      }
    }

  }  // class ExternalValuesWriter

}  // namespace Empiria.DynamicData.ExternalData
