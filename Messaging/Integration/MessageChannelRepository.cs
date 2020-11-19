///* Empiria Extensions Framework ******************************************************************************
//*                                                                                                            *
//*  Module   : Empiria Messaging                          Component : Integration Layer                       *
//*  Assembly : Empiria.Messaging.dll                      Pattern   : Data Services                           *
//*  Type     : MessageChannelRepository                   License   : Please read LICENSE.txt file            *
//*                                                                                                            *
//*  Summary  : Provides database read and write methods for message channels.                                 *
//*                                                                                                            *
//************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
//using System;
//using System.Collections.Generic;

//using Empiria.Data;

//namespace Empiria.Messaging {

//  /// <summary>Provides database read and write methods for message channels.</summary>
//  static internal class MessageChannelRepository {

//    #region Public methods

//    static internal List<Message> GetChannelMessages(MessageChannel channel) {
//      var sql = $"SELECT * FROM EXFMessages " +
//                $"WHERE MessageChannelId = {channel.Id} AND MessageStatus NOT IN ('C', 'X') " +
//                $"ORDER BY SentTime, MessageId";

//      return DataReader.GetList<Message>(DataOperation.Parse(sql));
//    }


//    static internal void WriteMessage(Message o) {
//      var op = DataOperation.Parse("writeEXFMessage", o.Id, o.MessageType.Id,
//                                    o.Queue.Id, o.UID, o.UnitOfWorkUID,
//                                    o.PostingTime, o.MessageData.ToString(),
//                                    o.ProcessingTime, o.ProcessingData.ToString(),
//                                    (char) o.ProcessingStatus);

//      DataWriter.Execute(op);
//    }

//    #endregion Internal methods

//  } // class MessageQueueData

//} // namespace Empiria.Messaging
