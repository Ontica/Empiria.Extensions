﻿/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria.DynamicData / Datasets             Component : Domain Layer                            *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Empiria Object                          *
*  Type     : Dataset                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds information for a data set.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.IO;

using Empiria.Contacts;
using Empiria.Json;
using Empiria.Storage;
using Empiria.StateEnums;

using Empiria.DynamicData.Datasets.Adapters;
using Empiria.DynamicData.Datasets.Data;

namespace Empiria.DynamicData.Datasets {

  /// <summary>Holds information for a data set.</summary>
  public class Dataset : BaseObject {

    #region Constructors and parsers

    private Dataset() {
      // Required by Empiria Framework.
    }


    public Dataset(DatasetInputDto baseData, InputFile fileData, FileInfo fileInfo) {
      Assertion.Require(baseData, "baseData");
      Assertion.Require(fileData, "fileData");
      Assertion.Require(fileInfo, "fileInfo");

      LoadData(baseData);
      LoadFileData(fileData, fileInfo);
    }


    static internal Dataset Parse(int id) {
      return BaseObject.ParseId<Dataset>(id);
    }


    static public Dataset Parse(string uid) {
      return BaseObject.ParseKey<Dataset>(uid);
    }


    static public Dataset Empty => BaseObject.ParseEmpty<Dataset>();


    #endregion Constructors and parsers

    #region Properties

    [DataField("ID_TIPO_ARCHIVO")]
    public DatasetFamily DatasetFamily {
      get;
      private set;
    }

    [DataField("SUBTIPO_ARCHIVO")]
    private string _datasetKind;

    public DatasetKind DatasetKind {
      get {
        return this.DatasetFamily.GetDatasetKind(_datasetKind);
      }
    }


    [DataField("FECHA_OPERACION")]
    public DateTime OperationDate {
      get;
      private set;
    }


    [DataField("FECHA_ACTUALIZACION")]
    public DateTime UpdatedTime {
      get;
      private set;
    }


    [DataField("ID_ACTUALIZADO_POR")]
    public Contact UploadedBy {
      get;
      private set;
    }


    [DataField("ARCHIVO_EXT_DATA")]
    internal JsonObject ExtData {
      get;
      private set;
    } = JsonObject.Empty;


    [DataField("NOMBRE_ORIGINAL")]
    internal string OriginalFileName {
      get;
      private set;
    }


    [DataField("RUTA")]
    internal string FileName {
      get;
      private set;
    }


    [DataField("MEDIA_LENGTH")]
    internal int MediaLength {
      get;
      private set;
    }


    [DataField("MEDIA_TYPE")]
    public string MediaType {
      get;
      private set;
    }


    internal string FileUrl {
      get {
        return "http://server/sicofin/files/archivo.xlsx";
      }
    }


    [DataField("STATUS", Default = EntityStatus.Pending)]
    public EntityStatus Status {
      get;
      private set;
    } = EntityStatus.Pending;


    public string FullPath {
      get {
        return FileUtilities.GetFullPath(this.FileName);
      }
    }

    #endregion Properties

    #region Methods

    internal void Delete() {
      this.Status = EntityStatus.Deleted;
    }


    private void LoadData(DatasetInputDto baseData) {
      this.DatasetFamily  = DatasetFamily.Parse(baseData.DatasetFamilyUID);
      _datasetKind        = baseData.DatasetKind;
      this.OperationDate  = baseData.Date;
      this.UpdatedTime    = DateTime.Now;
      this.UploadedBy     = ExecutionServer.CurrentContact;
    }


    private void LoadFileData(InputFile fileData, FileInfo fileInfo) {
      this.OriginalFileName = fileData.OriginalFileName;
      this.MediaType        = fileData.MediaType;
      this.FileName         = fileInfo.Name;
      this.MediaLength      = (int) fileInfo.Length;
    }


    protected override void OnSave() {
      DatasetData.WriteDataset(this);
    }

    #endregion Methods

  }  // class Dataset

}  // namespace Empiria.DynamicData.Datasets
