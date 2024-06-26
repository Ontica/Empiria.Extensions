﻿/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria.DynamicData / ExternalData         Component : Use cases Layer                         *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Use case interactor class               *
*  Type     : ExternalValuesUseCases                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to update and retrieve external values.                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Services;
using Empiria.Storage;

using Empiria.DynamicData.Datasets;
using Empiria.DynamicData.Datasets.Adapters;
using Empiria.DynamicData.Datasets.UseCases;

using Empiria.DynamicData.ExternalData.Adapters;
using Empiria.DynamicData.ExternalData.Data;

namespace Empiria.DynamicData.ExternalData.UseCases {

  /// <summary>Use cases used to update and retrieve external values.</summary>
  public class ExternalValuesUseCases : UseCase {

    #region Constructors and parsers

    protected ExternalValuesUseCases() {
      // no-op
    }

    static public ExternalValuesUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<ExternalValuesUseCases>();
    }


    #endregion Constructors and parsers

    #region Use cases

    public DatasetsLoadStatusDto CreateDataset(ExternalValuesDatasetDto dto,
                                               InputFile fileData) {
      Assertion.Require(dto, nameof(dto));
      Assertion.Require(fileData, nameof(fileData));

      dto.EnsureIsValid();

      using (var usecase = DatasetsUseCases.UseCaseInteractor()) {
        var mappedDto = dto.MapToCoreDatasetInputDto();

        Dataset dataset = usecase.CreateDataset(mappedDto, fileData);

        var reader = new ExternalValuesReader(dataset);

        try {

          FixedList<ExternalValueFields> entries = reader.GetEntries();

          var writer = new ExternalValuesWriter(dataset, entries);

          writer.Write();

        } catch {

          usecase.RemoveDataset(dataset.UID);

          throw;
        }
      }

      return GetDatasetsLoadStatus(dto);
    }


    public ExternalValuesDto GetExternalValues(ExternalValuesQuery query) {
      Assertion.Require(query, nameof(query));

      query.EnsureValid();

      ExternalVariablesSet variablesSet = query.GetExternalVariablesSet();

      var externalValuesDataSet = new ExternalValuesDataSet(variablesSet, query.Date);

      return ExternalValuesDataSetMapper.Map(query, externalValuesDataSet);
    }


    public DatasetsLoadStatusDto GetDatasetsLoadStatus(ExternalValuesDatasetDto dto) {
      Assertion.Require(dto, nameof(dto));

      dto.EnsureIsValid();

      using (var usecase = DatasetsUseCases.UseCaseInteractor()) {

        DatasetInputDto mappedDto = dto.MapToCoreDatasetInputDto();

        return usecase.GetDatasetsLoadStatus(mappedDto);
      }
    }


    public FixedList<ExternalVariableDto> GetExternalVariables(string setUID) {
      Assertion.Require(setUID, nameof(setUID));

      ExternalVariablesSet set = ExternalVariablesSet.Parse(setUID);

      return ExternalVariableMapper.Map(set.ExternalVariables);
    }


    public FixedList<NamedEntityDto> GetExternalVariablesSets() {
      FixedList<ExternalVariablesSet> sets = ExternalVariablesSet.GetList();

      return sets.MapToNamedEntityList();
    }


    public DatasetsLoadStatusDto RemoveDataset(string datasetUID) {
      Assertion.Require(datasetUID, nameof(datasetUID));

      using (var usecase = DatasetsUseCases.UseCaseInteractor()) {

        var dataset = Dataset.Parse(datasetUID);

        ExternalValuesData.RemoveDataset(dataset);

        return usecase.RemoveDataset(datasetUID);
      }
    }


    #endregion Use cases

  }  // class ExternalValuesUseCases

}  // namespace Empiria.DynamicData.ExternalData.UseCases
