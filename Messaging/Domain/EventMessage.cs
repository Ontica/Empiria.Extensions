///* Empiria Extensions ****************************************************************************************
//*                                                                                                            *
//*  Module   : Empiria Messaging                          Component : Domain Layer                            *
//*  Assembly : Empiria.Messaging.dll                      Pattern   : Information Holder                      *
//*  Type     : EventMessage                               License   : Please read LICENSE.txt file            *
//*                                                                                                            *
//*  Summary  : Contains information about an event occured in one object                                      *
//*                                                                                                            *
//************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
//using Empiria.Json;


//namespace Empiria.Messaging {

//  internal class EventMessage : Message {

//    #region Constructors and parsers

//    public EventMessage(string eventName) {
//      this.EventName = eventName;
//    }


//    public EventMessage(string eventName, JsonObject payload) {
//      this.EventName = eventName;
//      this.Payload = payload;
//    }


//    #endregion Constructors and parsers

//    #region Properties

//    public string EventName {
//      get;
//    }


//    public JsonObject Payload {
//      get;
//    } = new JsonObject();


//    #endregion Properties

//  }  // class EventMessage

//}  // namespace Empiria.Messaging
