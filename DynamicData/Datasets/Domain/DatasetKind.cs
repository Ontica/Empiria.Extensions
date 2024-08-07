﻿/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria.DynamicData / Datasets             Component : Domain Layer                            *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Information Holder                      *
*  Type     : DatasetKind                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds configuration rules and data for a dataset.                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;
using Empiria.Storage;

namespace Empiria.DynamicData.Datasets {

  /// <summary>Holds configuration rules and data for a dataset.</summary>
  public class DatasetKind {

    protected internal DatasetKind() {
      // no-op
    }

    static internal DatasetKind Parse(JsonObject json) {
      return new DatasetKind {
        UID = json.Get<string>("uid"),
        Name = json.Get<string>("name"),
        FileType = json.Get<FileType>("fileType", FileType.Excel),
        DataFormat = json.Get<string>("dataFormat", "Default"),
        Optional = json.Get<bool>("optional", false),
        Count = json.Get<int>("count", 1),
        TemplateId = json.Get<int>("templateId", -1)
      };
    }

    #region Properties

    public string UID {
      get; private set;
    }


    public string Name {
      get; private set;
    }


    public FileType FileType {
      get; private set;
    }


    public string DataFormat {
      get; private set;
    }


    public bool Optional {
      get; private set;
    }


    public int Count {
      get; private set;
    }


    public int TemplateId {
      get; private set;
    }


    public string TemplateUrl {
      get {
        if (TemplateId != -1) {
          return FileTemplateConfig.Parse(TemplateId).TemplateUrl;
        } else {
          return string.Empty;
        }
      }
    }

    #endregion Properties

  }  // class DatasetKind

}  // namespace Empiria.DynamicData.Datasets
