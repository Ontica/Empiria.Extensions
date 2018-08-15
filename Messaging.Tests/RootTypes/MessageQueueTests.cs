/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Message Queue Services                       Component : Message Queue                         *
*  Assembly : Empiria.Messaging.dll                        Pattern   : Testing methods class                 *
*  Type     : MessageQueueTests                            License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Test methods for message queues.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading;

using Xunit;

using Empiria.Json;
using Empiria.Security;

using Empiria.Messaging;

namespace Empiria.Tests {

  public class MessageQueueTests {


    [Fact]
    public void Should_Add_Queue_Message() {
      MessageQueue queue = GetTestingMessageQueue();

      var count = queue.Messages.Count;

      var newMessage = CreateMessage();

      queue.AddMessage(newMessage);

      Assert.Equal(count + 1, queue.Messages.Count);
    }


    [Fact]
    public void Should_Add_Messages_In_A_Unit_of_Work() {
      MessageQueue queue = GetTestingMessageQueue();

      string unitOfWorkUID = Guid.NewGuid().ToString();

      Message newMessage = CreateMessage();
      queue.AddMessage(newMessage, unitOfWorkUID);

      newMessage = CreateMessage();
      queue.AddMessage(newMessage, unitOfWorkUID);

      newMessage = CreateMessage();
      queue.AddMessage(newMessage, unitOfWorkUID);

      Assert.Equal(3, queue.GetUnitOfWorkMessages(unitOfWorkUID).Count);
    }


    [Fact]
    public void Should_Process_A_Unit_of_Work() {
      MessageQueue queue = GetTestingMessageQueue();

      var unitOfWorkUID = queue.TryGetNextUnitOfWork();

      if (unitOfWorkUID == null) {
        return;
      }

      FixedList<Message> messages = queue.GetUnitOfWorkMessages(unitOfWorkUID);

      int counter = 1;
      foreach (var message in messages) {
        var data = new JsonObject();

        data.Add("processingOrder", counter);

        queue.MarkAsProcessed(message, data);

        counter++;
      }

      Assert.Empty(queue.GetUnitOfWorkMessages(unitOfWorkUID));
    }


    [Fact]
    public void Should_Mark_The_First_Message_As_Processed() {
      MessageQueue queue = GetTestingMessageQueue();

      Message nextMessage = queue.TryGetNextMessage();

      if (nextMessage == null) {
        nextMessage = CreateMessage();

        queue.AddMessage(nextMessage);
      }

      var processData = new JsonObject();

      processData.Add("testing_method", "Should_Mark_The_First_Message_As_Processed");
      processData.Add("proc_by", ExecutionServer.CurrentUserId);

      queue.MarkAsProcessed(nextMessage, processData);

      Assert.True(nextMessage.ProcessingStatus == StateEnums.ExecutionStatus.Completed);
    }


    #region Auxiliary methods

    private void Authenticate() {
      string sessionToken = ConfigurationData.GetString("Testing.SessionToken");

      Thread.CurrentPrincipal = AuthenticationService.Authenticate(sessionToken);
    }


    private Message CreateMessage() {
      var data = new JsonObject();

      data.Add("subject", "Subject del mensaje");
      data.Add("value", DateTime.Now.Ticks);

      return new Message(data);
    }


    private MessageQueue GetTestingMessageQueue() {
      return MessageQueue.Parse("TestingMessageQueue");
    }


    #endregion Auxiliary methods

  }  // MessageQueueTests

}  // namespace Empiria.Tests
