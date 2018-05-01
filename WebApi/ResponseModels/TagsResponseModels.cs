/* Empiria Steps *********************************************************************************************
*                                                                                                            *
*  Module   : Project Management                           Component : Web Api                               *
*  Assembly : Empiria.ProjectManagement.WebApi.dll         Pattern   : Response methods                      *
*  Type     : TagResponseModels                            License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Response static methods for tag values.                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;

namespace Empiria.Collections {

  /// <summary>Response static methods for tag values.</summary>
  static public class TagResponseModels {

    static public ICollection ToResponse(this TagsCollection tags) {
      ArrayList array = new ArrayList(tags.Count);

      foreach (var tag in tags.Items) {
        var item = new {
          name = tag,
          color = "default",
        };
        array.Add(item);
      }
      return array;
    }

  }  // class TagResponseModels

}  // namespace Empiria.Collections
