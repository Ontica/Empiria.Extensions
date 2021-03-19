/* Empiria Postings ******************************************************************************************
*                                                                                                            *
*  Module   : Empiria Postings                             Component : Domain services                       *
*  Assembly : Empiria.Postings.dll                         Pattern   : Data Service                          *
*  Type     : PostingsData                                 License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Data read and write methods for postings.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;

using Empiria.Ontology;

namespace Empiria.Postings {

  /// <summary>Data read and write methods for postings.</summary>
  static internal class PostingsData {

    static private readonly string POSTINGS_TABLE_NAME =
                                          ConfigurationData.Get("Postings.TableName", "EXFPostings");

    static internal FixedList<T> GetNodeObjectsList<T>(string postingType) where T : BaseObject {
      var typeInfo = ObjectTypeInfo.Parse<T>();

      var dataSource = typeInfo.DataSource;
      var dataSourceUIDFieldName = typeInfo.NamedIdFieldName;

      string sql = $"SELECT DISTINCT {dataSource}.* FROM {dataSource} INNER JOIN {POSTINGS_TABLE_NAME} " +
                   $"ON {dataSource}.{dataSourceUIDFieldName} = {POSTINGS_TABLE_NAME}.NodeObjectUID " +
                   $"WHERE ({POSTINGS_TABLE_NAME}.PostingType = '{postingType}' AND " +
                   $"{POSTINGS_TABLE_NAME}.PostingStatus <> 'X') " +
                   $"ORDER BY {POSTINGS_TABLE_NAME}.PostingIndex, {POSTINGS_TABLE_NAME}.PostingTime";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<T>(op);
    }


    static internal FixedList<T> GetNodeObjectsList<T>(BaseObject postedItem,
                                                       string postingType) where T : BaseObject {
      var typeInfo = ObjectTypeInfo.Parse<T>();

      var dataSource = typeInfo.DataSource;
      var dataSourceUIDFieldName = typeInfo.NamedIdFieldName;

      string sql = $"SELECT DISTINCT {dataSource}.* FROM {dataSource} INNER JOIN {POSTINGS_TABLE_NAME} " +
                   $"ON {dataSource}.{dataSourceUIDFieldName} = {POSTINGS_TABLE_NAME}.NodeObjectUID " +
                   $"WHERE ({POSTINGS_TABLE_NAME}.PostedItemUID = '{postedItem.UID}' AND " +
                   $"{POSTINGS_TABLE_NAME}.PostingType = '{postingType}' AND " +
                   $"{POSTINGS_TABLE_NAME}.PostingStatus <> 'X') " +
                   $"ORDER BY {POSTINGS_TABLE_NAME}.PostingIndex, {POSTINGS_TABLE_NAME}.PostingTime";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<T>(op);
    }


    static internal FixedList<T> GetPostedItemsList<T>(string postingType) where T : BaseObject {
      var typeInfo = ObjectTypeInfo.Parse<T>();

      var dataSource = typeInfo.DataSource;
      var dataSourceUIDFieldName = typeInfo.NamedIdFieldName;

      string sql = $"SELECT DISTINCT {dataSource}.* FROM {dataSource} INNER JOIN {POSTINGS_TABLE_NAME} " +
                   $"ON {dataSource}.{dataSourceUIDFieldName} = {POSTINGS_TABLE_NAME}.PostedItemUID " +
                   $"WHERE ({POSTINGS_TABLE_NAME}.PostingType = '{postingType}' " +
                   $"AND {POSTINGS_TABLE_NAME}.PostingStatus <> 'X') " +
                   $"ORDER BY {POSTINGS_TABLE_NAME}.PostingIndex, {POSTINGS_TABLE_NAME}.PostingTime";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<T>(op);
    }


    static internal FixedList<T> GetPostedItemsList<T>(BaseObject nodeObject) where T : BaseObject {
      var typeInfo = ObjectTypeInfo.Parse<T>();

      var dataSource = typeInfo.DataSource;
      var dataSourceUIDFieldName = typeInfo.NamedIdFieldName;

      string sql = $"SELECT {dataSource}.* FROM {dataSource} INNER JOIN {POSTINGS_TABLE_NAME} " +
                   $"ON {dataSource}.{dataSourceUIDFieldName} = {POSTINGS_TABLE_NAME}.PostedItemUID " +
                   $"WHERE ({POSTINGS_TABLE_NAME}.NodeObjectUID = '{nodeObject.UID}' AND " +
                   $"{POSTINGS_TABLE_NAME}.PostingStatus <> 'X') " +
                   $"ORDER BY {POSTINGS_TABLE_NAME}.PostingIndex, {POSTINGS_TABLE_NAME}.PostingTime";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<T>(op);
    }


    static internal FixedList<T> GetPostedItemsList<T>(BaseObject nodeObject,
                                                       string postingType) where T : BaseObject {
      var typeInfo = ObjectTypeInfo.Parse<T>();

      var dataSource = typeInfo.DataSource;
      var dataSourceUIDFieldName = typeInfo.NamedIdFieldName;

      string sql = $"SELECT {dataSource}.* FROM {dataSource} INNER JOIN {POSTINGS_TABLE_NAME} " +
                   $"ON {dataSource}.{dataSourceUIDFieldName} = {POSTINGS_TABLE_NAME}.PostedItemUID " +
                   $"WHERE ({POSTINGS_TABLE_NAME}.NodeObjectUID = '{nodeObject.UID}' AND " +
                   $"{POSTINGS_TABLE_NAME}.PostingType = '{postingType}' AND " +
                   $"{POSTINGS_TABLE_NAME}.PostingStatus <> 'X') " +
                   $"ORDER BY {POSTINGS_TABLE_NAME}.PostingIndex, {POSTINGS_TABLE_NAME}.PostingTime";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<T>(op);
    }


    static internal Posting GetPosting(BaseObject nodeObject, BaseObject postedItem) {
      string sql = $"SELECT {POSTINGS_TABLE_NAME}.* FROM {POSTINGS_TABLE_NAME} " +
                   $"WHERE ({POSTINGS_TABLE_NAME}.NodeObjectUID = '{nodeObject.UID}' AND " +
                   $"{POSTINGS_TABLE_NAME}.PostedItemUID = '{postedItem.UID}' AND " +
                   $"{POSTINGS_TABLE_NAME}.PostingStatus <> 'X') ";
      var op = DataOperation.Parse(sql);

      return DataReader.GetObject<Posting>(op);
    }


    static internal FixedList<Posting> GetPostingsList(string postingType) {
      string sql = $"SELECT {POSTINGS_TABLE_NAME}.* FROM {POSTINGS_TABLE_NAME} " +
                   $"WHERE ({POSTINGS_TABLE_NAME}.PostingType = '{postingType}' AND " +
                   $"{POSTINGS_TABLE_NAME}.PostingStatus <> 'X') " +
                   $"ORDER BY {POSTINGS_TABLE_NAME}.PostingIndex, {POSTINGS_TABLE_NAME}.PostingTime";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<Posting>(op);
    }


    static internal FixedList<Posting> GetPostingsList(BaseObject nodeObject, string postingType) {
      string sql = $"SELECT {POSTINGS_TABLE_NAME}.* FROM {POSTINGS_TABLE_NAME} " +
                   $"WHERE ({POSTINGS_TABLE_NAME}.NodeObjectUID = '{nodeObject.UID}' AND " +
                   $"{POSTINGS_TABLE_NAME}.PostingType = '{postingType}' AND " +
                   $"{POSTINGS_TABLE_NAME}.PostingStatus <> 'X') " +
                   $"ORDER BY {POSTINGS_TABLE_NAME}.PostingIndex, {POSTINGS_TABLE_NAME}.PostingTime";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<Posting>(op);
    }


    static internal FixedList<Posting> GetPostingsList(BaseObject nodeObject,
                                                       BaseObject postedItem,
                                                       string postingType) {
      string sql = $"SELECT {POSTINGS_TABLE_NAME}.* FROM {POSTINGS_TABLE_NAME} " +
                   $"WHERE ({POSTINGS_TABLE_NAME}.NodeObjectUID = '{nodeObject.UID}' AND " +
                   $"{POSTINGS_TABLE_NAME}.PostedItemUID = '{postedItem.UID}' AND " +
                   $"{POSTINGS_TABLE_NAME}.PostingType = '{postingType}' AND " +
                   $"{POSTINGS_TABLE_NAME}.PostingStatus <> 'X') " +
                   $"ORDER BY {POSTINGS_TABLE_NAME}.PostingIndex, {POSTINGS_TABLE_NAME}.PostingId";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<Posting>(op);
    }


    static internal FixedList<Posting> GetPostingsForPostedItemList(BaseObject postedItem, string postingType) {
      string sql = $"SELECT {POSTINGS_TABLE_NAME}.* FROM {POSTINGS_TABLE_NAME} " +
                   $"WHERE ({POSTINGS_TABLE_NAME}.PostedItemUID = '{postedItem.UID}' AND " +
                   $"{POSTINGS_TABLE_NAME}.PostingType = '{postingType}' AND " +
                   $"{POSTINGS_TABLE_NAME}.PostingStatus <> 'X') " +
                   $"ORDER BY {POSTINGS_TABLE_NAME}.PostingIndex, {POSTINGS_TABLE_NAME}.PostingTime";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<Posting>(op);
    }


    static internal void WritePosting(Posting o) {
      var op = DataOperation.Parse("writeEXFPosting",
                                    o.Id, o.UID, o.PostingType,
                                    o.NodeObjectUID, o.PostedItemUID,
                                    o.Index, o.ExtensionData.ToString(),
                                    o.PostingTime, o.PostedBy.Id, (char) o.Status);

      DataWriter.Execute(op);
    }


    // To be deprecated (ObjectPosting)

    static internal FixedList<ObjectPosting> GetObjectPostingsList(string objectUID, string keywords = "") {
      string filter = $"(ObjectUID = '{objectUID}' AND Status <> 'X')";

      if (keywords.Length != 0) {
        filter += " AND ";
        filter += SearchExpression.ParseAndLike("Keywords", EmpiriaString.BuildKeywords(keywords));
      }

      return BaseObject.GetList<ObjectPosting>(filter, "PostingTime")
                       .ToFixedList();
    }


    static internal void WritePosting(ObjectPosting o) {
      var op = DataOperation.Parse("writePosting",
                                    o.Id, o.GetEmpiriaType().Id, o.UID,
                                    o.ObjectUID, o.Authors,
                                    o.Title, o.Body, o.Tags,
                                    o.FilePath, o.Keywords, o.Updated,
                                    (char) o.AccessMode, o.Owner.Id,
                                    o.ParentId, o.Date, (char) o.Status);
      DataWriter.Execute(op);
    }


  }  // class PostingsData

}  // namespace Empiria.Postings
