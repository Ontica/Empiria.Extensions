/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Data Objects                       Component : Service Provider                        *
*  Assembly : Empiria.Data.DataObjects.dll               Pattern   : Information Holder                      *
*  Type     : DataStore                                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Data stores describe elements responsible of store and persist data, like databases, file      *
*             systems, e-mail storages, spreedsheets, or PDF files. Typically they allow read or write       *
*             access using special protocols and secure connections.                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.IO;
using Empiria.Json;

namespace Empiria.Data.DataObjects {

  /// <summary>Data stores describe elements responsible of store and persist data, like databases, file
  /// systems, e-mail storages, spreedsheets, or PDF files. Typically they allow read or write
  /// access using special protocols and secure connections.</summary>
  public class DataStore : DataItem {

    static private string TEMPLATES_FOLDER = ConfigurationData.Get<string>("DataStoreTemplates.FolderPath");

    #region Constructors and parsers

    protected DataStore(DataItemType powertype) : base(powertype) {
      // Required by Empiria Framework for all partitioned types.
    }


    static internal new DataStore Parse(int id) {
      return BaseObject.ParseId<DataStore>(id);
    }


    static public new DataStore Parse(string uid) {
      return BaseObject.ParseKey<DataStore>(uid);
    }


    static public FixedList<DataStore> GetList() {
      return DataItemsRepository.GetDataStores();
    }


    static public new DataStore Empty => BaseObject.ParseEmpty<DataStore>();


    #endregion Constructors and parsers

    #region Properties

    [DataField("DataItemActions")]
    public string Actions {
      get;
      private set;
    }


    [DataField("DataItemKnowledgeBases")]
    public string KnowledgeBases {
      get;
      private set;
    }


    [DataField("DataItemTemplate")]
    public string Template {
      get;
      private set;
    }


    public FileInfo TemplateFileInfo {
      get {
        var filePath = GetFilePath();

        return new FileInfo(filePath);
      }
    }

    public DirectoryInfo TemplateFolderInfo {
      get {
        var filePath = GetFilePath();

        return new DirectoryInfo(filePath);
      }
    }


   public string GetMediaFormat() {
      string template = this.Template;
      string namedKey = this.NamedKey;

      if (template.EndsWith("pdf") || namedKey.Contains("PDF")) {
        return "PDF";

      } else if (template.EndsWith("xlsx") || namedKey.Contains("Excel")) {
        return "Excel";

      } else if (namedKey.Contains("WebForm")) {
        return "WebForm";

      } else if (namedKey.Contains("WebGrid")) {
        return "WebGrid";

      } else if (namedKey.Contains("DataFile")) {
        return "DataFile";

      } else if (namedKey.Contains("Folder")) {
        return "Folder";

      } else if (namedKey.Contains("Database")) {
        return "Database";

      }
      return "Unknown";
    }

    #endregion Properties

    private string GetFilePath() {
      var path = Template.Replace("~", TEMPLATES_FOLDER);

      path = path.Replace("/", @"\");

      return path;
    }

    public FixedList<DataFormField> GetFormFields() {
      return base.ExtensionData.GetList<DataFormField>("fields").ToFixedList();
    }

    public JsonObject GetPDFFormFields() {
      return base.ExtensionData.Slice("autofillFields", false);
    }


  }  // class DataStore

}  // namespace Empiria.Data.DataObjects
