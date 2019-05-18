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


    static internal FixedList<T> GetPostingsList<T>(BaseObject nodeObject,
                                                    string postingType) where T : BaseObject {
      var typeInfo = ObjectTypeInfo.Parse<T>();

      var dataSource = typeInfo.DataSource;
      var dataSourceUIDFieldName = typeInfo.NamedIdFieldName;

      string sql = $"SELECT {dataSource}.* FROM {dataSource} INNER JOIN EXFPostings " +
                   $"ON {dataSource}.{dataSourceUIDFieldName} = EXFPostings.PostedItemUID " +
                   $"WHERE (EXFPostings.NodeObjectUID = '{nodeObject.UID}' AND " +
                   $"EXFPostings.PostingType = '{postingType}' AND EXFPostings.PostingStatus <> 'X') " +
                   $"ORDER BY EXFPostings.PostingIndex, EXFPostings.PostingTime";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<T>(op);
    }


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


    static internal void WritePosting(Posting o) {
      var op = DataOperation.Parse("writeEXFPosting",
                                    o.Id, o.UID, o.PostingType,
                                    o.NodeObjectUID, o.PostedItemUID,
                                    o.Index, o.ExtensionData.ToString(),
                                    o.PostingTime, o.PostedBy.Id, (char) o.Status);

      DataWriter.Execute(op);
    }

  }  // class PostingsData

}  // namespace Empiria.Postings
