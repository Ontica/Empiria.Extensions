/* Empiria Steps *********************************************************************************************
*                                                                                                            *
*  Module   : Project Management                           Component : Web Api                               *
*  Assembly : Empiria.ProjectManagement.WebApi.dll         Pattern   : Response methods                      *
*  Type     : ContactResponseModels                        License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Response static methods for contact entities.                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;
using System.Collections.Generic;

namespace Empiria.WebApi {

  /// <summary>Response static methods for contact entities.</summary>
  static public class KeyValueResponseModels {


    static public ICollection ToIdResponse<T>(this ICollection<T> list,
                                              Func<T, string> getNameFunction) where T : IIdentifiable {
      ArrayList array = new ArrayList(list.Count);

      foreach (var item in list) {
        array.Add(ToIdentifiableResponse(item.Id.ToString(), getNameFunction.Invoke(item)));
      }
      return array;
    }


    static public object ToIdentifiableResponse<T>(this T instance,
                                                   Func<T, string> getNameFunction) where T : IIdentifiable {
      return ToIdentifiableResponse(instance.UID.ToString(), getNameFunction.Invoke(instance));
    }


    static public ICollection ToIdentifiableResponse<T>(this ICollection<T> list,
                                                        Func<T, string> getNameFunction) where T : IIdentifiable {
      ArrayList array = new ArrayList(list.Count);

      foreach (var item in list) {
        var uid = String.IsNullOrWhiteSpace(item.UID) ? item.Id.ToString() : item.UID;

        array.Add(ToIdentifiableResponse(uid, getNameFunction.Invoke(item)));
      }
      return array;
    }


    static public object ToIdentifiableResponse(string uid, string name) {
      return new {
        uid,
        name
      };
    }


  }  // class KeyValueResponseModels

}  // namespace Empiria.WebApi
