/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria.DynamicData / ExternalData         Component : Domain Layer                            *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Empiria Data Object                     *
*  Type     : ExternalValue                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds the value(s) of an external variable like a financial indicator or business income.      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Contacts;
using Empiria.Json;
using Empiria.StateEnums;

using Empiria.DynamicData.Datasets;

using Empiria.DynamicData.ExternalData.Adapters;
using Empiria.DynamicData.ExternalData.Data;

namespace Empiria.DynamicData.ExternalData {

  /// <summary>Holds the value(s) of an external variable like a financial indicator or business income.</summary>
  public class ExternalValue : BaseObject {

    #region Constructors and parsers

    protected ExternalValue() {
      // Required by Empiria Framework.
    }

    public ExternalValue(ExternalValueFields dto) {
      Assertion.Require(dto, nameof(dto));

      this.Load(dto);
    }


    static public ExternalValue Parse(int id) {
      return BaseObject.ParseId<ExternalValue>(id);
    }


    static public ExternalValue Empty {
      get {
        return ExternalValue.ParseEmpty<ExternalValue>();
      }
    }

    #endregion Constructors and parsers

    #region Properties

    [DataField("ID_VARIABLE_EXTERNA")]
    public ExternalVariable ExternalVariable {
      get;
      private set;
    }

    [DataField("VALORES_VARIABLE")]
    internal JsonObject ValuesExtData {
      get;
      private set;
    } = new JsonObject();


    [DataField("FECHA_APLICACION")]
    public DateTime ApplicationDate {
      get;
      private set;
    }


    [DataField("ID_EDITADO_POR")]
    public Contact UpdatedBy {
      get;
      private set;
    }


    [DataField("FECHA_EDICION")]
    public DateTime UpdatedDate {
      get;
      private set;
    }


    [DataField("ID_ARCHIVO")]
    public Dataset SourceDataset {
      get;
      private set;
    }


    [DataField("STATUS_VALOR_EXTERNO", Default = EntityStatus.Active)]
    public EntityStatus Status {
      get;
      private set;
    }

    #endregion Properties

    internal void Delete() {
      this.Status = EntityStatus.Deleted;
    }


    private void Load(ExternalValueFields dto) {
      this.ExternalVariable = ExternalVariable.Parse(dto.VariableUID);
      this.ValuesExtData = dto.GetDynamicFieldsAsJson();
      this.ApplicationDate = dto.ApplicationDate;
      this.SourceDataset = dto.Dataset;
      this.UpdatedBy = dto.UpdatedBy;
      this.UpdatedDate = dto.UpdatedDate;
      this.Status = dto.Status;
    }


    protected override void OnSave() {
      ExternalValuesData.Write(this);
    }


    public decimal GetTotalField(string fieldName) {
      return ValuesExtData.Get<decimal>(fieldName, 0m);
    }


    public DynamicFields GetDynamicFields() {
      var rawValues = this.ValuesExtData.ToDictionary();

      var fieldNames = this.ExternalVariable.Set.DataColumns.FindAll(x => x.Type == "decimal")
                                                            .Select(x => x.Field);

      var fields = new DynamicFields();

      foreach (var fieldName in fieldNames) {
        if (rawValues.ContainsKey(fieldName)) {
          fields.SetTotalField(fieldName, Convert.ToDecimal(rawValues[fieldName]));
        }
      }

      return fields;
    }

  } // class ExternalValue

}  // namespace Empiria.DynamicData.ExternalData
