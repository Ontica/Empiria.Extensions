/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Message Queue Services                       Component : Message Queue                         *
*  Assembly : Empiria.Messaging.dll                        Pattern   : Data Services                         *
*  Type     : MessageQueueData                             License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Provides database read and write methods for message queues.                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using Empiria.Data;

namespace Empiria.Messaging {

  /// <summary>Provides database read and write methods for message queues.</summary>
  static internal class MessageQueueData {

    #region Public methods

    static internal List<Message> GetQueueMessages(MessageQueue queue) {
      string sql = $"SELECT * FROM QueuedMessages " +
                   $"WHERE QueueId = {queue.Id} AND ProcessingStatus <> 'X' " +
                   $"ORDER BY PostingTime, MessageId";

      return DataReader.GetList<Message>(DataOperation.Parse(sql));
    }


    static internal void WriteMessage(Message o) {
      var op = DataOperation.Parse("writeQueuedMessage", o.Id, o.MessageType.Id,
                                    o.Queue.Id, o.UID, o.UnitOfWorkUID,
                                    o.PostingTime, o.MessageData.ToString(),
                                    o.ProcessingTime, o.ProcessingData.ToString(),
                                    o.ProcessingStatus);

      DataWriter.Execute(op);
    }

    #endregion Internal methods

  } // class MessageQueueData

} // namespace Empiria.Messaging
