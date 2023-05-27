/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Messaging Services                           Component : EMail Services                        *
*  Assembly : Empiria.Messaging.dll                        Pattern   : Service Provider                      *
*  Type     : SmtpEmailSender                              License   : Please read LICENSE.txt file      *
*                                                                                                            *
*  Summary  : Provides email delivery services using SMTP protocol.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

using Empiria.StateEnums;

namespace Empiria.Messaging.EMailDelivery {

  /// <summary>Provides email delivery services using SMTP protocol.</summary>
  internal class SmtpEmailSender : IEmailSender {

    #region Methods

    public void Send(SendTo sendTo, EmailContent content) {
      Assertion.Require(sendTo, "sendTo");
      Assertion.Require(content, "content");

      MailMessage message = SmtpEmailSender.GetMailMessage(sendTo, content);

      try {

        using (SmtpClient smtp = SmtpEmailSender.GetSmptClient()) {
          smtp.Send(message);
        }

      } finally {
        DisposeMessage(message);
      }
    }


    public async Task SendAsync(SendTo sendTo, EmailContent content) {
      Assertion.Require(sendTo, "sendTo");
      Assertion.Require(content, "content");

      MailMessage message = SmtpEmailSender.GetMailMessage(sendTo, content);

      Action<Task> disposeMessage = (task) => Task.Run(() => DisposeMessage(message));

      using (SmtpClient smtp = SmtpEmailSender.GetSmptClient()) {
        await smtp.SendMailAsync(message)
                  .ContinueWith(disposeMessage);
      }
    }

    #endregion Methods

    #region Helpers

    static private void DisposeMessage(MailMessage message) {
      foreach (var attachment in message.Attachments) {
        attachment.Dispose();
      }
      message.Attachments.Dispose();
      message.Dispose();
    }


    static private EmailConfig _emailConfig = null;
    static private EmailConfig GetDefaultEmailConfig() {
      if (_emailConfig == null) {
        _emailConfig = EmailConfig.Default;
      }
      return _emailConfig;
    }


    static private MailMessage GetMailMessage(SendTo sendTo, EmailContent content) {
      EmailConfig eMailConfig = SmtpEmailSender.GetDefaultEmailConfig();

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
      EmailConfig eMailConfig = SmtpEmailSender.GetDefaultEmailConfig();

      var smtp = new SmtpClient(eMailConfig.Host, eMailConfig.Port);

      smtp.EnableSsl = eMailConfig.EnableSsl;
      smtp.UseDefaultCredentials = eMailConfig.UseDefaultCredentials;
      smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

      if (!smtp.UseDefaultCredentials) {
        smtp.Credentials = new NetworkCredential(eMailConfig.NetworkUserName,
                                                 eMailConfig.NetworkUserPasssword);
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

    #endregion Helpers

  } //class SmtpEmailSender

} //namespace Empiria.Messaging.EMailDelivery
