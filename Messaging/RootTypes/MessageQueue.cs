/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Message Queue Services                       Component : Message Queue                         *
*  Assembly : Empiria.Messaging.dll                        Pattern   : Information Holder                    *
*  Type     : MessageQueue                                 License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Queue of messages sent between applications, used to decouple systems                          *
*             through an asynchronous communication protocol.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using System.Collections.Generic;

using Empiria.Json;
using Empiria.StateEnums;

namespace Empiria.Messaging {

  /// <summary>Queue of messages sent between applications, used to decouple systems through
  /// an asynchronous communication protocol.</summary>
  public class MessageQueue: BaseObject {

    #region Fields

    private Lazy<List<Message>> messages = new Lazy<List<Message>>(() => new List<Message>());

    #endregion Fields

    #region Constructor and parsers

    private MessageQueue() {
      // Required by Empiria Framework.
    }


    static public MessageQueue Parse(int id) {
      return BaseObject.ParseId<MessageQueue>(id);
    }


    static public MessageQueue Parse(string queueUID) {
      return BaseObject.ParseKey<MessageQueue>(queueUID);
    }


    static internal MessageQueue Empty {
      get {
        return BaseObject.ParseEmpty<MessageQueue>();
      }
    }


    protected override void OnLoadObjectData(System.Data.DataRow row) {
      messages = new Lazy<List<Message>>(() => MessageQueueData.GetQueueMessages(this));
    }


    #endregion Constructor and parsers

    #region Read properties and methods


    public FixedList<Message> Messages {
      get {
        return messages.Value.ToFixedList();
      }
    }


    public FixedList<Message> GetNextMessages() {
      return this.Messages.FindAll(x => x.IsInProcessStatus);
    }


    public FixedList<Message> GetUnitOfWorkMessages(string unitOfWorkUID) {
      Assertion.AssertObject(unitOfWorkUID, "unitOfWorkUID");

      return this.Messages.FindAll(x => x.UnitOfWorkUID == unitOfWorkUID &&
                                        x.IsInProcessStatus);
    }


    public Message TryGetNextMessage() {
      return this.Messages.Find(x => x.IsInProcessStatus);
    }


    public string TryGetNextUnitOfWork() {
      var message = this.Messages.Find(x => x.UnitOfWorkUID.Length != 0 &&
                                            x.IsInProcessStatus);

      if (message != null) {
        return message.UnitOfWorkUID;
      } else {
        return null;
      }
    }


    #endregion Read properties and methods

    #region Update methods

    public void AddMessage(Message message) {
      Assertion.AssertObject(message, "message");

      message.Enqueue(this);

      this.messages.Value.Add(message);
    }


    public void AddMessage(Message message, string unitOfWorkUID) {
      Assertion.AssertObject(message, "message");
      Assertion.AssertObject(unitOfWorkUID, "unitOfWorkUID");

      message.Enqueue(this, unitOfWorkUID);

      this.messages.Value.Add(message);
    }


    public void MarkAsProcessed(Message message, JsonObject processingData = null,
                                ExecutionStatus status = ExecutionStatus.Completed) {
      Assertion.AssertObject(message, "message");
      Assertion.Assert(status != ExecutionStatus.Pending,
                       "Can't set processing status as Pending.");

      processingData = processingData ?? new JsonObject();

      message.ChangeProcessingStatus(processingData, status);

    }


    #endregion Update methods

  } // class MessageQueue

} //namespace Empiria.Messaging
