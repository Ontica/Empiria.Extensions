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

namespace Empiria.Contacts {

  /// <summary>Response static methods for contact entities.</summary>
  static public class ContactResponseModels {

    static public ICollection ToShortResponse(this IList<Contact> list) {
      ArrayList array = new ArrayList(list.Count);

      foreach (var contact in list) {
        array.Add(contact.ToShortResponse());
      }
      return array;
    }


    static public object ToShortResponse(this Contact contact) {
      return new {
        uid = contact.UID,
        name = contact.Nickname.Length != 0 ? contact.Nickname :
                                              (contact.Alias.Length != 0 ? contact.Alias : contact.FullName)
      };
    }

  }  // class ContactResponseModels

}  // namespace Empiria.Contacts
