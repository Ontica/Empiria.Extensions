﻿/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Dynamic Data                       Component : Common Types                            *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Information holder                      *
*  Type     : DataTableColumn                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Describes a data table column.                                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Json;

namespace Empiria.DynamicData {

  /// <summary>Describes a data table column.</summary>
  public class DataTableColumn {

    #region Constructors and parsers

    private DataTableColumn() {
      // no-op
    }


    public DataTableColumn(string field, string title, string type, int digits = 2) {
      this.Field = field;
      this.Title = title;
      this.Type = type;

      if (type == "decimal") {
        this.Digits = digits;
      }
    }


    static public DataTableColumn Parse(JsonObject json) {
      Assertion.Require(json, nameof(json));

      return new DataTableColumn {
        Field = json.Get<string>("field"),
        Title = json.Get<string>("title"),
        Type = json.Get<string>("type"),
        Digits = json.Get<int>("digits", 2),
        Column = json.Get<string>("column", string.Empty),
        Formula = json.Get<string>("formula", string.Empty),
        Tags = json.Get<string>("tags", string.Empty),
        Hidden = json.Get<bool>("hidden", false)
      };
    }


    #endregion Constructors and parsers

    #region Properties

    public string Field {
      get; private set;
    }


    public string Title {
      get; private set;
    }


    public string Type {
      get; private set;
    }


    public string Column {
      get; private set;
    } = string.Empty;


    public int Digits {
      get; private set;
    }

    public string Tags {
      get; private set;
    }

    public string Formula {
      get; private set;
    } = string.Empty;


    public bool IsCalculated {
      get {
        return EmpiriaString.TrimAll(Formula).Length != 0;
      }
    }


    public bool Show {
      get {
        return !Hidden;
      }
    }


    private bool Hidden {
      get; set;
    }


    #endregion Properties

  }  // class DataTableColumn

}  // namespace Empiria.DynamicData
