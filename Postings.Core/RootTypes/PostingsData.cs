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

namespace Empiria.Postings {

  /// <summary>Data read and write methods for postings.</summary>
  static internal class PostingsData {

    static internal void WritePosting(ObjectPosting o) {
      var op = DataOperation.Parse("writePosting",
                                    o.Id, o.GetEmpiriaType().Id, o.UID,
                                    o.ObjectType, o.ObjectUID,
                                    o.ExtensionData.ToString(), o.Text, o.Keywords,
                                    (char) o.AccessMode, o.Owner.Id, o.ParentId,
                                    o.Date, (char) o.Status);

      DataWriter.Execute(op);
    }

    static internal FixedList<ObjectPosting> GetObjectPostingsList(string objectType, string objectUID) {
      string filter = $"(ObjectType = '{objectType}' AND ObjectUID = '{objectUID}' AND Status <> 'X')";

      return BaseObject.GetList<ObjectPosting>(filter, "PostingDate")
                       .ToFixedList();
    }

  }  // class PostingsData

}  // namespace Empiria.Postings
