/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Message Queue Services                       Component : Message Queue                         *
*  Assembly : Empiria.Messaging.dll                        Pattern   : Information Holder                    *
*  Type     : Message                                      License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Describes a message.                                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;
using Empiria.Ontology;
using Empiria.StateEnums;

namespace Empiria.Messaging {

  /// <summary>Describes a message.</summary>
  [PartitionedType(typeof(MessageType))]
  public class Message : BaseObject {

    #region Constructors and parsers

    protected Message(MessageType messageType): base(messageType) {
      // Required by Empiria Framework.
    }


    public Message(JsonObject data) : this(MessageType.Default, data) {
      // no-op
    }


    protected Message(MessageType messageType, JsonObject data): base(messageType) {
      Assertion.AssertObject(data, "data");

      this.MessageData = data;
    }


    static internal Message Parse(int id) {
      return BaseObject.ParseId<Message>(id);
    }

    public bool IsReadyToProcess() {
      return this.IsInProcessStatus && this.PostingTime.AddMinutes(this.Queue.DefaultProcessingDelayMinutes) > DateTime.Now;
    }

    private bool IsInProcessStatus {
      get {
        return (this.ProcessingStatus == ExecutionStatus.Pending || this.ProcessingStatus == ExecutionStatus.Failed);
      }
    }

    static internal Message Parse(string uid) {
      return BaseObject.ParseKey<Message>(uid);
    }


    static public Message Empty {
      get {
        return BaseObject.ParseEmpty<Message>();
      }
    }


    #endregion Constructor and parsers

    #region Properties

    public MessageType MessageType {
      get {
        return (MessageType) base.GetEmpiriaType();
      }
    }


    [DataField("QueueId")]
    public MessageQueue Queue {
      get;
      private set;
    }


    [DataField("UnitOfWorkUID")]
    public string UnitOfWorkUID {
      get;
      private set;
    }


    [DataField("MessageData")]
    public JsonObject MessageData {
      get;
      private set;
    }


    [DataField("PostingTime")]
    public DateTime PostingTime {
      get;
      private set;
    }


    [DataField("ProcessingTime")]
    public DateTime ProcessingTime {
      get;
      private set;
    }


    [DataField("ProcessingData")]
    public JsonObject ProcessingData {
      get;
      private set;
    }


    [DataField("ProcessingStatus", Default = ExecutionStatus.Pending)]
    public ExecutionStatus ProcessingStatus {
      get;
      private set;
    }


    #endregion Properties

    #region Methods

    internal void ChangeProcessingStatus(JsonObject processingData, ExecutionStatus status) {
      this.ProcessingData = processingData;
      this.ProcessingStatus = status;
      this.ProcessingTime = DateTime.Now;

      this.Save();
    }


    internal void Enqueue(MessageQueue queue, string unitOfWorkUID = null) {
      Assertion.AssertObject(queue, "queue");

      this.Queue = queue;
      this.UnitOfWorkUID = unitOfWorkUID ?? String.Empty;

      this.Save();
    }


    protected override void OnSave() {
      Assertion.AssertObject(this.Queue, "Message's queue can't be null.");

      if (base.IsNew) {
        this.PostingTime = DateTime.Now;
        this.ProcessingTime = ExecutionServer.DateMaxValue;
      }

      MessageQueueData.WriteMessage(this);
    }

    #endregion Methods

  } // class Message

} //namespace Empiria.Messaging
