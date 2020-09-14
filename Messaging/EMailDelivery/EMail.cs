/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Messaging Services                           Component : EMail Services                        *
*  Assembly : Empiria.Messaging.dll                        Pattern   : Service Provider                      *
*  Type     : EMail                                        License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Provides email delivery services.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

using Empiria.StateEnums;

namespace Empiria.Messaging.EMailDelivery {

  /// <summary>Provides email delivery services.</summary>
  static public class EMail {

    #region Public methods

    static public bool IsValidAddress(string address) {
      try {
        var addr = new MailAddress(address);

        return addr.Address == address;

      } catch {
        return false;
      }
    }


    static public void Send(SendTo sendTo, EMailContent content) {
      Assertion.AssertObject(sendTo, "sendTo");
      Assertion.AssertObject(content, "content");

      MailMessage message = EMail.GetMailMessage(sendTo, content);

      try {

        using (SmtpClient smtp = EMail.GetSmptClient()) {
          smtp.Send(message);
        }

      } finally {
        DisposeMessage(message);
      }
    }


    static public async Task SendAsync(SendTo sendTo, EMailContent content) {
      Assertion.AssertObject(sendTo, "sendTo");
      Assertion.AssertObject(content, "content");

      MailMessage message = EMail.GetMailMessage(sendTo, content);

      Action<Task> disposeMessage = (task) => task = Task.Run(() => DisposeMessage(message));

      using (SmtpClient smtp = EMail.GetSmptClient()) {
        await smtp.SendMailAsync(message)
                  .ContinueWith(disposeMessage);
      }

    }


    [Obsolete("Use EMail.Send(string, EMailContent) method instead.")]
    static public void Send(string address, string subject, string body,
                            bool isBodyHtml = false, bool highPriority = false,
                            FileInfo[] attachments = null) {
      Assertion.AssertObject(address, "address");
      Assertion.AssertObject(subject, "subject");
      Assertion.AssertObject(body, "body");
      attachments = attachments ?? new FileInfo[0];

      var content = new EMailContent(subject, body, isBodyHtml);

      foreach (var file in attachments) {
        content.AddAttachment(file);
      }

      var sendTo = new SendTo(address);

      if (highPriority) {
        sendTo.SetPriority(Priority.High);
      }

      Send(sendTo, content);
    }


    #endregion Public methods

    #region Private methods


    static private void DisposeMessage(MailMessage message) {
      foreach (var attachment in message.Attachments) {
        attachment.Dispose();
      }
      message.Attachments.Dispose();
      message.Dispose();
    }


    static private EMailConfig _eMailConfig = null;
    static private EMailConfig GetDefaultEMailConfig() {
      if (_eMailConfig == null) {
        _eMailConfig = EMailConfig.Default;
      }
      return _eMailConfig;
    }


    static private MailMessage GetMailMessage(SendTo sendTo, EMailContent content) {
      EMailConfig eMailConfig = EMail.GetDefaultEMailConfig();

      var message = new MailMessage();

      message.From = new MailAddress(eMailConfig.SenderEMailAddress, eMailConfig.SenderName);

      SetMessageReceiverAddresses(message, sendTo.Address);
      SetMessagePriority(message, sendTo.Priority);

      if (eMailConfig.BccMirrorEMailAddress.Length != 0) {
        message.Bcc.Add(new MailAddress(eMailConfig.BccMirrorEMailAddress));
      }

      message.Subject = content.Subject;
      message.Body = content.Body;
      message.IsBodyHtml = content.IsBodyHtml;
      message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

      foreach (var file in content.Attachments) {
        message.Attachments.Add(new Attachment(file.FullName));
      }

      return message;
    }


    static private SmtpClient GetSmptClient() {
      EMailConfig eMailConfig = EMail.GetDefaultEMailConfig();

      var smtp = new SmtpClient(eMailConfig.Host, eMailConfig.Port);

      smtp.EnableSsl = eMailConfig.EnableSsl;
      smtp.UseDefaultCredentials = eMailConfig.UseDefaultCredentials;
      smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

      if (!smtp.UseDefaultCredentials) {
        smtp.Credentials = new NetworkCredential(eMailConfig.SenderEMailAddress,
                                                 eMailConfig.SenderEMailPassword);
      }

      return smtp;
    }


    static private void SetMessagePriority(MailMessage message, Priority priority) {
      if (priority == Priority.High) {
        message.Priority = MailPriority.High;

      } else if (priority == Priority.Low) {
        message.Priority = MailPriority.Low;

      } else {
        message.Priority = MailPriority.Normal;
      }
    }


    static private void SetMessageReceiverAddresses(MailMessage message, string address) {
      // Converts any semicolon to comma and removes spaces
      if (address.Contains(";")) {
        address = address.Replace(";", ",");
      }
      address = address.Replace(" ", String.Empty);

      if (address.Contains(",")) {
        message.To.Add(address);  // Multiple addresses separated by commas
      } else {
        message.To.Add(new MailAddress(address));
      }
    }

    #endregion Private methods

  } //class EMail

} //namespace Empiria.Messaging.EMailDelivery
