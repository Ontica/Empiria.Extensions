///* Empiria Extensions ****************************************************************************************
//*                                                                                                            *
//*  Module   : Empiria Messaging                          Component : Domain Layer                            *
//*  Assembly : Empiria.Messaging.dll                      Pattern   : Mediator                                *
//*  Type     : MessageBroker                              License   : Please read LICENSE.txt file            *
//*                                                                                                            *
//*  Summary  : Decouples the message sender from the receiver(s), determinating both the message channel      *
//*             and the receiver(s) inspecting the message content and type. It's the hub part of hub-spoke.   *
//*                                                                                                            *
//************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
//using System;

//namespace Empiria.Messaging {

//  /// <summary>Decouples the message sender from the receiver(s), determinating both the message channel
//  /// and the receiver(s) inspecting the message content and type. It's the hub part of hub-spoke.</summary>
//  internal class MessageBroker {

//    #region Static services

//    static internal MessageChannel GetMessageChannelFor(Message message) {
//      throw new NotImplementedException();
//    }


//    internal static void SendMessage(EventMessage message) {
//      MessageChannel channel = MessageBroker.GetMessageChannelFor(message);
//      MessageChannel receiver = MessageBroker.GetReceivers(message);

//      channel.SendMessage(message);
//    }

//    #endregion Static services

//  }  // class MessageBroker

//}  //namespace Empiria.Messaging
