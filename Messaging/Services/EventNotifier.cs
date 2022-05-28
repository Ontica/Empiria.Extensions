/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Messaging                          Component : Services Layer                          *
*  Assembly : Empiria.Messaging.dll                      Pattern   : Service provider                        *
*  Type     : EventNotifier                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Service to notify or announce that an event was occured.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

namespace Empiria.Messaging {

  /// <summary>Service to notify or announce that an event was occured.</summary>
  static public class EventNotifier {

    #region Public services

    static public void Notify(string eventName) {
      NotifyImplementation(eventName);
    }


    static public void Notify(Enum eventName) {
      NotifyImplementation(eventName.ToString());
    }


    static public void Notify(string eventName, JsonObject payload) {
      NotifyImplementation(eventName, payload);
    }


    static public void Notify(string eventName, object payload) {
      NotifyImplementation(eventName, JsonObject.Parse(payload));
    }


    static public void Notify(Enum eventName, JsonObject payload) {
      NotifyImplementation(eventName.ToString(), payload);
    }


    static public void Notify(Enum eventName, object payload) {
      NotifyImplementation(eventName.ToString(), JsonObject.Parse(payload));
    }

    #endregion Public services

    #region Private methods

    static private void NotifyImplementation(string eventName) {
      Assertion.Require(eventName, "eventName");

      //var message = new EventMessage(eventName);

      //MessageBroker.SendMessage(message);
    }


    static private void NotifyImplementation(string eventName, JsonObject payload) {
      Assertion.Require(eventName, "eventName");
      Assertion.Require(payload, "payload");

      //var message = new EventMessage(eventName, payload);

      //MessageBroker.SendMessage(message);
    }


    #endregion Private methods

  }  // class EventNotifier

} // namespace Empiria.Messaging
