///* Empiria Extensions ****************************************************************************************
//*                                                                                                            *
//*  Module   : Empiria Messaging                          Component : Domain Layer                            *
//*  Assembly : Empiria.Messaging.dll                      Pattern   : Information Holder                      *
//*  Type     : MessageChannel                             License   : Please read LICENSE.txt file            *
//*                                                                                                            *
//*  Summary  : A channel is a named resource that connects a message sender with its receiver.                *
//*             There are point-to-point and publish-subscribe channel types.                                  *
//*                                                                                                            *
//************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
//using System;

//using System.Collections.Generic;

//using Empiria.Json;
//using Empiria.StateEnums;

//namespace Empiria.Messaging {

//  /// <summary>A channel is a named resource that connects a message sender with its receiver.
//  /// There are point-to-point and publish-subscribe channel types.</summary>
//  public class MessageChannel : BaseObject {

//    #region Fields

//    private Lazy<List<Message>> messages = new Lazy<List<Message>>(() => new List<Message>());

//    #endregion Fields

//    #region Constructor and parsers

//    private MessageChannel() {
//      // Required by Empiria Framework.
//    }


//    static public MessageChannel Parse(int id) {
//      return BaseObject.ParseId<MessageChannel>(id);
//    }


//    static public MessageChannel Parse(string channelUID) {
//      return BaseObject.ParseKey<MessageChannel>(channelUID);
//    }


//    static internal MessageChannel Empty {
//      get {
//        return BaseObject.ParseEmpty<MessageChannel>();
//      }
//    }


//    protected override void OnLoadObjectData(System.Data.DataRow row) {
//      messages = new Lazy<List<Message>>(() => MessageChannelRepository.GetChannelMessages(this));
//    }


//    #endregion Constructor and parsers

//    #region Read properties and methods


//    public FixedList<Message> Messages {
//      get {
//        return messages.Value.ToFixedList();
//      }
//    }


//    public FixedList<Message> GetNextMessages() {
//      var messages = this.Messages.FindAll(x => x.IsInProcessStatus);

//      messages.Sort((x, y) => x.PostingTime.CompareTo(y.PostingTime));

//      return messages;
//    }


//    public FixedList<Message> GetUnitOfWorkMessages(string unitOfWorkUID) {
//      Assertion.AssertObject(unitOfWorkUID, "unitOfWorkUID");

//      return this.Messages.FindAll(x => x.UnitOfWorkUID == unitOfWorkUID &&
//                                        x.IsInProcessStatus);
//    }

//    internal void SendMessage(EventMessage message) {
//      throw new NotImplementedException();
//    }

//    public Message TryGetNextMessage() {
//      return this.Messages.Find(x => x.IsInProcessStatus);
//    }


//    public string TryGetNextUnitOfWork() {
//      var message = this.Messages.Find(x => x.UnitOfWorkUID.Length != 0 &&
//                                            x.IsInProcessStatus);

//      if (message != null) {
//        return message.UnitOfWorkUID;
//      } else {
//        return null;
//      }
//    }


//    #endregion Read properties and methods

//    #region Update methods

//    public void AddMessage(Message message) {
//      Assertion.AssertObject(message, "message");

//      message.Enqueue(this);

//      this.messages.Value.Add(message);
//    }


//    public void AddMessage(Message message, string unitOfWorkUID) {
//      Assertion.AssertObject(message, "message");
//      Assertion.AssertObject(unitOfWorkUID, "unitOfWorkUID");

//      message.Enqueue(this, unitOfWorkUID);

//      this.messages.Value.Add(message);
//    }


//    public void MarkAsProcessed(Message message, JsonObject processingData = null,
//                                ExecutionStatus status = ExecutionStatus.Completed) {
//      Assertion.AssertObject(message, "message");
//      Assertion.Assert(status != ExecutionStatus.Pending,
//                       "Can't set processing status as Pending.");

//      processingData = processingData ?? new JsonObject();

//      message.ChangeProcessingStatus(processingData, status);

//    }


//    #endregion Update methods

//  } // class MessageChannel

//} //namespace Empiria.Messaging
